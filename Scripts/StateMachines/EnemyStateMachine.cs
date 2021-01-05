using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //to edit ui stuff such as health bars or buttons
using System;
using UnityEngine.Experimental.AI;
using System.Diagnostics;

public class EnemyStateMachine : MonoBehaviour
{

    Vector2 OgLocation;

    public BaseMonster enemy;
    private BattleStateMachine BSM;
    public GameObject arrow;
    public GameObject arrow_s;
    public GameObject arrow_w;
    public ShakeBehavior mainC;
    public enum EnemyStates
    {
        OPENANIMATIONS,
        ATTACKBARFILLING,
        CHOOSINGMOVE,
        ADDMOVETOBSM,
        PERFORMMOVE,
        DEAD,
        IDLE    //ATK BAR NOT MOVING
    }
    public EnemyStates EnemyState;

    //progressbar/hpbar stuff
    public float current_CD = 0f;
    public float max_CD = 10f;
    private Image progressBar; //we will drag our progress bar visual here
    public GameObject atkBar;
    public GameObject hpBar;


    //Link to Enemy and stuff for attacking
    public GameObject HerotoAttack;
    private bool actionStarted = false;
    public Vector3 StartPositon;
    private float animSpeed = 5f;
    public bool EnemyAlive = true;

    //to Select this Hero
    public PolygonCollider2D col;
    public bool selected = false;

    public bool moving = false;

    public int isStun = 0;
    public bool SelfFullAtkBar = false;
    public bool b = false;
    public int c = 0;


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
    private void Awake()
    {
        SetUp();
    }

    void Start()
    {
        enemy.UpdateStats();

    }


    void Update()
    {
        updateHpBar();
        SelectedCharacter();
        checkifDebuffed();

        switch (EnemyState)       //basically if else for all enum we choose below, self explanatory
        {
            case (EnemyStates.OPENANIMATIONS):
                //play open animation and wait till done
                EnemyState = EnemyStates.ATTACKBARFILLING;
                break;


            case (EnemyStates.ATTACKBARFILLING):
                UpdateProgressBar();
                break;


            case (EnemyStates.ADDMOVETOBSM):
                //func to choose enemy
                EnemyState = EnemyStates.CHOOSINGMOVE;
                break;


            case (EnemyStates.IDLE):

                updateHpBar();
                break;


            case (EnemyStates.CHOOSINGMOVE):

                ChooseEnemyMove();  //chosen move, target and populated TurnInformation (perform list is filled)

                break;


            case (EnemyStates.PERFORMMOVE):

                StartCoroutine(TimeForAction());


                break;


            case (EnemyStates.DEAD):                            //we reach this when hp drops to or below 0
                if (!EnemyAlive) return;
                else
                {
                    current_CD = 0;
                    col.enabled = false;
                    //change heros tag to keep track of dead
                    this.gameObject.tag = "DeadEnemy";

                    //hero is not attackable anymore
                    BSM.EnemiesInBattle.Remove(this.gameObject);     //takes him out of list of heroes in battle thus making him untargetable

                    //deactivate selector over hero if its on
                    arrow.SetActive(false);                         //arrow selector thing gone

                    //if hes in performlist take out of performlist
                    for (int i = 0; i < BSM.PerformList.Count; i++)    //we iterate thru perform list checking if this hero is in there, if so remove that handle turn

                        if (BSM.PerformList[i].AttackerGameObject == this.gameObject)    //if we find the term
                        {
                            BSM.PerformList.Remove(BSM.PerformList[i]); //takes out the handle turn from the perform list
                        }
                }
                //play death animation
                //set dead sprite or w.e

                EnemyAlive = false;
                BSM.DeadEnemies.Add(this.gameObject);
                //reset hero input
                BSM.battleStates = BattleStateMachine.performAction.CHECKALIVE; break;


        }
    }



    //functions
    public void SetUp()
    {
       // enemy.UpdateStats();

        OgLocation = new Vector2(this.transform.position.x, this.transform.position.y);
        GetComponent<Follower>().enabled = false;



        hpBar.transform.localScale = new Vector3(6f, 4f, 0f);
        atkBar.transform.localScale = new Vector3(6f, 4f, 0f);
        hpBar.transform.localPosition = new Vector3(4f, -3.76f, 0f);
        atkBar.transform.localPosition = new Vector3(4f, -4.5f, 0f);
        atkBar.GetComponent<SpriteRenderer>().flipX = true;
        hpBar.GetComponent<SpriteRenderer>().flipX = true;
        StartPositon = this.transform.position;

        col = gameObject.GetComponent<PolygonCollider2D>();
        col.enabled = false;
        selected = false;

        //arrow set up
        arrow.transform.localPosition = new Vector3(arrow.transform.localPosition.x, arrow.transform.localPosition.y + 5.5f, arrow.transform.localPosition.z);
        arrow_s.transform.localPosition = new Vector3(arrow_s.transform.localPosition.x, arrow_s.transform.localPosition.y + 5.5f, arrow_s.transform.localPosition.z);
        arrow_w.transform.localPosition = new Vector3(arrow_w.transform.localPosition.x, arrow_w.transform.localPosition.y + 5.5f, arrow_w.transform.localPosition.z);
        arrow.SetActive(false); //auto disable arrow at start
        arrow_s.SetActive(false);
        arrow_w.SetActive(false);

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
        EnemyState = EnemyStates.IDLE;
        StartPositon = transform.position;
        mainC = GameObject.Find("Main Camera").GetComponent<ShakeBehavior>();
    }
    void SelectedCharacter()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                foreach (GameObject enemy in BSM.EnemiesInBattleTotal)
                {
                    if (hit.transform.position == enemy.transform.position)
                    {
                        enemy.GetComponent<EnemyStateMachine>().selected = true;
                    }
                }
            }

        }
    }
    public void updateHpBar()
    {
        float x;
        float z = Math.Abs(hpBar.transform.localScale.x / 6 - enemy.HpCurrent / enemy.HpMax);
        if (z > 0.01)
        {
            x = hpBar.transform.localScale.x;
            if ((hpBar.transform.localScale.x / 6 - enemy.HpCurrent / enemy.HpMax) > 0.005)
            {
                x -= 6 * Time.deltaTime;
                hpBar.transform.localScale = new Vector3(Mathf.Clamp(x, 0, 6), hpBar.transform.localScale.y, hpBar.transform.localScale.z);//Update bar.
            }
            if ((hpBar.transform.localScale.x / 6 - enemy.HpCurrent / enemy.HpMax) < 0.005)
            {
                x += 6 * Time.deltaTime;
                hpBar.transform.localScale = new Vector3(Mathf.Clamp(x, 0, 6), hpBar.transform.localScale.y, hpBar.transform.localScale.z);//Update bar.
            }
        }
    }
    private bool movetospot(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }
    void UpdateProgressBar()
    {

        current_CD += (enemy.SpeedCurrent / 2) * Time.deltaTime; //calculation to be changed
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

            if (enemy.dotDeBuffTurns > 0)
            {
                TakeDmg(enemy.HpMax / 10);

            }
            if (enemy.recBuffTurns > 0)
            {
                TakeHeal(enemy.HpMax / 10);

            }

            if (isStun > 0)
            {
                current_CD = 0;
            }
            else
            {
                if (EnemyState != EnemyStates.DEAD)
                {
                    foreach (GameObject enemy in BSM.EnemiesInBattle)      //iterate thru all enemy in battle
                    {
                        if (enemy.GetComponent<EnemyStateMachine>() != this)
                        {
                            enemy.GetComponent<EnemyStateMachine>().EnemyState = EnemyStateMachine.EnemyStates.IDLE; //move all to waiting
                        }
                    }
                    foreach (GameObject hero in BSM.HeroesInBattle)
                    {

                        hero.GetComponent<HeroStateMachine>().HeroState = HeroStateMachine.HeroStates.IDLE; //move all to waiting

                    }
                    EnemyState = EnemyStates.CHOOSINGMOVE;
                }
            }
        }

    }


    public void TakeDmg(float GetDmgAmount)   //gotta be public to call from hero
    {
        enemy.HpCurrent -= GetDmgAmount;
        if (enemy.HpCurrent <= 0)
        {
            atkBar.transform.localScale = new Vector3(0f, atkBar.transform.localScale.y, atkBar.transform.localScale.z);
            enemy.HpCurrent = 0;
            EnemyState = EnemyStates.DEAD;
        }
    }
    
    private IEnumerator TimeForAction() //at the end we have to bsm>atkbarfilling  enemystatemachine>atkbarfilling
    {


        //current_CD to 0 and currentState to processing
        current_CD = 0f;
        if (actionStarted)
        {
            yield break; //breaks out of ienumerator
        }
        //animate enemy moving to hero
        actionStarted = true;
       
        

        //check if we gotta move
        if (BSM.PerformList[0].ChosenAttack.MoveToTarget)
        {
            Vector3 EnemyPosition = new Vector3(HerotoAttack.transform.position.x + 1.5f, HerotoAttack.transform.position.y, HerotoAttack.transform.position.z); //x-1.5f for heros and x+1.5 for enemies since they on opposite sides

            moving = true;
            while (movetospot(EnemyPosition)) { yield return null; }          //dont really get but moves to spot in mvoetospot func

        }
        yield return new WaitForSeconds(BSM.PerformList[0].ChosenAttack.animationTimeBeforeEffect);  //needed idk why says OUTOFRANGE ERROR OTHERWISE
        //REPEAT FOR ALL EFFECTS

        if (BSM.PerformList[0].ChosenAttack == enemy.attacks[0])
            Cd1 = enemy.attacks[0].CDAttack;
        if (BSM.PerformList[0].ChosenAttack == enemy.attacks[1])
            Cd2 = enemy.attacks[1].CDAttack;
        if (BSM.PerformList[0].ChosenAttack == enemy.attacks[2])
            Cd3 = enemy.attacks[2].CDAttack;
        if (BSM.PerformList[0].ChosenAttack == enemy.attacks[3])
            Cd4 = enemy.attacks[3].CDAttack;

        //do effects
        doDmg(BSM.PerformList[0].ChosenAttack);
        //REPEAT FOR ALLEFFECTS
        //1 heal
        DoHeal(BSM.PerformList[0].ChosenAttack);
        //2 buff
        DoBuff(BSM.PerformList[0].ChosenAttack);
        //3 debuff
        doDeBuff(BSM.PerformList[0].ChosenAttack);
        //4
        doRevive(BSM.PerformList[0].ChosenAttack);
        //5
        doStun(BSM.PerformList[0].ChosenAttack);
        //6
        doAtkBarDecrease(BSM.PerformList[0].ChosenAttack);
        //7
        doAtkBarIncrease(BSM.PerformList[0].ChosenAttack);
        //8
        DoCleanse(BSM.PerformList[0].ChosenAttack);
        DoStrip(BSM.PerformList[0].ChosenAttack);
        
        GameObject y = null;
        float atime = 0;
        RuntimeAnimatorController ac;
        if (BSM.PerformList[0].ChosenAttack.Animation != null)
        {
            if ((BSM.PerformList[0].ChosenAttack.IsItEntirePartyAtkBarBoost
                || BSM.PerformList[0].ChosenAttack.IsItEntirePartyBuff
                || BSM.PerformList[0].ChosenAttack.IsItEntirePartyCleanse
                || BSM.PerformList[0].ChosenAttack.IsItEntirePartyHeal)
                && c < 1)
            {
                foreach (GameObject target in BSM.EnemiesInBattle)
                {
                    y = Instantiate(BSM.PerformList[0].ChosenAttack.Animation, target.transform.position, Quaternion.identity, target.transform);
                    y.transform.localScale = new Vector3(-10, 10, 1);
                    y.transform.localPosition += new Vector3(0, 4f, 0);
                    y.GetComponent<SpriteRenderer>().sortingOrder = 5;
                }
                b = true;
                Animator animator = y.GetComponent<Animator>();
                ac = animator.runtimeAnimatorController; 
                atime = ac.animationClips[0].length;
                c++;
                UnityEngine.Debug.Log("atime =" + atime);
            }
            yield return new WaitForSeconds(atime + 0.1f);
            if (BSM.PerformList[0].ChosenAttack.Animation != null && c > 0)
            {
                foreach (GameObject target in BSM.EnemiesInBattle)
                {
                    if (target.transform.Find("Animation(Clone)").gameObject != null)
                    {
                        if (target.transform.Find("Animation(Clone)").gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1 &&
                        !target.transform.Find("Animation(Clone)").gameObject.GetComponent<Animator>().IsInTransition(0))
                        {
                            Destroy(target.transform.Find("Animation(Clone)").gameObject);
                        }
                    }
                    
                }
            }
        }

        //animate going back
        if (moving)
        {
            Vector3 firstPosition = StartPositon;
            while (movetospot(StartPositon)) { yield return null; }

        }

        finishTimeForAction();
    }

    void finishTimeForAction()
    {
        //remove performer from list in BSM
        BSM.PerformList.RemoveAt(0);
        //reset enemy state
        actionStarted = false;
        //reset enemy state > current_CD to 0 and currentState to processing
        //current_CD = 0f;
        if (BSM.battleStates != BattleStateMachine.performAction.WIN && BSM.battleStates != BattleStateMachine.performAction.LOSE)   //this only happens if battle not done
        {
            BSM.battleStates = BattleStateMachine.performAction.ATTACKBARSLOADING;
            //turn ALL TO PROCESSING
            foreach (GameObject hero in BSM.HeroesInBattle)      //iterate thru all heroes in battle
            {
                hero.GetComponent<HeroStateMachine>().HeroState = HeroStateMachine.HeroStates.ATTACKBARFILLING;    //move them all to waiting
            }
            foreach (GameObject enemy in BSM.EnemiesInBattle)   //iterate thru all enemies in battle
            {

                enemy.GetComponent<EnemyStateMachine>().EnemyState = EnemyStateMachine.EnemyStates.ATTACKBARFILLING; //move all to waiting

            }
        }
        else if (BSM.battleStates == BattleStateMachine.performAction.LOSE)
        {

            foreach (GameObject hero in BSM.HeroesInBattle)      //iterate thru all heroes in battle
            {
                hero.GetComponent<HeroStateMachine>().HeroState = HeroStateMachine.HeroStates.ACTUALIDLE;    //move them all to waiting
            }
            foreach (GameObject enemy in BSM.EnemiesInBattle)   //iterate thru all enemies in battle
            {

                enemy.GetComponent<EnemyStateMachine>().EnemyState = EnemyStateMachine.EnemyStates.IDLE; //move all to waiting

            }

        }
        b = false;
    }



    //effects
    void doDmg(BaseAttack x)
    {
        if (x.Effect[0] == true)
        {
            float critchance;
            bool crit = false;
            float calculateddmg;
            if (x.IsItEntirePartyDmg)
            {
                foreach (GameObject target in BSM.HeroesInBattle)
                {
                    critchance = UnityEngine.Random.Range(0, 100);

                    calculateddmg = enemy.AtkCurrent * BSM.PerformList[0].ChosenAttack.DmgScalings[0];    //dmg to be done
                    calculateddmg += enemy.HpMax * BSM.PerformList[0].ChosenAttack.DmgScalings[1];    //dmg to be done
                    calculateddmg += enemy.SpeedCurrent * BSM.PerformList[0].ChosenAttack.DmgScalings[2];    //dmg to be done
                    calculateddmg += enemy.DefCurrent * BSM.PerformList[0].ChosenAttack.DmgScalings[3];    //dmg to be done                   
                    if (TypeMatch(enemy, target.GetComponent<HeroStateMachine>().hero) == 2)
                    {
                        calculateddmg *= 1.3f;
                    }
                    if (TypeMatch(enemy, target.GetComponent<HeroStateMachine>().hero) == 0)
                    {
                        calculateddmg *= 0.7f;
                    }
                    calculateddmg += target.GetComponent<HeroStateMachine>().hero.HpMax * x.DmgPercent / 100;
                    if (critchance <= enemy.Critrate)
                    {
                        calculateddmg *= enemy.Critdamage;
                        crit = true;
                    }
                    target.GetComponent<HeroStateMachine>().TakeDamage(calculateddmg);
                    if (crit)
                    {
                        UnityEngine.Debug.Log("crit");
                        mainC.TriggerShake();
                    }
                    crit = false;
                }
            }
            else
            {
                critchance = UnityEngine.Random.Range(0, 100);
                calculateddmg = enemy.AtkCurrent * BSM.PerformList[0].ChosenAttack.DmgScalings[0];    //dmg to be done
                calculateddmg += enemy.HpMax * BSM.PerformList[0].ChosenAttack.DmgScalings[1];    //dmg to be done
                calculateddmg += enemy.SpeedCurrent * BSM.PerformList[0].ChosenAttack.DmgScalings[2];    //dmg to be done
                calculateddmg += enemy.DefCurrent * BSM.PerformList[0].ChosenAttack.DmgScalings[3];    //dmg to be done

                if (TypeMatch(enemy, HerotoAttack.GetComponent<HeroStateMachine>().hero) == 2)
                {
                    calculateddmg *= 1.3f;
                }
                if (TypeMatch(enemy, HerotoAttack.GetComponent<HeroStateMachine>().hero) == 0)
                {
                    calculateddmg *= 0.7f;
                }
                calculateddmg += HerotoAttack.GetComponent<HeroStateMachine>().hero.HpMax * x.DmgPercent / 100;
                if (critchance <= enemy.Critrate)
                {
                    calculateddmg *= enemy.Critdamage;
                    crit = true;
                }

                HerotoAttack.GetComponent<HeroStateMachine>().TakeDamage(calculateddmg);
                if (crit)
                {
                    UnityEngine.Debug.Log("crit");
                    mainC.TriggerShake();
                }
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
                foreach (GameObject target in BSM.EnemiesInBattle)
                {
                    if (x.IsItPercentHeal)
                    {
                        calculatedHeal = target.GetComponent<EnemyStateMachine>().enemy.HpMax * x.HealPercent / 100;
                        target.GetComponent<EnemyStateMachine>().TakeHeal(calculatedHeal);
                    }
                    if (x.IsItFlatHeal) //not percent heal
                    {
                        calculatedHeal = 0;
                        calculatedHeal = enemy.AtkCurrent * BSM.PerformList[0].ChosenAttack.HealScalings[0];
                        calculatedHeal += enemy.HpMax * BSM.PerformList[0].ChosenAttack.HealScalings[1];
                        calculatedHeal += enemy.SpeedCurrent * BSM.PerformList[0].ChosenAttack.HealScalings[2];
                        calculatedHeal += enemy.DefCurrent * BSM.PerformList[0].ChosenAttack.HealScalings[3];

                        target.GetComponent<EnemyStateMachine>().TakeHeal(calculatedHeal);
                    }
                }
            }

            else                           //not entire party
            {
                if (x.IsItPercentHeal)
                {

                    calculatedHeal = HerotoAttack.GetComponent<EnemyStateMachine>().enemy.HpMax * x.HealPercent / 100;
                    HerotoAttack.GetComponent<EnemyStateMachine>().TakeHeal(calculatedHeal);
                }
                if (x.IsItFlatHeal) //not percent heal
                {
                    calculatedHeal = 0;
                    calculatedHeal = enemy.AtkCurrent * BSM.PerformList[0].ChosenAttack.HealScalings[0];
                    calculatedHeal += enemy.HpMax * BSM.PerformList[0].ChosenAttack.HealScalings[1];
                    calculatedHeal += enemy.SpeedCurrent * BSM.PerformList[0].ChosenAttack.HealScalings[2];
                    calculatedHeal += enemy.DefCurrent * BSM.PerformList[0].ChosenAttack.HealScalings[3];
                    HerotoAttack.GetComponent<EnemyStateMachine>().TakeHeal(calculatedHeal);

                }
            }
        }
    }
    void TakeHeal(float HealingAmount)
    {
        if (enemy.HpCurrent + HealingAmount > enemy.HpMax)
        {
            enemy.HpCurrent = enemy.HpMax;
        }
        else
        {
            enemy.HpCurrent += HealingAmount;
        }
    }
    void DoBuff(BaseAttack x)
    {
        if (x.Effect[2] == true)
        {
            if (x.IsItEntirePartyBuff)      //entire party
            {
                foreach (GameObject target in BSM.EnemiesInBattle)
                {
                    if (x.StatToBuff[0])//atk
                    {
                        if (x.BuffTurns > target.GetComponent<EnemyStateMachine>().enemy.atkBuffTurns)
                        {
                            target.GetComponent<EnemyStateMachine>().enemy.atkBuffTurns = x.BuffTurns;
                            if (x.BuffTurns + 1 > target.GetComponent<EnemyStateMachine>().enemy.atkBuffTurns && target.GetComponent<EnemyStateMachine>().enemy == this.enemy)
                            {
                                this.enemy.atkBuffTurns++;
                            }
                        }
                    }
                    if (x.StatToBuff[1])//spd
                    {
                        if (x.BuffTurns > target.GetComponent<EnemyStateMachine>().enemy.spdBuffTurns)
                        {
                            target.GetComponent<EnemyStateMachine>().enemy.spdBuffTurns = x.BuffTurns;
                        }
                        if (x.BuffTurns + 1 > target.GetComponent<EnemyStateMachine>().enemy.spdBuffTurns && target.GetComponent<EnemyStateMachine>().enemy == this.enemy)
                        {
                            this.enemy.spdBuffTurns++;
                        }
                    }
                    if (x.StatToBuff[2])//def
                    {
                        if (x.BuffTurns > target.GetComponent<EnemyStateMachine>().enemy.defBuffTurns)
                        {
                            target.GetComponent<EnemyStateMachine>().enemy.defBuffTurns = x.BuffTurns;
                        }
                        if (x.BuffTurns + 1 > target.GetComponent<EnemyStateMachine>().enemy.defBuffTurns && target.GetComponent<EnemyStateMachine>().enemy == this.enemy)
                        {
                            this.enemy.defBuffTurns++;
                        }
                    }
                    if (x.StatToBuff[3])//Recovery
                    {
                        if (x.BuffTurns > target.GetComponent<EnemyStateMachine>().enemy.recBuffTurns)
                        {
                            target.GetComponent<EnemyStateMachine>().enemy.recBuffTurns = x.BuffTurns;
                        }
                        if (x.BuffTurns + 1 > target.GetComponent<EnemyStateMachine>().enemy.recBuffTurns && target.GetComponent<EnemyStateMachine>().enemy == this.enemy)
                        {
                            this.enemy.recBuffTurns++;
                        }
                    }
                    target.GetComponent<EnemyStateMachine>().statChangeCheck();
                }
            }

            else                           //not entire party
            {
                if (x.StatToBuff[0])//atk
                {
                    if (x.BuffTurns > HerotoAttack.GetComponent<EnemyStateMachine>().enemy.atkBuffTurns)
                    {
                        HerotoAttack.GetComponent<EnemyStateMachine>().enemy.atkBuffTurns = x.BuffTurns;
                    }
                    if (x.BuffTurns + 1 > HerotoAttack.GetComponent<EnemyStateMachine>().enemy.atkBuffTurns && HerotoAttack.GetComponent<EnemyStateMachine>().enemy == this.enemy)
                    {
                        this.enemy.atkBuffTurns++;
                    }
                }
                if (x.StatToBuff[1])//spd
                {
                    if (x.BuffTurns > HerotoAttack.GetComponent<EnemyStateMachine>().enemy.spdBuffTurns)
                    {
                        HerotoAttack.GetComponent<EnemyStateMachine>().enemy.spdBuffTurns = x.BuffTurns;
                    }
                    if (x.BuffTurns + 1 > HerotoAttack.GetComponent<EnemyStateMachine>().enemy.spdBuffTurns && HerotoAttack.GetComponent<EnemyStateMachine>().enemy == this.enemy)
                    {
                        this.enemy.spdBuffTurns++;
                    }
                }
                if (x.StatToBuff[2])//def
                {
                    if (x.BuffTurns > HerotoAttack.GetComponent<EnemyStateMachine>().enemy.defBuffTurns)
                    {
                        HerotoAttack.GetComponent<EnemyStateMachine>().enemy.defBuffTurns = x.BuffTurns;
                    }
                    if (x.BuffTurns + 1 > HerotoAttack.GetComponent<EnemyStateMachine>().enemy.defBuffTurns && HerotoAttack.GetComponent<EnemyStateMachine>().enemy == this.enemy)
                    {
                        this.enemy.defBuffTurns++;
                    }
                }
                if (x.StatToBuff[3])//Rec
                {
                    if (x.BuffTurns > HerotoAttack.GetComponent<EnemyStateMachine>().enemy.recBuffTurns)
                    {
                        HerotoAttack.GetComponent<EnemyStateMachine>().enemy.recBuffTurns = x.BuffTurns;
                    }
                    if (x.BuffTurns + 1 > HerotoAttack.GetComponent<EnemyStateMachine>().enemy.recBuffTurns && HerotoAttack.GetComponent<EnemyStateMachine>().enemy == this.enemy)
                    {
                        this.enemy.recBuffTurns++;
                    }
                }
                HerotoAttack.GetComponent<EnemyStateMachine>().statChangeCheck();
            }
        }
    }
    void TurnCounters()
    {
        enemy.turns++;
        if (enemy.atkBuffTurns > 0)
        {
            enemy.atkBuffTurns--;
        }
        if (enemy.defBuffTurns > 0)
        {
            enemy.defBuffTurns--;
        }
        if (enemy.spdBuffTurns > 0)
        {
            enemy.spdBuffTurns--;
        }
        if (enemy.recBuffTurns > 0)
        {
            enemy.recBuffTurns--;
        }
        if (enemy.atkDeBuffTurns > 0)
        {
            enemy.atkDeBuffTurns--;
        }
        if (enemy.defDeBuffTurns > 0)
        {
            enemy.defDeBuffTurns--;
        }
        if (enemy.spdDeBuffTurns > 0)
        {
            enemy.spdDeBuffTurns--;
        }
        if (enemy.dotDeBuffTurns > 0)
        {
            enemy.dotDeBuffTurns--;
        }
        if (isStun > 0)
        {
            isStun--;
        }
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
        if (enemy.atkBuffTurns > 0 && enemy.atkDeBuffTurns == 0)
        {
            enemy.AtkCurrent = enemy.AtkMax * 1.5f;
        }
        else if (enemy.atkDeBuffTurns > 0 && enemy.atkBuffTurns == 0)
        {
            enemy.AtkCurrent = enemy.AtkMax * 0.5f;
        }
        else
        {
            enemy.AtkCurrent = enemy.AtkMax;
        }
    }
    void defCheck()
    {
        if (enemy.defBuffTurns > 0 && enemy.defDeBuffTurns == 0)
        {
            enemy.DefCurrent = enemy.DefMax * 1.5f;
        }
        else if (enemy.defDeBuffTurns > 0 && enemy.defBuffTurns == 0)
        {
            enemy.DefCurrent = enemy.DefMax * 0.5f;
        }
        else
        {
            enemy.DefCurrent = enemy.DefMax;
        }
    }
    void spdCheck()
    {
        if (enemy.spdBuffTurns > 0 && enemy.spdDeBuffTurns == 0)
        {
            enemy.SpeedCurrent = enemy.SpeedMax * 1.5f;
        }
        else if (enemy.spdDeBuffTurns > 0 && enemy.spdBuffTurns == 0)
        {
            enemy.SpeedCurrent = enemy.SpeedMax * 0.5f;
        }
        else
        {
            enemy.SpeedCurrent = enemy.SpeedMax;
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
                foreach (GameObject target in BSM.HeroesInBattle)
                {
                    ResAnimation = false;
                    LandingChance = target.GetComponent<HeroStateMachine>().hero.Resistance - this.enemy.Accuracy;
                    if (LandingChance < 10)
                        LandingChance = 10;
                    if (LandingChance > 90)
                        LandingChance = 90;
                    if (x.StatToDeBuff[0])//atk
                    {
                        if (x.DeBuffTurns > target.GetComponent<HeroStateMachine>().hero.atkDeBuffTurns)
                        {
                            OutofHundred = UnityEngine.Random.Range(0, 100);
                            if (OutofHundred > LandingChance)
                                target.GetComponent<HeroStateMachine>().hero.atkDeBuffTurns = x.DeBuffTurns;
                            else
                                ResAnimation = true;
                        }
                    }
                    if (x.StatToDeBuff[1])//spd
                    {
                        if (x.DeBuffTurns > target.GetComponent<HeroStateMachine>().hero.spdDeBuffTurns)
                        {
                            OutofHundred = UnityEngine.Random.Range(0, 100);
                            if (OutofHundred > LandingChance)
                                target.GetComponent<HeroStateMachine>().hero.spdDeBuffTurns = x.DeBuffTurns;
                            else
                                ResAnimation = true;
                        }
                    }
                    if (x.StatToDeBuff[2])//atk
                    {
                        if (x.DeBuffTurns > target.GetComponent<HeroStateMachine>().hero.defDeBuffTurns)
                        {
                            OutofHundred = UnityEngine.Random.Range(0, 100);
                            if (OutofHundred > LandingChance)
                                target.GetComponent<HeroStateMachine>().hero.defDeBuffTurns = x.DeBuffTurns;
                            else
                                ResAnimation = true;
                        }
                    }
                    if (x.StatToDeBuff[3])//dot
                    {
                        if (x.DeBuffTurns > target.GetComponent<HeroStateMachine>().hero.dotDeBuffTurns)
                        {
                            OutofHundred = UnityEngine.Random.Range(0, 100);
                            if (OutofHundred > LandingChance)
                                target.GetComponent<HeroStateMachine>().hero.dotDeBuffTurns = x.DeBuffTurns;
                            else
                                ResAnimation = true;
                        }
                    }
                    target.GetComponent<HeroStateMachine>().statChangeCheck();
                    if (ResAnimation)
                    {//play res animation
                    }
                }
            }

            else                           //not entire party
            {
                ResAnimation = false;
                LandingChance = HerotoAttack.GetComponent<HeroStateMachine>().hero.Resistance - this.enemy.Accuracy;
                if (LandingChance < 10)
                    LandingChance = 10;
                if (LandingChance > 90)
                    LandingChance = 90;
                UnityEngine.Debug.Log(LandingChance);

                if (x.StatToDeBuff[0])//atk
                {
                    if (x.DeBuffTurns > HerotoAttack.GetComponent<HeroStateMachine>().hero.atkDeBuffTurns)
                    {
                        OutofHundred = UnityEngine.Random.Range(0, 100);
                        if (OutofHundred > LandingChance)
                            HerotoAttack.GetComponent<HeroStateMachine>().hero.atkDeBuffTurns = x.DeBuffTurns;
                        else
                            ResAnimation = true;
                    }
                }
                if (x.StatToDeBuff[1])//spd
                {
                    if (x.DeBuffTurns > HerotoAttack.GetComponent<HeroStateMachine>().hero.spdDeBuffTurns)
                    {
                        OutofHundred = UnityEngine.Random.Range(0, 100);
                        if (OutofHundred > LandingChance)
                            HerotoAttack.GetComponent<HeroStateMachine>().hero.spdDeBuffTurns = x.DeBuffTurns;
                        else
                            ResAnimation = true;
                    }
                }
                if (x.StatToDeBuff[2])//atk
                {
                    if (x.DeBuffTurns > HerotoAttack.GetComponent<HeroStateMachine>().hero.defDeBuffTurns)
                    {
                        OutofHundred = UnityEngine.Random.Range(0, 100);
                        if (OutofHundred > LandingChance)
                            HerotoAttack.GetComponent<HeroStateMachine>().hero.defDeBuffTurns = x.DeBuffTurns;
                        else
                            ResAnimation = true;
                    }
                }
                if (x.StatToDeBuff[3])//dot
                {
                    if (x.DeBuffTurns > HerotoAttack.GetComponent<HeroStateMachine>().hero.dotDeBuffTurns)
                    {
                        OutofHundred = UnityEngine.Random.Range(0, 100);
                        if (OutofHundred > LandingChance)
                            HerotoAttack.GetComponent<HeroStateMachine>().hero.dotDeBuffTurns = x.DeBuffTurns;
                        else
                            ResAnimation = true;
                    }
                }
                HerotoAttack.GetComponent<HeroStateMachine>().statChangeCheck();
                if (ResAnimation)
                {//play res animation

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
                foreach (GameObject target in BSM.HeroesInBattle)
                {
                    if (target.GetComponent<HeroStateMachine>().isStun < x.StunTurns + 1)
                    {
                        target.GetComponent<HeroStateMachine>().isStun = x.StunTurns + 1;
                    }
                }
            }
            else
            {
                if (HerotoAttack.GetComponent<HeroStateMachine>().isStun < x.StunTurns + 1)
                {
                    HerotoAttack.GetComponent<HeroStateMachine>().isStun = x.StunTurns + 1;
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
                foreach (GameObject target in BSM.EnemiesInBattleTotal)
                {
                    if (target.GetComponent<EnemyStateMachine>().EnemyAlive == false)
                    {
                        target.tag = "Enemy";
                        BSM.EnemiesInBattle.Add(target);
                        target.GetComponent<EnemyStateMachine>().EnemyAlive = true;
                        BSM.DeadEnemies.Remove(target);

                        if (x.IsItPercentReviveHp)
                        {

                            calculatedHeal = target.GetComponent<EnemyStateMachine>().enemy.HpMax * x.PercentHealOnRevive / 100;
                            target.GetComponent<EnemyStateMachine>().TakeHeal(calculatedHeal);
                        }
                        else
                        {
                            calculatedHeal = enemy.AtkCurrent * BSM.PerformList[0].ChosenAttack.ReviveHealScalings[0];
                            calculatedHeal += enemy.HpMax * BSM.PerformList[0].ChosenAttack.ReviveHealScalings[1];
                            calculatedHeal += enemy.SpeedCurrent * BSM.PerformList[0].ChosenAttack.ReviveHealScalings[2];
                            calculatedHeal += enemy.DefCurrent * BSM.PerformList[0].ChosenAttack.ReviveHealScalings[3];

                            target.GetComponent<EnemyStateMachine>().TakeHeal(calculatedHeal);
                        }
                    }
                }
                foreach (GameObject target in BSM.EnemiesInBattleTotal)
                {
                    if (target.GetComponent<EnemyStateMachine>().EnemyState == EnemyStateMachine.EnemyStates.DEAD)
                    {
                        target.GetComponent<EnemyStateMachine>().EnemyState = EnemyStateMachine.EnemyStates.IDLE;
                    }
                }
            }
            else
            {

                HerotoAttack.tag = "Hero";
                BSM.EnemiesInBattle.Add(HerotoAttack);
                HerotoAttack.GetComponent<EnemyStateMachine>().EnemyAlive = true;
                BSM.DeadEnemies.Remove(HerotoAttack);

                if (x.IsItPercentReviveHp)
                {

                    calculatedHeal = HerotoAttack.GetComponent<EnemyStateMachine>().enemy.HpMax * x.PercentHealOnRevive / 100;

                    HerotoAttack.GetComponent<EnemyStateMachine>().TakeHeal(calculatedHeal);
                }
                else
                {
                    calculatedHeal = enemy.AtkCurrent * BSM.PerformList[0].ChosenAttack.ReviveHealScalings[0];
                    calculatedHeal += enemy.HpMax * BSM.PerformList[0].ChosenAttack.ReviveHealScalings[1];
                    calculatedHeal += enemy.SpeedCurrent * BSM.PerformList[0].ChosenAttack.ReviveHealScalings[2];
                    calculatedHeal += enemy.DefCurrent * BSM.PerformList[0].ChosenAttack.ReviveHealScalings[3];

                    HerotoAttack.GetComponent<EnemyStateMachine>().TakeHeal(calculatedHeal);
                }
                if (HerotoAttack.GetComponent<EnemyStateMachine>().EnemyState == EnemyStateMachine.EnemyStates.DEAD)
                {
                    HerotoAttack.GetComponent<EnemyStateMachine>().EnemyState = EnemyStateMachine.EnemyStates.IDLE;
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
                foreach (GameObject target in BSM.EnemiesInBattle)
                {
                    if (target.GetComponent<EnemyStateMachine>().current_CD + target.GetComponent<EnemyStateMachine>().max_CD * x.AtkBarBoostPercent / 100 < target.GetComponent<EnemyStateMachine>().max_CD)
                    {
                        target.GetComponent<EnemyStateMachine>().current_CD += target.GetComponent<EnemyStateMachine>().max_CD * x.AtkBarBoostPercent / 100;
                    }
                    else
                    {
                        target.GetComponent<EnemyStateMachine>().current_CD = target.GetComponent<EnemyStateMachine>().max_CD;
                    }
                    target.GetComponent<EnemyStateMachine>().CheckProgressBar();
                }
            }
            else
            {
                if (HerotoAttack == this.gameObject)
                {
                    SelfFullAtkBar = true;
                }
                else if (HerotoAttack.GetComponent<EnemyStateMachine>().current_CD + HerotoAttack.GetComponent<EnemyStateMachine>().max_CD * x.AtkBarBoostPercent / 100 < HerotoAttack.GetComponent<EnemyStateMachine>().max_CD)
                {
                    HerotoAttack.GetComponent<EnemyStateMachine>().current_CD += HerotoAttack.GetComponent<EnemyStateMachine>().max_CD * x.AtkBarBoostPercent / 100;
                }
                else
                {
                    HerotoAttack.GetComponent<EnemyStateMachine>().current_CD = HerotoAttack.GetComponent<EnemyStateMachine>().max_CD;
                }
                HerotoAttack.GetComponent<EnemyStateMachine>().CheckProgressBar();
            }
        }
    }
    void doAtkBarDecrease(BaseAttack x)
    {
        if (x.Effect[6])
        {
            if (x.IsItEntirePartyAtkBarReduction)
            {
                foreach (GameObject target in BSM.HeroesInBattle)
                {
                    if (target.GetComponent<HeroStateMachine>().current_CD - target.GetComponent<HeroStateMachine>().max_CD * x.AtkBarReductionPercent / 100 > 0)
                    {
                        target.GetComponent<HeroStateMachine>().current_CD -= target.GetComponent<HeroStateMachine>().max_CD * x.AtkBarReductionPercent / 100;
                    }
                    else
                    {
                        target.GetComponent<HeroStateMachine>().current_CD = 0;
                    }

                    target.GetComponent<HeroStateMachine>().CheckProgressBar();
                }
            }
            else
            {
                if (HerotoAttack.GetComponent<HeroStateMachine>().current_CD - HerotoAttack.GetComponent<HeroStateMachine>().max_CD * x.AtkBarReductionPercent / 100 > 0)
                {
                    HerotoAttack.GetComponent<HeroStateMachine>().current_CD -= HerotoAttack.GetComponent<HeroStateMachine>().max_CD * x.AtkBarReductionPercent / 100;
                }
                else
                {
                    HerotoAttack.GetComponent<HeroStateMachine>().current_CD = 0;
                }
                HerotoAttack.GetComponent<HeroStateMachine>().CheckProgressBar();
            }
        }
    }
    void BuffDebuffSprites()
    {
        destroyBoxes();
        int x;
        string boxnum = "box ";


        if (enemy.atkBuffTurns > 0)
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
        if (enemy.spdBuffTurns > 0)
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
        if (enemy.defBuffTurns > 0)
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
        if (enemy.recBuffTurns > 0)
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
        if (enemy.atkDeBuffTurns > 0)
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
        if (enemy.spdDeBuffTurns > 0)
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
        if (enemy.defDeBuffTurns > 0)
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
        if (enemy.dotDeBuffTurns > 0)
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
                    foreach (GameObject target in BSM.EnemiesInBattle)
                    {
                        target.GetComponent<EnemyStateMachine>().enemy.atkDeBuffTurns = 0;
                        target.GetComponent<EnemyStateMachine>().enemy.defDeBuffTurns = 0;
                        target.GetComponent<EnemyStateMachine>().enemy.dotDeBuffTurns = 0;
                        target.GetComponent<EnemyStateMachine>().enemy.spdDeBuffTurns = 0;
                        target.GetComponent<EnemyStateMachine>().statChangeCheck();
                    }
                }
                else
                {

                    int c;
                    List<int> DeBuffTrue = new List<int>();
                    foreach (GameObject target in BSM.EnemiesInBattle)
                    {
                        for (int i = 0; i < x.DeBuffstoCleanse; i++)
                        {
                            DeBuffTrue.Clear();
                            if (target.GetComponent<EnemyStateMachine>().enemy.atkDeBuffTurns > 0)
                                DeBuffTrue.Add(0);
                            if (target.GetComponent<EnemyStateMachine>().enemy.spdDeBuffTurns > 0)
                                DeBuffTrue.Add(1);
                            if (target.GetComponent<EnemyStateMachine>().enemy.defDeBuffTurns > 0)
                                DeBuffTrue.Add(2);
                            if (target.GetComponent<EnemyStateMachine>().enemy.dotDeBuffTurns > 0)
                                DeBuffTrue.Add(3);

                            int a = DeBuffTrue.Count;
                            if (a > 0)
                            {
                                int num = UnityEngine.Random.Range(0, a);
                                c = DeBuffTrue[num];

                                if (c == 0)
                                    target.GetComponent<EnemyStateMachine>().enemy.atkDeBuffTurns = 0;
                                if (c == 1)
                                    target.GetComponent<EnemyStateMachine>().enemy.spdDeBuffTurns = 0;
                                if (c == 2)
                                    target.GetComponent<EnemyStateMachine>().enemy.defDeBuffTurns = 0;
                                if (c == 3)
                                    target.GetComponent<EnemyStateMachine>().enemy.dotDeBuffTurns = 0;
                            }
                        }
                        target.GetComponent<EnemyStateMachine>().statChangeCheck();
                    }
                }
            }
            else
            {
                if (x.CleanseAll)
                {
                    HerotoAttack.GetComponent<EnemyStateMachine>().enemy.atkDeBuffTurns = 0;
                    HerotoAttack.GetComponent<EnemyStateMachine>().enemy.defDeBuffTurns = 0;
                    HerotoAttack.GetComponent<EnemyStateMachine>().enemy.dotDeBuffTurns = 0;
                    HerotoAttack.GetComponent<EnemyStateMachine>().enemy.spdDeBuffTurns = 0;

                }
                else
                {
                    int c;
                    List<int> DeBuffTrue = new List<int>();
                    for (int i = 0; i < x.DeBuffstoCleanse; i++)
                    {
                        DeBuffTrue.Clear();
                        if (HerotoAttack.GetComponent<EnemyStateMachine>().enemy.atkDeBuffTurns > 0)
                            DeBuffTrue.Add(0);
                        if (HerotoAttack.GetComponent<EnemyStateMachine>().enemy.spdDeBuffTurns > 0)
                            DeBuffTrue.Add(1);
                        if (HerotoAttack.GetComponent<EnemyStateMachine>().enemy.defDeBuffTurns > 0)
                            DeBuffTrue.Add(2);
                        if (HerotoAttack.GetComponent<EnemyStateMachine>().enemy.dotDeBuffTurns > 0)
                            DeBuffTrue.Add(3);

                        int a = DeBuffTrue.Count;
                        if (a > 0)
                        {
                            int num = UnityEngine.Random.Range(0, a);
                            c = DeBuffTrue[num];
                            if (c == 0)
                                HerotoAttack.GetComponent<EnemyStateMachine>().enemy.atkDeBuffTurns = 0;
                            if (c == 1)
                                HerotoAttack.GetComponent<EnemyStateMachine>().enemy.spdDeBuffTurns = 0;
                            if (c == 2)
                                HerotoAttack.GetComponent<EnemyStateMachine>().enemy.defDeBuffTurns = 0;
                            if (c == 3)
                                HerotoAttack.GetComponent<EnemyStateMachine>().enemy.dotDeBuffTurns = 0;
                        }
                    }
                    HerotoAttack.GetComponent<EnemyStateMachine>().statChangeCheck();
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
                    foreach (GameObject target in BSM.HeroesInBattle)
                    {
                        target.GetComponent<HeroStateMachine>().hero.atkBuffTurns = 0;
                        target.GetComponent<HeroStateMachine>().hero.defBuffTurns = 0;
                        target.GetComponent<HeroStateMachine>().hero.recBuffTurns = 0;
                        target.GetComponent<HeroStateMachine>().hero.spdBuffTurns = 0;
                        target.GetComponent<HeroStateMachine>().statChangeCheck();
                    }
                }
                else
                {

                    int c;
                    List<int> BuffTrue = new List<int>();
                    foreach (GameObject target in BSM.HeroesInBattle)
                    {
                        for (int i = 0; i < x.BuffsToStrip; i++)
                        {
                            BuffTrue.Clear();
                            if (target.GetComponent<HeroStateMachine>().hero.atkBuffTurns > 0)
                                BuffTrue.Add(0);
                            if (target.GetComponent<HeroStateMachine>().hero.spdBuffTurns > 0)
                                BuffTrue.Add(1);
                            if (target.GetComponent<HeroStateMachine>().hero.defBuffTurns > 0)
                                BuffTrue.Add(2);
                            if (target.GetComponent<HeroStateMachine>().hero.recBuffTurns > 0)
                                BuffTrue.Add(3);

                            int a = BuffTrue.Count;
                            if (a > 0)
                            {
                                int num = UnityEngine.Random.Range(0, a);
                                c = BuffTrue[num];

                                if (c == 0)
                                    target.GetComponent<HeroStateMachine>().hero.atkBuffTurns = 0;
                                if (c == 1)
                                    target.GetComponent<HeroStateMachine>().hero.spdBuffTurns = 0;
                                if (c == 2)
                                    target.GetComponent<HeroStateMachine>().hero.defBuffTurns = 0;
                                if (c == 3)
                                    target.GetComponent<HeroStateMachine>().hero.recBuffTurns = 0;
                            }
                        }
                        target.GetComponent<HeroStateMachine>().statChangeCheck();
                    }
                }
            }
            else
            {
                if (x.StripAll)
                {
                    HerotoAttack.GetComponent<HeroStateMachine>().hero.atkDeBuffTurns = 0;
                    HerotoAttack.GetComponent<HeroStateMachine>().hero.defDeBuffTurns = 0;
                    HerotoAttack.GetComponent<HeroStateMachine>().hero.dotDeBuffTurns = 0;
                    HerotoAttack.GetComponent<HeroStateMachine>().hero.spdDeBuffTurns = 0;

                }
                else
                {
                    int c;
                    List<int> DeBuffTrue = new List<int>();
                    for (int i = 0; i < x.DeBuffstoCleanse; i++)
                    {
                        DeBuffTrue.Clear();
                        if (HerotoAttack.GetComponent<HeroStateMachine>().hero.atkBuffTurns > 0)
                            DeBuffTrue.Add(0);
                        if (HerotoAttack.GetComponent<HeroStateMachine>().hero.spdBuffTurns > 0)
                            DeBuffTrue.Add(1);
                        if (HerotoAttack.GetComponent<HeroStateMachine>().hero.defBuffTurns > 0)
                            DeBuffTrue.Add(2);
                        if (HerotoAttack.GetComponent<HeroStateMachine>().hero.recBuffTurns > 0)
                            DeBuffTrue.Add(3);

                        int a = DeBuffTrue.Count;
                        if (a > 0)
                        {
                            int num = UnityEngine.Random.Range(0, a);
                            c = DeBuffTrue[num];

                            if (c == 0)
                                HerotoAttack.GetComponent<HeroStateMachine>().hero.atkBuffTurns = 0;
                            if (c == 1)
                                HerotoAttack.GetComponent<HeroStateMachine>().hero.spdBuffTurns = 0;
                            if (c == 2)
                                HerotoAttack.GetComponent<HeroStateMachine>().hero.defBuffTurns = 0;
                            if (c == 3)
                                HerotoAttack.GetComponent<HeroStateMachine>().hero.recBuffTurns = 0;
                        }
                    }
                    HerotoAttack.GetComponent<HeroStateMachine>().statChangeCheck();
                }
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
    public int FindOpenBoxLocation()
    {
        for (int i = 0; i < 9; i++)
        {
            if (boxArray[i] == false)
                return i + 1;
        }
        return 0;

    }


   
    public int  checkifDebuffed()   //returns int #of debuffs
    {
        int x = 0;
        if (enemy.defDeBuffTurns != 0)
            x++;
        if (enemy.dotDeBuffTurns != 0)
            x++;
        if (isStun != 0)
            x++;
        if (enemy.spdDeBuffTurns != 0)
            x++;
        if (enemy.atkDeBuffTurns != 0)
            x++;

        return x;
    }
    public int checkifBuffed()   //returns int #of debuffs
    {
        int x = 0;
        if (enemy.defBuffTurns != 0)
            x++;
        if (enemy.recBuffTurns != 0)
            x++;
        if (enemy.spdBuffTurns != 0)
            x++;
        if (enemy.atkBuffTurns != 0)
            x++;

        return x;
    }




    public bool checkHpStatus(float percent)  //true means lower than percent
    {
        if (enemy.HpCurrent <= enemy.HpMax * percent / 100)
        {
            return true;
        }
        if (enemy.HpCurrent > enemy.HpMax * percent / 100)
        {
            return false;
        }
        return false;
    }
    public int TypeMatch(BaseMonster a, BaseMonster b)//Neutral : x = 1, Strong : x = 2, Weak : x = 0
    {
        int x = 1; 
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
    void ChooseEnemyMove()
    {
        TurnInformation EnemyAttack = new TurnInformation();
        bool MoveChosen = false;
        EnemyAttack.AttackerGameObject = this.gameObject;
        EnemyAttack.Attacker = name;
        bool MandatoryRevive = false;
        bool MandatoryHeal = false;
        bool MandatoryCleanse = false;
        bool RemainingMoves = false;

        //100% REVIVE
        if (enemy.attacks[1].Effect[4] || enemy.attacks[2].Effect[4] || enemy.attacks[3].Effect[4])
        {
            if (enemy.attacks[3].Effect[4] && !MoveChosen)
            {
                if (IsReviveUsable(enemy.attacks[3]) && isUsable(enemy.attacks[3]))
                {
                    MoveChosen = true;
                    MandatoryRevive = true;
                    EnemyAttack.ChosenAttack = enemy.attacks[3];
                    UnityEngine.Debug.Log("chosen revive, skill 4");
                }
            }
            if (enemy.attacks[2].Effect[4] && !MoveChosen)
            {
                if (IsReviveUsable(enemy.attacks[2]) && isUsable(enemy.attacks[2]))
                {
                    MoveChosen = true;
                    MandatoryRevive = true;
                    EnemyAttack.ChosenAttack = enemy.attacks[2];
                    UnityEngine.Debug.Log("chosen revive, skill 3");
                }
            }
            if (enemy.attacks[1].Effect[4] && !MoveChosen)
            {
                if (IsReviveUsable(enemy.attacks[1]) && isUsable(enemy.attacks[1]))
                {
                    MoveChosen = true;
                    MandatoryRevive = true;
                    EnemyAttack.ChosenAttack = enemy.attacks[1];
                    UnityEngine.Debug.Log("chosen revive, skill 2");
                }
            }
        }
        if (MoveChosen && MandatoryRevive)
        {
            List<GameObject> DeadEnemies = new List<GameObject>();
            DeadEnemies.Clear();
            foreach (GameObject target in BSM.EnemiesInBattleTotal)
            {
                if (!target.GetComponent<EnemyStateMachine>().EnemyAlive)
                    DeadEnemies.Add(target);
            }
            int enemytoRevive = UnityEngine.Random.Range(0, DeadEnemies.Count);
            UnityEngine.Debug.Log("enemytoRevive int is " + enemytoRevive + "   and the target is " + DeadEnemies[enemytoRevive]);
            EnemyAttack.AttackersTarget = DeadEnemies[enemytoRevive];
        }


        //HEAL  50 %
        if (enemy.attacks[1].Effect[1] || enemy.attacks[2].Effect[1] || enemy.attacks[3].Effect[1] && !MoveChosen)
        {
            bool checkhp50 = false;
            foreach (GameObject target in BSM.EnemiesInBattle)
            {
                if (target.GetComponent<EnemyStateMachine>().checkHpStatus(50))
                    checkhp50 = true;
            }

            if (enemy.attacks[3].Effect[1] && !MoveChosen)
            {
                if (isUsable(enemy.attacks[3]) && checkhp50)
                {
                    MoveChosen = true;
                    MandatoryHeal = true;
                    EnemyAttack.ChosenAttack = enemy.attacks[3];
                    UnityEngine.Debug.Log("chosen heal 50%, skill 4");
                }
            }
            if (enemy.attacks[2].Effect[1] && !MoveChosen)
            {
                if (isUsable(enemy.attacks[2]) && checkhp50)
                {
                    MoveChosen = true;
                    MandatoryHeal = true;
                    EnemyAttack.ChosenAttack = enemy.attacks[2];
                    UnityEngine.Debug.Log("chosen heal 50%, skill 3");
                }
            }
            if (enemy.attacks[1].Effect[1] && !MoveChosen)
            {
                if (isUsable(enemy.attacks[1]) && checkhp50)
                {
                    MoveChosen = true;
                    MandatoryHeal = true;
                    EnemyAttack.ChosenAttack = enemy.attacks[1];
                    UnityEngine.Debug.Log("chosen heal 50%, skill 2");
                }
            }
        }
        if (MoveChosen && MandatoryHeal)
        {
            float lowestHpPercent = 100f;
            foreach (GameObject target in BSM.EnemiesInBattle)
            {
                if (target.GetComponent<EnemyStateMachine>().enemy.HpCurrent / target.GetComponent<EnemyStateMachine>().enemy.HpBase * 100f < lowestHpPercent)
                    lowestHpPercent = target.GetComponent<EnemyStateMachine>().enemy.HpCurrent / target.GetComponent<EnemyStateMachine>().enemy.HpBase * 100f;
            }
            List<GameObject> EnemieswithLowestHp = new List<GameObject>();
            EnemieswithLowestHp.Clear();
            foreach (GameObject target in BSM.EnemiesInBattle)
            {
                if (Math.Abs(lowestHpPercent - target.GetComponent<EnemyStateMachine>().enemy.HpCurrent / target.GetComponent<EnemyStateMachine>().enemy.HpBase * 100f) < 1f)
                    EnemieswithLowestHp.Add(target);

            }
            int enemytoHeal = UnityEngine.Random.Range(0, EnemieswithLowestHp.Count);
            UnityEngine.Debug.Log("enemytoHeal int is " + enemytoHeal + "   and the target is " + EnemieswithLowestHp[enemytoHeal]);
            EnemyAttack.AttackersTarget = EnemieswithLowestHp[enemytoHeal];
        }


        //80% CLEANSE
        int a = UnityEngine.Random.Range(0, 100);
        float deBuffCount = 0;
        foreach (GameObject target in BSM.EnemiesInBattle)
        {
            if (target.GetComponent<EnemyStateMachine>().checkifDebuffed() > deBuffCount)
            {
                deBuffCount = target.GetComponent<EnemyStateMachine>().checkifDebuffed();
            }
        }
        if (deBuffCount > 2 || (deBuffCount > 1 && a > 20) || (deBuffCount > 0 && a > 35))
        {
            if (enemy.attacks[1].Effect[8] || enemy.attacks[2].Effect[8] || enemy.attacks[3].Effect[8] && !MoveChosen)
            {
                if (enemy.attacks[3].Effect[8] && !MoveChosen)
                {
                    if (isUsable(enemy.attacks[1]))
                    {
                        MoveChosen = true;
                        MandatoryCleanse = true;
                        EnemyAttack.ChosenAttack = enemy.attacks[3];
                        UnityEngine.Debug.Log("chosen Cleanse 50%, skill 4");
                    }
                }
                if (enemy.attacks[2].Effect[8] && !MoveChosen)
                {
                    if (isUsable(enemy.attacks[2]))
                    {
                        MoveChosen = true;
                        MandatoryCleanse = true;
                        EnemyAttack.ChosenAttack = enemy.attacks[2];
                        UnityEngine.Debug.Log("chosen Cleanse 50%, skill 3");
                    }
                }
                if (enemy.attacks[1].Effect[8] && !MoveChosen)
                {
                    if (isUsable(enemy.attacks[1]))
                    {
                        MoveChosen = true;
                        MandatoryCleanse = true;
                        EnemyAttack.ChosenAttack = enemy.attacks[1];
                        UnityEngine.Debug.Log("chosen Cleanse 50%, skill 2");
                    }
                }
            }
        }
        if (MoveChosen && MandatoryCleanse)
        {
            List<GameObject> EnemiestoCleanse = new List<GameObject>();
            EnemiestoCleanse.Clear();
            foreach (GameObject target in BSM.EnemiesInBattle)
            {
                if (target.GetComponent<EnemyStateMachine>().checkifDebuffed() == deBuffCount)
                    EnemiestoCleanse.Add(target);
            }
            int EnemytoCleanse = UnityEngine.Random.Range(0, EnemiestoCleanse.Count);
            UnityEngine.Debug.Log("enemytoCleanse int is " + EnemytoCleanse + "   and the target is " + EnemiestoCleanse[EnemytoCleanse]);
            EnemyAttack.AttackersTarget = EnemiestoCleanse[EnemytoCleanse];
        }
        a = UnityEngine.Random.Range(0, 100);
        if (a > 20 && !MoveChosen)
        {
            if (isUsable(enemy.attacks[3]) && ShouldUseMove(enemy.attacks[3]))
            {
                RemainingMoves = true; MoveChosen = true;
                EnemyAttack.ChosenAttack = enemy.attacks[3];
            }
        }
        a = UnityEngine.Random.Range(0, 100);
        if (a > 20 && !MoveChosen)
        {
            if (isUsable(enemy.attacks[2]) && ShouldUseMove(enemy.attacks[2]))
            {
                RemainingMoves = true; MoveChosen = true;
                EnemyAttack.ChosenAttack = enemy.attacks[2];
            }
        }
        a = UnityEngine.Random.Range(0, 100);
        if (a > 20 && !MoveChosen)
        {
            if (isUsable(enemy.attacks[1]) && ShouldUseMove(enemy.attacks[1]))
            {
                RemainingMoves = true; MoveChosen = true;
                EnemyAttack.ChosenAttack = enemy.attacks[1];
            }
        }
        if (!MoveChosen)    //no need for a if we get here we have to use this
        {
            if (isUsable(enemy.attacks[0]) && ShouldUseMove(enemy.attacks[0]))
            {
                RemainingMoves = true; MoveChosen = true;
                EnemyAttack.ChosenAttack = enemy.attacks[0];
            }
        }


        if (MoveChosen && RemainingMoves)
        {
            bool targetChosen = false;
            //target depends on move
            BaseAttack x = EnemyAttack.ChosenAttack; //store in x just to make it easier to write, fuck writing enemyattack.chosenattack every time

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
                foreach (GameObject hero in BSM.HeroesInBattle)
                {

                    if (hero.GetComponent<HeroStateMachine>().TypeMatch(enemy, hero.GetComponent<HeroStateMachine>().hero) == 2)
                    {
                        indListAll_Strong.Add(hero);

                        if (hero.GetComponent<HeroStateMachine>().hero.defDeBuffTurns > 0)
                        {
                            indList_defbroken_Strong.Add(hero);
                        }
                        else
                        {
                            indList_Strong.Add(hero);
                        }
                    }
                    else if (hero.GetComponent<HeroStateMachine>().TypeMatch(enemy, hero.GetComponent<HeroStateMachine>().hero) == 1)
                    {
                        indListAll_Neut.Add(hero);

                        if (hero.GetComponent<HeroStateMachine>().hero.defDeBuffTurns > 0)
                        {
                            indList_defbroken_Neut.Add(hero);
                        }
                        else
                        {
                            indList_Neut.Add(hero);
                        }
                    }
                    if (hero.GetComponent<HeroStateMachine>().TypeMatch(enemy, hero.GetComponent<HeroStateMachine>().hero) == 0)
                    {
                        indListAll_Weak.Add(hero);

                        if (hero.GetComponent<HeroStateMachine>().hero.defDeBuffTurns > 0)
                        {
                            indList_defbroken_Weak.Add(hero);
                        }
                        else
                        {
                            indList_Weak.Add(hero);
                        }
                    }
                }
                if (x.Effect[9] && !targetChosen && !x.Effect[0])
                {
                    int curr_B = 0;
                    GameObject curr_tar = BSM.HeroesInBattle[0];
                    UnityEngine.Debug.Log(indListAll_Strong.Count);
                    List<GameObject> indListAll_Final = new List<GameObject>();
                    foreach (GameObject target in indListAll_Strong)
                    {
                        if (target.GetComponent<HeroStateMachine>().checkifBuffed() > curr_B)
                        {
                            curr_B = target.GetComponent<HeroStateMachine>().checkifBuffed();
                        }
                    }
                    foreach (GameObject target in indListAll_Strong)
                    {

                        if (target.GetComponent<HeroStateMachine>().checkifBuffed() == curr_B)
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
                        if (target.GetComponent<HeroStateMachine>().checkifBuffed() > curr_B)
                        {
                            UnityEngine.Debug.Log("Neutral" + target.GetComponent<HeroStateMachine>().checkifBuffed());
                            curr_B = target.GetComponent<HeroStateMachine>().checkifBuffed();
                        }
                    }
                    foreach (GameObject target in indListAll_Neut)
                    {

                        if (target.GetComponent<HeroStateMachine>().checkifBuffed() == curr_B && curr_B != 0)
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
                        if (target.GetComponent<HeroStateMachine>().checkifBuffed() > curr_B)
                        {
                            curr_B = target.GetComponent<HeroStateMachine>().checkifBuffed();
                        }
                    }
                    foreach (GameObject target in indListAll_Weak)
                    {

                        if (target.GetComponent<HeroStateMachine>().checkifBuffed() == curr_B && curr_B != 0)
                        {
                            indListAll_Final.Add(target);
                        }
                    }
                    if (curr_B > n && curr_B > i)
                    {
                        curr_tar = indListAll_Final[UnityEngine.Random.Range(0, indListAll_Final.Count)];
                        indListAll_Final.Clear();
                    }


                    UnityEngine.Debug.Log(curr_tar + "is stripped");
                    EnemyAttack.AttackersTarget = curr_tar;
                    targetChosen = true;
                }

                if (x.Effect[9] && !targetChosen)
                {
                    int curr_B = 0;
                    GameObject curr_tar = BSM.HeroesInBattle[0];
                    UnityEngine.Debug.Log(indListAll_Strong.Count);
                    List<GameObject> indListAll_Final = new List<GameObject>();
                    foreach (GameObject target in indList_defbroken_Strong)
                    {
                        if (target.GetComponent<HeroStateMachine>().checkifBuffed() > curr_B)
                        {
                            curr_B = target.GetComponent<HeroStateMachine>().checkifBuffed();
                        }
                    }
                    foreach (GameObject target in indList_defbroken_Strong)
                    {

                        if (target.GetComponent<HeroStateMachine>().checkifBuffed() == curr_B)
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
                        if (target.GetComponent<HeroStateMachine>().checkifBuffed() > curr_B)
                        {
                            UnityEngine.Debug.Log("Neutral" + target.GetComponent<HeroStateMachine>().checkifBuffed());
                            curr_B = target.GetComponent<HeroStateMachine>().checkifBuffed();
                        }
                    }
                    foreach (GameObject target in indList_defbroken_Neut)
                    {

                        if (target.GetComponent<HeroStateMachine>().checkifBuffed() == curr_B && curr_B != 0)
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
                        if (target.GetComponent<HeroStateMachine>().checkifBuffed() > curr_B)
                        {
                            curr_B = target.GetComponent<HeroStateMachine>().checkifBuffed();
                        }
                    }
                    foreach (GameObject target in indListAll_Strong)
                    {

                        if (target.GetComponent<HeroStateMachine>().checkifBuffed() == curr_B && curr_B != 0)
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
                        if (target.GetComponent<HeroStateMachine>().checkifBuffed() > curr_B)
                        {
                            curr_B = target.GetComponent<HeroStateMachine>().checkifBuffed();
                        }
                    }
                    foreach (GameObject target in indList_defbroken_Weak)
                    {

                        if (target.GetComponent<HeroStateMachine>().checkifBuffed() == curr_B && curr_B != 0)
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
                        if (target.GetComponent<HeroStateMachine>().checkifBuffed() > curr_B)
                        {
                            curr_B = target.GetComponent<HeroStateMachine>().checkifBuffed();
                        }
                    }
                    foreach (GameObject target in indList_Neut)
                    {

                        if (target.GetComponent<HeroStateMachine>().checkifBuffed() == curr_B && curr_B != 0)
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
                        if (target.GetComponent<HeroStateMachine>().checkifBuffed() > curr_B)
                        {
                            curr_B = target.GetComponent<HeroStateMachine>().checkifBuffed();
                        }
                    }
                    foreach (GameObject target in indList_Weak)
                    {

                        if (target.GetComponent<HeroStateMachine>().checkifBuffed() == curr_B && curr_B != 0)
                        {
                            indListAll_Final.Add(target);
                        }
                    }
                    if (curr_B > n && curr_B > i && curr_B > g && curr_B > ge && curr_B > r)
                    {
                        curr_tar = indListAll_Final[UnityEngine.Random.Range(0, indListAll_Final.Count)];
                        indListAll_Final.Clear();
                    }
                    UnityEngine.Debug.Log(curr_tar + "is stripped");
                    EnemyAttack.AttackersTarget = curr_tar;
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
                                EnemyAttack.AttackersTarget = target;
                                targetChosen = true;
                            }
                        }
                        if (!targetChosen)
                        {
                            EnemyAttack.AttackersTarget = indList_defbroken_Strong[indList_defbroken_Strong.Count - 1];
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
                                EnemyAttack.AttackersTarget = target;
                                targetChosen = true;
                            }
                        }
                        if (!targetChosen)
                        {
                            EnemyAttack.AttackersTarget = indList_defbroken_Neut[indList_defbroken_Neut.Count - 1];
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
                                EnemyAttack.AttackersTarget = target;
                                targetChosen = true;
                            }
                        }
                        if (!targetChosen)
                        {
                            EnemyAttack.AttackersTarget = indList_Strong[indList_Strong.Count - 1];
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
                                EnemyAttack.AttackersTarget = target;
                                targetChosen = true;
                            }
                        }
                        if (!targetChosen)
                        {
                            EnemyAttack.AttackersTarget = indList_defbroken_Weak[indList_defbroken_Weak.Count - 1];
                        }
                        UnityEngine.Debug.Log("chosen defbroken weak target " + EnemyAttack.AttackersTarget);
                    }
                    else if (indList_Neut.Count > 0)
                    {
                        indList_Neut = SortByHpLowest(indList_Neut);

                        foreach (GameObject target in indList_Neut)
                        {
                            e = UnityEngine.Random.Range(0, 100);
                            UnityEngine.Debug.Log("Neutral e is " + e);
                            if (e > 20 && !targetChosen)
                            {
                                EnemyAttack.AttackersTarget = target;
                                targetChosen = true;
                            }
                        }
                        if (!targetChosen)
                        {
                            EnemyAttack.AttackersTarget = indList_Neut[indList_Neut.Count - 1];
                        }
                        UnityEngine.Debug.Log("chosen neut target " + EnemyAttack.AttackersTarget);
                    }
                    else if (indList_Weak.Count > 0)
                    {
                        indList_Weak = SortByHpLowest(indList_Weak);

                        foreach (GameObject target in indList_Weak)
                        {
                            e = UnityEngine.Random.Range(0, 100);
                            UnityEngine.Debug.Log("Weak e is " + e);
                            if (e > 20 && !targetChosen)
                            {
                                EnemyAttack.AttackersTarget = target;
                                targetChosen = true;
                            }
                        }
                        if (!targetChosen)
                        {
                            EnemyAttack.AttackersTarget = indList_Weak[indList_Weak.Count - 1];
                        }
                        UnityEngine.Debug.Log("chosen weak target " + EnemyAttack.AttackersTarget);
                    }
                    targetChosen = true;
                }

                if (x.Effect[3] || x.Effect[5] && !targetChosen)
                {
                    if (indListAll_Strong.Count > 0 && !targetChosen)
                    {
                        EnemyAttack.AttackersTarget = indListAll_Strong[UnityEngine.Random.Range(0, indListAll_Strong.Count)];
                        targetChosen = true;
                        UnityEngine.Debug.Log("debug for Effective, target is" + EnemyAttack.AttackersTarget);
                    }
                    else if (indListAll_Neut.Count > 0 && !targetChosen)
                    {
                        EnemyAttack.AttackersTarget = indListAll_Neut[UnityEngine.Random.Range(0, indListAll_Neut.Count)];
                        targetChosen = true;
                        UnityEngine.Debug.Log("debug for Neutral, target is" + EnemyAttack.AttackersTarget);

                    }
                    else if (indListAll_Weak.Count > 0 && !targetChosen)
                    {
                        EnemyAttack.AttackersTarget = indListAll_Weak[UnityEngine.Random.Range(0, indListAll_Weak.Count)];
                        targetChosen = true;
                        UnityEngine.Debug.Log("debug for Ineffective, target is" + EnemyAttack.AttackersTarget);

                    }

                }

                
            }
            if (x.Effect[1] && !targetChosen)   //HEAL ACTIVATES IF ALLY BELOW 70% TARGETS LOWEST HP
            {

                int d = UnityEngine.Random.Range(0, 100);
                if (d > 0 && ShouldUseMove(EnemyAttack.ChosenAttack))
                {
                    float lowestHpPercent = 100;
                    List<GameObject> EnemiesToHeal = new List<GameObject>();
                    foreach (GameObject target in BSM.EnemiesInBattle)
                    {
                        if (target.GetComponent<EnemyStateMachine>().enemy.HpCurrent / target.GetComponent<EnemyStateMachine>().enemy.HpBase * 100 < lowestHpPercent)
                            lowestHpPercent = target.GetComponent<EnemyStateMachine>().enemy.HpCurrent / target.GetComponent<EnemyStateMachine>().enemy.HpBase * 100;
                    }
                    foreach (GameObject target in BSM.EnemiesInBattle)
                    {
                        if (Math.Abs(target.GetComponent<EnemyStateMachine>().enemy.HpCurrent / target.GetComponent<EnemyStateMachine>().enemy.HpBase * 100 - lowestHpPercent) < 1)
                        {
                            EnemiesToHeal.Add(target);
                        }
                    }
                    EnemyAttack.AttackersTarget = EnemiesToHeal[UnityEngine.Random.Range(0, EnemiesToHeal.Count)];
                    targetChosen = true;
                    UnityEngine.Debug.Log("Healing " + EnemyAttack.AttackersTarget);
                }
            }

            if (x.Effect[7] && !targetChosen)     //ATK BAR BOOST. ALWAYS ACTIVATES TARGETS LOWEST ATKBAR
            {
                float LowestAtkBarPercent = 100;
                List<GameObject> TargetsToBoost = new List<GameObject>();
                foreach (GameObject target in BSM.EnemiesInBattle)
                {
                    if (target.GetComponent<EnemyStateMachine>().current_CD / target.GetComponent<EnemyStateMachine>().max_CD * 100 < LowestAtkBarPercent)
                        LowestAtkBarPercent = target.GetComponent<EnemyStateMachine>().current_CD / target.GetComponent<EnemyStateMachine>().max_CD * 100;
                }
                foreach (GameObject target in BSM.EnemiesInBattle)
                {
                    if (Math.Abs(target.GetComponent<EnemyStateMachine>().current_CD / target.GetComponent<EnemyStateMachine>().max_CD * 100 - LowestAtkBarPercent) < 1)
                        TargetsToBoost.Add(target);
                }
                EnemyAttack.AttackersTarget = TargetsToBoost[UnityEngine.Random.Range(0, TargetsToBoost.Count)];
                targetChosen = true;
                UnityEngine.Debug.Log("AtkBar Boosting " + EnemyAttack.AttackersTarget);
            }

            if (x.Effect[2] && !targetChosen)   //BUFF ALWAYS ACTIVATES TARGETS RANDOM
            {
                EnemyAttack.AttackersTarget = BSM.EnemiesInBattle[UnityEngine.Random.Range(0, BSM.EnemiesInBattle.Count)];
            }
            
            if (x.Effect[6] && !targetChosen)   //ATKBAR REDUCTION TARGETS HIGHEST ATKBAR
            {
                float highestAtkBarPercent = 0;
                foreach (GameObject target in BSM.HeroesInBattle)
                {
                    if (target.GetComponent<HeroStateMachine>().current_CD / target.GetComponent<HeroStateMachine>().max_CD * 100f > highestAtkBarPercent)
                        highestAtkBarPercent = target.GetComponent<HeroStateMachine>().current_CD / target.GetComponent<HeroStateMachine>().max_CD * 100f;
                }
                List<GameObject> AtkBarRedTargets = new List<GameObject>();
                foreach (GameObject target in BSM.HeroesInBattle)
                {
                    if (Math.Abs(highestAtkBarPercent - target.GetComponent<HeroStateMachine>().current_CD / target.GetComponent<HeroStateMachine>().max_CD * 100f) < 1f)
                        AtkBarRedTargets.Add(target);
                }
                int d = UnityEngine.Random.Range(0, AtkBarRedTargets.Count);
                EnemyAttack.AttackersTarget = AtkBarRedTargets[d];
                UnityEngine.Debug.Log("AtkBarReduction target is " + AtkBarRedTargets[d] + "with an atk bar percent of" + AtkBarRedTargets[d].GetComponent<HeroStateMachine>().current_CD / AtkBarRedTargets[d].GetComponent<HeroStateMachine>().max_CD * 100f);
            }
        }

        EnemyAttack.type = "Enemy";
        BSM.CollectActions(EnemyAttack);
        EnemyState = EnemyStates.IDLE;

    }
    bool isUsable(BaseAttack x)
    {
        if(x == enemy.attacks[0])
        {
            if (Cd1 == 0)
                return true;
        }
        if (x == enemy.attacks[1])
        {
            if (Cd2 == 0)
                return true;
        }
        if (x == enemy.attacks[2])
        {
            if (Cd3 == 0)
                return true;
        }
        if (x == enemy.attacks[3])
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
            foreach(GameObject target in BSM.EnemiesInBattleTotal)
            {
                if (!target.GetComponent<EnemyStateMachine>().EnemyAlive)
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
            foreach (GameObject target in BSM.EnemiesInBattle)
            {
                if (target.GetComponent<EnemyStateMachine>().checkHpStatus(70))
                    return true;
            }
        }
        if (x.Effect[8])
        {
            foreach (GameObject target in BSM.EnemiesInBattle)
            {
                if (target.GetComponent<EnemyStateMachine>().checkifDebuffed() > 0)
                    return true;
            }
        }
        if (x.Effect[9])
        {
            foreach (GameObject target in BSM.HeroesInBattle)
            {
                if (target.GetComponent<HeroStateMachine>().checkifBuffed() > 0)
                    return true;
            }
        }
        if (x.Effect[0] || x.Effect[2]||  x.Effect[3]||  x.Effect[5]||  x.Effect[6] || x.Effect[7] && !x.Effect[4])
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
        foreach(GameObject target in SortList)
        {
            if (target.GetComponent<HeroStateMachine>().hero.HpCurrent / target.GetComponent<HeroStateMachine>().hero.HpBase * 100 < lowestHpPercent)
                lowestHpPercent = target.GetComponent<HeroStateMachine>().hero.HpCurrent / target.GetComponent<HeroStateMachine>().hero.HpBase * 100;
        }
        foreach (GameObject target in SortList)
        {
            if (Math.Abs(target.GetComponent<HeroStateMachine>().hero.HpCurrent / target.GetComponent<HeroStateMachine>().hero.HpBase * 100 - lowestHpPercent) < 1)
                return target;
        }
        return null;
    }
}
