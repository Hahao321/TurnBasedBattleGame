using PathCreation.Examples;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BigData : MonoBehaviour
{
    public static int Money = 10000;
    public int MoneyCopy;

    public string ScenarioName = "";
    public int SubNumber = 0;
    public List<GameObject> AllMonsters = new List<GameObject>();
    public bool KeepThis = false;
    // public List<float> CurrentHp;
    //public List<int> CD2;
    //public List<int> CD3;
    //public List<int> CD4;

    public float[] CurrentHp = { 0, 0, 0, 0 };
    public int[] CD2 = { 0, 0, 0, 0 };
    public int[] CD3 = { 0, 0, 0, 0 };
    public int[] CD4 = { 0, 0, 0, 0 };
    public bool duringBattle = false;


    public BattleData1 xyz;
    //public static List<BattleData1> BattleDataList;
    public  List<BattleData1> BattleDataList = new List<BattleData1>();
    public static List<BattleData1> BattleDataListS = new List<BattleData1>();
    public List<BattleData1> BattleDataListCopy = new List<BattleData1>();



    public static List<GameObject> HeroList = new List<GameObject>();
    public  List<GameObject> VisableHeroList = new List<GameObject>();

    public static List<BaseMonster> HeroListm = new List<BaseMonster>();
    public List<BaseMonster> VisableHeroListm = new List<BaseMonster>();

    public static List<BaseMonster> ChosenHeroList = new List<BaseMonster>();


    public List<Sprite> BackGroundImage = new List<Sprite>();       //background image given



    /// ////////////////////////////////////////////////////////////////////////
    ///  ////////////////////////////////////////////////////////////////////////
    ///   ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    /// ////////////////////////////////////////////////////////////////////////
    /// ////////////////////////////////////////////////////////////////////////
    ///  ////////////////////////////////////////////////////////////////////////
    ///   ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    /// ////////////////////////////////////////////////////////////////////////
    /// ////////////////////////////////////////////////////////////////////////
    ///  ////////////////////////////////////////////////////////////////////////
    ///   ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    ///
    /// 
    ///
    public List<int> NumberofStages1 = new List<int>();             //using int WhichStage we have number of stages in selected sublocation (ie for boss we will get 4)
    public List<int> NumberofStages2 = new List<int>();
    public List<int> NumberofStages3 = new List<int>();
    public List<int> NumberofStages4 = new List<int>();
    public List<int> NumberofStages5 = new List<int>();
    public List<int> NumberofStages6 = new List<int>();
    public List<int> NumberofStages7 = new List<int>();


    ////////////////////////////////////////////////////////////////////////
    /// ////////////////////////////////////////////////////////////////////////
    ///  ////////////////////////////////////////////////////////////////////////
    ///   ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    /// ////////////////////////////////////////////////////////////////////////
    /// ////////////////////////////////////////////////////////////////////////
    ///  ////////////////////////////////////////////////////////////////////////
    ///   ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    /// ////////////////////////////////////////////////////////////////////////
    /// ////////////////////////////////////////////////////////////////////////
    ///  ////////////////////////////////////////////////////////////////////////
    ///   ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////


    public List<int> MonstersInScenario1Sub1 = new List<int>();     //if we are on faimon final and theres 4 stages then this lsit is 4 long, eg {5,5,5,1} meaning 5 monsters each stage till boss
    public List<int> MonstersInScenario1Sub2 = new List<int>();
    public List<int> MonstersInScenario1Sub3 = new List<int>();
    public List<int> MonstersInScenario1Sub4 = new List<int>();
    public List<int> MonstersInScenario1Sub5 = new List<int>();
    public List<int> MonstersInScenario1Sub6 = new List<int>();
    public List<int> MonstersInScenario1Sub7 = new List<int>();

    public List<int> MonstersInScenario2Sub1 = new List<int>();
    public List<int> MonstersInScenario2Sub2 = new List<int>();
    public List<int> MonstersInScenario2Sub3 = new List<int>();
    public List<int> MonstersInScenario2Sub4 = new List<int>();
    public List<int> MonstersInScenario2Sub5 = new List<int>();
    public List<int> MonstersInScenario2Sub6 = new List<int>();
    public List<int> MonstersInScenario2Sub7 = new List<int>();

    public List<int> MonstersInScenario3Sub1 = new List<int>();
    public List<int> MonstersInScenario3Sub2 = new List<int>();
    public List<int> MonstersInScenario3Sub3 = new List<int>();
    public List<int> MonstersInScenario3Sub4 = new List<int>();
    public List<int> MonstersInScenario3Sub5 = new List<int>();
    public List<int> MonstersInScenario3Sub6 = new List<int>();
    public List<int> MonstersInScenario3Sub7 = new List<int>();

    public List<int> MonstersInScenario4Sub1 = new List<int>();
    public List<int> MonstersInScenario4Sub2 = new List<int>();
    public List<int> MonstersInScenario4Sub3 = new List<int>();
    public List<int> MonstersInScenario4Sub4 = new List<int>();
    public List<int> MonstersInScenario4Sub5 = new List<int>();
    public List<int> MonstersInScenario4Sub6 = new List<int>();
    public List<int> MonstersInScenario4Sub7 = new List<int>();

    public List<int> MonstersInScenario5Sub1 = new List<int>();
    public List<int> MonstersInScenario5Sub2 = new List<int>();
    public List<int> MonstersInScenario5Sub3 = new List<int>();
    public List<int> MonstersInScenario5Sub4 = new List<int>();
    public List<int> MonstersInScenario5Sub5 = new List<int>();
    public List<int> MonstersInScenario5Sub6 = new List<int>();
    public List<int> MonstersInScenario5Sub7 = new List<int>();

    public List<int> MonstersInScenario6Sub1 = new List<int>();
    public List<int> MonstersInScenario6Sub2 = new List<int>();
    public List<int> MonstersInScenario6Sub3 = new List<int>();
    public List<int> MonstersInScenario6Sub4 = new List<int>();
    public List<int> MonstersInScenario6Sub5 = new List<int>();
    public List<int> MonstersInScenario6Sub6 = new List<int>();
    public List<int> MonstersInScenario6Sub7 = new List<int>();

    public List<int> MonstersInScenario7Sub1 = new List<int>();
    public List<int> MonstersInScenario7Sub2 = new List<int>();
    public List<int> MonstersInScenario7Sub3 = new List<int>();
    public List<int> MonstersInScenario7Sub4 = new List<int>();
    public List<int> MonstersInScenario7Sub5 = new List<int>();
    public List<int> MonstersInScenario7Sub6 = new List<int>();
    public List<int> MonstersInScenario7Sub7 = new List<int>();

    ////////////////////////////////////////////////////////////////////////
    /// ////////////////////////////////////////////////////////////////////////
    ///  ////////////////////////////////////////////////////////////////////////
    ///   ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    /// ////////////////////////////////////////////////////////////////////////
    ///  ////////////////////////////////////////////////////////////////////////
    ///   ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    /// ////////////////////////////////////////////////////////////////////////
    ///  ////////////////////////////////////////////////////////////////////////
    ///   ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////


    public List<BaseMonster> PossibleEnemiesScenario1Sub1Stage1 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario1Sub1Stage2 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario1Sub1Stage3 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario1Sub2Stage1 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario1Sub2Stage2 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario1Sub2Stage3 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario1Sub3Stage1 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario1Sub3Stage2 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario1Sub3Stage3 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario1Sub4Stage1 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario1Sub4Stage2 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario1Sub4Stage3 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario1Sub5Stage1 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario1Sub5Stage2 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario1Sub5Stage3 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario1Sub6Stage1 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario1Sub6Stage2 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario1Sub6Stage3 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario1Sub7Stage1 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario1Sub7Stage2 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario1Sub7Stage3 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario1Sub7Stage4 = new List<BaseMonster>();

    public List<BaseMonster> PossibleEnemiesScenario2Sub1Stage1 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario2Sub1Stage2 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario2Sub1Stage3 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario2Sub2Stage1 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario2Sub2Stage2 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario2Sub2Stage3 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario2Sub3Stage1 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario2Sub3Stage2 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario2Sub3Stage3 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario2Sub4Stage1 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario2Sub4Stage2 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario2Sub4Stage3 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario2Sub5Stage1 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario2Sub5Stage2 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario2Sub5Stage3 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario2Sub6Stage1 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario2Sub6Stage2 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario2Sub6Stage3 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario2Sub7Stage1 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario2Sub7Stage2 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario2Sub7Stage3 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario2Sub7Stage4 = new List<BaseMonster>();

    public List<BaseMonster> PossibleEnemiesScenario3Sub1Stage1 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario3Sub1Stage2 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario3Sub1Stage3 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario3Sub2Stage1 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario3Sub2Stage2 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario3Sub2Stage3 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario3Sub3Stage1 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario3Sub3Stage2 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario3Sub3Stage3 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario3Sub4Stage1 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario3Sub4Stage2 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario3Sub4Stage3 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario3Sub5Stage1 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario3Sub5Stage2 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario3Sub5Stage3 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario3Sub6Stage1 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario3Sub6Stage2 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario3Sub6Stage3 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario3Sub7Stage1 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario3Sub7Stage2 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario3Sub7Stage3 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario3Sub7Stage4 = new List<BaseMonster>();

    public List<BaseMonster> PossibleEnemiesScenario4Sub1Stage1 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario4Sub1Stage2 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario4Sub1Stage3 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario4Sub2Stage1 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario4Sub2Stage2 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario4Sub2Stage3 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario4Sub3Stage1 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario4Sub3Stage2 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario4Sub3Stage3 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario4Sub4Stage1 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario4Sub4Stage2 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario4Sub4Stage3 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario4Sub5Stage1 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario4Sub5Stage2 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario4Sub5Stage3 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario4Sub6Stage1 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario4Sub6Stage2 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario4Sub6Stage3 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario4Sub7Stage1 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario4Sub7Stage2 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario4Sub7Stage3 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario4Sub7Stage4 = new List<BaseMonster>();

    public List<BaseMonster> PossibleEnemiesScenario5Sub1Stage1 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario5Sub1Stage2 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario5Sub1Stage3 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario5Sub2Stage1 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario5Sub2Stage2 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario5Sub2Stage3 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario5Sub3Stage1 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario5Sub3Stage2 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario5Sub3Stage3 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario5Sub4Stage1 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario5Sub4Stage2 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario5Sub4Stage3 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario5Sub5Stage1 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario5Sub5Stage2 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario5Sub5Stage3 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario5Sub6Stage1 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario5Sub6Stage2 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario5Sub6Stage3 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario5Sub7Stage1 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario5Sub7Stage2 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario5Sub7Stage3 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario5Sub7Stage4 = new List<BaseMonster>();

    public List<BaseMonster> PossibleEnemiesScenario6Sub1Stage1 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario6Sub1Stage2 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario6Sub1Stage3 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario6Sub2Stage1 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario6Sub2Stage2 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario6Sub2Stage3 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario6Sub3Stage1 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario6Sub3Stage2 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario6Sub3Stage3 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario6Sub4Stage1 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario6Sub4Stage2 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario6Sub4Stage3 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario6Sub5Stage1 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario6Sub5Stage2 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario6Sub5Stage3 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario6Sub6Stage1 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario6Sub6Stage2 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario6Sub6Stage3 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario6Sub7Stage1 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario6Sub7Stage2 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario6Sub7Stage3 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario6Sub7Stage4 = new List<BaseMonster>();

    public List<BaseMonster> PossibleEnemiesScenario7Sub1Stage1 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario7Sub1Stage2 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario7Sub1Stage3 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario7Sub2Stage1 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario7Sub2Stage2 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario7Sub2Stage3 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario7Sub3Stage1 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario7Sub3Stage2 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario7Sub3Stage3 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario7Sub4Stage1 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario7Sub4Stage2 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario7Sub4Stage3 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario7Sub5Stage1 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario7Sub5Stage2 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario7Sub5Stage3 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario7Sub6Stage1 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario7Sub6Stage2 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario7Sub6Stage3 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario7Sub7Stage1 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario7Sub7Stage2 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario7Sub7Stage3 = new List<BaseMonster>();
    public List<BaseMonster> PossibleEnemiesScenario7Sub7Stage4 = new List<BaseMonster>();


    ////////////////////////////////////////////////////////////////////////
    /// ////////////////////////////////////////////////////////////////////////
    ///  ////////////////////////////////////////////////////////////////////////
    ///   ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    /// ////////////////////////////////////////////////////////////////////////
    ///  ////////////////////////////////////////////////////////////////////////
    ///   ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    /// ////////////////////////////////////////////////////////////////////////
    ///  ////////////////////////////////////////////////////////////////////////
    ///   ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////


    public List<Vector3> EnemyLocationsS1 = new List<Vector3>();    //{1,2,3,4,5,1,2,3,4}
    public List<Vector3> EnemyLocationsS2 = new List<Vector3>();
    public List<Vector3> EnemyLocationsS3 = new List<Vector3>();
    public List<Vector3> EnemyLocationsS4 = new List<Vector3>();
    public List<Vector3> EnemyLocationsS5 = new List<Vector3>();
    public List<Vector3> EnemyLocationsS6 = new List<Vector3>();
    public List<Vector3> EnemyLocationsS7 = new List<Vector3>();


    ////////////////////////////////////////////////////////////////////////
    /// ////////////////////////////////////////////////////////////////////////
    ///  ////////////////////////////////////////////////////////////////////////
    ///   ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    /// ////////////////////////////////////////////////////////////////////////
    ///  ////////////////////////////////////////////////////////////////////////
    ///   ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    /// ////////////////////////////////////////////////////////////////////////
    ///  ////////////////////////////////////////////////////////////////////////
    ///   ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    ///
    public List<int> LevelsInScenario1 = new List<int>();

    public List<int> LevelsInScenario2 = new List<int>();

    public List<int> LevelsInScenario3 = new List<int>();

    public List<int> LevelsInScenario4 = new List<int>();

    public List<int> LevelsInScenario5 = new List<int>();

    public List<int> LevelsInScenario6 = new List<int>();

    public List<int> LevelsInScenario7 = new List<int>();
    ////////////////////////////////////////////////////////////////////////
    /// ////////////////////////////////////////////////////////////////////////
    ///  ////////////////////////////////////////////////////////////////////////
    ///   ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    /// ////////////////////////////////////////////////////////////////////////
    ///  ////////////////////////////////////////////////////////////////////////
    ///   ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    /// ////////////////////////////////////////////////////////////////////////
    ///  ////////////////////////////////////////////////////////////////////////
    ///   ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    ///

    public List<GameObject> OwnedEquipment = new List<GameObject>();

    //  ////////////////////////////////////////////////////////////////////////
    ///   ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    /// ////////////////////////////////////////////////////////////////////////
    ///  ////////////////////////////////////////////////////////////////////////
    ///   ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    /// ////////////////////////////////////////////////////////////////////////
    ///  ////////////////////////////////////////////////////////////////////////
    ///   ////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////
    ///


    //reward list
    private void Awake()
    {

        GameObject x = GameObject.FindGameObjectWithTag("LocationData");
        DontDestroyOnLoad(this.gameObject);
        UnityEngine.Debug.Log("awake for locdata");

        UnityEngine.Debug.Log("list length" + BattleDataList.Count);


        

    }



    public List<BaseMonster> ChooseEnemies(int Numberofenemies, List<BaseMonster> possibleEnemies)
    {
        

        List<BaseMonster> returnEnemies = new List<BaseMonster>();

        for (int i = 0; i < Numberofenemies; i++)
        {

            int a = UnityEngine.Random.Range(0, possibleEnemies.Count);

             returnEnemies.Add(possibleEnemies[a]) ;
        }

        return returnEnemies;
    }
    private void Update()
    {
        BattleDataListCopy = BattleDataListS;
        MoneyCopy = Money;
    }

    public void FillBattleDataList(string Scenario, int Sub, string Difficulty, int level)
    {
        UnityEngine.Debug.Log("starting fill");


        int i = 0;  //for loop variable 
        i = StageNumforForLoop(Scenario, Sub);

        UnityEngine.Debug.Log("i = " + i);

        //fill with info
        for (int x = 0; x < i; x++)
        {
            UnityEngine.Debug.Log("x = " + x);
            //get List of enemies
            // Debug.Log("# enemies is " + ReturnNumMons(Scenario, Sub, x));
            // Debug.Log("    first possible enemy in list is  " + ReturnPossibleEnemies(Scenario, Sub, x)[0]);

            xyz = new BattleData1(ReturnBackGroundImage(Scenario), ChooseEnemies(ReturnNumMons(Scenario, Sub, x), ReturnPossibleEnemies(Scenario, Sub, x)), ReturnLocations(Scenario), level);
           UnityEngine.Debug.Log("battle data xyz created");

            //BattleDataList.Add(xyz);sp
            BattleDataListS.Add(xyz);


            UnityEngine.Debug.Log("added "+ BattleDataListS.Count);

            // BattleDataInstance.EnemyList = abc;
            //BattleInstance1.EnemyList = abc;



            //get List of Location 


            //BattleDataInstance.EnemylocationListofSpawningEnemies = ReturnLocations(Scenario);
            //BattleInstance1.EnemylocationListofSpawningEnemies = ReturnLocations(Scenario);



        }

        //pass into static list

    }


    public List<Vector3> ReturnLocations(string Scenario)
    {
        List<Vector3> returnL = new List<Vector3>();

        if (Scenario == "Scenario 1")
        {
            returnL = EnemyLocationsS1;
        }
        else if (Scenario == "Scenario 2")
        {
            returnL = EnemyLocationsS2;
        }
        else if (Scenario == "Scenario 3")
        {
            returnL = EnemyLocationsS3;
        }
        else if (Scenario == "Scenario 4")
        {
            returnL = EnemyLocationsS4;
        }
        else if (Scenario == "Scenario 5")
        {
            returnL = EnemyLocationsS5;
        }
        else if (Scenario == "Scenario 6")
        {
            returnL = EnemyLocationsS6;
        }
        else if (Scenario == "Scenario 7")
        {
            returnL = EnemyLocationsS7;
        }
        return returnL;
    }
    public int StageNumforForLoop(string Scenario, int Sub)
    {
        int returnNum = 0;
        if (Scenario == "Scenario 1")
        {
            returnNum = NumberofStages1[Sub];
        }
        else if (Scenario == "Scenario 2")
        {
            returnNum = NumberofStages2[Sub];
        }
        else if (Scenario == "Scenario 3")
        {
            returnNum = NumberofStages3[Sub];
        }
        else if (Scenario == "Scenario 4")
        {
            returnNum = NumberofStages4[Sub];
        }
        else if (Scenario == "Scenario 5")
        {
            returnNum = NumberofStages5[Sub];
        }
        else if (Scenario == "Scenario 6")
        {
            returnNum = NumberofStages6[Sub];
        }
        else if (Scenario == "Scenario 7")
        {
            returnNum = NumberofStages7[Sub];
        }
        return returnNum;
    }
    public int LevelsForSubScenario(string Scenario, int Sub)
    {
        int returnNum = 0;
        if (Scenario == "Scenario 1")
        {
            if (Sub == 1)
                returnNum = LevelsInScenario1[0];
            if (Sub == 2)
                returnNum = LevelsInScenario1[1];
            if (Sub == 3)
                returnNum = LevelsInScenario1[2];
            if (Sub == 4)
                returnNum = LevelsInScenario1[3];
            if (Sub == 5)
                returnNum = LevelsInScenario1[4];
            if (Sub == 6)
                returnNum = LevelsInScenario1[5];
            if (Sub == 7)
                returnNum = LevelsInScenario1[6];
        }
        else if (Scenario == "Scenario 2")
        {
            if (Sub == 1)
                returnNum = LevelsInScenario2[0];
            if (Sub == 2)
                returnNum = LevelsInScenario2[1];
            if (Sub == 3)
                returnNum = LevelsInScenario2[2];
            if (Sub == 4)
                returnNum = LevelsInScenario2[3];
            if (Sub == 5)
                returnNum = LevelsInScenario2[4];
            if (Sub == 6)
                returnNum = LevelsInScenario2[5];
            if (Sub == 7)
                returnNum = LevelsInScenario2[6];
        }
        else if (Scenario == "Scenario 3")
        {
            if (Sub == 1)
                returnNum = LevelsInScenario3[0];
            if (Sub == 2)
                returnNum = LevelsInScenario3[1];
            if (Sub == 3)
                returnNum = LevelsInScenario3[2];
            if (Sub == 4)
                returnNum = LevelsInScenario3[3];
            if (Sub == 5)
                returnNum = LevelsInScenario3[4];
            if (Sub == 6)
                returnNum = LevelsInScenario3[5];
            if (Sub == 7)
                returnNum = LevelsInScenario3[6];
        }
        else if (Scenario == "Scenario 4")
        {
            if (Sub == 1)
                returnNum = LevelsInScenario4[0];
            if (Sub == 2)
                returnNum = LevelsInScenario4[1];
            if (Sub == 3)
                returnNum = LevelsInScenario4[2];
            if (Sub == 4)
                returnNum = LevelsInScenario4[3];
            if (Sub == 5)
                returnNum = LevelsInScenario4[4];
            if (Sub == 6)
                returnNum = LevelsInScenario4[5];
            if (Sub == 7)
                returnNum = LevelsInScenario4[6];
        }
        else if (Scenario == "Scenario 5")
        {
            if (Sub == 1)
                returnNum = LevelsInScenario5[0];
            if (Sub == 2)
                returnNum = LevelsInScenario5[1];
            if (Sub == 3)
                returnNum = LevelsInScenario5[2];
            if (Sub == 4)
                returnNum = LevelsInScenario5[3];
            if (Sub == 5)
                returnNum = LevelsInScenario5[4];
            if (Sub == 6)
                returnNum = LevelsInScenario5[5];
            if (Sub == 7)
                returnNum = LevelsInScenario5[6];
        }
        else if (Scenario == "Scenario 6")
        {
            if (Sub == 1)
                returnNum = LevelsInScenario6[0];
            if (Sub == 2)
                returnNum = LevelsInScenario6[1];
            if (Sub == 3)
                returnNum = LevelsInScenario6[2];
            if (Sub == 4)
                returnNum = LevelsInScenario6[3];
            if (Sub == 5)
                returnNum = LevelsInScenario6[4];
            if (Sub == 6)
                returnNum = LevelsInScenario6[5];
            if (Sub == 7)
                returnNum = LevelsInScenario6[6];
        }
        else if (Scenario == "Scenario 7")
        {
            if (Sub == 1)
                returnNum = LevelsInScenario7[0];
            if (Sub == 2)
                returnNum = LevelsInScenario7[1];
            if (Sub == 3)
                returnNum = LevelsInScenario7[2];
            if (Sub == 4)
                returnNum = LevelsInScenario7[3];
            if (Sub == 5)
                returnNum = LevelsInScenario7[4];
            if (Sub == 6)
                returnNum = LevelsInScenario7[5];
            if (Sub == 7)
                returnNum = LevelsInScenario7[6];
        }
        return returnNum;
    }
    public Sprite ReturnBackGroundImage(string Scenario)
    {
        Sprite backgroundreturn = BackGroundImage[0];
        if (Scenario == "Scenario 1")
        {
            backgroundreturn = BackGroundImage[0];
        }
        else if (Scenario == "Scenario 2")
        {
            backgroundreturn = BackGroundImage[1];
        }
        else if (Scenario == "Scenario 3")
        {
            backgroundreturn = BackGroundImage[2];
        }
        else if (Scenario == "Scenario 4")
        {
            backgroundreturn = BackGroundImage[3];
        }
        else if (Scenario == "Scenario 5")
        {
            backgroundreturn = BackGroundImage[4];
        }
        else if (Scenario == "Scenario 6")
        {
            backgroundreturn  = BackGroundImage[5];
        }
        else if (Scenario == "Scenario 7")
        {
            backgroundreturn = BackGroundImage[6];
        }
        return backgroundreturn;
    }
    public int ReturnNumMons(string Scenario, int Sub, int i)   //i for for loop of stages
    {
        int returnnum = 0;
        if (Scenario == "Scenario 1")
        {
            if (Sub == 1)
                returnnum = MonstersInScenario1Sub1[i];
            if (Sub == 2)
                returnnum = MonstersInScenario1Sub2[i];
            if (Sub == 3)
                returnnum = MonstersInScenario1Sub3[i];
            if (Sub == 4)
                returnnum = MonstersInScenario1Sub4[i];
            if (Sub == 5)
                returnnum = MonstersInScenario1Sub5[i];
            if (Sub == 6)
                returnnum = MonstersInScenario1Sub6[i];
            if (Sub == 7)
                returnnum = MonstersInScenario1Sub7[i];
        }
        else if (Scenario == "Scenario 2")
        {
            if (Sub == 1)
                returnnum = MonstersInScenario2Sub1[i];
            if (Sub == 2)
                returnnum = MonstersInScenario2Sub2[i];
            if (Sub == 3)
                returnnum = MonstersInScenario2Sub3[i];
            if (Sub == 4)
                returnnum = MonstersInScenario2Sub4[i];
            if (Sub == 5)
                returnnum = MonstersInScenario2Sub5[i];
            if (Sub == 6)
                returnnum = MonstersInScenario2Sub6[i];
            if (Sub == 7)
                returnnum = MonstersInScenario2Sub7[i];
        }
        else if (Scenario == "Scenario 3")
        {
            if (Sub == 1)
                returnnum = MonstersInScenario3Sub1[i];
            if (Sub == 2)
                returnnum = MonstersInScenario3Sub2[i];
            if (Sub == 3)
                returnnum = MonstersInScenario3Sub3[i];
            if (Sub == 4)
                returnnum = MonstersInScenario3Sub4[i];
            if (Sub == 5)
                returnnum = MonstersInScenario3Sub5[i];
            if (Sub == 6)
                returnnum = MonstersInScenario3Sub6[i];
            if (Sub == 7)
                returnnum = MonstersInScenario3Sub7[i];
        }
        else if (Scenario == "Scenario 4")
        {
            if (Sub == 1)
                returnnum = MonstersInScenario4Sub1[i];
            if (Sub == 2)
                returnnum = MonstersInScenario4Sub2[i];
            if (Sub == 3)
                returnnum = MonstersInScenario4Sub3[i];
            if (Sub == 4)
                returnnum = MonstersInScenario4Sub4[i];
            if (Sub == 5)
                returnnum = MonstersInScenario4Sub5[i];
            if (Sub == 6)
                returnnum = MonstersInScenario4Sub6[i];
            if (Sub == 7)
                returnnum = MonstersInScenario4Sub7[i];
        }
        else if (Scenario == "Scenario 5")
        {
            if (Sub == 1)
                returnnum = MonstersInScenario5Sub1[i];
            if (Sub == 2)
                returnnum = MonstersInScenario5Sub2[i];
            if (Sub == 3)
                returnnum = MonstersInScenario5Sub3[i];
            if (Sub == 4)
                returnnum = MonstersInScenario5Sub4[i];
            if (Sub == 5)
                returnnum = MonstersInScenario5Sub5[i];
            if (Sub == 6)
                returnnum = MonstersInScenario5Sub6[i];
            if (Sub == 7)
                returnnum = MonstersInScenario5Sub7[i];
        }
        else if (Scenario == "Scenario 6")
        {
            if (Sub == 1)
                returnnum = MonstersInScenario6Sub1[i];
            if (Sub == 2)
                returnnum = MonstersInScenario6Sub2[i];
            if (Sub == 3)
                returnnum = MonstersInScenario6Sub3[i];
            if (Sub == 4)
                returnnum = MonstersInScenario6Sub4[i];
            if (Sub == 5)
                returnnum = MonstersInScenario6Sub5[i];
            if (Sub == 6)
                returnnum = MonstersInScenario6Sub6[i];
            if (Sub == 7)
                returnnum = MonstersInScenario6Sub7[i];
        }
        else if (Scenario == "Scenario 7")
        {
            if (Sub == 1)
                returnnum = MonstersInScenario7Sub1[i];
            if (Sub == 2)
                returnnum = MonstersInScenario7Sub2[i];
            if (Sub == 3)
                returnnum = MonstersInScenario7Sub3[i];
            if (Sub == 4)
                returnnum = MonstersInScenario7Sub4[i];
            if (Sub == 5)
                returnnum = MonstersInScenario7Sub5[i];
            if (Sub == 6)
                returnnum = MonstersInScenario7Sub6[i];
            if (Sub == 7)
                returnnum = MonstersInScenario7Sub7[i];
        }
        return returnnum;
    }
    public List<BaseMonster> ReturnPossibleEnemies(string Scenario, int Sub, int i)   //i for for loop of stages
    {
        i += 1;
        //Debug.Log(Scenario + "  sub is " + Sub + "  i is " + i);
        List<BaseMonster> returnL = new List<BaseMonster>();
        if (Scenario == "Scenario 1")
        {
            if (Sub == 1)
            {
                if (i == 1)
                    returnL = PossibleEnemiesScenario1Sub1Stage1;
                if (i == 2)
                    returnL = PossibleEnemiesScenario1Sub1Stage2;
                if (i == 3)
                    returnL = PossibleEnemiesScenario1Sub1Stage3;

            }
            if (Sub == 2)
            {
                if (i == 1)
                    returnL = PossibleEnemiesScenario1Sub2Stage1;
                if (i == 2)
                    returnL = PossibleEnemiesScenario1Sub2Stage2;
                if (i == 3)
                    returnL = PossibleEnemiesScenario1Sub2Stage3;

            }
            if (Sub == 3)
            {
                if (i == 1)
                    returnL = PossibleEnemiesScenario1Sub3Stage1;
                if (i == 2)
                    returnL = PossibleEnemiesScenario1Sub3Stage2;
                if (i == 3)
                    returnL = PossibleEnemiesScenario1Sub3Stage3;

            }
            if (Sub == 4)
            {
                if (i == 1)
                    returnL = PossibleEnemiesScenario1Sub4Stage1;
                if (i == 2)
                    returnL = PossibleEnemiesScenario1Sub4Stage2;
                if (i == 3)
                    returnL = PossibleEnemiesScenario1Sub4Stage3;

            }
            if (Sub == 5)
            {
                if (i == 1)
                    returnL = PossibleEnemiesScenario1Sub5Stage1;
                if (i == 2)
                    returnL = PossibleEnemiesScenario1Sub5Stage2;
                if (i == 3)
                    returnL = PossibleEnemiesScenario1Sub5Stage3;

            }
            if (Sub == 6)
            {
                if (i == 1)
                    returnL = PossibleEnemiesScenario1Sub6Stage1;
                if (i == 2)
                    returnL = PossibleEnemiesScenario1Sub6Stage2;
                if (i == 3)
                    returnL = PossibleEnemiesScenario1Sub6Stage3;

            }
            if (Sub == 7)
            {
                if (i == 1)
                    returnL = PossibleEnemiesScenario1Sub7Stage1;
                if (i == 2)
                    returnL = PossibleEnemiesScenario1Sub7Stage2;
                if (i == 3)
                    returnL = PossibleEnemiesScenario1Sub7Stage3;
                if (i == 4)
                    returnL = PossibleEnemiesScenario1Sub7Stage4;

            }
        }
        else if (Scenario == "Scenario 2")
        {
            if (Sub == 1)
            {
                if (i == 1)
                    returnL = PossibleEnemiesScenario2Sub1Stage1;
                if (i == 2)
                    returnL = PossibleEnemiesScenario2Sub1Stage2;
                if (i == 3)
                    returnL = PossibleEnemiesScenario2Sub1Stage3;

            }
            if (Sub == 2)
            {
                if (i == 1)
                    returnL = PossibleEnemiesScenario2Sub2Stage1;
                if (i == 2)
                    returnL = PossibleEnemiesScenario2Sub2Stage2;
                if (i == 3)
                    returnL = PossibleEnemiesScenario2Sub2Stage3;

            }
            if (Sub == 3)
            {
                if (i == 1)
                    returnL = PossibleEnemiesScenario2Sub3Stage1;
                if (i == 2)
                    returnL = PossibleEnemiesScenario2Sub3Stage2;
                if (i == 3)
                    returnL = PossibleEnemiesScenario2Sub3Stage3;

            }
            if (Sub == 4)
            {
                if (i == 1)
                    returnL = PossibleEnemiesScenario2Sub4Stage1;
                if (i == 2)
                    returnL = PossibleEnemiesScenario2Sub4Stage2;
                if (i == 3)
                    returnL = PossibleEnemiesScenario2Sub4Stage3;

            }
            if (Sub == 5)
            {
                if (i == 1)
                    returnL = PossibleEnemiesScenario2Sub5Stage1;
                if (i == 2)
                    returnL = PossibleEnemiesScenario2Sub5Stage2;
                if (i == 3)
                    returnL = PossibleEnemiesScenario2Sub5Stage3;

            }
            if (Sub == 6)
            {
                if (i == 1)
                    returnL = PossibleEnemiesScenario2Sub6Stage1;
                if (i == 2)
                    returnL = PossibleEnemiesScenario2Sub6Stage2;
                if (i == 3)
                    returnL = PossibleEnemiesScenario2Sub6Stage3;

            }
            if (Sub == 7)
            {
                if (i == 1)
                    returnL = PossibleEnemiesScenario2Sub7Stage1;
                if (i == 2)
                    returnL = PossibleEnemiesScenario2Sub7Stage2;
                if (i == 3)
                    returnL = PossibleEnemiesScenario2Sub7Stage3;
                if (i == 4)
                    returnL = PossibleEnemiesScenario2Sub7Stage4;

            }
        }
        else if (Scenario == "Scenario 3")
        {
            if (Sub == 1)
            {
                if (i == 1)
                    returnL = PossibleEnemiesScenario3Sub1Stage1;
                if (i == 2)
                    returnL = PossibleEnemiesScenario3Sub1Stage2;
                if (i == 3)
                    returnL = PossibleEnemiesScenario3Sub1Stage3;

            }
            if (Sub == 2)
            {
                if (i == 1)
                    returnL = PossibleEnemiesScenario3Sub2Stage1;
                if (i == 2)
                    returnL = PossibleEnemiesScenario3Sub2Stage2;
                if (i == 3)
                    returnL = PossibleEnemiesScenario3Sub2Stage3;

            }
            if (Sub == 3)
            {
                if (i == 1)
                    returnL = PossibleEnemiesScenario3Sub3Stage1;
                if (i == 2)
                    returnL = PossibleEnemiesScenario3Sub3Stage2;
                if (i == 3)
                    returnL = PossibleEnemiesScenario3Sub3Stage3;

            }
            if (Sub == 4)
            {
                if (i == 1)
                    returnL = PossibleEnemiesScenario3Sub4Stage1;
                if (i == 2)
                    returnL = PossibleEnemiesScenario3Sub4Stage2;
                if (i == 3)
                    returnL = PossibleEnemiesScenario3Sub4Stage3;

            }
            if (Sub == 5)
            {
                if (i == 1)
                    returnL = PossibleEnemiesScenario3Sub5Stage1;
                if (i == 2)
                    returnL = PossibleEnemiesScenario3Sub5Stage2;
                if (i == 3)
                    returnL = PossibleEnemiesScenario3Sub5Stage3;

            }
            if (Sub == 6)
            {
                if (i == 1)
                    returnL = PossibleEnemiesScenario3Sub6Stage1;
                if (i == 2)
                    returnL = PossibleEnemiesScenario3Sub6Stage2;
                if (i == 3)
                    returnL = PossibleEnemiesScenario3Sub6Stage3;

            }
            if (Sub == 7)
            {
                if (i == 1)
                    returnL = PossibleEnemiesScenario3Sub7Stage1;
                if (i == 2)
                    returnL = PossibleEnemiesScenario3Sub7Stage2;
                if (i == 3)
                    returnL = PossibleEnemiesScenario3Sub7Stage3;
                if (i == 4)
                    returnL = PossibleEnemiesScenario3Sub7Stage4;

            }
        }
        else if (Scenario == "Scenario 4")
        {
            if (Sub == 1)
            {
                if (i == 1)
                    returnL = PossibleEnemiesScenario4Sub1Stage1;
                if (i == 2)
                    returnL = PossibleEnemiesScenario4Sub1Stage2;
                if (i == 3)
                    returnL = PossibleEnemiesScenario4Sub1Stage3;

            }
            if (Sub == 2)
            {
                if (i == 1)
                    returnL = PossibleEnemiesScenario4Sub2Stage1;
                if (i == 2)
                    returnL = PossibleEnemiesScenario4Sub2Stage2;
                if (i == 3)
                    returnL = PossibleEnemiesScenario4Sub2Stage3;

            }
            if (Sub == 3)
            {
                if (i == 1)
                    returnL = PossibleEnemiesScenario4Sub3Stage1;
                if (i == 2)
                    returnL = PossibleEnemiesScenario4Sub3Stage2;
                if (i == 3)
                    returnL = PossibleEnemiesScenario4Sub3Stage3;

            }
            if (Sub == 4)
            {
                if (i == 1)
                    returnL = PossibleEnemiesScenario4Sub4Stage1;
                if (i == 2)
                    returnL = PossibleEnemiesScenario4Sub4Stage2;
                if (i == 3)
                    returnL = PossibleEnemiesScenario4Sub4Stage3;

            }
            if (Sub == 5)
            {
                if (i == 1)
                    returnL = PossibleEnemiesScenario4Sub5Stage1;
                if (i == 2)
                    returnL = PossibleEnemiesScenario4Sub5Stage2;
                if (i == 3)
                    returnL = PossibleEnemiesScenario4Sub5Stage3;

            }
            if (Sub == 6)
            {
                if (i == 1)
                    returnL = PossibleEnemiesScenario4Sub6Stage1;
                if (i == 2)
                    returnL = PossibleEnemiesScenario4Sub6Stage2;
                if (i == 3)
                    returnL = PossibleEnemiesScenario4Sub6Stage3;

            }
            if (Sub == 7)
            {
                if (i == 1)
                    returnL = PossibleEnemiesScenario4Sub7Stage1;
                if (i == 2)
                    returnL = PossibleEnemiesScenario4Sub7Stage2;
                if (i == 3)
                    returnL = PossibleEnemiesScenario4Sub7Stage3;
                if (i == 4)
                    returnL = PossibleEnemiesScenario4Sub7Stage4;

            }
        }
        else if (Scenario == "Scenario 5")
        {
            if (Sub == 1)
            {
                if (i == 1)
                    returnL = PossibleEnemiesScenario5Sub1Stage1;
                if (i == 2)
                    returnL = PossibleEnemiesScenario5Sub1Stage2;
                if (i == 3)
                    returnL = PossibleEnemiesScenario5Sub1Stage3;

            }
            if (Sub == 2)
            {
                if (i == 1)
                    returnL = PossibleEnemiesScenario5Sub2Stage1;
                if (i == 2)
                    returnL = PossibleEnemiesScenario5Sub2Stage2;
                if (i == 3)
                    returnL = PossibleEnemiesScenario5Sub2Stage3;

            }
            if (Sub == 3)
            {
                if (i == 1)
                    returnL = PossibleEnemiesScenario5Sub3Stage1;
                if (i == 2)
                    returnL = PossibleEnemiesScenario5Sub3Stage2;
                if (i == 3)
                    returnL = PossibleEnemiesScenario5Sub3Stage3;

            }
            if (Sub == 4)
            {
                if (i == 1)
                    returnL = PossibleEnemiesScenario5Sub4Stage1;
                if (i == 2)
                    returnL = PossibleEnemiesScenario5Sub4Stage2;
                if (i == 3)
                    returnL = PossibleEnemiesScenario5Sub4Stage3;

            }
            if (Sub == 5)
            {
                if (i == 1)
                    returnL = PossibleEnemiesScenario5Sub5Stage1;
                if (i == 2)
                    returnL = PossibleEnemiesScenario5Sub5Stage2;
                if (i == 3)
                    returnL = PossibleEnemiesScenario5Sub5Stage3;

            }
            if (Sub == 6)
            {
                if (i == 1)
                    returnL = PossibleEnemiesScenario5Sub6Stage1;
                if (i == 2)
                    returnL = PossibleEnemiesScenario5Sub6Stage2;
                if (i == 3)
                    returnL = PossibleEnemiesScenario5Sub6Stage3;

            }
            if (Sub == 7)
            {
                if (i == 1)
                    returnL = PossibleEnemiesScenario5Sub7Stage1;
                if (i == 2)
                    returnL = PossibleEnemiesScenario5Sub7Stage2;
                if (i == 3)
                    returnL = PossibleEnemiesScenario5Sub7Stage3;
                if (i == 4)
                    returnL = PossibleEnemiesScenario5Sub7Stage4;

            }
        }
        else if (Scenario == "Scenario 6")
        {
            if (Sub == 1)
            {
                if (i == 1)
                    returnL = PossibleEnemiesScenario6Sub1Stage1;
                if (i == 2)
                    returnL = PossibleEnemiesScenario6Sub1Stage2;
                if (i == 3)
                    returnL = PossibleEnemiesScenario6Sub1Stage3;

            }
            if (Sub == 2)
            {
                if (i == 1)
                    returnL = PossibleEnemiesScenario6Sub2Stage1;
                if (i == 2)
                    returnL = PossibleEnemiesScenario6Sub2Stage2;
                if (i == 3)
                    returnL = PossibleEnemiesScenario6Sub2Stage3;

            }
            if (Sub == 3)
            {
                if (i == 1)
                    returnL = PossibleEnemiesScenario6Sub3Stage1;
                if (i == 2)
                    returnL = PossibleEnemiesScenario6Sub3Stage2;
                if (i == 3)
                    returnL = PossibleEnemiesScenario6Sub3Stage3;

            }
            if (Sub == 4)
            {
                if (i == 1)
                    returnL = PossibleEnemiesScenario6Sub4Stage1;
                if (i == 2)
                    returnL = PossibleEnemiesScenario6Sub4Stage2;
                if (i == 3)
                    returnL = PossibleEnemiesScenario6Sub4Stage3;

            }
            if (Sub == 5)
            {
                if (i == 1)
                    returnL = PossibleEnemiesScenario6Sub5Stage1;
                if (i == 2)
                    returnL = PossibleEnemiesScenario6Sub5Stage2;
                if (i == 3)
                    returnL = PossibleEnemiesScenario6Sub5Stage3;

            }
            if (Sub == 6)
            {
                if (i == 1)
                    returnL = PossibleEnemiesScenario6Sub6Stage1;
                if (i == 2)
                    returnL = PossibleEnemiesScenario6Sub6Stage2;
                if (i == 3)
                    returnL = PossibleEnemiesScenario6Sub6Stage3;

            }
            if (Sub == 7)
            {
                if (i == 1)
                    returnL = PossibleEnemiesScenario6Sub7Stage1;
                if (i == 2)
                    returnL = PossibleEnemiesScenario6Sub7Stage2;
                if (i == 3)
                    returnL = PossibleEnemiesScenario6Sub7Stage3;
                if (i == 4)
                    returnL = PossibleEnemiesScenario6Sub7Stage4;

            }
        }
        else if (Scenario == "Scenario 7")
        {
            if (Sub == 1)
            {
                if (i == 1)
                    returnL = PossibleEnemiesScenario7Sub1Stage1;
                if (i == 2)
                    returnL = PossibleEnemiesScenario7Sub1Stage2;
                if (i == 3)
                    returnL = PossibleEnemiesScenario7Sub1Stage3;

            }
            if (Sub == 2)
            {
                if (i == 1)
                    returnL = PossibleEnemiesScenario7Sub2Stage1;
                if (i == 2)
                    returnL = PossibleEnemiesScenario7Sub2Stage2;
                if (i == 3)
                    returnL = PossibleEnemiesScenario7Sub2Stage3;

            }
            if (Sub == 3)
            {
                if (i == 1)
                    returnL = PossibleEnemiesScenario7Sub3Stage1;
                if (i == 2)
                    returnL = PossibleEnemiesScenario7Sub3Stage2;
                if (i == 3)
                    returnL = PossibleEnemiesScenario7Sub3Stage3;

            }
            if (Sub == 4)
            {
                if (i == 1)
                    returnL = PossibleEnemiesScenario7Sub4Stage1;
                if (i == 2)
                    returnL = PossibleEnemiesScenario7Sub4Stage2;
                if (i == 3)
                    returnL = PossibleEnemiesScenario7Sub4Stage3;

            }
            if (Sub == 5)
            {
                if (i == 1)
                    returnL = PossibleEnemiesScenario7Sub5Stage1;
                if (i == 2)
                    returnL = PossibleEnemiesScenario7Sub5Stage2;
                if (i == 3)
                    returnL = PossibleEnemiesScenario7Sub5Stage3;

            }
            if (Sub == 6)
            {
                if (i == 1)
                    returnL = PossibleEnemiesScenario7Sub6Stage1;
                if (i == 2)
                    returnL = PossibleEnemiesScenario7Sub6Stage2;
                if (i == 3)
                    returnL = PossibleEnemiesScenario7Sub6Stage3;

            }
            if (Sub == 7)
            {
                if (i == 1)
                    returnL = PossibleEnemiesScenario7Sub7Stage1;
                if (i == 2)
                    returnL = PossibleEnemiesScenario7Sub7Stage2;
                if (i == 3)
                    returnL = PossibleEnemiesScenario7Sub7Stage3;
                if (i == 4)
                    returnL = PossibleEnemiesScenario7Sub7Stage4;

            }
            return returnL;
        }
        return returnL;
    }


    public void Spawn(BattleData1 x)
    {
        NewSetUp SetUpScript = GameObject.Find("Main Camera").GetComponent<NewSetUp>();

        if (!KeepThis)
            KeepThis = true;

        List<BaseMonster> EnemiestoSpawn = x.EnemyList;
        List<Vector3> EnemyLocations = x.EnemylocationListofSpawningEnemies;
        Sprite BG = x.Background;


        SpriteRenderer BGspriteRenderer = GameObject.Find("Background").GetComponent<SpriteRenderer>();
        BGspriteRenderer.sprite = BG;
        int LocationIndexH = 0;

        for (int i = 0; i < EnemiestoSpawn.Count; i++)
        {
            GameObject CurrentEnemy = SetUpScript.SpawnEnemies(EnemiestoSpawn[i], EnemyLocations[i]);
            CurrentEnemy.name = CurrentEnemy.GetComponent<EnemyStateMachine>().enemy.name + CurrentEnemy.GetComponent<EnemyStateMachine>().enemy.MonsterIndex + "Enemy";
            CurrentEnemy.GetComponent<EnemyStateMachine>().enemy.level = x.level;
        }


        Vector3 heroLoc = new Vector3(0,0,0);
        foreach(BaseMonster hero in ChosenHeroList)
        {
            if (LocationIndexH == 0)
                heroLoc =  new Vector3 (-6, -3, 0);
            if (LocationIndexH == 1)
                heroLoc = new Vector3(-5, -1, 0);
            if (LocationIndexH == 2)
                heroLoc = new Vector3(-8, 1, 0);
            if (LocationIndexH == 3)
                heroLoc = new Vector3(-4, 2, 0);

            GameObject NewHeroGO = SetUpScript.SpawnHeroes(hero, heroLoc);
            NewHeroGO.name = NewHeroGO.GetComponent<HeroStateMachine>().hero.Name + NewHeroGO.GetComponent<HeroStateMachine>().hero.MonsterIndex;

            if (duringBattle)
            {
                if (LocationIndexH == 0)
                {
                    NewHeroGO.GetComponent<HeroStateMachine>().hero.HpCurrent = CurrentHp[0];
                    NewHeroGO.GetComponent<HeroStateMachine>().Cd2 = CD2[0];
                    NewHeroGO.GetComponent<HeroStateMachine>().Cd3 = CD3[0];
                    NewHeroGO.GetComponent<HeroStateMachine>().Cd4 = CD4[0];
                }
                if (LocationIndexH == 1)
                {
                    NewHeroGO.GetComponent<HeroStateMachine>().hero.HpCurrent = CurrentHp[1];
                    NewHeroGO.GetComponent<HeroStateMachine>().Cd2 = CD2[1];
                    NewHeroGO.GetComponent<HeroStateMachine>().Cd3 = CD3[1];
                    NewHeroGO.GetComponent<HeroStateMachine>().Cd4 = CD4[1];
                }
                if (LocationIndexH == 2)
                {
                    NewHeroGO.GetComponent<HeroStateMachine>().hero.HpCurrent = CurrentHp[2];
                    NewHeroGO.GetComponent<HeroStateMachine>().Cd2 = CD2[2];
                    NewHeroGO.GetComponent<HeroStateMachine>().Cd3 = CD3[2];
                    NewHeroGO.GetComponent<HeroStateMachine>().Cd4 = CD4[2];
                }
                if (LocationIndexH == 3)
                {
                    NewHeroGO.GetComponent<HeroStateMachine>().hero.HpCurrent = CurrentHp[3];
                    NewHeroGO.GetComponent<HeroStateMachine>().Cd2 = CD2[3];
                    NewHeroGO.GetComponent<HeroStateMachine>().Cd3 = CD3[3];
                    NewHeroGO.GetComponent<HeroStateMachine>().Cd4 = CD4[3];
                }
            }
            
            LocationIndexH++;
        }

        List<GameObject> enemyListNames = new List<GameObject>();
        enemyListNames.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        List<GameObject> heroListNames = new List<GameObject>();
      //  heroListNames.AddRange(GameObject.FindGameObjectsWithTag("Hero"));
     //   heroListNames.AddRange(GameObject.FindGameObjectsWithTag("DeadHero"));
     //   if(heroListNames.Count>1)
     //       DifferentNames(heroListNames);
        if (enemyListNames.Count > 1)
            DifferentNames(enemyListNames);


    }

    public void DifferentNames(List<GameObject> x)      
    {
        int a = 0;
        for (int i = 0; i < x.Count ; i++)
        {
            for (int j = 0; j < x.Count; j++)
            {
                if(x[i].name == x[j].name && i != j)
                {
                    x[i].name += a;
                    a++;
                }
            }
        }
    }
}
