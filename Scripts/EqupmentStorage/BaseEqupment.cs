using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEqupment : MonoBehaviour
{
    public bool IsEquiped = false;
    //public GameObject Monster;
    // public BaseMonster MonsterB;

    public int EquipmentIndex;
    public int MonsterIndex; //0 means no monster on it

    public Sprite EquipmentSprite;
    public int level;
    public string grade;   //Common, Rare, VeryRare, SuperRare, Legend
    public int Stars;
    public int slot;   //1-4

    public string Set; //can be 2/3/4 of anything
    public int SetBonus;

    public int CostInfo;

    public string BaseStat;
    public int BaseStatValue;


    public int SS1;    //stat val
    public int SS2;
    public int SS3;

    public string SS1stat; //atk def hp spd acc res crit cd
    public string SS2stat;
    public string SS3stat;

    public bool IsSS1Flat;
    public bool IsSS2Flat;
    public bool IsSS3Flat;


    public int SellingValue;

    public int calculateCurrentSellingPrice()
    {
        //run calcualation;
        return SellingValue;
    }
    public int CalcualteUpgradePrice()
    {
        //level grade set, do set later
        int reeturnInt = CostInfo * level;

        return reeturnInt;

    }

    //func to calculate upgradecost
    //func to calculate sell cost

    // random range 1.2  percent or bool
    //rand range 1,8 stat
    //rand range stat range
}
