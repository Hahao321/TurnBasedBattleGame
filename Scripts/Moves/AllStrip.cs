using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using PathCreation;
[System.Serializable]


public class AllStrip : BaseMonster
{
    public AllStrip()
    {

       
        Name = "test1" ;
    HpMax = 100;
    HpBase = 100;
    HpCurrent = 100;


     AtkMax = 10;
     AtkBase = 10;
     AtkCurrent =10 ;

        DefMax = 10;
        DefBase = 10;
        DefCurrent = 10;

        SpeedMax = 10;
        SpeedBase = 10;
        SpeedCurrent = 10;

        Resistance = 10;
        Accuracy = 10;

        Critrate = 15;
      Critdamage = 1.5f;


        MonsterType = Type.DARK;



        rarity = Rarity.FIVESTAR;

        //


}
}
