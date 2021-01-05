using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllBuff : BaseAttack
{
   public AllBuff()
    {
        NameAttack = "AllBuffs3Turns";
        DescriptionAttack = "duh";
        CDAttack = 3;
        SelectingEnemy = false;

        animationTimeAfterEffect = 0.5f;
        animationTimeBeforeEffect = 0.5f;

        Effect[2] = true;//buff
        StatToBuff[0] = true;
        StatToBuff[1] = true;
        StatToBuff[2] = true;
        BuffTurns = 3;
        IsItEntirePartyBuff = true;
}
}
