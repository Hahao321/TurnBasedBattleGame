using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class BattleStateMachine : MonoBehaviour
{
    public Canvas ExitCanvas;

    public bool AutoEnabled = false;
    public int rend = 0;

    public bool testbool = false;

    public BaseAttack x;
    public MonsterData Monsterdata;
   // public LocationData locationData;
    public BigData bigData;

    public List<TurnInformation> PerformList = new List<TurnInformation>();
    public List<GameObject> HeroesInBattle = new List<GameObject>();
    public List<GameObject> EnemiesInBattle = new List<GameObject>();
    public List<GameObject> EnemiesInBattleTotal = new List<GameObject>();
    public List<Vector3> EnemyLocations = new List<Vector3>();
    public List<GameObject> HeroesInBattleTotal = new List<GameObject>();
    public List<Vector3> HeroLocations = new List<Vector3>();
    
    
    //bsm states
    public enum performAction
    {
        ATTACKBARSLOADING,
        TAKEACTION,
        PERFORMACTION,
        CHECKALIVE,
        WIN,
        LOSE,
    }
    public performAction battleStates;
   
    //hero gui aka attack panel states
    public enum HeroGUI
    {
        CHOOSEMOVE,
        CHOOSETARGET,
        IDLE,
        ACTUALIDLE
    }
    public HeroGUI HeroInput;
    public GameObject AttackPanel;

    public List<GameObject> HeroestoManage = new List<GameObject>();
    public TurnInformation HerosTurnPlan;

    public GameObject Button1;
    public GameObject Button2;
    public GameObject Button3;
    public GameObject Button4;

    public List<GameObject> DeadEnemies = new List<GameObject>();


    public Sprite AtkBuff;
    public Sprite SpdBuff;
    public Sprite DefBuff;
    public Sprite Recovery;
    public Sprite AtkDeBuff;
    public Sprite SpdDeBuff;
    public Sprite DefDeBuff;
    public Sprite Dot;
    public Sprite Stun;

    // Start is called before the first frame update
    private void Awake()
    {
        ExitCanvas.enabled = false;


        bigData = GameObject.Find("LocData").GetComponent<BigData>();

        //if (bigData.BattleDataList.Count > 0)
        //{
         //   Debug.Log("Spawn called");
          //  bigData.Spawn(bigData.BattleDataList[0]);
        //}
        if (BigData.BattleDataListS.Count > 0)
        {
            Debug.Log("Spawn called");
            bigData.Spawn(BigData.BattleDataListS[0]);
        }

        Debug.Log("awake bsn");
        //call spawning function

        
        

        // var b = Instantiate(data.MonsterData[0], new Vector3(-5, 0, 0), quaternion.identity);
        // b.name = b.GetComponent<HeroStateMachine>().hero.Name;


        SetUp();    //fills lists of characters and sets them to open animations 
       
        AttackPanel.SetActive(false);       //keep them off till we turn them on
        
    }
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (rend < 1)
        {
            foreach (GameObject target in HeroesInBattle)
            {
                target.GetComponent<SpriteRenderer>().sprite = target.GetComponent<HeroStateMachine>().GetComponent<BaseMonster>().MonsterSprite;
                Debug.Log("sprite rend");
            }
            rend++;
        }
        foreach (GameObject target in HeroesInBattle)
        {
            target.GetComponent<HeroStateMachine>().updateHpBar();
        }
        foreach (GameObject target in EnemiesInBattle)
        {
            target.GetComponent<EnemyStateMachine>().updateHpBar();
        }

        IfHeroSelected();
        IfEnemySelected();


        switch (battleStates)
        {
            case (performAction.ATTACKBARSLOADING):
               
                if (PerformList.Count > 0)
                {
                    foreach (GameObject enemy in EnemiesInBattle)
                        enemy.GetComponent<EnemyStateMachine>().c = 0;
                    battleStates = performAction.TAKEACTION;
                }
                break;
            case (performAction.TAKEACTION):
                GameObject performer = GameObject.Find(PerformList[0].Attacker);
                GameObject performer2 = PerformList[0].AttackerGameObject;


                if (PerformList[0].type == "Enemy")
                {
                    EnemyStateMachine ESM = performer2.GetComponent<EnemyStateMachine>();
                    
                    ESM.HerotoAttack = PerformList[0].AttackersTarget;
                    ESM.EnemyState = EnemyStateMachine.EnemyStates.PERFORMMOVE;
                    battleStates = performAction.PERFORMACTION;

                }
                if (PerformList[0].type == "Hero")
                {
                    
                    HeroStateMachine HSM = performer.GetComponent<HeroStateMachine>();
                    HSM.EnemytoAttack = PerformList[0].AttackersTarget;
                    HSM.HeroState = HeroStateMachine.HeroStates.PERFORMMOVE;
                }
                battleStates = performAction.PERFORMACTION;
                break;


            case (performAction.PERFORMACTION):

                break;


            case (performAction.CHECKALIVE):    //win/lose conditions and tracking live vs dead enemies
                if (HeroesInBattle.Count < 1)
                {
                    battleStates = performAction.LOSE;
                }
                else if (EnemiesInBattle.Count < 1)
                {
                    battleStates = performAction.WIN;
                }
                else
                {
                    battleStates = performAction.ATTACKBARSLOADING;
                }
                break;


            case (performAction.WIN):
                {
                    

                    for (int i = 0; i < HeroesInBattle.Count; i++)
                    {
                        if(HeroesInBattle[i].GetComponent<HeroStateMachine>().HeroState == HeroStateMachine.HeroStates.PERFORMMOVE)
                        {
                            if (HeroesInBattle[i].GetComponent<HeroStateMachine>().Cd2 > 0)
                                HeroesInBattle[i].GetComponent<HeroStateMachine>().Cd2--;
                            if (HeroesInBattle[i].GetComponent<HeroStateMachine>().Cd3 > 0)
                                HeroesInBattle[i].GetComponent<HeroStateMachine>().Cd3--;
                            if (HeroesInBattle[i].GetComponent<HeroStateMachine>().Cd4 > 0)
                                HeroesInBattle[i].GetComponent<HeroStateMachine>().Cd4--;
                        }

                        HeroesInBattle[i].GetComponent<HeroStateMachine>().HeroState = HeroStateMachine.HeroStates.IDLE;   //fast forward so we can ignore all other
                                                                                                                           //shit our heroes were gonna do cuz we won
                    }

                    /* if (bigData.BattleDataList.Count > 0)
                         bigData.BattleDataList.RemoveAt(0);
                     if (bigData.BattleDataList.Count > 0)
                     {
                         bigData.duringBattle = true;
                         UpdateHeroList();
                         Debug.Log("scene loading");
                         SceneManager.LoadScene("BattleScene");
                     }*/

                    if (BigData.BattleDataListS.Count > 0)
                        BigData.BattleDataListS.RemoveAt(0);
                    if (BigData.BattleDataListS.Count > 0)
                    {
                        bigData.duringBattle = true;
                        UpdateHeroList();
                        Debug.Log("scene loading");
                        SceneManager.LoadScene("BattleSceneCopy");
                    }
                    else
                    {
                        bigData.duringBattle = false;
                        ExitCanvas.enabled = true;

                    }

                    


                }
                break;


            case (performAction.LOSE):
                {
                    for (int i = 0; i < HeroesInBattle.Count; i++)
                    {
                        EnemiesInBattle[i].GetComponent<EnemyStateMachine>().EnemyState = EnemyStateMachine.EnemyStates.IDLE;   //fast forward so we can ignore all other
                                                                                                                           //shit our heroes were gonna do cuz we won
                    }
                    ExitCanvas.enabled = true;
                }
                break;
        }


        switch (HeroInput)
        {
            case (HeroGUI.CHOOSEMOVE):

                if (HeroestoManage.Count > 0)        //if we have shit to do, we will start shit
                   // if (HeroestoManage[0].GetComponent<HeroStateMachine>().HeroState == HeroStateMachine.HeroStates.ADDMOVETOBSM)
                   // {
                        {
                            HeroestoManage[0].GetComponent<HeroStateMachine>().arrow.SetActive(true);

                            HerosTurnPlan = new TurnInformation();
                            AttackPanel.SetActive(true);                                                //set up attack panel
                                                                                                        //make atk buttons
                            CreateAtkButtons();

                            HeroInput = HeroGUI.ACTUALIDLE;                                                //move to idle until move clicked
                        }
                  //  }
                break;

            case (HeroGUI.CHOOSETARGET): //we go here if move clicked
                //AttackPanel.SetActive(false);
                HerosTurnPlan.AttackerGameObject.GetComponent<HeroStateMachine>().arrow.SetActive(false);
                if (HerosTurnPlan.ChosenAttack.SelectingEnemy)  //means we choosing enemy target
                {
                    foreach(GameObject enemy in EnemiesInBattle)
                    {
                        enemy.GetComponent<EnemyStateMachine>().col.enabled = true;
                        if (enemy.GetComponent<EnemyStateMachine>().TypeMatch(HerosTurnPlan.AttackerGameObject.GetComponent<HeroStateMachine>().hero, enemy.GetComponent<EnemyStateMachine>().enemy) == 2)
                        {
                            enemy.GetComponent<EnemyStateMachine>().arrow_s.SetActive(true);
                        }
                        if (enemy.GetComponent<EnemyStateMachine>().TypeMatch(HerosTurnPlan.AttackerGameObject.GetComponent<HeroStateMachine>().hero, enemy.GetComponent<EnemyStateMachine>().enemy) == 1)
                        {
                            enemy.GetComponent<EnemyStateMachine>().arrow.SetActive(true);
                        }
                        if (enemy.GetComponent<EnemyStateMachine>().TypeMatch(HerosTurnPlan.AttackerGameObject.GetComponent<HeroStateMachine>().hero, enemy.GetComponent<EnemyStateMachine>().enemy) == 0)
                        {
                            enemy.GetComponent<EnemyStateMachine>().arrow_w.SetActive(true);
                        }
                    }
                }
                else if(HerosTurnPlan.ChosenAttack.Effect[4])
                {
                    foreach (GameObject hero in HeroesInBattleTotal)
                    {
                        if (hero.GetComponent<HeroStateMachine>().HeroAlive == false)
                        {
                            hero.GetComponent<HeroStateMachine>().col.enabled = true;
                            hero.GetComponent<HeroStateMachine>().arrow.SetActive(true);
                        }
                    }
                    foreach (GameObject hero in HeroesInBattle)
                    {
                        hero.GetComponent<HeroStateMachine>().col.enabled = false;
                        hero.GetComponent<HeroStateMachine>().arrow.SetActive(false);
                    }

                }
                else
                {
                    foreach (GameObject hero in HeroesInBattle)
                    {
                        hero.GetComponent<HeroStateMachine>().col.enabled = true;
                        hero.GetComponent<HeroStateMachine>().arrow.SetActive(true);
                    }
                }
                break;

            case (HeroGUI.IDLE):
                HeroTurnDone();

                break;


            case (HeroGUI.ACTUALIDLE):

                break;


        }

    }



    //functions

    void SetUp()
    {

        battleStates = performAction.ATTACKBARSLOADING;

        EnemiesInBattle.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));       //gets all with tag
        EnemiesInBattleTotal.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));       //gets all with tag
        foreach (GameObject enemy in EnemiesInBattleTotal)
        {
            EnemyLocations.Add(enemy.transform.position);
            enemy.GetComponent<EnemyStateMachine>().enemy.HpMax = enemy.GetComponent<EnemyStateMachine>().enemy.HpBase;             //edit when runes available
            enemy.GetComponent<EnemyStateMachine>().EnemyState = EnemyStateMachine.EnemyStates.OPENANIMATIONS;
        }

        HeroesInBattle.AddRange(GameObject.FindGameObjectsWithTag("Hero"));         //gets all with tag      
        HeroesInBattleTotal.AddRange(GameObject.FindGameObjectsWithTag("Hero"));       //gets all with tag
        foreach (GameObject hero in HeroesInBattleTotal)
        {
            HeroLocations.Add(hero.transform.position);
            hero.GetComponent<HeroStateMachine>().hero.HpMax = hero.GetComponent<HeroStateMachine>().hero.HpBase;             //edit when runes available
            hero.GetComponent<HeroStateMachine>().HeroState = HeroStateMachine.HeroStates.OPENANIMATIONS;

        }
        foreach(GameObject target in HeroesInBattle)
        {
           target.GetComponent<HeroStateMachine>().statChangeCheck();
        }
        foreach (GameObject target in EnemiesInBattle)
        {
          target.GetComponent<EnemyStateMachine>().statChangeCheck();
        }

    } 
    void IfEnemySelected()
    {
        
        foreach (GameObject enemy in EnemiesInBattleTotal)
        {
            if (enemy.GetComponent<EnemyStateMachine>().selected == true)
            {
                enemy.GetComponent<EnemyStateMachine>().col.enabled = false;
                enemy.GetComponent<EnemyStateMachine>().selected = false;  //reset
                DoMove(enemy);

            }
        }
    }
    void IfHeroSelected()
    {
        foreach (GameObject hero in HeroesInBattleTotal)
        {
            if (hero.GetComponent<HeroStateMachine>().selected == true)
            {
                hero.GetComponent<HeroStateMachine>().col.enabled = false;
                hero.GetComponent<HeroStateMachine>().selected = false;  //reset
                DoMove(hero);

            }
        }
    }
    void HeroTurnDone()
    {
       
        foreach (GameObject enemy in EnemiesInBattle)
        {
            enemy.GetComponent<EnemyStateMachine>().arrow.SetActive(false);
            enemy.GetComponent<EnemyStateMachine>().arrow_w.SetActive(false);
            enemy.GetComponent<EnemyStateMachine>().arrow_s.SetActive(false);
        }
        foreach (GameObject hero in HeroesInBattle)
        {
            hero.GetComponent<HeroStateMachine>().arrow.SetActive(false);
        }

        HeroestoManage[0].GetComponent<HeroStateMachine>().arrow.SetActive(false);

        //now remove current input of this hero so we can go to next
        HeroestoManage.RemoveAt(0); //cut our current hero we just finished in. Note that lists will force position 1 into position 0 if we do this
        PerformList.Add(HerosTurnPlan);  //adding handle turn called HeroesChoice into perform list; a list of handleturns holding info to do moves
        HeroInput = HeroGUI.CHOOSEMOVE;
    }
    void CreateAtkButtons()
    {
        Button1.GetComponent<Image>().sprite = HeroestoManage[0].GetComponent<HeroStateMachine>().hero.Attack1;
        Button2.GetComponent<Image>().sprite = HeroestoManage[0].GetComponent<HeroStateMachine>().hero.Attack2;
        Button3.GetComponent<Image>().sprite = HeroestoManage[0].GetComponent<HeroStateMachine>().hero.Attack3;
        Button4.GetComponent<Image>().sprite = HeroestoManage[0].GetComponent<HeroStateMachine>().hero.Attack4;
        if(HeroestoManage[0].GetComponent<HeroStateMachine>().Cd1> 0)
            Button1.GetComponent<Button>().interactable = false;
        else if(HeroestoManage[0].GetComponent<HeroStateMachine>().Cd1 == 0)
            Button1.GetComponent<Button>().interactable = true;
        if (HeroestoManage[0].GetComponent<HeroStateMachine>().Cd2 > 0)
            Button2.GetComponent<Button>().interactable = false;
        else if (HeroestoManage[0].GetComponent<HeroStateMachine>().Cd2 == 0)
            Button2.GetComponent<Button>().interactable = true;
        if (HeroestoManage[0].GetComponent<HeroStateMachine>().Cd3 > 0)
            Button3.GetComponent<Button>().interactable = false;
        else if (HeroestoManage[0].GetComponent<HeroStateMachine>().Cd3 == 0)
            Button3.GetComponent<Button>().interactable = true;
        if (HeroestoManage[0].GetComponent<HeroStateMachine>().Cd4 > 0)
            Button4.GetComponent<Button>().interactable = false;
        else if (HeroestoManage[0].GetComponent<HeroStateMachine>().Cd4 == 0)
            Button4.GetComponent<Button>().interactable = true;

    }
    public void CollectActions(TurnInformation input)     //public so we can access outside to edit from other classes
    {
        PerformList.Add(input);     //at start, we have an empty list of type handleturns. now our input handle turn is taking spot 1 on that empty list. 

    }
    void DoMove(GameObject ChosenEnemy)
    {
        AttackPanel.SetActive(false);

        HerosTurnPlan.AttackersTarget = ChosenEnemy;
        HeroInput = HeroGUI.IDLE;
        HeroStateMachine HSM = HeroestoManage[0].GetComponent<HeroStateMachine>();
        HSM.HeroState = HeroStateMachine.HeroStates.ACTUALIDLE;
    }
    
    

    
    public void doMove(BaseAttack attackchosen)
    {
        HeroInput = HeroGUI.IDLE;
    }

    public void doMoveAgain()
    {

        
        HerosTurnPlan.Attacker = HeroestoManage[0].name;
        HerosTurnPlan.AttackerGameObject = HeroestoManage[0];
        HerosTurnPlan.type = "Hero";
        HeroInput = HeroGUI.CHOOSETARGET;
        HerosTurnPlan.ChosenAttack = x;

    }
    public void AttackChosen1()
    {
        HerosTurnPlan.Attacker = HeroestoManage[0].name;
        HerosTurnPlan.AttackerGameObject = HeroestoManage[0];
        HerosTurnPlan.ChosenAttack = HeroestoManage[0].GetComponent<HeroStateMachine>().hero.attacks[0];
        HerosTurnPlan.type = "Hero";
        HeroInput = HeroGUI.CHOOSETARGET;

        TurnOffAllArrowsandColiders();


    }
    public void AttackChosen2()
    {
        HerosTurnPlan.ChosenAttack = HeroestoManage[0].GetComponent<HeroStateMachine>().hero.attacks[1];
        HerosTurnPlan.Attacker = HeroestoManage[0].name;
        HerosTurnPlan.AttackerGameObject = HeroestoManage[0];
        HerosTurnPlan.type = "Hero";
        HeroInput = HeroGUI.CHOOSETARGET;

        TurnOffAllArrowsandColiders();

    }
    public void AttackChosen3()
    {
        HerosTurnPlan.ChosenAttack = HeroestoManage[0].GetComponent<HeroStateMachine>().hero.attacks[2];
        HerosTurnPlan.Attacker = HeroestoManage[0].name;
        HerosTurnPlan.AttackerGameObject = HeroestoManage[0];
        HerosTurnPlan.type = "Hero";
        HeroInput = HeroGUI.CHOOSETARGET;

        TurnOffAllArrowsandColiders();
    }
    public void AttackChosen4()
    {
        HerosTurnPlan.ChosenAttack = HeroestoManage[0].GetComponent<HeroStateMachine>().hero.attacks[3];
        HerosTurnPlan.Attacker = HeroestoManage[0].name;
        HerosTurnPlan.AttackerGameObject = HeroestoManage[0];
        HerosTurnPlan.type = "Hero";
        HeroInput = HeroGUI.CHOOSETARGET;
        TurnOffAllArrowsandColiders();

    }

    public void UpdateHeroList()
    {
        foreach(GameObject hero in HeroesInBattleTotal)
        {
            HeroStateMachine TempHsm = hero.GetComponent<HeroStateMachine>();
            if (hero.transform.position == new Vector3(-6, -3, 0))
            {
                bigData.CurrentHp[0] = TempHsm.hero.HpCurrent;
                bigData.CD2[0] = TempHsm.Cd2;
                bigData.CD3[0] = TempHsm.Cd3;
                bigData.CD4[0] = TempHsm.Cd4;

            }
            if (hero.transform.position == new Vector3(-5, -1, 0))
            {
                bigData.CurrentHp[1] = TempHsm.hero.HpCurrent;
                bigData.CD2[1] = TempHsm.Cd2;
                bigData.CD3[1] = TempHsm.Cd3;
                bigData.CD4[1] = TempHsm.Cd4;
            }
            if (hero.transform.position == new Vector3(-8, 1, 0))
            {
                bigData.CurrentHp[2] = TempHsm.hero.HpCurrent;
                bigData.CD2[2] = TempHsm.Cd2;
                bigData.CD3[2] = TempHsm.Cd3;
                bigData.CD4[2] = TempHsm.Cd4;
            }
            if (hero.transform.position == new Vector3(-4, 2, 0))
            {
                bigData.CurrentHp[3] = TempHsm.hero.HpCurrent;
                bigData.CD2[3] = TempHsm.Cd2;
                bigData.CD3[3] = TempHsm.Cd3;
                bigData.CD4[3] = TempHsm.Cd4;
            }


        }


    }
    private void TurnOffAllArrowsandColiders()
    {
        foreach (GameObject target in HeroesInBattleTotal)
        {
            if (target != HerosTurnPlan.AttackerGameObject)
                target.GetComponent<HeroStateMachine>().arrow.SetActive(false);

            target.GetComponent<PolygonCollider2D>().enabled = false;
        }
        foreach (GameObject target in EnemiesInBattleTotal)
        {
            target.GetComponent<EnemyStateMachine>().arrow.SetActive(false);
            target.GetComponent<PolygonCollider2D>().enabled = false;

        }
    }

    public void HomeButton()
    {
        ExitCanvas.enabled = false;
        SceneManager.LoadScene("HomeScreen");
    }
    public void ScenarioButton()
    {
        ExitCanvas.enabled = false;
        SceneManager.LoadScene("ScenarioScreen");
    }
}
 