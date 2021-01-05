using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllCleanse : BaseAttack
{
    public AllCleanse()
    {
        NameAttack = "Cleaner";
        CDAttack = 3;
        SelectingEnemy = false;
        MoveToTarget = false;
        animationTimeAfterEffect = 0.5f;
        animationTimeBeforeEffect = 0.5f;
        Effect[8] = true;
        //1
        IsItEntirePartyCleanse = true;
    }
}
