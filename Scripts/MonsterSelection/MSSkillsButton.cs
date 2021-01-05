using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MSSkillsButton : MonoBehaviour
{
    public GameObject Monster1;
    public BaseMonster Monster;
    private void Update()
    {
        Monster = GameObject.Find("MonsterPanelCanvas").transform.Find("MonsterOptionsPanel").GetComponent<MSOptionsPanel>().Monster;
        FillInSkillsPanelInfo(Monster);
    }

    public void TurnOnSkillPanel()
    {
        GameObject.Find("MonsterPanelCanvas").transform.Find("SkillsPanel").gameObject.SetActive(true);
        GameObject.Find("MonsterPanelCanvas").transform.Find("HeroStatsPanel").gameObject.SetActive(false);
    }
    private void FillInSkillsPanelInfo(BaseMonster Monster)
    {
        BaseMonster x = Monster;
        GameObject.Find("MonsterPanelCanvas").transform.Find("SkillsPanel").transform.Find("HeroSprite").GetComponent<Image>().sprite = x.MonsterSprite;
        //set element image
        GameObject.Find("MonsterPanelCanvas").transform.Find("SkillsPanel").transform.Find("NameT").GetComponent<TextMeshProUGUI>().text = x.Name;

        GameObject a = GameObject.Find("MonsterPanelCanvas").transform.Find("SkillsPanel").transform.Find("SkillB1").gameObject;
        GameObject b = GameObject.Find("MonsterPanelCanvas").transform.Find("SkillsPanel").transform.Find("SkillB2").gameObject;
        GameObject c = GameObject.Find("MonsterPanelCanvas").transform.Find("SkillsPanel").transform.Find("SkillB3").gameObject;
        GameObject d = GameObject.Find("MonsterPanelCanvas").transform.Find("SkillsPanel").transform.Find("SkillB4").gameObject;
        a.GetComponent<Image>().sprite = x.Attack1;
        b.GetComponent<Image>().sprite = x.Attack2;
        c.GetComponent<Image>().sprite = x.Attack3;
        d.GetComponent<Image>().sprite = x.Attack4;

        a.GetComponent<MSSingleSkillButton>().SkillText = x.attacks[0].DescriptionAttack;
        b.GetComponent<MSSingleSkillButton>().SkillText = x.attacks[1].DescriptionAttack;
        c.GetComponent<MSSingleSkillButton>().SkillText = x.attacks[2].DescriptionAttack;
        d.GetComponent<MSSingleSkillButton>().SkillText = x.attacks[3].DescriptionAttack;
    }

    public void CloseSkillsPanel()
    {
        GameObject.Find("MonsterPanelCanvas").transform.Find("SkillsPanel").gameObject.SetActive(false);
    }
}
