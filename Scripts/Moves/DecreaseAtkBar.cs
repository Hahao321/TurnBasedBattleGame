using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseAtkBar : BaseAttack
{ 
    public DecreaseAtkBar()
    {
        NameAttack = "30% atkBar dec";
        CDAttack = 3;
        SelectingEnemy = true;
        MoveToTarget = true;
        animationTimeAfterEffect = 0.5f;
        animationTimeBeforeEffect = 0.5f;
        Effect[6] = true;

        IsItEntirePartyAtkBarReduction = false;
        AtkBarReductionPercent = 50;
    }
}
