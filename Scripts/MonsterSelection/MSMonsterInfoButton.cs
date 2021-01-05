using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MSMonsterInfoButton : MonoBehaviour
{

    public GameObject Monster1;
    public BaseMonster Monster;

    private void Update()
    {
        Monster = GameObject.Find("MonsterPanelCanvas").transform.Find("MonsterOptionsPanel").GetComponent<MSOptionsPanel>().Monster;
        FillHeroStatsInfo(Monster);
    }

    public void TurnOnOptions()
    {
        GameObject.Find("MonsterPanelCanvas").transform.Find("HeroStatsPanel").gameObject.SetActive(true);
        GameObject.Find("MonsterPanelCanvas").transform.Find("SkillsPanel").gameObject.SetActive(false);

    }
    public void FillHeroStatsInfo(BaseMonster Monster)
    {
        BaseMonster x = Monster;
        GameObject StatPanel = GameObject.Find("MonsterPanelCanvas").transform.Find("HeroStatsPanel").gameObject;

        StatPanel.transform.Find("HeroSprite").GetComponent<Image>().sprite = x.MonsterSprite;
        StatPanel.transform.Find("NameT").GetComponent<TextMeshProUGUI>().text = x.Name;
        string lvl = "" + x.level;
        StatPanel.transform.Find("LevelT").GetComponent<TextMeshProUGUI>().text = lvl;
        //element symbol
    }
    public void TurnOffOptions()
    {
        GameObject.Find("MonsterPanelCanvas").transform.Find("HeroStatsPanel").gameObject.SetActive(false);

    }
}
