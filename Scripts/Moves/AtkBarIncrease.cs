using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkBarIncrease : BaseAttack
{ 
public AtkBarIncrease()
    {
        NameAttack = "30% atkBar";
        CDAttack = 3;
        SelectingEnemy = false;
        MoveToTarget = false;
        animationTimeAfterEffect = 1f;
        animationTimeBeforeEffect = 0.5f;
        Effect[7] = true;

        IsItEntirePartyAtkBarBoost = false;
        AtkBarBoostPercent = 30f;                    //for full do 100%
}
}
