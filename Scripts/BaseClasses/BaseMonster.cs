using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using System.Runtime.InteropServices;
using UnityEngine.PlayerLoop;

[System.Serializable]
public class BaseMonster : MonoBehaviour
{

    public int EquipIndex1; 
    public int EquipIndex2;
    public int EquipIndex3;
    public int EquipIndex4;
    public int MonsterIndex;    //0 when monster deleted

    public int level; //1-50;
    public Sprite MonsterSprite;

    public string Name;

    public float HpMax;     //in bsm with buffs     //hp base + all buffs
    public float HpBase;    //hp gained + hp base
    public float HpCurrent;
    public float HpLevel;   //hp gained per lvl
    public float HpBaseStat; //hp base


    public float AtkMax;
    public float AtkBase;
    public float AtkCurrent;
    public float AtkLevel;
    public float AtkBaseStat;


    public float DefMax;
    public float DefBase;
    public float DefCurrent;
    public float DefLevel;
    public float DefBaseStat;

    public float SpeedMax;
    public float SpeedBase;
    public float SpeedCurrent;


    public float Resistance = 0;
    public float Accuracy;


    public float Critrate = 0;
    public float Critdamage = 0;


    public int turns=0;
    public int atkBuffTurns = 0;
    public int spdBuffTurns = 0;
    public int defBuffTurns = 0;
    public int recBuffTurns = 0;
    public int atkDeBuffTurns = 0;
    public int spdDeBuffTurns = 0;
    public int defDeBuffTurns = 0;
    public int dotDeBuffTurns = 0;



    public enum Type        //enum for distint types 
    {
        GRASS, FIRE, WATER, LIGHT, DARK
    }
    public Type MonsterType;

    public int stars;

    public enum Rarity          //big for gachas
    {
        FIVESTAR, FOURSTAR, THREESTAR, TWOSTAR, ONESTAR
    }
    public Rarity rarity;



    public List<BaseAttack> attacks = new List<BaseAttack>();

   
    public Sprite Attack1;
    public Sprite Attack2;
    public Sprite Attack3;
    public Sprite Attack4;


    public void UpdateStats()
    {


        Debug.Log("stats");
        HpBase = HpBaseStat + HpLevel * level;
        AtkBase = AtkBaseStat + AtkLevel * level;
        DefBase = DefBaseStat + DefLevel * level;

        int HpPercentEquip = 0;
        int HpFlatEquip = 0;
        int AtkPercentEquip = 0;
        int AtkFlatEquip = 0;
        int DefPercentEquip = 0;
        int DefFlatEquip = 0;
        int SpeedFlatEquip = 0;

        int CritRateEquip = 0;
        int CritDamageEquip = 0;
        int AccuracyEquip = 0;
        int ResistanceEquip = 0;

        string[] Sets = { "", "", "", "" };


        //E1
        if (Equipment1 != null) //1 is hp
        {
            Sets[0] = Equipment1.Set;

            HpPercentEquip += Equipment1.BaseStatValue;

            if(Equipment1.level >= 3)
            {
                if(Equipment1.SS1stat == "Hp" && Equipment1.IsSS1Flat)
                {
                    HpFlatEquip += Equipment1.SS1;
                }
                if (Equipment1.SS1stat == "Hp" && !Equipment1.IsSS1Flat)
                {
                    HpPercentEquip += Equipment1.SS1;
                }

                if (Equipment1.SS1stat == "Atk" && Equipment1.IsSS1Flat)
                {
                    AtkFlatEquip += Equipment1.SS1;
                }
                if (Equipment1.SS1stat == "Atk" && !Equipment1.IsSS1Flat)
                {
                    AtkPercentEquip += Equipment1.SS1;
                }

                if (Equipment1.SS1stat == "Def" && Equipment1.IsSS1Flat)
                {
                    DefFlatEquip += Equipment1.SS1;
                }
                if (Equipment1.SS1stat == "Def" && !Equipment1.IsSS1Flat)
                {
                    DefPercentEquip += Equipment1.SS1;
                }
                if (Equipment1.SS1stat == "CritRate" && !Equipment1.IsSS1Flat)
                {
                    CritRateEquip += Equipment1.SS1;
                }
                if (Equipment1.SS1stat == "CritDamage" && !Equipment1.IsSS1Flat)
                {
                    CritDamageEquip += Equipment1.SS1;
                }
                if (Equipment1.SS1stat == "Acc" && !Equipment1.IsSS1Flat)
                {
                    AccuracyEquip += Equipment1.SS1;
                }
                if (Equipment1.SS1stat == "Res" && !Equipment1.IsSS1Flat)
                {
                    ResistanceEquip += Equipment1.SS1;
                }
                if (Equipment1.SS1stat == "Spd" && !Equipment1.IsSS1Flat)
                {
                    SpeedFlatEquip += Equipment1.SS1;
                }

            }
            if (Equipment1.level >= 6)
            {
                if (Equipment1.SS2stat == "Spd" && !Equipment1.IsSS2Flat)
                {
                    SpeedFlatEquip += Equipment1.SS2;
                }
                if (Equipment1.SS2stat == "Hp" && Equipment1.IsSS2Flat)
                {
                    HpFlatEquip += Equipment1.SS2;
                }
                if (Equipment1.SS2stat == "Hp" && !Equipment1.IsSS2Flat)
                {
                    HpPercentEquip += Equipment1.SS2;
                }

                if (Equipment1.SS2stat == "Atk" && Equipment1.IsSS2Flat)
                {
                    AtkFlatEquip += Equipment1.SS2;
                }
                if (Equipment1.SS2stat == "Atk" && !Equipment1.IsSS2Flat)
                {
                    AtkPercentEquip += Equipment1.SS2;
                }

                if (Equipment1.SS2stat == "Def" && Equipment1.IsSS2Flat)
                {
                    DefFlatEquip += Equipment1.SS2;
                }
                if (Equipment1.SS2stat == "Def" && !Equipment1.IsSS2Flat)
                {
                    DefPercentEquip += Equipment1.SS2;
                }
                if (Equipment1.SS2stat == "CritRate" && !Equipment1.IsSS2Flat)
                {
                    CritRateEquip += Equipment1.SS2;
                }
                if (Equipment1.SS2stat == "CritDamage" && !Equipment1.IsSS2Flat)
                {
                    CritDamageEquip += Equipment1.SS2;
                }
                if (Equipment1.SS2stat == "Acc" && !Equipment1.IsSS2Flat)
                {
                    AccuracyEquip += Equipment1.SS2;
                }
                if (Equipment1.SS2stat == "Res" && !Equipment1.IsSS2Flat)
                {
                    ResistanceEquip += Equipment1.SS2;
                }

            }
            if (Equipment1.level >= 10)
            {
                if (Equipment1.SS3stat == "Spd" && !Equipment1.IsSS3Flat)
                {
                    SpeedFlatEquip += Equipment1.SS3;
                }
                if (Equipment1.SS3stat == "Hp" && Equipment1.IsSS3Flat)
                {
                    HpFlatEquip += Equipment1.SS3;
                }
                if (Equipment1.SS3stat == "Hp" && !Equipment1.IsSS3Flat)
                {
                    HpPercentEquip += Equipment1.SS3;
                }

                if (Equipment1.SS3stat == "Atk" && Equipment1.IsSS3Flat)
                {
                    AtkFlatEquip += Equipment1.SS3;
                }
                if (Equipment1.SS3stat == "Atk" && !Equipment1.IsSS3Flat)
                {
                    AtkPercentEquip += Equipment1.SS3;
                }

                if (Equipment1.SS3stat == "Def" && Equipment1.IsSS3Flat)
                {
                    DefFlatEquip += Equipment1.SS3;
                }
                if (Equipment1.SS3stat == "Def" && !Equipment1.IsSS3Flat)
                {
                    DefPercentEquip += Equipment1.SS3;
                }
                if (Equipment1.SS3stat == "CritRate" && !Equipment1.IsSS3Flat)
                {
                    CritRateEquip += Equipment1.SS3;
                }
                if (Equipment1.SS3stat == "CritDamage" && !Equipment1.IsSS3Flat)
                {
                    CritDamageEquip += Equipment1.SS3;
                }
                if (Equipment1.SS3stat == "Acc" && !Equipment1.IsSS3Flat)
                {
                    AccuracyEquip += Equipment1.SS3;
                }
                if (Equipment1.SS3stat == "Res" && !Equipment1.IsSS3Flat)
                {
                    ResistanceEquip += Equipment1.SS3;
                }

            }

        }
        if (Equipment2 != null) //1 is hp
        {
            Sets[1] = Equipment2.Set;

            HpPercentEquip += Equipment2.BaseStatValue;

            if (Equipment2.level >= 3)
            {
                if (Equipment2.SS1stat == "Spd" && !Equipment2.IsSS1Flat)
                {
                    SpeedFlatEquip += Equipment2.SS1;
                }
                if (Equipment2.SS1stat == "Hp" && Equipment2.IsSS1Flat)
                {
                    HpFlatEquip += Equipment2.SS1;
                }
                if (Equipment2.SS1stat == "Hp" && !Equipment2.IsSS1Flat)
                {
                    HpPercentEquip += Equipment2.SS1;
                }

                if (Equipment2.SS1stat == "Atk" && Equipment2.IsSS1Flat)
                {
                    AtkFlatEquip += Equipment2.SS1;
                }
                if (Equipment2.SS1stat == "Atk" && !Equipment2.IsSS1Flat)
                {
                    AtkPercentEquip += Equipment2.SS1;
                }

                if (Equipment2.SS1stat == "Def" && Equipment2.IsSS1Flat)
                {
                    DefFlatEquip += Equipment2.SS1;
                }
                if (Equipment2.SS1stat == "Def" && !Equipment2.IsSS1Flat)
                {
                    DefPercentEquip += Equipment2.SS1;
                }
                if (Equipment2.SS1stat == "CritRate" && !Equipment2.IsSS1Flat)
                {
                    CritRateEquip += Equipment2.SS1;
                }
                if (Equipment2.SS1stat == "CritDamage" && !Equipment2.IsSS1Flat)
                {
                    CritDamageEquip += Equipment2.SS1;
                }
                if (Equipment2.SS1stat == "Acc" && !Equipment2.IsSS1Flat)
                {
                    AccuracyEquip += Equipment2.SS1;
                }
                if (Equipment2.SS1stat == "Res" && !Equipment2.IsSS1Flat)
                {
                    ResistanceEquip += Equipment2.SS1;
                }

            }
            if (Equipment2.level >= 6)
            {
                if (Equipment2.SS2stat == "Spd" && !Equipment2.IsSS2Flat)
                {
                    SpeedFlatEquip += Equipment2.SS2;
                }
                if (Equipment2.SS2stat == "Hp" && Equipment2.IsSS2Flat)
                {
                    HpFlatEquip += Equipment2.SS2;
                }
                if (Equipment2.SS2stat == "Hp" && !Equipment2.IsSS2Flat)
                {
                    HpPercentEquip += Equipment2.SS2;
                }

                if (Equipment2.SS2stat == "Atk" && Equipment2.IsSS2Flat)
                {
                    AtkFlatEquip += Equipment2.SS2;
                }
                if (Equipment2.SS2stat == "Atk" && !Equipment2.IsSS2Flat)
                {
                    AtkPercentEquip += Equipment2.SS2;
                }

                if (Equipment2.SS2stat == "Def" && Equipment2.IsSS2Flat)
                {
                    DefFlatEquip += Equipment2.SS2;
                }
                if (Equipment2.SS2stat == "Def" && !Equipment2.IsSS2Flat)
                {
                    DefPercentEquip += Equipment2.SS2;
                }
                if (Equipment2.SS2stat == "CritRate" && !Equipment2.IsSS2Flat)
                {
                    CritRateEquip += Equipment2.SS2;
                }
                if (Equipment2.SS2stat == "CritDamage" && !Equipment2.IsSS2Flat)
                {
                    CritDamageEquip += Equipment2.SS2;
                }
                if (Equipment2.SS2stat == "Acc" && !Equipment2.IsSS2Flat)
                {
                    AccuracyEquip += Equipment2.SS2;
                }
                if (Equipment2.SS2stat == "Res" && !Equipment2.IsSS2Flat)
                {
                    ResistanceEquip += Equipment2.SS2;
                }

            }
            if (Equipment2.level >= 10)
            {
                if (Equipment2.SS3stat == "Spd" && !Equipment2.IsSS3Flat)
                {
                    SpeedFlatEquip += Equipment2.SS3;
                }
                if (Equipment2.SS3stat == "Hp" && Equipment2.IsSS3Flat)
                {
                    HpFlatEquip += Equipment2.SS3;
                }
                if (Equipment2.SS3stat == "Hp" && !Equipment2.IsSS3Flat)
                {
                    HpPercentEquip += Equipment2.SS3;
                }

                if (Equipment2.SS3stat == "Atk" && Equipment2.IsSS3Flat)
                {
                    AtkFlatEquip += Equipment2.SS3;
                }
                if (Equipment2.SS3stat == "Atk" && !Equipment2.IsSS3Flat)
                {
                    AtkPercentEquip += Equipment2.SS3;
                }

                if (Equipment2.SS3stat == "Def" && Equipment2.IsSS3Flat)
                {
                    DefFlatEquip += Equipment2.SS3;
                }
                if (Equipment2.SS3stat == "Def" && !Equipment2.IsSS3Flat)
                {
                    DefPercentEquip += Equipment2.SS3;
                }
                if (Equipment2.SS3stat == "CritRate" && !Equipment2.IsSS3Flat)
                {
                    CritRateEquip += Equipment2.SS3;
                }
                if (Equipment2.SS3stat == "CritDamage" && !Equipment2.IsSS3Flat)
                {
                    CritDamageEquip += Equipment2.SS3;
                }
                if (Equipment2.SS3stat == "Acc" && !Equipment2.IsSS3Flat)
                {
                    AccuracyEquip += Equipment2.SS3;
                }
                if (Equipment2.SS3stat == "Res" && !Equipment2.IsSS3Flat)
                {
                    ResistanceEquip += Equipment2.SS3;
                }

            }

        }
        if (Equipment3 != null) //1 is hp
        {
            Sets[2] = Equipment3.Set;

            HpPercentEquip += Equipment3.BaseStatValue;

            if (Equipment3.level >= 3)
            {
                if (Equipment3.SS1stat == "Spd" && !Equipment3.IsSS1Flat)
                {
                    SpeedFlatEquip += Equipment3.SS1;
                }
                if (Equipment3.SS1stat == "Hp" && Equipment3.IsSS1Flat)
                {
                    HpFlatEquip += Equipment3.SS1;
                }
                if (Equipment3.SS1stat == "Hp" && !Equipment3.IsSS1Flat)
                {
                    HpPercentEquip += Equipment3.SS1;
                }

                if (Equipment3.SS1stat == "Atk" && Equipment3.IsSS1Flat)
                {
                    AtkFlatEquip += Equipment3.SS1;
                }
                if (Equipment3.SS1stat == "Atk" && !Equipment3.IsSS1Flat)
                {
                    AtkPercentEquip += Equipment3.SS1;
                }

                if (Equipment3.SS1stat == "Def" && Equipment3.IsSS1Flat)
                {
                    DefFlatEquip += Equipment3.SS1;
                }
                if (Equipment3.SS1stat == "Def" && !Equipment3.IsSS1Flat)
                {
                    DefPercentEquip += Equipment3.SS1;
                }
                if (Equipment3.SS1stat == "CritRate" && !Equipment3.IsSS1Flat)
                {
                    CritRateEquip += Equipment3.SS1;
                }
                if (Equipment3.SS1stat == "CritDamage" && !Equipment3.IsSS1Flat)
                {
                    CritDamageEquip += Equipment3.SS1;
                }
                if (Equipment3.SS1stat == "Acc" && !Equipment3.IsSS1Flat)
                {
                    AccuracyEquip += Equipment3.SS1;
                }
                if (Equipment3.SS1stat == "Res" && !Equipment3.IsSS1Flat)
                {
                    ResistanceEquip += Equipment3.SS1;
                }

            }
            if (Equipment3.level >= 6)
            {
                if (Equipment3.SS2stat == "Spd" && !Equipment3.IsSS2Flat)
                {
                    SpeedFlatEquip += Equipment3.SS2;
                }
                if (Equipment3.SS2stat == "Hp" && Equipment3.IsSS2Flat)
                {
                    HpFlatEquip += Equipment3.SS2;
                }
                if (Equipment3.SS2stat == "Hp" && !Equipment3.IsSS2Flat)
                {
                    HpPercentEquip += Equipment3.SS2;
                }

                if (Equipment3.SS2stat == "Atk" && Equipment3.IsSS2Flat)
                {
                    AtkFlatEquip += Equipment3.SS2;
                }
                if (Equipment3.SS2stat == "Atk" && !Equipment3.IsSS2Flat)
                {
                    AtkPercentEquip += Equipment3.SS2;
                }

                if (Equipment3.SS2stat == "Def" && Equipment3.IsSS2Flat)
                {
                    DefFlatEquip += Equipment3.SS2;
                }
                if (Equipment3.SS2stat == "Def" && !Equipment3.IsSS2Flat)
                {
                    DefPercentEquip += Equipment3.SS2;
                }
                if (Equipment3.SS2stat == "CritRate" && !Equipment3.IsSS2Flat)
                {
                    CritRateEquip += Equipment3.SS2;
                }
                if (Equipment3.SS2stat == "CritDamage" && !Equipment3.IsSS2Flat)
                {
                    CritDamageEquip += Equipment3.SS2;
                }
                if (Equipment3.SS2stat == "Acc" && !Equipment3.IsSS2Flat)
                {
                    AccuracyEquip += Equipment3.SS2;
                }
                if (Equipment3.SS2stat == "Res" && !Equipment3.IsSS2Flat)
                {
                    ResistanceEquip += Equipment3.SS2;
                }

            }
            if (Equipment3.level >= 10)
            {
                if (Equipment3.SS3stat == "Spd" && !Equipment3.IsSS3Flat)
                {
                    SpeedFlatEquip += Equipment1.SS3;
                }
                if (Equipment3.SS3stat == "Hp" && Equipment3.IsSS3Flat)
                {
                    HpFlatEquip += Equipment3.SS3;
                }
                if (Equipment3.SS3stat == "Hp" && !Equipment3.IsSS3Flat)
                {
                    HpPercentEquip += Equipment3.SS3;
                }

                if (Equipment3.SS3stat == "Atk" && Equipment3.IsSS3Flat)
                {
                    AtkFlatEquip += Equipment3.SS3;
                }
                if (Equipment3.SS3stat == "Atk" && !Equipment3.IsSS3Flat)
                {
                    AtkPercentEquip += Equipment3.SS3;
                }

                if (Equipment3.SS3stat == "Def" && Equipment3.IsSS3Flat)
                {
                    DefFlatEquip += Equipment3.SS3;
                }
                if (Equipment3.SS3stat == "Def" && !Equipment3.IsSS3Flat)
                {
                    DefPercentEquip += Equipment3.SS3;
                }
                if (Equipment3.SS3stat == "CritRate" && !Equipment3.IsSS3Flat)
                {
                    CritRateEquip += Equipment3.SS3;
                }
                if (Equipment3.SS3stat == "CritDamage" && !Equipment3.IsSS3Flat)
                {
                    CritDamageEquip += Equipment3.SS3;
                }
                if (Equipment3.SS3stat == "Acc" && !Equipment3.IsSS3Flat)
                {
                    AccuracyEquip += Equipment3.SS3;
                }
                if (Equipment3.SS3stat == "Res" && !Equipment3.IsSS3Flat)
                {
                    ResistanceEquip += Equipment3.SS3;
                }

            }

        }
        if (Equipment4 != null) //1 is hp
        {
            Sets[3] = Equipment4.Set;

            HpPercentEquip += Equipment4.BaseStatValue;

            if (Equipment4.level >= 3)
            {
                if (Equipment4.SS1stat == "Spd" && !Equipment4.IsSS1Flat)
                {
                    SpeedFlatEquip += Equipment4.SS1;
                }
                if (Equipment4.SS1stat == "Hp" && Equipment4.IsSS1Flat)
                {
                    HpFlatEquip += Equipment4.SS1;
                }
                if (Equipment4.SS1stat == "Hp" && !Equipment4.IsSS1Flat)
                {
                    HpPercentEquip += Equipment4.SS1;
                }

                if (Equipment4.SS1stat == "Atk" && Equipment4.IsSS1Flat)
                {
                    AtkFlatEquip += Equipment4.SS1;
                }
                if (Equipment4.SS1stat == "Atk" && !Equipment4.IsSS1Flat)
                {
                    AtkPercentEquip += Equipment4.SS1;
                }

                if (Equipment4.SS1stat == "Def" && Equipment4.IsSS1Flat)
                {
                    DefFlatEquip += Equipment4.SS1;
                }
                if (Equipment4.SS1stat == "Def" && !Equipment4.IsSS1Flat)
                {
                    DefPercentEquip += Equipment4.SS1;
                }
                if (Equipment4.SS1stat == "CritRate" && !Equipment4.IsSS1Flat)
                {
                    CritRateEquip += Equipment4.SS1;
                }
                if (Equipment4.SS1stat == "CritDamage" && !Equipment4.IsSS1Flat)
                {
                    CritDamageEquip += Equipment4.SS1;
                }
                if (Equipment4.SS1stat == "Acc" && !Equipment4.IsSS1Flat)
                {
                    AccuracyEquip += Equipment4.SS1;
                }
                if (Equipment4.SS1stat == "Res" && !Equipment4.IsSS1Flat)
                {
                    ResistanceEquip += Equipment4.SS1;
                }

            }
            if (Equipment4.level >= 6)
            {
                if (Equipment4.SS2stat == "Spd" && !Equipment4.IsSS2Flat)
                {
                    SpeedFlatEquip += Equipment4.SS2;
                }
                if (Equipment4.SS2stat == "Hp" && Equipment4.IsSS2Flat)
                {
                    HpFlatEquip += Equipment4.SS2;
                }
                if (Equipment4.SS2stat == "Hp" && !Equipment4.IsSS2Flat)
                {
                    HpPercentEquip += Equipment4.SS2;
                }

                if (Equipment4.SS2stat == "Atk" && Equipment4.IsSS2Flat)
                {
                    AtkFlatEquip += Equipment4.SS2;
                }
                if (Equipment4.SS2stat == "Atk" && !Equipment4.IsSS2Flat)
                {
                    AtkPercentEquip += Equipment4.SS2;
                }

                if (Equipment4.SS2stat == "Def" && Equipment4.IsSS2Flat)
                {
                    DefFlatEquip += Equipment4.SS2;
                }
                if (Equipment4.SS2stat == "Def" && !Equipment4.IsSS2Flat)
                {
                    DefPercentEquip += Equipment4.SS2;
                }
                if (Equipment4.SS2stat == "CritRate" && !Equipment4.IsSS2Flat)
                {
                    CritRateEquip += Equipment4.SS2;
                }
                if (Equipment4.SS2stat == "CritDamage" && !Equipment4.IsSS2Flat)
                {
                    CritDamageEquip += Equipment4.SS2;
                }
                if (Equipment4.SS2stat == "Acc" && !Equipment4.IsSS2Flat)
                {
                    AccuracyEquip += Equipment4.SS2;
                }
                if (Equipment4.SS2stat == "Res" && !Equipment4.IsSS2Flat)
                {
                    ResistanceEquip += Equipment4.SS2;
                }

            }
            if (Equipment4.level >= 10)
            {
                if (Equipment4.SS3stat == "Spd" && !Equipment4.IsSS3Flat)
                {
                    SpeedFlatEquip += Equipment4.SS3;
                }
                if (Equipment4.SS3stat == "Hp" && Equipment4.IsSS3Flat)
                {
                    HpFlatEquip += Equipment4.SS3;
                }
                if (Equipment4.SS3stat == "Hp" && !Equipment4.IsSS3Flat)
                {
                    HpPercentEquip += Equipment4.SS3;
                }

                if (Equipment4.SS3stat == "Atk" && Equipment4.IsSS3Flat)
                {
                    AtkFlatEquip += Equipment4.SS3;
                }
                if (Equipment4.SS3stat == "Atk" && !Equipment4.IsSS3Flat)
                {
                    AtkPercentEquip += Equipment4.SS3;
                }

                if (Equipment4.SS3stat == "Def" && Equipment4.IsSS3Flat)
                {
                    DefFlatEquip += Equipment4.SS3;
                }
                if (Equipment4.SS3stat == "Def" && !Equipment4.IsSS3Flat)
                {
                    DefPercentEquip += Equipment4.SS3;
                }
                if (Equipment4.SS3stat == "CritRate" && !Equipment4.IsSS3Flat)
                {
                    CritRateEquip += Equipment4.SS3;
                }
                if (Equipment4.SS3stat == "CritDamage" && !Equipment4.IsSS3Flat)
                {
                    CritDamageEquip += Equipment4.SS3;
                }
                if (Equipment4.SS3stat == "Acc" && !Equipment4.IsSS3Flat)
                {
                    AccuracyEquip += Equipment4.SS3;
                }
                if (Equipment4.SS3stat == "Res" && !Equipment4.IsSS3Flat)
                {
                    ResistanceEquip += Equipment4.SS3;
                }

            }

        }


        HpCurrent = HpBase * (1+HpPercentEquip / 100) + HpFlatEquip;
        AtkCurrent = AtkBase * (1+AtkPercentEquip / 100) + AtkFlatEquip;
        DefCurrent = DefBase * (1+DefPercentEquip / 100) + DefFlatEquip;
        //SpeedCurrent = SpeedBase + SpeedFlatEquip;
        Resistance = 15 + ResistanceEquip;
        Accuracy = AccuracyEquip;
        Critrate = 15 + CritRateEquip;
        Critdamage = (50 + CritDamageEquip)/100;



        HpMax = HpCurrent;
        AtkMax = HpCurrent;
        DefMax = HpCurrent;

        Debug.Log(SpeedCurrent + "is spd");
    }

    //Equipment

    public BaseEqupment Equipment1;
    public BaseEqupment Equipment2;
    public BaseEqupment Equipment3;
    public BaseEqupment Equipment4;

    public void ResetTurns()
    {
        turns = 0;
        atkBuffTurns = 0;
        spdBuffTurns = 0;
        defBuffTurns = 0;
        recBuffTurns = 0;
        atkDeBuffTurns = 0;
        defDeBuffTurns = 0;
        spdDeBuffTurns = 0;
        dotDeBuffTurns = 0;
    }

}
