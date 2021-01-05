using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MSSortSubButton : MonoBehaviour
{
    public MSManager msManager;
    public MSSortImage sortImage;
    public TextMeshProUGUI Sorttext;
    public void SortSubButtonClicked()
    {
        List<BaseMonster> ReturnMonsters = new List<BaseMonster>();

        if(Sorttext.text == "Element")
        {
            //fire water grass light dark

            foreach(BaseMonster x in msManager.Monsters)
            {
                if (x.MonsterType == BaseMonster.Type.FIRE)
                    ReturnMonsters.Add(x);
            }
            foreach (BaseMonster x in msManager.Monsters)
            {
                if (x.MonsterType == BaseMonster.Type.WATER)
                    ReturnMonsters.Add(x);
            }
            foreach (BaseMonster x in msManager.Monsters)
            {
                if (x.MonsterType == BaseMonster.Type.GRASS)
                    ReturnMonsters.Add(x);
            }
            foreach (BaseMonster x in msManager.Monsters)
            {
                if (x.MonsterType == BaseMonster.Type.LIGHT)
                    ReturnMonsters.Add(x);
            }
            foreach (BaseMonster x in msManager.Monsters)
            {
                if (x.MonsterType == BaseMonster.Type.DARK)
                    ReturnMonsters.Add(x);
            }

        }
        if (Sorttext.text == "Stars")
        {
            ReturnMonsters = msManager.Monsters;
        }
        if (Sorttext.text == "Level")
        {
            for (int i = 40; i > 0; i--)
            {
                foreach(BaseMonster x in msManager.Monsters)
                {
                    if (x.level == i)
                        ReturnMonsters.Add(x);
                }
            }
        }


        msManager.SortedMonsters = ReturnMonsters;

        msManager.UpdateMonsterContent();
        sortImage.MoveSortImageUp();
    }
}
