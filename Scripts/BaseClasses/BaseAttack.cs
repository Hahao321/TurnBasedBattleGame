using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using PathCreation;
[System.Serializable]


public class BaseAttack : MonoBehaviour
{
    public GameObject Animation;

    
    public bool path;

    public string NameAttack;
    public string DescriptionAttack;  
    public int CDAttack;    //cool down
    public bool SelectingEnemy = true;
    public bool MoveToTarget;

    public float animationTimeAfterEffect = 0.5f;
    public float animationTimeBeforeEffect = 0.5f;

    public bool[] Effect = {false, false, false, false, false, false, false, false, false, false};     //0-DMG, 1-Heal, 2-Buff, 3-Debuff, 4-Revive 5-Stun, 6-AtkBarReduction, 7-AtkBarBoost, 8-Cleanse, 9-Strip

    //0
    public bool IsItPercentDmg = false;
    public bool IsItFlatDmg = false;
    public bool IsItEntirePartyDmg = false;
    public float DmgPercent = 0f;
    public float[] DmgScalings = { 0, 0, 0, 0 };                      //0-Atk, 1-Hp, 2-Spd, 3-Def       value is scaling to multiply with stat

    //1
    public bool IsItPercentHeal = false;
    public bool IsItFlatHeal = false;
    public float HealPercent = 0f;
    public float[] HealScalings = { 0, 0, 0, 0 };                     //0-Atk, 1-Hp, 2-Spd, 3-Def       value is scaling to multiply with stat
    public bool IsItEntirePartyHeal = false;
    //assuming we never heal more than 1 target but less than everyone

    //2
    public bool[] StatToBuff = {false, false, false, false};         //0-Atk, 1-Spd, 2-Def 3-heal  SET PERCENTAGE
    public int BuffTurns = 0;
    public bool IsItEntirePartyBuff = false;


    //3
    public bool[] StatToDeBuff = { false, false, false, false};         //0-Atk, 1-Spd, 2-Def, 3-Dot  SET PERCENTAGE
    public int DeBuffTurns = 0;
    public bool IsItEntirePartyDeBuff = false;
    //4
    public bool IsItEntirePartyRevive = false;
    public bool IsItPercentReviveHp = false;
    public float PercentHealOnRevive = 0f;
    public float[] ReviveHealScalings = { 0, 0, 0, 0 };                     //0-Atk, 1-Hp, 2-Spd, 3-Def       value is scaling to multiply with stat

    //5
    public bool IsItEntirePartyStun = false;
    public int StunTurns = 0;

    //6
    public bool IsItEntirePartyAtkBarReduction = false;
    public float AtkBarReductionPercent = 0f;                           //for full do 100%

    //7
    public bool IsItEntirePartyAtkBarBoost = false;
    public float AtkBarBoostPercent = 0f;                           //for full do 100%

    //8
    public bool IsItEntirePartyCleanse = false;
    public bool CleanseAll = false;
    public int DeBuffstoCleanse = 0;

    //9
    public bool IsItEntirePartyStrip = false;
    public int BuffsToStrip = 0;
    public bool StripAll = false;
}
