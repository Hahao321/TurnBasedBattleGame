using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSMonsterButton : MonoBehaviour
{
    public BaseMonster Monster;
    public MSManager msManager;

    private void Awake()
    {
        msManager = GameObject.Find("Main Camera").GetComponent<MSManager>();
    }

    public void MonsterButtonClicked()
    {
        msManager.CurrentMonster = Monster;
        msManager.PerformUpdate(Monster, msManager.PanelIndex);

    }

}
