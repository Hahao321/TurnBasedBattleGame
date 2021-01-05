using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicHealFlat : BasicAttack
{
    public BasicHealFlat()
    {
        NameAttack ="BasicFlatHeal";
        DescriptionAttack = "Duh";
        CDAttack = 2;
        SelectingEnemy = false;
        MoveToTarget = false;
        animationTimeAfterEffect = 0.7f;
        animationTimeBeforeEffect = 1f;
        Effect[0] = false;
        Effect[1] =true;     //0-DMG, 1-Heal, 2-Buff, 3-Debuff, 4-Revive 5-Stun, 6-AtkBarReduction, 7-AtkBarBoost

        IsItPercentHeal = false;
        HealPercent = 0f;
        IsItEntirePartyHeal = false;
        HealScalings[0] = 1;

    }
}
