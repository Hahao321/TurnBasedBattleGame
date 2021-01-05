using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeBuffAll : BaseAttack
{
    public DeBuffAll()
    {
        NameAttack = "DebufAll";
        CDAttack = 3;
        SelectingEnemy = true;
        MoveToTarget = true;
        animationTimeAfterEffect = 0.5f;
        animationTimeBeforeEffect = 0.5f;
        Effect[3] = true;
        //1
        StatToDeBuff[0] = true;        //0-Atk, 1-Spd, 2-Def
        StatToDeBuff[1] = true;        //0-Atk, 1-Spd, 2-Def
        StatToDeBuff[2] = true;        //0-Atk, 1-Spd, 2-Def
        DeBuffTurns = 3;
        IsItEntirePartyDeBuff = true;
}
}
