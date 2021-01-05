using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackButton : MonoBehaviour
{
    public BattleStateMachine BSM;
    public BaseAttack AttackToPerform;
    public void UseAttack()
    {
        BattleStateMachine BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();      //allows us to communicate with BSM

        if (this.name == "Button 1")
        {
            AttackToPerform = BSM.HeroestoManage[0].GetComponent<HeroStateMachine>().hero.attacks[0];
        }
        if (this.name == "Button 2")
        {
            AttackToPerform = BSM.HeroestoManage[0].GetComponent<HeroStateMachine>().hero.attacks[1];
        }
        if (this.name == "Button 3")
        {
            AttackToPerform = BSM.HeroestoManage[0].GetComponent<HeroStateMachine>().hero.attacks[2];
        }
        if (this.name == "Button 4")
        {
            AttackToPerform = BSM.HeroestoManage[0].GetComponent<HeroStateMachine>().hero.attacks[3];
        }
      //  GameObject.Find("BattleManager").GetComponent<BattleStateMachine>().AttackChosen(AttackToPerform);

    }

    public void eawf()
    {
        Debug.Log("rsfg");
    }
}
