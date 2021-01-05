using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotAll : BaseAttack
{
    public DotAll()
    {
        NameAttack = "DotAll";
        CDAttack = 3;
        SelectingEnemy = true;
        MoveToTarget = true;
        animationTimeAfterEffect = 0.5f;
        animationTimeBeforeEffect = 0.5f;
        Effect[3] = true; 
        StatToDeBuff[3] = true; //3 is dot
        DeBuffTurns = 3;
        IsItEntirePartyDeBuff = true;
    }
}

