using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class TurnInformation 
{
    public string Attacker;   //attacker name
    public GameObject AttackerGameObject;
    public GameObject AttackersTarget;
    //which attack is formed
    public BaseAttack ChosenAttack;
    public string type; //hero or enemy
}
