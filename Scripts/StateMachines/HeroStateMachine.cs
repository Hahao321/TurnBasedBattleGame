using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //to edit ui stuff such as health bars or buttons
using System;
using System.Xml.Linq;
using System.IO;
using PathCreation;

public class HeroStateMachine : MonoBehaviour
{
    Transform OgLocation;
    Vector2 OgLocationV2;
    PathCreator Path;


    public BaseMonster hero;
    private BattleStateMachine BSM;
    public GameObject arrow;
    public ShakeBehavior mainC;
    public enum HeroStates
    {
        OPENANIMATIONS,
        ATTACKBARFILLING,
        CHOOSINGMOVE,
        ADDMOVETOBSM,
        PERFORMMOVE,
        DEAD,
        IDLE,
        ACTUALIDLE,
        CHOOSINGMOVEAUTO
    }
    public HeroStates HeroState;

    //progressbar/hpbar stuff
    public float current_CD = 0f;
    public float max_CD = 10f;
    public GameObject atkBar;
    public GameObject hpBar;


    //Link to Enemy and stuff for attacking
    public GameObject EnemytoAttack;
    private bool actionStarted = false;
    private Vector3 StartPositon;
    private float animSpeed = 5f;
    public bool HeroAlive = true;

    //to Select this Hero
    public PolygonCollider2D col;
    public bool selected = false;

    //check if moved or not
    public bool moving = false;

    public int isStun = 0;
    public bool SelfFullAtkBar = false;

    public GameObject Stat1Image;
    public GameObject Stat2Image;
    public GameObject Stat3Image;
    public GameObject Stat4Image;
    public GameObject Stat5Image;
    public GameObject Stat6Image;
    public GameObject Stat7Image;
    public GameObject Stat8Image;
    public GameObject Stat9Image;
    public GameObject Stat10Image;
    public List<Vector3> statImages = new List<Vector3>();
    public bool[] boxArray = { false, false, false, false, false, false, false, false, false };

    public int Cd1 = 0;
    public int Cd2 = 0;
    public int Cd3 = 0;
    public int Cd4 = 0;

    public GameObject FloatingTextTest;
    private void Awake()
    {
        SetUp();
    }
    void Start()
    {
        hero.UpdateStats();
    }

    private void Update()
    {
        updateHpBar();
        selectedCharacter();        //check if selected

        switch (HeroState)       //basically if else for all enum we choose below, self explanatory
        {
            case (HeroStates.OPENANIMATIONS):
                //play open animation and wait till done
                HeroState = HeroStates.ATTACKBARFILLING;
                break;


            case (HeroStates.ATTACKBARFILLING):
                updateHpBar();
                UpdateProgressBar();
                break;


            case (HeroStates.ADDMOVETOBSM):
                updateHpBar();
                HeroState = HeroStates.CHOOSINGMOVE;
                break;


            case (HeroStates.IDLE):
                updateHpBar();
                break;

            case (HeroStates.CHOOSINGMOVE):     //redundant and to be replaced everywhere with idle, just for debugging
                updateHpBar();
                if (BSM.HeroestoManage.Count == 0)
                {
                    BSM.HeroestoManage.Add(this.gameObject);
                }
                //BSM.AttackPanel.SetActive(true); DONE  IN CHOOSING MOVE
                break;


            case (HeroStates.PERFORMMOVE):
                updateHpBar();


                StartCoroutine(TimeForAction());



                break;


            case (HeroStates.DEAD):                            //we reach this when hp drops to or below 0 //cant put all this in one func idk why
                updateHpBar();
                col.enabled = false;

                if (!HeroAlive) return;
                else
                {
                    current_CD = 0;
                    //change heros tag to keep track of dead
                    this.gameObject.tag = "DeadHero";

                    //hero is not attackable anymore
                    BSM.HeroesInBattle.Remove(this.gameObject);     //takes him out of list of heroes in battle thus making him untargetable

                    //not able to use this hero
                    BSM.HeroestoManage.Remove(this.gameObject);     //^ but for our own team so he doesnt get turns n shit if his turn is next

                    //deactivate selector over hero if its on
                    arrow.SetActive(false);                         //arrow selector thing gone

                    //reset gui     panels get rid of his options
                    BSM.AttackPanel.SetActive(false);               //his attack panel gets yeeted for the 

                    //if hes in performlist take out of performlist
                    for (int i = 0; i < BSM.PerformList.Count; i++)    //we iterate thru perform list checking if this hero is in there, if so remove that handle turn

                        if (BSM.PerformList[i].AttackerGameObject == this.gameObject)    //if we find the term
                        {
                            BSM.PerformList.Remove(BSM.PerformList[i]); //takes out the handle turn from the perform list
                        }
                }
                //play death animation
                //set dead sprite or w.e

                HeroAlive = false;
                //reset hero input
                BSM.battleStates = BattleStateMachine.performAction.CHECKALIVE;

                break;
            case (HeroStates.CHOOSINGMOVEAUTO):
                ChooseHeroMove();
                break;

            case (HeroStates.ACTUALIDLE):
                updateHpBar();
                break;

        }


    }



    //function list
    void UpdateProgressBar()
    {
        //Debug.Log("Update progress bar runnung + "+ hero.SpeedCurrent);

        current_CD += (hero.SpeedCurrent / 2) * Time.deltaTime; //calculation to be changed
        float calc_CD = 6 * (current_CD / max_CD);                          //calculating how long till ready ie comparing current cd vs max cd, when = or above reset and give turn
                                                                            //we edit bar by using x scale. 100% atk bar is 1, 0 is 0, so we finna work as % to make it ez
                                                                            //currently scale is 2 not 1
        atkBar.transform.localScale = new Vector3(Mathf.Clamp(calc_CD, 0, 6), atkBar.transform.localScale.y, atkBar.transform.localScale.z);
        //edit only x. others are current val. clamp forces calc_CD to be between 0 and 1 by cutting lower vals at 0 and higher at 1 im guessing so x scale doesnt get too big or flip  

        if (SelfFullAtkBar)
        {
            SelfFullAtkBar = false;
            current_CD = max_CD;
        }

        if (current_CD >= max_CD)   //our turn
        {
            TurnCounters();
            statChangeCheck();
            if (hero.dotDeBuffTurns > 0)
            {
                TakeDamage(hero.HpMax / 10);

            }
            if (hero.recBuffTurns > 0)
            {
                TakeHeal(hero.HpMax / 10);

            }
            if (isStun > 0)
            {
                current_CD = 0;
            }
            else
            {
                if (HeroState != HeroStates.DEAD)
                {
                    foreach (GameObject enemy in BSM.EnemiesInBattle)      //iterate thru all enemy in battle
                    {
                        enemy.GetComponent<EnemyStateMachine>().EnemyState = EnemyStateMachine.EnemyStates.IDLE;    //move them all to waiting
                    }
                    foreach (GameObject hero in BSM.HeroesInBattle)
                    {
                        if (hero.GetComponent<HeroStateMachine>() != this)
                        {
                            hero.GetComponent<HeroStateMachine>().HeroState = HeroStateMachine.HeroStates.IDLE; //move all to waiting
                        }
                    }
                    if(!BSM.AutoEnabled)
                        HeroState = HeroStates.CHOOSINGMOVE;
                    else
                        HeroState = HeroStates.CHOOSINGMOVEAUTO;
                }
            }
        }

    }

    private bool movetospot(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }   //simple move func
    public void TakeDamage(float getDamageAmount)
    {
        hero.HpCurrent -= getDamageAmount;
        if (hero.HpCurrent <= 0)
        {
            hero.HpCurrent = 0;
            atkBar.transform.localScale = new Vector3(0f, atkBar.transform.localScale.y, atkBar.transform.localScale.z);
            HeroState = HeroStates.DEAD;
        }
    }

    public void updateHpBar()
    {
        float x;
        float z = Math.Abs(hpBar.transform.localScale.x / 6 - hero.HpCurrent / hero.HpMax);

        if (z > 0.01)
        {
            x = hpBar.transform.localScale.x;
            if ((hpBar.transform.localScale.x / 6 - hero.HpCurrent / hero.HpMax) > 0.005)
            {
                x -= 6 * Time.deltaTime;
                hpBar.transform.localScale = new Vector3(Mathf.Clamp(x, 0, 6), hpBar.transform.localScale.y, hpBar.transform.localScale.z);//Update bar.
            }
            if ((hpBar.transform.localScale.x / 6 - hero.HpCurrent / hero.HpMax) < 0.005)
            {
                x += 6 * Time.deltaTime;
                hpBar.transform.localScale = new Vector3(Mathf.Clamp(x, 0, 6), hpBar.transform.localScale.y, hpBar.transform.localScale.z);//Update bar.
            }
        }
    }
    void selectedCharacter()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                foreach (GameObject hero in BSM.HeroesInBattleTotal)
                {
                    if (hit.transform.position == hero.transform.position)
                    {
                        hero.GetComponent<HeroStateMachine>().selected = true;
                    }
                }
            }

        }
    }
    public void SetUp()
    {
       // hero.UpdateStats(); done in start now


        OgLocation = this.transform;
        OgLocationV2 = new Vector2(this.transform.position.x, this.transform.position.y);

        GetComponent<Follower>().enabled = false;
        hpBar.transform.localScale = new Vector3(6f, 4f, 0f);
        atkBar.transform.localScale = new Vector3(6f, 4f, 0f);
        hpBar.transform.localPosition = new Vector3(-5.48f, -3.76f, 0f);
        atkBar.transform.localPosition = new Vector3(-5.48f, -4.5f, 0f);
        atkBar.GetComponent<SpriteRenderer>().flipX = false;
        hpBar.GetComponent<SpriteRenderer>().flipX = false;

        col = gameObject.GetComponent<PolygonCollider2D>();
        col.enabled = false;
        selected = false;

        //arrow set up
        arrow.transform.localPosition = new Vector3(arrow.transform.localPosition.x, arrow.transform.localPosition.y + 5.5f, arrow.transform.localPosition.z);
        arrow.SetActive(false); //auto disable arrow at start

        Stat1Image.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        Stat2Image.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        Stat3Image.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        Stat4Image.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        Stat5Image.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        Stat6Image.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        Stat7Image.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        Stat8Image.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        Stat9Image.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        Stat10Image.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        Stat1Image.GetComponent<SpriteRenderer>().sortingOrder = 2;
        Stat2Image.GetComponent<SpriteRenderer>().sortingOrder = 2;
        Stat3Image.GetComponent<SpriteRenderer>().sortingOrder = 2;
        Stat4Image.GetComponent<SpriteRenderer>().sortingOrder = 2;
        Stat5Image.GetComponent<SpriteRenderer>().sortingOrder = 2;
        Stat6Image.GetComponent<SpriteRenderer>().sortingOrder = 2;
        Stat7Image.GetComponent<SpriteRenderer>().sortingOrder = 2;
        Stat8Image.GetComponent<SpriteRenderer>().sortingOrder = 2;
        Stat9Image.GetComponent<SpriteRenderer>().sortingOrder = 2;
        Stat9Image.GetComponent<SpriteRenderer>().sortingOrder = 3;
        statImages.Add(Stat1Image.transform.localPosition);
        statImages.Add(Stat2Image.transform.localPosition);
        statImages.Add(Stat3Image.transform.localPosition);
        statImages.Add(Stat4Image.transform.localPosition);
        statImages.Add(Stat5Image.transform.localPosition);
        statImages.Add(Stat6Image.transform.localPosition);
        statImages.Add(Stat7Image.transform.localPosition);
        statImages.Add(Stat8Image.transform.localPosition);
        statImages.Add(Stat9Image.transform.localPosition);
        Destroy(Stat1Image);
        Destroy(Stat2Image);
        Destroy(Stat3Image);
        Destroy(Stat4Image);
        Destroy(Stat5Image);
        Destroy(Stat6Image);
        Destroy(Stat7Image);
        Destroy(Stat8Image);
        Destroy(Stat9Image);

        BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();      //allows us to communicate with BSM 
        StartPositon = transform.position;
        HeroState = HeroStates.IDLE;
        mainC = GameObject.Find("Main Camera").GetComponent<ShakeBehavior>();
    }
    private IEnumerator TimeForAction()
    {
        //Stat1Image.GetComponent<SpriteRenderer>().sprite = BSM.AtkBuff;

        //reset enemy state > current_CD to 0 and currentState to processing
        current_CD = 0f;
        if (actionStarted)
        {
            yield break; //breaks out of ienumerator
        }
        actionStarted = true;
        Vector3 EnemyPosition = new Vector3(EnemytoAttack.transform.position.x - 1.5f, EnemytoAttack.transform.position.y, EnemytoAttack.transform.position.z); //x-1.5f for heros and x+1.5 for enemies since they on opposite sides

        //check if we gotta move not on path
        if (BSM.PerformList[0].ChosenAttack.MoveToTarget && !BSM.PerformList[0].ChosenAttack.path)
        {
            moving = true;
            while (movetospot(EnemyPosition)) { yield return null; }          //dont really get but moves to spot in mvoetospot func

        }

        //if theres path
        else if (BSM.PerformList[0].ChosenAttack.MoveToTarget && BSM.PerformList[0].ChosenAttack.path)
        {
            GetComponent<Follower>().enabled = true;
            moving = true;
        }

        yield return new WaitForSeconds(BSM.PerformList[0].ChosenAttack.animationTimeBeforeEffect + 3);  //needed idk why says OUTOFRANGE ERROR OTHERWISE

        if(GetComponent<Follower>().enabled)
            GetComponent<Follower>().enabled = false;


       


        //set cd
        if (BSM.PerformList[0].ChosenAttack == hero.attacks[0])
            Cd1 = hero.attacks[0].CDAttack + 1;
        if (BSM.PerformList[0].ChosenAttack == hero.attacks[1])
            Cd2 = hero.attacks[1].CDAttack + 1;
        if (BSM.PerformList[0].ChosenAttack == hero.attacks[2])
            Cd3 = hero.attacks[2].CDAttack + 1;
        if (BSM.PerformList[0].ChosenAttack == hero.attacks[3])
            Cd4 = hero.attacks[3].CDAttack + 1;

        //do effects
        //0 dmg
        DoDmg(BSM.PerformList[0].ChosenAttack);
        //1 heal
        DoHeal(BSM.PerformList[0].ChosenAttack);
        //2 buff
        DoBuff(BSM.PerformList[0].ChosenAttack);
        //3 debuff
        doDeBuff(BSM.PerformList[0].ChosenAttack);
        //4 revive
        doRevive(BSM.PerformList[0].ChosenAttack);
        //5 stun
        doStun(BSM.PerformList[0].ChosenAttack);
        //6 atkbarinc
        doAtkBarDecrease(BSM.PerformList[0].ChosenAttack);
        //7 atkbardec
        doAtkBarIncrease(BSM.PerformList[0].ChosenAttack);
        //8
        DoCleanse(BSM.PerformList[0].ChosenAttack);
        DoStrip(BSM.PerformList[0].ChosenAttack);

        //animate going back
        if (moving && !BSM.PerformList[0].ChosenAttack.path)
        {
            Vector3 firstPosition = StartPositon;
            while (movetospot(firstPosition)) { yield return null; }
        }
        moving = false;
        if (BSM.PerformList[0].ChosenAttack.MoveToTarget && BSM.PerformList[0].ChosenAttack.path)
        {
            //reset distance traveled
            GetComponent<Follower>().distancetraveled = 0;


            Vector2 currentP = new Vector2(this.transform.position.x, this.transform.position.y);
            Vector2[] backPath = { currentP, OgLocationV2 };
            BezierPath bezierPath = new BezierPath(backPath, false, PathSpace.xy); //false is for closed loop. xy pathspace is cuz its 2d

            Path = GameObject.Find("Path").GetComponent<PathCreator>();      //allows us to communicate with BSM
            Path.bezierPath = bezierPath;
            if (!GetComponent<Follower>().enabled)
                GetComponent<Follower>().enabled = true;
        }


        yield return new WaitForSeconds(BSM.PerformList[0].ChosenAttack.animationTimeBeforeEffect + 2);  //needed idk why says OUTOFRANGE ERROR OTHERWISE
        UnityEngine.Debug.Log("finished waiting for path 2");
        if (GetComponent<Follower>().enabled)
            GetComponent<Follower>().enabled = false;

        yield return new WaitForSeconds(BSM.PerformList[0].ChosenAttack.animationTimeAfterEffect);
        FinishTimeForAction();
    }
    void FinishTimeForAction()
    {
        //remove performer from list in BSM
        BSM.PerformList.RemoveAt(0);

        //reset BSM -> from perform to wait

        if (BSM.battleStates != BattleStateMachine.performAction.WIN && BSM.battleStates != BattleStateMachine.performAction.LOSE)   //this only happens if battle not done
        {
            BSM.battleStates = BattleStateMachine.performAction.ATTACKBARSLOADING;

            //reset enemy state
            actionStarted = false;



            foreach (GameObject enemy in BSM.EnemiesInBattle)      //iterate thru all enemy in battle
            {
                enemy.GetComponent<EnemyStateMachine>().EnemyState = EnemyStateMachine.EnemyStates.ATTACKBARFILLING;    //move them all to waiting
            }
            foreach (GameObject hero in BSM.HeroesInBattle)
            {

                hero.GetComponent<HeroStateMachine>().HeroState = HeroStates.ATTACKBARFILLING; //move all to waiting

            }
        }
        else
        {
            HeroState = HeroStates.ACTUALIDLE;
        }
        BSM.HeroInput = BattleStateMachine.HeroGUI.CHOOSEMOVE;

    }




    //effects
    void DoDmg(BaseAttack x)
    {
        float critchance;
        bool crit = false;
        if (x.Effect[0] == true)
        {
            float calculateddmg = 0;
            if (x.IsItEntirePartyDmg)
            {
                foreach (GameObject target in BSM.EnemiesInBattle)
                {
                    critchance = UnityEngine.Random.Range(0, 100);
                    calculateddmg = hero.AtkCurrent * BSM.PerformList[0].ChosenAttack.DmgScalings[0];    //dmg to be done
                    calculateddmg += hero.HpMax * BSM.PerformList[0].ChosenAttack.DmgScalings[1];    //dmg to be done
                    calculateddmg += hero.SpeedCurrent * BSM.PerformList[0].ChosenAttack.DmgScalings[2];    //dmg to be done
                    calculateddmg += hero.DefCurrent * BSM.PerformList[0].ChosenAttack.DmgScalings[3];    //dmg to be done 
                    if (TypeMatch(hero, target.GetComponent<EnemyStateMachine>().enemy) == 2)
                    {
                        calculateddmg *= 1.3f;
                    }
                    if (TypeMatch(hero, target.GetComponent<EnemyStateMachine>().enemy) == 0)
                    {
                        calculateddmg *= 0.7f;
                    }
                    calculateddmg += target.GetComponent<EnemyStateMachine>().enemy.HpMax * x.DmgPercent / 100;
                    if (critchance <= hero.Critrate)
                    {
                        calculateddmg *= hero.Critdamage;
                        crit = true;
                    }
                    target.GetComponent<EnemyStateMachine>().TakeDmg(calculateddmg);
                    if (crit)
                    {
                        ShowFloatingText(target);
                        mainC.TriggerShake();
                    }
                    else
                        crit = false;
                }
            }
            else
            {
                critchance = UnityEngine.Random.Range(0, 100);
                calculateddmg = hero.AtkCurrent * BSM.PerformList[0].ChosenAttack.DmgScalings[0];    //dmg to be done
                calculateddmg += hero.HpMax * BSM.PerformList[0].ChosenAttack.DmgScalings[1];    //dmg to be done
                calculateddmg += hero.SpeedCurrent * BSM.PerformList[0].ChosenAttack.DmgScalings[2];    //dmg to be done
                calculateddmg += hero.DefCurrent * BSM.PerformList[0].ChosenAttack.DmgScalings[3];    //dmg to be done

                if (TypeMatch(hero, EnemytoAttack.GetComponent<EnemyStateMachine>().enemy) == 2)
                {
                    calculateddmg *= 1.3f;
                }
                if (TypeMatch(hero, EnemytoAttack.GetComponent<EnemyStateMachine>().enemy) == 0)
                {
                    calculateddmg *= 0.7f;
                }
                calculateddmg += EnemytoAttack.GetComponent<EnemyStateMachine>().enemy.HpMax * x.DmgPercent / 100;
                if (critchance <= hero.Critrate)
                {
                    ShowFloatingText(EnemytoAttack);
                    calculateddmg *= hero.Critdamage;
                    crit = true;
                }
                if (crit)
                {
                    // UnityEngine.Debug.Log("crit");
                }
                else
                    // UnityEngine.Debug.Log("no crit");
                    EnemytoAttack.GetComponent<EnemyStateMachine>().TakeDmg(calculateddmg);
                crit = false;
            }

        }
    }
    void DoHeal(BaseAttack x)
    {
        if (x.Effect[1] == true)
        {
            float calculatedHeal;
            if (x.IsItEntirePartyHeal)      //entire party
            {
                foreach (GameObject target in BSM.HeroesInBattle)
                {
                    if (x.IsItPercentHeal)
                    {
                        calculatedHeal = target.GetComponent<HeroStateMachine>().hero.HpMax * x.HealPercent / 100;
                        target.GetComponent<HeroStateMachine>().TakeHeal(calculatedHeal);
                    }
                    if (x.IsItFlatHeal) //not percent heal
                    {
                        calculatedHeal = 0;
                        calculatedHeal = hero.AtkCurrent * BSM.PerformList[0].ChosenAttack.HealScalings[0];
                        calculatedHeal += hero.HpMax * BSM.PerformList[0].ChosenAttack.HealScalings[1];
                        calculatedHeal += hero.SpeedCurrent * BSM.PerformList[0].ChosenAttack.HealScalings[2];
                        calculatedHeal += hero.DefCurrent * BSM.PerformList[0].ChosenAttack.HealScalings[3];

                        target.GetComponent<HeroStateMachine>().TakeHeal(calculatedHeal);
                    }
                }
            }

            else                           //not entire party
            {
                if (x.IsItPercentHeal)
                {

                    calculatedHeal = EnemytoAttack.GetComponent<HeroStateMachine>().hero.HpMax * x.HealPercent / 100;
                    EnemytoAttack.GetComponent<HeroStateMachine>().TakeHeal(calculatedHeal);
                }
                if (x.IsItFlatHeal) //not percent heal
                {
                    calculatedHeal = 0;
                    calculatedHeal = hero.AtkCurrent * BSM.PerformList[0].ChosenAttack.HealScalings[0];
                    calculatedHeal += hero.HpMax * BSM.PerformList[0].ChosenAttack.HealScalings[1];
                    calculatedHeal += hero.SpeedCurrent * BSM.PerformList[0].ChosenAttack.HealScalings[2];
                    calculatedHeal += hero.DefCurrent * BSM.PerformList[0].ChosenAttack.HealScalings[3];
                    EnemytoAttack.GetComponent<HeroStateMachine>().TakeHeal(calculatedHeal);

                }
            }
        }
    }
    void TakeHeal(float HealingAmount)
    {
        if (hero.HpCurrent + HealingAmount > hero.HpMax)
        {
            hero.HpCurrent = hero.HpMax;
        }
        else
        {
            hero.HpCurrent += HealingAmount;
        }
    }
    void DoBuff(BaseAttack x)
    {
        if (x.Effect[2] == true)
        {
            if (x.IsItEntirePartyBuff)      //entire party
            {
                foreach (GameObject target in BSM.HeroesInBattle)
                {
                    if (x.StatToBuff[0])//atk
                    {
                        if (x.BuffTurns > target.GetComponent<HeroStateMachine>().hero.atkBuffTurns)
                        {
                            target.GetComponent<HeroStateMachine>().hero.atkBuffTurns = x.BuffTurns;
                        }
                        if (x.BuffTurns + 1 > target.GetComponent<HeroStateMachine>().hero.atkBuffTurns && target.GetComponent<HeroStateMachine>().hero == this.hero)
                        {
                            this.hero.atkBuffTurns++;
                        }
                    }
                    if (x.StatToBuff[1])//spd
                    {
                        if (x.BuffTurns > target.GetComponent<HeroStateMachine>().hero.spdBuffTurns)
                        {
                            target.GetComponent<HeroStateMachine>().hero.spdBuffTurns = x.BuffTurns;
                        }
                        if (x.BuffTurns + 1 > target.GetComponent<HeroStateMachine>().hero.spdBuffTurns && target.GetComponent<HeroStateMachine>().hero == this.hero)
                        {
                            this.hero.spdBuffTurns++;
                        }
                    }
                    if (x.StatToBuff[2])//def
                    {
                        if (x.BuffTurns > target.GetComponent<HeroStateMachine>().hero.defBuffTurns)
                        {
                            target.GetComponent<HeroStateMachine>().hero.defBuffTurns = x.BuffTurns;
                        }
                        if (x.BuffTurns + 1 > target.GetComponent<HeroStateMachine>().hero.defBuffTurns && target.GetComponent<HeroStateMachine>().hero == this.hero)
                        {
                            this.hero.defBuffTurns++;
                        }
                    }
                    if (x.StatToBuff[3])//rec
                    {
                        if (x.BuffTurns > target.GetComponent<HeroStateMachine>().hero.recBuffTurns)
                        {
                            target.GetComponent<HeroStateMachine>().hero.recBuffTurns = x.BuffTurns;
                        }
                        if (x.BuffTurns + 1 > target.GetComponent<HeroStateMachine>().hero.recBuffTurns && target.GetComponent<HeroStateMachine>().hero == this.hero)
                        {
                            this.hero.recBuffTurns++;
                        }
                    }
                    target.GetComponent<HeroStateMachine>().statChangeCheck();
                }
            }

            else                           //not entire party
            {
                if (x.StatToBuff[0])//atk
                {
                    if (x.BuffTurns > EnemytoAttack.GetComponent<HeroStateMachine>().hero.atkBuffTurns)
                    {
                        EnemytoAttack.GetComponent<HeroStateMachine>().hero.atkBuffTurns = x.BuffTurns;
                    }
                    if (x.BuffTurns + 1 > EnemytoAttack.GetComponent<HeroStateMachine>().hero.atkBuffTurns && EnemytoAttack.GetComponent<HeroStateMachine>().hero == this.hero)
                    {
                        this.hero.atkBuffTurns++;
                    }
                }
                if (x.StatToBuff[1])//spd
                {
                    if (x.BuffTurns > EnemytoAttack.GetComponent<HeroStateMachine>().hero.spdBuffTurns)
                    {
                        EnemytoAttack.GetComponent<HeroStateMachine>().hero.spdBuffTurns = x.BuffTurns;
                        if (x.BuffTurns + 1 > EnemytoAttack.GetComponent<HeroStateMachine>().hero.spdBuffTurns && EnemytoAttack.GetComponent<HeroStateMachine>().hero == this.hero)
                        {
                            this.hero.spdBuffTurns++;
                        }
                    }
                }
                if (x.StatToBuff[2])//def
                {
                    if (x.BuffTurns > EnemytoAttack.GetComponent<HeroStateMachine>().hero.defBuffTurns)
                    {
                        EnemytoAttack.GetComponent<HeroStateMachine>().hero.defBuffTurns = x.BuffTurns;
                    }
                    if (x.BuffTurns + 1 > EnemytoAttack.GetComponent<HeroStateMachine>().hero.defBuffTurns && EnemytoAttack.GetComponent<HeroStateMachine>().hero == this.hero)
                    {
                        this.hero.defBuffTurns++;
                    }
                }
                if (x.StatToBuff[3])//rec
                {
                    if (x.BuffTurns > EnemytoAttack.GetComponent<HeroStateMachine>().hero.recBuffTurns)
                    {
                        EnemytoAttack.GetComponent<HeroStateMachine>().hero.recBuffTurns = x.BuffTurns;
                    }
                    if (x.BuffTurns + 1 > EnemytoAttack.GetComponent<HeroStateMachine>().hero.recBuffTurns && EnemytoAttack.GetComponent<HeroStateMachine>().hero == this.hero)
                    {
                        this.hero.recBuffTurns++;
                    }
                }
                EnemytoAttack.GetComponent<HeroStateMachine>().statChangeCheck();
            }
        }
    }
    void TurnCounters()
    {
        hero.turns++;
        if (hero.atkBuffTurns > 0)
        {
            hero.atkBuffTurns--;
        }
        if (hero.defBuffTurns > 0)
        {
            hero.defBuffTurns--;
        }
        if (hero.spdBuffTurns > 0)
        {
            hero.spdBuffTurns--;
        }
        if (hero.recBuffTurns > 0)
        {
            hero.recBuffTurns--;
        }
        if (hero.atkDeBuffTurns > 0)
        {
            hero.atkDeBuffTurns--;
        }
        if (hero.defDeBuffTurns > 0)
        {
            hero.defDeBuffTurns--;
        }
        if (hero.spdDeBuffTurns > 0)
        {
            hero.spdDeBuffTurns--;
        }
        if (isStun > 0)
        {
            isStun--;
        }
        if (hero.dotDeBuffTurns > 0)
        {
            hero.dotDeBuffTurns--;
        }
        if (Cd1 > 0)
            Cd1--;
        if (Cd2 > 0)
            Cd2--;
        if (Cd3 > 0)
            Cd3--;
        if (Cd4 > 0)
            Cd4--;

    }
    public void statChangeCheck()
    {

        atkCheck();
        defCheck();
        spdCheck();
        BuffDebuffSprites();
    }
    void atkCheck()
    {
        if (hero.atkBuffTurns > 0 && hero.atkDeBuffTurns == 0)
        {
            hero.AtkCurrent = hero.AtkMax * 1.5f;
        }
        else if (hero.atkDeBuffTurns > 0 && hero.atkBuffTurns == 0)
        {
            hero.AtkCurrent = hero.AtkMax * 0.5f;
        }
        else
        {
            hero.AtkCurrent = hero.AtkMax;
        }
    }
    void defCheck()
    {
        if (hero.defBuffTurns > 0 && hero.defDeBuffTurns == 0)
        {
            hero.DefCurrent = hero.DefMax * 1.5f;
        }
        else if (hero.defDeBuffTurns > 0 && hero.defBuffTurns == 0)
        {
            hero.DefCurrent = hero.DefMax * 0.5f;
        }
        else
        {
            hero.DefCurrent = hero.DefMax;
        }
    }
    void spdCheck()
    {
        if (hero.spdBuffTurns > 0 && hero.spdDeBuffTurns == 0)
        {
            hero.SpeedCurrent = hero.SpeedMax * 1.5f;
        }
        else if (hero.spdDeBuffTurns > 0 && hero.spdBuffTurns == 0)
        {
            hero.SpeedCurrent = hero.SpeedMax * 0.5f;
        }
        else
        {
            hero.SpeedCurrent = hero.SpeedMax;
        }
    }
    void destroyBoxes()
    {
        if (transform.Find("box 1") != null)
            Destroy(transform.Find("box 1").gameObject);
        if (transform.Find("box 2") != null)
            Destroy(transform.Find("box 2").gameObject);
        if (transform.Find("box 3") != null)
            Destroy(transform.Find("box 3").gameObject);
        if (transform.Find("box 4") != null)
            Destroy(transform.Find("box 4").gameObject);
        if (transform.Find("box 5") != null)
            Destroy(transform.Find("box 5").gameObject);
        if (transform.Find("box 6") != null)
            Destroy(transform.Find("box 6").gameObject);
        if (transform.Find("box 7") != null)
            Destroy(transform.Find("box 7").gameObject);
        if (transform.Find("box 8") != null)
            Destroy(transform.Find("box 8").gameObject);
        if (transform.Find("box 9") != null)
            Destroy(transform.Find("box 9").gameObject);
        for (int i = 0; i < 9; i++)
        {
            boxArray[i] = false;
        }

    }
    void BuffDebuffSprites()
    {
        destroyBoxes();
        int x;
        string boxnum = "box ";
        if (hero.atkBuffTurns > 0)
        {
            var a = Instantiate(Stat10Image, new Vector3(0, 0, 0), Quaternion.identity);
            a.transform.SetParent(this.transform);
            a.transform.localScale = Stat10Image.transform.localScale;
            a.transform.localPosition = statImages[0];
            a.GetComponent<SpriteRenderer>().sprite = BSM.AtkBuff;
            a.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            a.GetComponent<SpriteRenderer>().sortingOrder = 3;
            a.name = "box 1";
            boxArray[0] = true;
        }
        if (hero.spdBuffTurns > 0)
        {
            x = FindOpenBoxLocation();
            var a = Instantiate(Stat10Image, new Vector3(0, 0, 0), Quaternion.identity);
            a.transform.SetParent(this.transform);
            a.transform.localScale = Stat10Image.transform.localScale;
            a.transform.localPosition = statImages[x - 1];
            a.GetComponent<SpriteRenderer>().sprite = BSM.SpdBuff;
            a.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            a.GetComponent<SpriteRenderer>().sortingOrder = 3;
            a.name = boxnum + x;
            boxArray[x - 1] = true;

        }
        if (hero.defBuffTurns > 0)
        {
            x = FindOpenBoxLocation();
            var a = Instantiate(Stat10Image, new Vector3(0, 0, 0), Quaternion.identity);
            a.transform.SetParent(this.transform);
            a.transform.localScale = Stat10Image.transform.localScale;
            a.transform.localPosition = statImages[x - 1];
            a.GetComponent<SpriteRenderer>().sprite = BSM.DefBuff;
            a.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            a.GetComponent<SpriteRenderer>().sortingOrder = 3;
            a.name = boxnum + x;
            boxArray[x - 1] = true;

        }
        if (hero.recBuffTurns > 0)
        {
            x = FindOpenBoxLocation();
            var a = Instantiate(Stat10Image, new Vector3(0, 0, 0), Quaternion.identity);
            a.transform.SetParent(this.transform);
            a.transform.localScale = Stat10Image.transform.localScale;
            a.transform.localPosition = statImages[x - 1];
            a.GetComponent<SpriteRenderer>().sprite = BSM.Recovery;
            a.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            a.GetComponent<SpriteRenderer>().sortingOrder = 3;
            a.name = boxnum + x;
            boxArray[x - 1] = true;

        }
        if (hero.atkDeBuffTurns > 0)
        {
            x = FindOpenBoxLocation();
            var a = Instantiate(Stat10Image, new Vector3(0, 0, 0), Quaternion.identity);
            a.transform.SetParent(this.transform);
            a.transform.localScale = Stat10Image.transform.localScale;
            a.transform.localPosition = statImages[x - 1];
            a.GetComponent<SpriteRenderer>().sprite = BSM.AtkDeBuff;
            a.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            a.GetComponent<SpriteRenderer>().sortingOrder = 3;
            a.name = boxnum + x;
            boxArray[x - 1] = true;

        }
        if (hero.spdDeBuffTurns > 0)
        {
            x = FindOpenBoxLocation();
            var a = Instantiate(Stat10Image, new Vector3(0, 0, 0), Quaternion.identity);
            a.transform.SetParent(this.transform);
            a.transform.localScale = Stat10Image.transform.localScale;
            a.transform.localPosition = statImages[x - 1];
            a.GetComponent<SpriteRenderer>().sprite = BSM.SpdDeBuff;
            a.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            a.GetComponent<SpriteRenderer>().sortingOrder = 3;
            a.name = boxnum + x;
            boxArray[x - 1] = true;

        }
        if (hero.defDeBuffTurns > 0)
        {
            x = FindOpenBoxLocation();
            var a = Instantiate(Stat10Image, new Vector3(0, 0, 0), Quaternion.identity);
            a.transform.SetParent(this.transform);
            a.transform.localScale = Stat10Image.transform.localScale;
            a.transform.localPosition = statImages[x - 1];
            a.GetComponent<SpriteRenderer>().sprite = BSM.DefDeBuff;
            a.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            a.GetComponent<SpriteRenderer>().sortingOrder = 3;
            a.name = boxnum + x;
            boxArray[x - 1] = true;

        }
        if (hero.dotDeBuffTurns > 0)
        {
            x = FindOpenBoxLocation();
            var a = Instantiate(Stat10Image, new Vector3(0, 0, 0), Quaternion.identity);
            a.transform.SetParent(this.transform);
            a.transform.localScale = Stat10Image.transform.localScale;
            a.transform.localPosition = statImages[x - 1];
            a.GetComponent<SpriteRenderer>().sprite = BSM.Dot;
            a.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            a.GetComponent<SpriteRenderer>().sortingOrder = 3;
            a.name = boxnum + x;
            boxArray[x - 1] = true;

        }
        if (isStun > 0)
        {
            x = FindOpenBoxLocation();
            var a = Instantiate(Stat10Image, new Vector3(0, 0, 0), Quaternion.identity);
            a.transform.SetParent(this.transform);
            a.transform.localScale = Stat10Image.transform.localScale;
            a.transform.localPosition = statImages[x - 1];
            a.GetComponent<SpriteRenderer>().sprite = BSM.Stun;
            a.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            a.GetComponent<SpriteRenderer>().sortingOrder = 3;
            a.name = boxnum + x;
            boxArray[x - 1] = true;

        }
    }
    void doDeBuff(BaseAttack x)
    {
        if (x.Effect[3] == true)
        {
            float LandingChance;
            int OutofHundred;
            bool ResAnimation = false;
            if (x.IsItEntirePartyDeBuff)      //entire party
            {
                foreach (GameObject target in BSM.EnemiesInBattle)
                {
                    ResAnimation = false;
                    LandingChance = target.GetComponent<EnemyStateMachine>().enemy.Resistance - this.hero.Accuracy;
                    if (LandingChance < 10)
                        LandingChance = 10;
                    if (LandingChance > 90)
                        LandingChance = 90;
                    if (x.StatToDeBuff[0])//atk
                    {
                        if (x.DeBuffTurns > target.GetComponent<EnemyStateMachine>().enemy.atkDeBuffTurns)
                        {
                            OutofHundred = UnityEngine.Random.Range(0, 100);
                            if (OutofHundred > LandingChance)
                                target.GetComponent<EnemyStateMachine>().enemy.atkDeBuffTurns = x.DeBuffTurns;
                            else
                                ResAnimation = true;
                        }
                    }
                    if (x.StatToDeBuff[1])//spd
                    {
                        if (x.DeBuffTurns > target.GetComponent<EnemyStateMachine>().enemy.spdDeBuffTurns)
                        {
                            OutofHundred = UnityEngine.Random.Range(0, 100);
                            if (OutofHundred > LandingChance)
                                target.GetComponent<EnemyStateMachine>().enemy.spdDeBuffTurns = x.DeBuffTurns;
                            else
                                ResAnimation = true;
                        }
                    }
                    if (x.StatToDeBuff[2])//atk
                    {
                        if (x.DeBuffTurns > target.GetComponent<EnemyStateMachine>().enemy.defDeBuffTurns)
                        {
                            OutofHundred = UnityEngine.Random.Range(0, 100);
                            if (OutofHundred > LandingChance)
                                target.GetComponent<EnemyStateMachine>().enemy.defDeBuffTurns = x.DeBuffTurns;
                            else
                                ResAnimation = true;
                        }
                    }
                    if (x.StatToDeBuff[3])//dot
                    {
                        if (x.DeBuffTurns > target.GetComponent<EnemyStateMachine>().enemy.dotDeBuffTurns)
                        {
                            OutofHundred = UnityEngine.Random.Range(0, 100);
                            if (OutofHundred > LandingChance)
                                target.GetComponent<EnemyStateMachine>().enemy.dotDeBuffTurns = x.DeBuffTurns;
                            else
                                ResAnimation = true;
                        }
                    }
                    target.GetComponent<EnemyStateMachine>().statChangeCheck();
                    if (ResAnimation)
                    {
                        //PLAY RES ANIMATION
                    }
                }
            }

            else                           //not entire party
            {
                ResAnimation = false;
                LandingChance = EnemytoAttack.GetComponent<EnemyStateMachine>().enemy.Resistance - this.hero.Accuracy;
                if (LandingChance < 10)
                    LandingChance = 10;
                if (LandingChance > 90)
                    LandingChance = 90;
                UnityEngine.Debug.Log(LandingChance);

                if (x.StatToDeBuff[0])//atk
                {
                    if (x.DeBuffTurns > EnemytoAttack.GetComponent<EnemyStateMachine>().enemy.atkDeBuffTurns)
                    {
                        OutofHundred = UnityEngine.Random.Range(0, 100);
                        if (OutofHundred > LandingChance)
                            EnemytoAttack.GetComponent<EnemyStateMachine>().enemy.atkDeBuffTurns = x.DeBuffTurns;
                        else
                            ResAnimation = true;
                    }
                }
                if (x.StatToDeBuff[1])//spd
                {
                    if (x.DeBuffTurns > EnemytoAttack.GetComponent<EnemyStateMachine>().enemy.spdDeBuffTurns)
                    {
                        OutofHundred = UnityEngine.Random.Range(0, 100);
                        if (OutofHundred > LandingChance)
                            EnemytoAttack.GetComponent<EnemyStateMachine>().enemy.spdDeBuffTurns = x.DeBuffTurns;
                        else
                            ResAnimation = true;
                    }
                }
                if (x.StatToDeBuff[2])//atk
                {
                    if (x.DeBuffTurns > EnemytoAttack.GetComponent<EnemyStateMachine>().enemy.defDeBuffTurns)
                    {
                        OutofHundred = UnityEngine.Random.Range(0, 100);
                        if (OutofHundred > LandingChance)
                            EnemytoAttack.GetComponent<EnemyStateMachine>().enemy.defDeBuffTurns = x.DeBuffTurns;
                        else
                            ResAnimation = true;
                    }
                }
                if (x.StatToDeBuff[3])//dot
                {
                    if (x.DeBuffTurns > EnemytoAttack.GetComponent<EnemyStateMachine>().enemy.dotDeBuffTurns)
                    {
                        OutofHundred = UnityEngine.Random.Range(0, 100);
                        if (OutofHundred > LandingChance)
                            EnemytoAttack.GetComponent<EnemyStateMachine>().enemy.dotDeBuffTurns = x.DeBuffTurns;
                        else
                            ResAnimation = true;
                    }
                }
                EnemytoAttack.GetComponent<EnemyStateMachine>().statChangeCheck();
                if (ResAnimation)
                {
                    UnityEngine.Debug.Log("Resisted" + EnemytoAttack);
                }
            }
        }

    }
    void doStun(BaseAttack x)
    {
        if (x.Effect[5] == true)
        {
            if (x.IsItEntirePartyStun)
            {
                foreach (GameObject target in BSM.EnemiesInBattle)
                {
                    if (target.GetComponent<EnemyStateMachine>().isStun < x.StunTurns + 1)
                    {
                        target.GetComponent<EnemyStateMachine>().isStun = x.StunTurns + 1;
                    }
                }
            }
            else
            {
                if (EnemytoAttack.GetComponent<EnemyStateMachine>().isStun < x.StunTurns + 1)
                {
                    EnemytoAttack.GetComponent<EnemyStateMachine>().isStun = x.StunTurns + 1;
                }
            }
        }
    }
    void doRevive(BaseAttack x)
    {
        if (x.Effect[4])
        {
            float calculatedHeal;
            if (x.IsItEntirePartyRevive)
            {
                foreach (GameObject target in BSM.HeroesInBattleTotal)
                {
                    if (target.GetComponent<HeroStateMachine>().HeroAlive == false)
                    {
                        target.tag = "Hero";
                        BSM.HeroesInBattle.Add(target);
                        target.GetComponent<HeroStateMachine>().HeroAlive = true;

                        if (x.IsItPercentReviveHp)
                        {

                            calculatedHeal = target.GetComponent<HeroStateMachine>().hero.HpMax * x.PercentHealOnRevive / 100;
                            Debug.Log(calculatedHeal);
                            Debug.Log(target.GetComponent<HeroStateMachine>().hero.HpMax + "  " + x.PercentHealOnRevive);
                            target.GetComponent<HeroStateMachine>().TakeHeal(calculatedHeal);
                        }
                        else
                        {
                            calculatedHeal = hero.AtkCurrent * BSM.PerformList[0].ChosenAttack.ReviveHealScalings[0];
                            calculatedHeal += hero.HpMax * BSM.PerformList[0].ChosenAttack.ReviveHealScalings[1];
                            calculatedHeal += hero.SpeedCurrent * BSM.PerformList[0].ChosenAttack.ReviveHealScalings[2];
                            calculatedHeal += hero.DefCurrent * BSM.PerformList[0].ChosenAttack.ReviveHealScalings[3];

                            target.GetComponent<HeroStateMachine>().TakeHeal(calculatedHeal);
                        }
                    }
                }
                foreach (GameObject target in BSM.HeroesInBattleTotal)
                {
                    if (target.GetComponent<HeroStateMachine>().HeroState == HeroStateMachine.HeroStates.DEAD)
                    {
                        target.GetComponent<HeroStateMachine>().HeroState = HeroStateMachine.HeroStates.IDLE;
                    }
                }
            }
            else
            {
                EnemytoAttack.tag = "Hero";
                BSM.HeroesInBattle.Add(EnemytoAttack);
                EnemytoAttack.GetComponent<HeroStateMachine>().HeroAlive = true;

                if (x.IsItPercentReviveHp)
                {

                    calculatedHeal = EnemytoAttack.GetComponent<HeroStateMachine>().hero.HpMax * x.PercentHealOnRevive / 100;
                    EnemytoAttack.GetComponent<HeroStateMachine>().TakeHeal(calculatedHeal);
                }
                else
                {
                    calculatedHeal = hero.AtkCurrent * BSM.PerformList[0].ChosenAttack.ReviveHealScalings[0];
                    calculatedHeal += hero.HpMax * BSM.PerformList[0].ChosenAttack.ReviveHealScalings[1];
                    calculatedHeal += hero.SpeedCurrent * BSM.PerformList[0].ChosenAttack.ReviveHealScalings[2];
                    calculatedHeal += hero.DefCurrent * BSM.PerformList[0].ChosenAttack.ReviveHealScalings[3];
                    EnemytoAttack.GetComponent<HeroStateMachine>().TakeHeal(calculatedHeal);
                }
                if (EnemytoAttack.GetComponent<HeroStateMachine>().HeroState == HeroStateMachine.HeroStates.DEAD)
                {
                    EnemytoAttack.GetComponent<HeroStateMachine>().HeroState = HeroStateMachine.HeroStates.IDLE;
                }
            }
        }
    }
    void doAtkBarIncrease(BaseAttack x)
    {
        if (x.Effect[7])
        {
            if (x.IsItEntirePartyAtkBarBoost)
            {

                foreach (GameObject target in BSM.HeroesInBattle)
                {
                    if (target.GetComponent<HeroStateMachine>().current_CD + target.GetComponent<HeroStateMachine>().max_CD * x.AtkBarBoostPercent / 100 < target.GetComponent<HeroStateMachine>().max_CD)
                    {
                        target.GetComponent<HeroStateMachine>().current_CD += target.GetComponent<HeroStateMachine>().max_CD * x.AtkBarBoostPercent / 100;
                    }
                    else
                    {
                        target.GetComponent<HeroStateMachine>().current_CD = target.GetComponent<HeroStateMachine>().max_CD;
                    }
                    target.GetComponent<HeroStateMachine>().CheckProgressBar();
                }
            }
            else
            {
                if (EnemytoAttack == this.gameObject)
                {
                    SelfFullAtkBar = true;
                }

                else if (EnemytoAttack.GetComponent<HeroStateMachine>().current_CD + EnemytoAttack.GetComponent<HeroStateMachine>().max_CD * x.AtkBarBoostPercent / 100 < EnemytoAttack.GetComponent<HeroStateMachine>().max_CD)
                {
                    EnemytoAttack.GetComponent<HeroStateMachine>().current_CD += EnemytoAttack.GetComponent<HeroStateMachine>().max_CD * x.AtkBarBoostPercent / 100;
                }
                else
                {
                    EnemytoAttack.GetComponent<HeroStateMachine>().current_CD = EnemytoAttack.GetComponent<HeroStateMachine>().max_CD;
                }
                EnemytoAttack.GetComponent<HeroStateMachine>().CheckProgressBar();
            }
        }
    }
    void doAtkBarDecrease(BaseAttack x)
    {
        if (x.Effect[6])
        {
            if (x.IsItEntirePartyAtkBarReduction)
            {
                foreach (GameObject target in BSM.EnemiesInBattle)
                {
                    if (target.GetComponent<EnemyStateMachine>().current_CD - target.GetComponent<EnemyStateMachine>().max_CD * x.AtkBarReductionPercent / 100 > 0)
                    {
                        target.GetComponent<EnemyStateMachine>().current_CD -= target.GetComponent<EnemyStateMachine>().max_CD * x.AtkBarReductionPercent / 100;
                    }
                    else
                    {
                        target.GetComponent<EnemyStateMachine>().current_CD = 0;
                    }

                    target.GetComponent<EnemyStateMachine>().CheckProgressBar();
                }
            }
            else
            {
                if (EnemytoAttack.GetComponent<EnemyStateMachine>().current_CD - EnemytoAttack.GetComponent<EnemyStateMachine>().max_CD * x.AtkBarReductionPercent / 100 > 0)
                {
                    EnemytoAttack.GetComponent<EnemyStateMachine>().current_CD -= EnemytoAttack.GetComponent<EnemyStateMachine>().max_CD * x.AtkBarReductionPercent / 100;
                }
                else
                {
                    EnemytoAttack.GetComponent<EnemyStateMachine>().current_CD = 0;
                }
                EnemytoAttack.GetComponent<EnemyStateMachine>().CheckProgressBar();
            }
        }
    }
    public void CheckProgressBar()
    {
        float calc_CD = 6 * (current_CD / max_CD);
        atkBar.transform.localScale = new Vector3(Mathf.Clamp(calc_CD, 0, 6), atkBar.transform.localScale.y, atkBar.transform.localScale.z);
    }
    void DoCleanse(BaseAttack x)
    {
        if (x.Effect[8] == true)
            if (x.IsItEntirePartyCleanse)
            {
                if (x.CleanseAll)
                {
                    foreach (GameObject target in BSM.HeroesInBattle)
                    {
                        target.GetComponent<HeroStateMachine>().hero.atkDeBuffTurns = 0;
                        target.GetComponent<HeroStateMachine>().hero.defDeBuffTurns = 0;
                        target.GetComponent<HeroStateMachine>().hero.dotDeBuffTurns = 0;
                        target.GetComponent<HeroStateMachine>().hero.spdDeBuffTurns = 0;
                        target.GetComponent<HeroStateMachine>().statChangeCheck();
                    }
                }
                else
                {

                    int c;
                    List<int> DeBuffTrue = new List<int>();
                    foreach (GameObject target in BSM.HeroesInBattle)
                    {
                        for (int i = 0; i < x.DeBuffstoCleanse; i++)
                        {
                            DeBuffTrue.Clear();
                            if (target.GetComponent<HeroStateMachine>().hero.atkDeBuffTurns > 0)
                                DeBuffTrue.Add(0);
                            if (target.GetComponent<HeroStateMachine>().hero.spdDeBuffTurns > 0)
                                DeBuffTrue.Add(1);
                            if (target.GetComponent<HeroStateMachine>().hero.defDeBuffTurns > 0)
                                DeBuffTrue.Add(2);
                            if (target.GetComponent<HeroStateMachine>().hero.dotDeBuffTurns > 0)
                                DeBuffTrue.Add(3);

                            int a = DeBuffTrue.Count;
                            Debug.Log("a = " + a);
                            if (a > 0)
                            {
                                int num = UnityEngine.Random.Range(0, a);
                                c = DeBuffTrue[num];

                                Debug.Log("num = " + num);
                                if (c == 0)
                                    target.GetComponent<HeroStateMachine>().hero.atkDeBuffTurns = 0;
                                if (c == 1)
                                    target.GetComponent<HeroStateMachine>().hero.spdDeBuffTurns = 0;
                                if (c == 2)
                                    target.GetComponent<HeroStateMachine>().hero.defDeBuffTurns = 0;
                                if (c == 3)
                                    target.GetComponent<HeroStateMachine>().hero.dotDeBuffTurns = 0;
                            }
                        }
                        target.GetComponent<HeroStateMachine>().statChangeCheck();
                    }
                }
            }
            else
            {
                if (x.CleanseAll)
                {
                    EnemytoAttack.GetComponent<HeroStateMachine>().hero.atkDeBuffTurns = 0;
                    EnemytoAttack.GetComponent<HeroStateMachine>().hero.defDeBuffTurns = 0;
                    EnemytoAttack.GetComponent<HeroStateMachine>().hero.dotDeBuffTurns = 0;
                    EnemytoAttack.GetComponent<HeroStateMachine>().hero.spdDeBuffTurns = 0;

                }
                else
                {
                    int c;
                    List<int> DeBuffTrue = new List<int>();
                    for (int i = 0; i < x.DeBuffstoCleanse; i++)
                    {
                        DeBuffTrue.Clear();
                        if (EnemytoAttack.GetComponent<HeroStateMachine>().hero.atkDeBuffTurns > 0)
                            DeBuffTrue.Add(0);
                        if (EnemytoAttack.GetComponent<HeroStateMachine>().hero.spdDeBuffTurns > 0)
                            DeBuffTrue.Add(1);
                        if (EnemytoAttack.GetComponent<HeroStateMachine>().hero.defDeBuffTurns > 0)
                            DeBuffTrue.Add(2);
                        if (EnemytoAttack.GetComponent<HeroStateMachine>().hero.dotDeBuffTurns > 0)
                            DeBuffTrue.Add(3);

                        int a = DeBuffTrue.Count;
                        Debug.Log("a = " + a);
                        if (a > 0)
                        {
                            int num = UnityEngine.Random.Range(0, a);
                            c = DeBuffTrue[num];

                            Debug.Log("num = " + num);
                            if (c == 0)
                                EnemytoAttack.GetComponent<HeroStateMachine>().hero.atkDeBuffTurns = 0;
                            if (c == 1)
                                EnemytoAttack.GetComponent<HeroStateMachine>().hero.spdDeBuffTurns = 0;
                            if (c == 2)
                                EnemytoAttack.GetComponent<HeroStateMachine>().hero.defDeBuffTurns = 0;
                            if (c == 3)
                                EnemytoAttack.GetComponent<HeroStateMachine>().hero.dotDeBuffTurns = 0;
                        }
                    }
                    EnemytoAttack.GetComponent<HeroStateMachine>().statChangeCheck();
                }
            }
    }
    void DoStrip(BaseAttack x)
    {
        if (x.Effect[9] == true)
            if (x.IsItEntirePartyStrip)
            {
                if (x.StripAll)
                {
                    foreach (GameObject target in BSM.EnemiesInBattle)
                    {
                        target.GetComponent<EnemyStateMachine>().enemy.atkBuffTurns = 0;
                        target.GetComponent<EnemyStateMachine>().enemy.defBuffTurns = 0;
                        target.GetComponent<EnemyStateMachine>().enemy.recBuffTurns = 0;
                        target.GetComponent<EnemyStateMachine>().enemy.spdBuffTurns = 0;
                        target.GetComponent<EnemyStateMachine>().statChangeCheck();
                    }
                }
                else
                {

                    int c;
                    List<int> BuffTrue = new List<int>();
                    foreach (GameObject target in BSM.EnemiesInBattle)
                    {
                        for (int i = 0; i < x.BuffsToStrip; i++)
                        {
                            BuffTrue.Clear();
                            if (target.GetComponent<EnemyStateMachine>().enemy.atkBuffTurns > 0)
                                BuffTrue.Add(0);
                            if (target.GetComponent<EnemyStateMachine>().enemy.spdBuffTurns > 0)
                                BuffTrue.Add(1);
                            if (target.GetComponent<EnemyStateMachine>().enemy.defBuffTurns > 0)
                                BuffTrue.Add(2);
                            if (target.GetComponent<EnemyStateMachine>().enemy.recBuffTurns > 0)
                                BuffTrue.Add(3);

                            int a = BuffTrue.Count;
                            Debug.Log("a = " + a);
                            if (a > 0)
                            {
                                int num = UnityEngine.Random.Range(0, a);
                                c = BuffTrue[num];

                                Debug.Log("num = " + num);
                                if (c == 0)
                                    target.GetComponent<EnemyStateMachine>().enemy.atkBuffTurns = 0;
                                if (c == 1)
                                    target.GetComponent<EnemyStateMachine>().enemy.spdBuffTurns = 0;
                                if (c == 2)
                                    target.GetComponent<EnemyStateMachine>().enemy.defBuffTurns = 0;
                                if (c == 3)
                                    target.GetComponent<EnemyStateMachine>().enemy.recBuffTurns = 0;
                            }
                        }
                        target.GetComponent<EnemyStateMachine>().statChangeCheck();
                    }
                }
            }
            else
            {
                if (x.StripAll)
                {
                    EnemytoAttack.GetComponent<EnemyStateMachine>().enemy.atkDeBuffTurns = 0;
                    EnemytoAttack.GetComponent<EnemyStateMachine>().enemy.defDeBuffTurns = 0;
                    EnemytoAttack.GetComponent<EnemyStateMachine>().enemy.dotDeBuffTurns = 0;
                    EnemytoAttack.GetComponent<EnemyStateMachine>().enemy.spdDeBuffTurns = 0;

                }
                else
                {
                    int c;
                    List<int> BuffTrue = new List<int>();
                    for (int i = 0; i < x.DeBuffstoCleanse; i++)
                    {
                        BuffTrue.Clear();
                        if (EnemytoAttack.GetComponent<EnemyStateMachine>().enemy.atkBuffTurns > 0)
                            BuffTrue.Add(0);
                        if (EnemytoAttack.GetComponent<EnemyStateMachine>().enemy.spdBuffTurns > 0)
                            BuffTrue.Add(1);
                        if (EnemytoAttack.GetComponent<EnemyStateMachine>().enemy.defBuffTurns > 0)
                            BuffTrue.Add(2);
                        if (EnemytoAttack.GetComponent<EnemyStateMachine>().enemy.recBuffTurns > 0)
                            BuffTrue.Add(3);

                        int a = BuffTrue.Count;
                        Debug.Log("a = " + a);
                        if (a > 0)
                        {
                            int num = UnityEngine.Random.Range(0, a);
                            c = BuffTrue[num];

                            Debug.Log("num = " + num);
                            if (c == 0)
                                EnemytoAttack.GetComponent<EnemyStateMachine>().enemy.atkBuffTurns = 0;
                            if (c == 1)
                                EnemytoAttack.GetComponent<EnemyStateMachine>().enemy.spdBuffTurns = 0;
                            if (c == 2)
                                EnemytoAttack.GetComponent<EnemyStateMachine>().enemy.defBuffTurns = 0;
                            if (c == 3)
                                EnemytoAttack.GetComponent<EnemyStateMachine>().enemy.recBuffTurns = 0;
                        }
                    }
                    EnemytoAttack.GetComponent<EnemyStateMachine>().statChangeCheck();
                }
            }
    }
    public int TypeMatch(BaseMonster a, BaseMonster b)
    {
        int x = 1; //Neutral : x = 1, Strong : x = 2, Weak : x = 0
        if (a.MonsterType == BaseMonster.Type.FIRE)
        {
            if (b.MonsterType == BaseMonster.Type.DARK || b.MonsterType == BaseMonster.Type.LIGHT || b.MonsterType == BaseMonster.Type.FIRE)
            {
                return x;
            }
            else if (b.MonsterType == BaseMonster.Type.WATER)
            {
                x -= 1;
                return x;
            }
            else if (b.MonsterType == BaseMonster.Type.GRASS)
            {
                x += 1;
                return x;
            }
        }
        else if (a.MonsterType == BaseMonster.Type.WATER)
        {
            if (b.MonsterType == BaseMonster.Type.DARK || b.MonsterType == BaseMonster.Type.LIGHT || b.MonsterType == BaseMonster.Type.WATER)
            {
                return x;
            }
            else if (b.MonsterType == BaseMonster.Type.GRASS)
            {
                x -= 1;
                return x;
            }
            else if (b.MonsterType == BaseMonster.Type.FIRE)
            {
                x += 1;
                return x;
            }
        }
        else if (a.MonsterType == BaseMonster.Type.GRASS)
        {
            if (b.MonsterType == BaseMonster.Type.DARK || b.MonsterType == BaseMonster.Type.LIGHT || b.MonsterType == BaseMonster.Type.GRASS)
            {
                return x;
            }
            else if (b.MonsterType == BaseMonster.Type.FIRE)
            {
                x -= 1;
                return x;
            }
            else if (b.MonsterType == BaseMonster.Type.WATER)
            {
                x += 1;
                return x;
            }
        }
        else if (a.MonsterType == BaseMonster.Type.DARK)
        {
            if (b.MonsterType == BaseMonster.Type.WATER || b.MonsterType == BaseMonster.Type.FIRE || b.MonsterType == BaseMonster.Type.GRASS || b.MonsterType == BaseMonster.Type.DARK)
            {
                return x;
            }
            else if (b.MonsterType == BaseMonster.Type.LIGHT)
            {
                x += 1;
                return x;
            }
        }
        else
        {
            if (b.MonsterType == BaseMonster.Type.WATER || b.MonsterType == BaseMonster.Type.FIRE || b.MonsterType == BaseMonster.Type.GRASS || b.MonsterType == BaseMonster.Type.LIGHT)
            {
                return x;
            }
            else if (b.MonsterType == BaseMonster.Type.DARK)
            {
                x += 1;
                return x;
            }
        }
        return 99;
    }
    public int FindOpenBoxLocation()
    {
        for (int i = 0; i < 9; i++)
        {
            if (boxArray[i] == false)
                return i + 1;
        }
        return 0;

    }

    public bool checkHpStatus(float percent)  //true means lower than percent
    {
        if (hero.HpCurrent <= hero.HpMax * percent / 100)
        {
            return true;
        }
        if (hero.HpCurrent > hero.HpMax * percent / 100)
        {
            return false;
        }
        return false;
    }
    public int checkifDebuffed()   //returns int #of debuffs
    {
        int x = 0;
        if (hero.defDeBuffTurns != 0)
            x++;
        if (hero.dotDeBuffTurns != 0)
            x++;
        if (isStun != 0)
            x++;
        if (hero.spdDeBuffTurns != 0)
            x++;
        if (hero.atkDeBuffTurns != 0)
            x++;

        return x;
    }
    public int checkifBuffed()   //returns int #of debuffs
    {
        int x = 0;
        if (hero.defBuffTurns != 0)
            x++;
        if (hero.recBuffTurns != 0)
            x++;
        if (hero.spdBuffTurns != 0)
            x++;
        if (hero.atkBuffTurns != 0)
            x++;

        return x;
    }

    public void ShowFloatingText(GameObject x)
    {
        GameObject y = Instantiate(FloatingTextTest, x.transform.position, Quaternion.identity, x.transform);
        y.transform.localScale = new Vector3(-1, 1, 1);
        y.transform.localPosition += new Vector3(0, 4f, 0);
    }
    
    VertexPath GeneratePath(Vector2[] points, bool closedPath)
    {
        // Create a closed, 2D bezier path from the supplied points array
        // These points are treated as anchors, which the path will pass through
        // The control points for the path will be generated automatically
        BezierPath bezierPath = new BezierPath(points, closedPath, PathSpace.xy);
        // Then create a vertex path from the bezier path, to be used for movement etc
        return new VertexPath(bezierPath);
    }

    bool isUsable(BaseAttack x)
    {
        if (x == hero.attacks[0])
        {
            if (Cd1 == 0)
                return true;
        }
        if (x == hero.attacks[1])
        {
            if (Cd2 == 0)
                return true;
        }
        if (x == hero.attacks[2])
        {
            if (Cd3 == 0)
                return true;
        }
        if (x == hero.attacks[3])
        {
            if (Cd4 == 0)
                return true;
        }
        return false;
    }
    bool IsReviveUsable(BaseAttack x)
    {
        if (x.Effect[4])
        {
            foreach (GameObject target in BSM.HeroesInBattleTotal)
            {
                if (!target.GetComponent<HeroStateMachine>().HeroAlive)
                    return true;
            }
        }
        return false;
    }
    bool ShouldUseMove(BaseAttack x)
    {
        if (IsReviveUsable(x))
            return true;
        if (x.Effect[2])
        {
            return true;
        }
        if (x.Effect[1])
        {
            foreach (GameObject target in BSM.HeroesInBattle)
            {
                if (target.GetComponent<HeroStateMachine>().checkHpStatus(70))
                    return true;
            }
        }
        if (x.Effect[8])
        {
            foreach (GameObject target in BSM.HeroesInBattle)
            {
                if (target.GetComponent<HeroStateMachine>().checkifDebuffed() > 0)
                    return true;
            }
        }
        if (x.Effect[9])
        {
            foreach (GameObject target in BSM.EnemiesInBattle)
            {
                if (target.GetComponent<EnemyStateMachine>().checkifBuffed() > 0)
                    return true;
            }
        }
        if (x.Effect[0] || x.Effect[2] || x.Effect[3] || x.Effect[5] || x.Effect[6] || x.Effect[7] && !x.Effect[4])
            return true;

        return false;
    }
    public List<GameObject> SortByHpLowest(List<GameObject> sortList)
    {
        List<GameObject> ReturnList = new List<GameObject>();

        for (int i = 0; i < sortList.Count; i++)
        {
            ReturnList.Add(LowestHpTarget(sortList));
            sortList.Remove(LowestHpTarget(sortList));
        }
        return ReturnList;
    }
    public GameObject LowestHpTarget(List<GameObject> SortList)
    {
        float lowestHpPercent = 100f;
        foreach (GameObject target in SortList)
        {
            if (target.GetComponent<EnemyStateMachine>().enemy.HpCurrent / target.GetComponent<EnemyStateMachine>().enemy.HpBase * 100 < lowestHpPercent)
                lowestHpPercent = target.GetComponent<EnemyStateMachine>().enemy.HpCurrent / target.GetComponent<EnemyStateMachine>().enemy.HpBase * 100;
        }
        foreach (GameObject target in SortList)
        {
            if (Math.Abs(target.GetComponent<EnemyStateMachine>().enemy.HpCurrent / target.GetComponent<EnemyStateMachine>().enemy.HpBase * 100 - lowestHpPercent) < 1)
                return target;
        }
        return null;
    }






    void ChooseHeroMove()  //Enemy attack to HeroAttack   
    {
        TurnInformation HeroAttack= new TurnInformation();
        bool MoveChosen = false;
        HeroAttack.AttackerGameObject = this.gameObject;
        HeroAttack.Attacker = name;
        bool MandatoryRevive = false;
        bool MandatoryHeal = false;
        bool MandatoryCleanse = false;
        bool RemainingMoves = false;

        //100% REVIVE
        if (hero.attacks[1].Effect[4] || hero.attacks[2].Effect[4] || hero.attacks[3].Effect[4])
        {
            if (hero.attacks[3].Effect[4] && !MoveChosen)
            {
                if (IsReviveUsable(hero.attacks[3]) && isUsable(hero.attacks[3]))
                {
                    MoveChosen = true;
                    MandatoryRevive = true;
                    HeroAttack.ChosenAttack = hero.attacks[3];
                }
            }
            if (hero.attacks[2].Effect[4] && !MoveChosen)
            {
                if (IsReviveUsable(hero.attacks[2]) && isUsable(hero.attacks[2]))
                {
                    MoveChosen = true;
                    MandatoryRevive = true;
                    HeroAttack.ChosenAttack = hero.attacks[2];

                }
            }
            if (hero.attacks[1].Effect[4] && !MoveChosen)
            {
                if (IsReviveUsable(hero.attacks[1]) && isUsable(hero.attacks[1]))
                {
                    MoveChosen = true;
                    MandatoryRevive = true;
                    HeroAttack.ChosenAttack = hero.attacks[1];

                }
            }
        }
        if (MoveChosen && MandatoryRevive)
        {
            List<GameObject> DeadEnemies = new List<GameObject>();
            DeadEnemies.Clear();
            foreach (GameObject target in BSM.HeroesInBattleTotal)
            {
                if (!target.GetComponent<HeroStateMachine>().HeroAlive)
                    DeadEnemies.Add(target);
            }
            int herotoRevive = UnityEngine.Random.Range(0, DeadEnemies.Count);
            HeroAttack.AttackersTarget = DeadEnemies[herotoRevive];
        }


        //HEAL  50 %
        if (hero.attacks[1].Effect[1] || hero.attacks[2].Effect[1] || hero.attacks[3].Effect[1] && !MoveChosen)
        {
            bool checkhp50 = false;
            foreach (GameObject target in BSM.HeroesInBattle)
            {
                if (target.GetComponent<HeroStateMachine>().checkHpStatus(50))
                    checkhp50 = true;
            }

            if (hero.attacks[3].Effect[1] && !MoveChosen)
            {
                if (isUsable(hero.attacks[3]) && checkhp50)
                {
                    MoveChosen = true;
                    MandatoryHeal = true;
                    HeroAttack.ChosenAttack = hero.attacks[3];

                }
            }
            if (hero.attacks[2].Effect[1] && !MoveChosen)
            {
                if (isUsable(hero.attacks[2]) && checkhp50)
                {
                    MoveChosen = true;
                    MandatoryHeal = true;
                    HeroAttack.ChosenAttack = hero.attacks[2];
                }
            }
            if (hero.attacks[1].Effect[1] && !MoveChosen)
            {
                if (isUsable(hero.attacks[1]) && checkhp50)
                {
                    MoveChosen = true;
                    MandatoryHeal = true;
                    HeroAttack.ChosenAttack = hero.attacks[1];
                }
            }
        }
        if (MoveChosen && MandatoryHeal)
        {
            float lowestHpPercent = 100f;
            foreach (GameObject target in BSM.HeroesInBattle)
            {
                if (target.GetComponent<HeroStateMachine>().hero.HpCurrent / target.GetComponent<HeroStateMachine>().hero.HpBase * 100f < lowestHpPercent)
                    lowestHpPercent = target.GetComponent<HeroStateMachine>().hero.HpCurrent / target.GetComponent<HeroStateMachine>().hero.HpBase * 100f;
            }
            List<GameObject> HeroeswithLowestHp = new List<GameObject>();
            HeroeswithLowestHp.Clear();
            foreach (GameObject target in BSM.HeroesInBattle)
            {
                if (Math.Abs(lowestHpPercent - target.GetComponent<HeroStateMachine>().hero.HpCurrent / target.GetComponent<HeroStateMachine>().hero.HpBase * 100f) < 1f)
                    HeroeswithLowestHp.Add(target);

            }
            int HerotoHeal = UnityEngine.Random.Range(0, HeroeswithLowestHp.Count);
            HeroAttack.AttackersTarget = HeroeswithLowestHp[HerotoHeal];
        }


        //80% CLEANSE
        int a = UnityEngine.Random.Range(0, 100);
        float deBuffCount = 0;
        foreach (GameObject target in BSM.HeroesInBattle)
        {
            if (target.GetComponent<HeroStateMachine>().checkifDebuffed() > deBuffCount)
            {
                deBuffCount = target.GetComponent<HeroStateMachine>().checkifDebuffed();
            }
        }
        if (deBuffCount > 2 || (deBuffCount > 1 && a > 20) || (deBuffCount > 0 && a > 35))
        {
            if (hero.attacks[1].Effect[8] || hero.attacks[2].Effect[8] || hero.attacks[3].Effect[8] && !MoveChosen)
            {
                if (hero.attacks[3].Effect[8] && !MoveChosen)
                {
                    if (isUsable(hero.attacks[1]))
                    {
                        MoveChosen = true;
                        MandatoryCleanse = true;
                        HeroAttack.ChosenAttack = hero.attacks[3];

                    }
                }
                if (hero.attacks[2].Effect[8] && !MoveChosen)
                {
                    if (isUsable(hero.attacks[2]))
                    {
                        MoveChosen = true;
                        MandatoryCleanse = true;
                        HeroAttack.ChosenAttack = hero.attacks[2];

                    }
                }
                if (hero.attacks[1].Effect[8] && !MoveChosen)
                {
                    if (isUsable(hero.attacks[1]))
                    {
                        MoveChosen = true;
                        MandatoryCleanse = true;
                        HeroAttack.ChosenAttack = hero.attacks[1];

                    }
                }
            }
        }
        if (MoveChosen && MandatoryCleanse)
        {
            List<GameObject> HeroestoCleanse = new List<GameObject>();
            HeroestoCleanse.Clear();
            foreach (GameObject target in BSM.HeroesInBattle)   
            {
                if (target.GetComponent<HeroStateMachine>().checkifDebuffed() == deBuffCount)
                    HeroestoCleanse.Add(target);
            }
            int HerotoCleanse = UnityEngine.Random.Range(0, HeroestoCleanse.Count);
            HeroAttack.AttackersTarget = HeroestoCleanse[HerotoCleanse];
        }
        a = UnityEngine.Random.Range(0, 100);
        if (a > 20 && !MoveChosen)
        {
            if (isUsable(hero.attacks[3]) && ShouldUseMove(hero.attacks[3]))
            {
                RemainingMoves = true; MoveChosen = true;
                HeroAttack.ChosenAttack = hero.attacks[3];
            }
        }
        a = UnityEngine.Random.Range(0, 100);
        if (a > 20 && !MoveChosen)
        {
            if (isUsable(hero.attacks[2]) && ShouldUseMove(hero.attacks[2]))
            {
                RemainingMoves = true; MoveChosen = true;
                HeroAttack.ChosenAttack = hero.attacks[2];
            }
        }
        a = UnityEngine.Random.Range(0, 100);
        if (a > 20 && !MoveChosen)
        {
            if (isUsable(hero.attacks[1]) && ShouldUseMove(hero.attacks[1]))
            {
                RemainingMoves = true; MoveChosen = true;
                HeroAttack.ChosenAttack = hero.attacks[1];
            }
        }
        if (!MoveChosen)    //no need for a if we get here we have to use this
        {
            if (isUsable(hero.attacks[0]) && ShouldUseMove(hero.attacks[0]))
            {
                RemainingMoves = true; MoveChosen = true;
                HeroAttack.ChosenAttack = hero.attacks[0];
            }
        }


        if (MoveChosen && RemainingMoves)
        {
            bool targetChosen = false;
            //target depends on move
            BaseAttack x = HeroAttack.ChosenAttack; //store in x just to make it easier to write, fuck writing enemyattack.chosenattack every time

            if (x.Effect[0] || x.Effect[3] || x.Effect[5] || x.Effect[9])
            {

                List<GameObject> indList_Strong = new List<GameObject>();
                List<GameObject> indList_Neut = new List<GameObject>();
                List<GameObject> indList_Weak = new List<GameObject>();
                List<GameObject> indList_defbroken_Strong = new List<GameObject>();
                List<GameObject> indList_defbroken_Neut = new List<GameObject>();
                List<GameObject> indList_defbroken_Weak = new List<GameObject>();
                List<GameObject> indListAll_Strong = new List<GameObject>();
                List<GameObject> indListAll_Neut = new List<GameObject>();
                List<GameObject> indListAll_Weak = new List<GameObject>();
                foreach (GameObject enemy in BSM.EnemiesInBattle)
                {

                    if (enemy.GetComponent<EnemyStateMachine>().TypeMatch(hero, enemy.GetComponent<EnemyStateMachine>().enemy) == 2)
                    {
                        indListAll_Strong.Add(enemy);

                        if (enemy.GetComponent<EnemyStateMachine>().enemy.defDeBuffTurns > 0)
                        {
                            indList_defbroken_Strong.Add(enemy);
                        }
                        else
                        {
                            indList_Strong.Add(enemy);
                        }
                    }
                    else if (enemy.GetComponent<EnemyStateMachine>().TypeMatch(hero, enemy.GetComponent<EnemyStateMachine>().enemy) == 1)
                    {
                        indListAll_Neut.Add(enemy);

                        if (enemy.GetComponent<EnemyStateMachine>().enemy.defDeBuffTurns > 0)
                        {
                            indList_defbroken_Neut.Add(enemy);
                        }
                        else
                        {
                            indList_Neut.Add(enemy);
                        }
                    }
                    if (enemy.GetComponent<EnemyStateMachine>().TypeMatch(hero, enemy.GetComponent<EnemyStateMachine>().enemy) == 0)
                    {
                        indListAll_Weak.Add(enemy);

                        if (enemy.GetComponent<EnemyStateMachine>().enemy.defDeBuffTurns > 0)
                        {
                            indList_defbroken_Weak.Add(enemy);
                        }
                        else
                        {
                            indList_Weak.Add(enemy);
                        }
                    }
                }
                if (x.Effect[9] && !targetChosen && !x.Effect[0])
                {
                    int curr_B = 0;
                    GameObject curr_tar = BSM.EnemiesInBattle[0];

                    List<GameObject> indListAll_Final = new List<GameObject>();
                    foreach (GameObject target in indListAll_Strong)
                    {
                        if (target.GetComponent<EnemyStateMachine>().checkifBuffed() > curr_B)
                        {
                            curr_B = target.GetComponent<EnemyStateMachine>().checkifBuffed();
                        }
                    }
                    foreach (GameObject target in indListAll_Strong)
                    {

                        if (target.GetComponent<EnemyStateMachine>().checkifBuffed() == curr_B)
                        {
                            indListAll_Final.Add(target);
                        }
                    }
                    if (indListAll_Strong.Count > 0)
                    {
                        curr_tar = indListAll_Final[UnityEngine.Random.Range(0, indListAll_Final.Count)];
                        indListAll_Final.Clear();
                    }
                    int n = curr_B;
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    foreach (GameObject target in indListAll_Neut)
                    {
                        if (target.GetComponent<EnemyStateMachine>().checkifBuffed() > curr_B)
                        {
                            curr_B = target.GetComponent<EnemyStateMachine>().checkifBuffed();
                        }
                    }
                    foreach (GameObject target in indListAll_Neut)
                    {

                        if (target.GetComponent<EnemyStateMachine>().checkifBuffed() == curr_B && curr_B != 0)
                        {
                            indListAll_Final.Add(target);
                        }
                    }
                    int i = curr_B;
                    if (curr_B > n)
                    {
                        curr_tar = indListAll_Final[UnityEngine.Random.Range(0, indListAll_Final.Count)];
                        indListAll_Final.Clear();
                    }
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    foreach (GameObject target in indListAll_Weak)
                    {
                        if (target.GetComponent<EnemyStateMachine>().checkifBuffed() > curr_B)
                        {
                            curr_B = target.GetComponent<EnemyStateMachine>().checkifBuffed();
                        }
                    }
                    foreach (GameObject target in indListAll_Weak)
                    {

                        if (target.GetComponent<EnemyStateMachine>().checkifBuffed() == curr_B && curr_B != 0)
                        {
                            indListAll_Final.Add(target);
                        }
                    }
                    if (curr_B > n && curr_B > i)
                    {
                        curr_tar = indListAll_Final[UnityEngine.Random.Range(0, indListAll_Final.Count)];
                        indListAll_Final.Clear();
                    }

                    HeroAttack.AttackersTarget = curr_tar;
                    targetChosen = true;
                }

                if (x.Effect[9] && !targetChosen)
                {
                    int curr_B = 0;
                    GameObject curr_tar = BSM.EnemiesInBattle[0];

                    List<GameObject> indListAll_Final = new List<GameObject>();
                    foreach (GameObject target in indList_defbroken_Strong)
                    {
                        if (target.GetComponent<EnemyStateMachine>().checkifBuffed() > curr_B)
                        {
                            curr_B = target.GetComponent<EnemyStateMachine>().checkifBuffed();
                        }
                    }
                    foreach (GameObject target in indList_defbroken_Strong)
                    {

                        if (target.GetComponent<EnemyStateMachine>().checkifBuffed() == curr_B)
                        {
                            indListAll_Final.Add(target);
                        }
                    }
                    if (indList_defbroken_Strong.Count > 0)
                    {
                        curr_tar = indListAll_Final[UnityEngine.Random.Range(0, indListAll_Final.Count)];
                        indListAll_Final.Clear();
                    }
                    int n = curr_B;
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    foreach (GameObject target in indList_defbroken_Neut)
                    {
                        if (target.GetComponent<EnemyStateMachine>().checkifBuffed() > curr_B)
                        {

                            curr_B = target.GetComponent<EnemyStateMachine>().checkifBuffed();
                        }
                    }
                    foreach (GameObject target in indList_defbroken_Neut)
                    {

                        if (target.GetComponent<EnemyStateMachine>().checkifBuffed() == curr_B && curr_B != 0)
                        {
                            indListAll_Final.Add(target);
                        }
                    }
                    int i = curr_B;
                    if (curr_B > n)
                    {
                        curr_tar = indListAll_Final[UnityEngine.Random.Range(0, indListAll_Final.Count)];
                        indListAll_Final.Clear();
                    }
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    foreach (GameObject target in indListAll_Strong)
                    {
                        if (target.GetComponent<EnemyStateMachine>().checkifBuffed() > curr_B)
                        {
                            curr_B = target.GetComponent<EnemyStateMachine>().checkifBuffed();
                        }
                    }
                    foreach (GameObject target in indListAll_Strong)
                    {

                        if (target.GetComponent<EnemyStateMachine>().checkifBuffed() == curr_B && curr_B != 0)
                        {
                            indListAll_Final.Add(target);
                        }
                    }
                    int g = curr_B;
                    if (curr_B > n && curr_B > i)
                    {
                        curr_tar = indListAll_Final[UnityEngine.Random.Range(0, indListAll_Final.Count)];
                        indListAll_Final.Clear();
                    }

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    foreach (GameObject target in indList_defbroken_Weak)
                    {
                        if (target.GetComponent<EnemyStateMachine>().checkifBuffed() > curr_B)
                        {
                            curr_B = target.GetComponent<EnemyStateMachine>().checkifBuffed();
                        }
                    }
                    foreach (GameObject target in indList_defbroken_Weak)
                    {

                        if (target.GetComponent<EnemyStateMachine>().checkifBuffed() == curr_B && curr_B != 0)
                        {
                            indListAll_Final.Add(target);
                        }
                    }
                    int ge = curr_B;
                    if (curr_B > n && curr_B > i && curr_B > g)
                    {
                        curr_tar = indListAll_Final[UnityEngine.Random.Range(0, indListAll_Final.Count)];
                        indListAll_Final.Clear();
                    }

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    foreach (GameObject target in indList_Neut)
                    {
                        if (target.GetComponent<EnemyStateMachine>().checkifBuffed() > curr_B)
                        {
                            curr_B = target.GetComponent<EnemyStateMachine>().checkifBuffed();
                        }
                    }
                    foreach (GameObject target in indList_Neut)
                    {

                        if (target.GetComponent<EnemyStateMachine>().checkifBuffed() == curr_B && curr_B != 0)
                        {
                            indListAll_Final.Add(target);
                        }
                    }
                    int r = curr_B;
                    if (curr_B > n && curr_B > i && curr_B > g && curr_B > ge)
                    {
                        curr_tar = indListAll_Final[UnityEngine.Random.Range(0, indListAll_Final.Count)];
                        indListAll_Final.Clear();
                    }

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    foreach (GameObject target in indList_Weak)
                    {
                        if (target.GetComponent<EnemyStateMachine>().checkifBuffed() > curr_B)
                        {
                            curr_B = target.GetComponent<EnemyStateMachine>().checkifBuffed();
                        }
                    }
                    foreach (GameObject target in indList_Weak)
                    {

                        if (target.GetComponent<EnemyStateMachine>().checkifBuffed() == curr_B && curr_B != 0)
                        {
                            indListAll_Final.Add(target);
                        }
                    }
                    if (curr_B > n && curr_B > i && curr_B > g && curr_B > ge && curr_B > r)
                    {
                        curr_tar = indListAll_Final[UnityEngine.Random.Range(0, indListAll_Final.Count)];
                        indListAll_Final.Clear();
                    }

                    HeroAttack.AttackersTarget = curr_tar;
                    targetChosen = true;
                }

                if (x.Effect[0] && !targetChosen)
                {

                    int e = UnityEngine.Random.Range(0, 100);
                    if (indList_defbroken_Strong.Count > 0)
                    {
                        indList_defbroken_Strong = SortByHpLowest(indList_defbroken_Strong);
                        foreach (GameObject target in indList_defbroken_Strong)
                        {
                            e = UnityEngine.Random.Range(0, 100);
                            if (e > 100 && !targetChosen)
                            {
                                HeroAttack.AttackersTarget = target;
                                targetChosen = true;
                            }
                        }
                        if (!targetChosen)
                        {
                            HeroAttack.AttackersTarget = indList_defbroken_Strong[indList_defbroken_Strong.Count - 1];
                        }

                    }
                    else if (indList_defbroken_Neut.Count > 0)
                    {
                        indList_defbroken_Neut = SortByHpLowest(indList_defbroken_Neut);

                        foreach (GameObject target in indList_defbroken_Neut)
                        {
                            e = UnityEngine.Random.Range(0, 100);
                            if (e > 20 && !targetChosen)
                            {
                                HeroAttack.AttackersTarget = target;
                                targetChosen = true;
                            }
                        }
                        if (!targetChosen)
                        {
                            HeroAttack.AttackersTarget = indList_defbroken_Neut[indList_defbroken_Neut.Count - 1];
                        }
                    }
                    else if (indList_Strong.Count > 0)
                    {
                        indList_Strong = SortByHpLowest(indList_Strong);

                        foreach (GameObject target in indList_Strong)
                        {
                            e = UnityEngine.Random.Range(0, 100);
                            if (e > 20 && !targetChosen)
                            {
                                HeroAttack.AttackersTarget = target;
                                targetChosen = true;
                            }
                        }
                        if (!targetChosen)
                        {
                            HeroAttack.AttackersTarget = indList_Strong[indList_Strong.Count - 1];
                        }
                    }
                    else if (indList_defbroken_Weak.Count > 0 && e > 50)
                    {
                        indList_defbroken_Weak = SortByHpLowest(indList_defbroken_Weak);

                        foreach (GameObject target in indList_defbroken_Weak)
                        {
                            e = UnityEngine.Random.Range(0, 100);
                            if (e > 20 && !targetChosen)
                            {
                                HeroAttack.AttackersTarget = target;
                                targetChosen = true;
                            }
                        }
                        if (!targetChosen)
                        {
                            HeroAttack.AttackersTarget = indList_defbroken_Weak[indList_defbroken_Weak.Count - 1];
                        }

                    }
                    else if (indList_Neut.Count > 0)
                    {
                        indList_Neut = SortByHpLowest(indList_Neut);

                        foreach (GameObject target in indList_Neut)
                        {
                            e = UnityEngine.Random.Range(0, 100);

                            if (e > 20 && !targetChosen)
                            {
                                HeroAttack.AttackersTarget = target;
                                targetChosen = true;
                            }
                        }
                        if (!targetChosen)
                        {
                            HeroAttack.AttackersTarget = indList_Neut[indList_Neut.Count - 1];
                        }

                    }
                    else if (indList_Weak.Count > 0)
                    {
                        indList_Weak = SortByHpLowest(indList_Weak);

                        foreach (GameObject target in indList_Weak)
                        {
                            e = UnityEngine.Random.Range(0, 100);

                            if (e > 20 && !targetChosen)
                            {
                                HeroAttack.AttackersTarget = target;
                                targetChosen = true;
                            }
                        }
                        if (!targetChosen)
                        {
                            HeroAttack.AttackersTarget = indList_Weak[indList_Weak.Count - 1];
                        }

                    }
                    targetChosen = true;
                }

                if (x.Effect[3] || x.Effect[5] && !targetChosen)
                {
                    if (indListAll_Strong.Count > 0 && !targetChosen)
                    {
                        HeroAttack.AttackersTarget = indListAll_Strong[UnityEngine.Random.Range(0, indListAll_Strong.Count)];
                        targetChosen = true;

                    }
                    else if (indListAll_Neut.Count > 0 && !targetChosen)
                    {
                        HeroAttack.AttackersTarget = indListAll_Neut[UnityEngine.Random.Range(0, indListAll_Neut.Count)];
                        targetChosen = true;


                    }
                    else if (indListAll_Weak.Count > 0 && !targetChosen)
                    {
                        HeroAttack.AttackersTarget = indListAll_Weak[UnityEngine.Random.Range(0, indListAll_Weak.Count)];
                        targetChosen = true;


                    }

                }


            }
            if (x.Effect[1] && !targetChosen)   //HEAL ACTIVATES IF ALLY BELOW 70% TARGETS LOWEST HP
            {

                int d = UnityEngine.Random.Range(0, 100);
                if (d > 0 && ShouldUseMove(HeroAttack.ChosenAttack))
                {
                    float lowestHpPercent = 100;
                    List<GameObject> HeroesToHeal = new List<GameObject>();
                    foreach (GameObject target in BSM.HeroesInBattle)
                    {
                        if (target.GetComponent<HeroStateMachine>().hero.HpCurrent / target.GetComponent<HeroStateMachine>().hero.HpBase * 100 < lowestHpPercent)
                            lowestHpPercent = target.GetComponent<HeroStateMachine>().hero.HpCurrent / target.GetComponent<HeroStateMachine>().hero.HpBase * 100;
                    }
                    foreach (GameObject target in BSM.HeroesInBattle)
                    {
                        if (Math.Abs(target.GetComponent<HeroStateMachine>().hero.HpCurrent / target.GetComponent<HeroStateMachine>().hero.HpBase * 100 - lowestHpPercent) < 1)
                        {
                            HeroesToHeal.Add(target);
                        }
                    }
                    HeroAttack.AttackersTarget = HeroesToHeal[UnityEngine.Random.Range(0, HeroesToHeal.Count)];
                    targetChosen = true;

                }
            }

            if (x.Effect[7] && !targetChosen)     //ATK BAR BOOST. ALWAYS ACTIVATES TARGETS LOWEST ATKBAR
            {
                float LowestAtkBarPercent = 100;
                List<GameObject> TargetsToBoost = new List<GameObject>();
                foreach (GameObject target in BSM.HeroesInBattle)
                {
                    if (target.GetComponent<HeroStateMachine>().current_CD / target.GetComponent<HeroStateMachine>().max_CD * 100 < LowestAtkBarPercent)
                        LowestAtkBarPercent = target.GetComponent<HeroStateMachine>().current_CD / target.GetComponent<HeroStateMachine>().max_CD * 100;
                }
                foreach (GameObject target in BSM.HeroesInBattle)
                {
                    if (Math.Abs(target.GetComponent<HeroStateMachine>().current_CD / target.GetComponent<HeroStateMachine>().max_CD * 100 - LowestAtkBarPercent) < 1)
                        TargetsToBoost.Add(target);
                }
                HeroAttack.AttackersTarget = TargetsToBoost[UnityEngine.Random.Range(0, TargetsToBoost.Count)];
                targetChosen = true;

            }

            if (x.Effect[2] && !targetChosen)   //BUFF ALWAYS ACTIVATES TARGETS RANDOM
            {
                HeroAttack.AttackersTarget = BSM.HeroesInBattle[UnityEngine.Random.Range(0, BSM.HeroesInBattle.Count)];
            }

            if (x.Effect[6] && !targetChosen)   //ATKBAR REDUCTION TARGETS HIGHEST ATKBAR
            {
                float highestAtkBarPercent = 0;
                foreach (GameObject target in BSM.EnemiesInBattle)
                {
                    if (target.GetComponent<EnemyStateMachine>().current_CD / target.GetComponent<EnemyStateMachine>().max_CD * 100f > highestAtkBarPercent)
                        highestAtkBarPercent = target.GetComponent<EnemyStateMachine>().current_CD / target.GetComponent<EnemyStateMachine>().max_CD * 100f;
                }
                List<GameObject> AtkBarRedTargets = new List<GameObject>();
                foreach (GameObject target in BSM.EnemiesInBattle)
                {
                    if (Math.Abs(highestAtkBarPercent - target.GetComponent<EnemyStateMachine>().current_CD / target.GetComponent<EnemyStateMachine>().max_CD * 100f) < 1f)
                        AtkBarRedTargets.Add(target);
                }
                int d = UnityEngine.Random.Range(0, AtkBarRedTargets.Count);
                HeroAttack.AttackersTarget = AtkBarRedTargets[d];
            }
        }

        HeroAttack.type = "Hero";
        BSM.CollectActions(HeroAttack);
        HeroState = HeroStates.IDLE;

    }





}