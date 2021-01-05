using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : BaseAttack
{
    public BasicAttack()
    {
        MoveToTarget = true;
        animationTimeAfterEffect = 0.3f;
        NameAttack = "Basic Attack";
        DescriptionAttack = "Its a fucking basic attack";
        CDAttack  = 0; //implement
        SelectingEnemy = true;

        Effect[0] = true;     //0-DMG, 1-Heal, 2-Buff, 3-Debuff, 4-Revive 5-Stun, 6-AtkBarReduction, 7-AtkBarBoost

        DmgScalings[0] = 3;                      //0-Atk, 1-Hp, 2-Spd, 3-Def       value is scaling to multiply with stat
    }
}
