using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class NewSetUp : MonoBehaviour
{

    public GameObject HeroBase;
    public GameObject EnemyBase;

    private void Awake()
    {
        /*
        Debug.Log("new set up started, speed should be  "+ GameObject.Find("LocData").GetComponent<BigData>().VisableHeroListm[0].SpeedCurrent);

        GameObject x = GameObject.Find("hellH");
        
        x.AddComponent<BaseMonster>();
        BaseMonster y = x.GetComponent<BaseMonster>();
        y.MonsterSprite = GameObject.Find("LocData").GetComponent<BigData>().VisableHeroListm[0].MonsterSprite;
        y.MonsterIndex = GameObject.Find("LocData").GetComponent<BigData>().VisableHeroListm[0].MonsterIndex;
        y.name = GameObject.Find("LocData").GetComponent<BigData>().VisableHeroListm[0].name;
        y.Name = GameObject.Find("LocData").GetComponent<BigData>().VisableHeroListm[0].Name;

        y.level = GameObject.Find("LocData").GetComponent<BigData>().VisableHeroListm[0].level;
        y.HpLevel = GameObject.Find("LocData").GetComponent<BigData>().VisableHeroListm[0].HpLevel;
        y.HpBaseStat = GameObject.Find("LocData").GetComponent<BigData>().VisableHeroListm[0].HpBaseStat;
        y.AtkLevel = GameObject.Find("LocData").GetComponent<BigData>().VisableHeroListm[0].AtkLevel;
        y.AtkBaseStat = GameObject.Find("LocData").GetComponent<BigData>().VisableHeroListm[0].AtkBaseStat;
        y.DefLevel = GameObject.Find("LocData").GetComponent<BigData>().VisableHeroListm[0].DefLevel;
        y.DefBaseStat = GameObject.Find("LocData").GetComponent<BigData>().VisableHeroListm[0].DefBaseStat;
        y.SpeedCurrent = GameObject.Find("LocData").GetComponent<BigData>().VisableHeroListm[0].SpeedCurrent;
        y.Resistance = GameObject.Find("LocData").GetComponent<BigData>().VisableHeroListm[0].Resistance;
        y.Accuracy = GameObject.Find("LocData").GetComponent<BigData>().VisableHeroListm[0].Accuracy;
        y.Critrate = GameObject.Find("LocData").GetComponent<BigData>().VisableHeroListm[0].Critrate;
        y.Critdamage = GameObject.Find("LocData").GetComponent<BigData>().VisableHeroListm[0].Critdamage;

        y.MonsterType = GameObject.Find("LocData").GetComponent<BigData>().VisableHeroListm[0].MonsterType;
        y.stars = GameObject.Find("LocData").GetComponent<BigData>().VisableHeroListm[0].stars;
        y.rarity = GameObject.Find("LocData").GetComponent<BigData>().VisableHeroListm[0].rarity;
        y.attacks = GameObject.Find("LocData").GetComponent<BigData>().VisableHeroListm[0].attacks;
        y.Attack1 = GameObject.Find("LocData").GetComponent<BigData>().VisableHeroListm[0].Attack1;
        y.Attack2 = GameObject.Find("LocData").GetComponent<BigData>().VisableHeroListm[0].Attack2;
        y.Attack3 = GameObject.Find("LocData").GetComponent<BigData>().VisableHeroListm[0].Attack3;
        y.Attack4 = GameObject.Find("LocData").GetComponent<BigData>().VisableHeroListm[0].Attack4;
        y.SpeedBase = 15;
        y.SpeedMax = 15;
        x.GetComponent<HeroStateMachine>().hero = y;

        Debug.Log("his speed rn at end of new set up is  "+ y.SpeedCurrent);
        */
    }


    public GameObject SpawnHeroes(BaseMonster x, Vector3 location)
    {
        GameObject HeroGo = Instantiate(HeroBase, location, Quaternion.identity);
        HeroGo.AddComponent<BaseMonster>();
        BaseMonster HeroBaseM = HeroGo.GetComponent<BaseMonster>();

        HeroBaseM.MonsterSprite = x.MonsterSprite;
        HeroBaseM.MonsterIndex = x.MonsterIndex;
        HeroBaseM.Name = x.Name;


        HeroBaseM.level = x.level;
        HeroBaseM.HpLevel = x.HpLevel;
        HeroBaseM.HpBaseStat = x.HpBaseStat;
        HeroBaseM.AtkLevel = x.AtkLevel;
        HeroBaseM.AtkBaseStat = x.AtkBaseStat;
        HeroBaseM.DefLevel = x.DefLevel;
        HeroBaseM.DefBaseStat = x.DefBaseStat;
        HeroBaseM.SpeedCurrent = x.SpeedCurrent;
        HeroBaseM.SpeedMax = x.SpeedMax;
        HeroBaseM.SpeedBase = x.SpeedBase;
        HeroBaseM.Resistance = x.Resistance;
        HeroBaseM.Accuracy = x.Accuracy;
        HeroBaseM.Critrate = x.Critrate;
        HeroBaseM.Critdamage = x.Critdamage;

        HeroBaseM.MonsterType = x.MonsterType;
        HeroBaseM.stars = x.stars;
        HeroBaseM.rarity = x.rarity;
        HeroBaseM.attacks = x.attacks;
        HeroBaseM.Attack1 = x.Attack1;
        HeroBaseM.Attack2 = x.Attack2;
        HeroBaseM.Attack3 = x.Attack3;
        HeroBaseM.Attack4 = x.Attack4;

        HeroGo.GetComponent<HeroStateMachine>().hero = HeroBaseM;

        return HeroGo;
    }

    public GameObject SpawnEnemies(BaseMonster x, Vector3 location) //ignore names hero
    {
        GameObject EnemyGo = Instantiate(EnemyBase, location, Quaternion.identity);
        EnemyGo.AddComponent<BaseMonster>();
        BaseMonster EnemyBaseM = EnemyGo.GetComponent<BaseMonster>();

        EnemyBaseM.MonsterSprite = x.MonsterSprite;
        EnemyBaseM.MonsterIndex = x.MonsterIndex;
        EnemyBaseM.Name = x.Name;


        EnemyBaseM.level = x.level;
        EnemyBaseM.HpLevel = x.HpLevel;
        EnemyBaseM.HpBaseStat = x.HpBaseStat;
        EnemyBaseM.AtkLevel = x.AtkLevel;
        EnemyBaseM.AtkBaseStat = x.AtkBaseStat;
        EnemyBaseM.DefLevel = x.DefLevel;
        EnemyBaseM.DefBaseStat = x.DefBaseStat;
        EnemyBaseM.SpeedCurrent = x.SpeedCurrent;
        EnemyBaseM.SpeedMax = x.SpeedMax;
        EnemyBaseM.SpeedBase = x.SpeedBase;
        EnemyBaseM.Resistance = x.Resistance;
        EnemyBaseM.Accuracy = x.Accuracy;
        EnemyBaseM.Critrate = x.Critrate;
        EnemyBaseM.Critdamage = x.Critdamage;

        EnemyBaseM.MonsterType = x.MonsterType;
        EnemyBaseM.stars = x.stars;
        EnemyBaseM.rarity = x.rarity;
        EnemyBaseM.attacks = x.attacks;
        EnemyBaseM.Attack1 = x.Attack1;
        EnemyBaseM.Attack2 = x.Attack2;
        EnemyBaseM.Attack3 = x.Attack3;
        EnemyBaseM.Attack4 = x.Attack4;

        EnemyGo.GetComponent<EnemyStateMachine>().enemy = EnemyBaseM;

        return EnemyGo;
    }


}
