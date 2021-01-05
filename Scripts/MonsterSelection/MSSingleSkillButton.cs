using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MSSingleSkillButton : MonoBehaviour
{
    public string SkillText;
   
    private void Update()
    {
        FillSkillDescriptionInfo(SkillText);
    }

    public void TurnOnSkillDescriptionImage()
    {
        GameObject.Find("MonsterPanelCanvas").transform.Find("SkillsPanel").transform.Find("SkillImage").gameObject.SetActive(true);
        GameObject.Find("MonsterPanelCanvas").transform.Find("SkillsPanel").transform.Find("SkillImage").transform.Find("SkillText").gameObject.SetActive(true);
        if (this.SkillText == GameObject.Find("MonsterPanelCanvas").transform.Find("SkillsPanel").transform.Find("SkillB1").GetComponent<MSSingleSkillButton>().SkillText)
            GameObject.Find("MonsterPanelCanvas").transform.Find("SkillsPanel").transform.Find("SkillImage").GetComponent<MSSkillImage>().SkillIndex = 1;
        else if (this.SkillText == GameObject.Find("MonsterPanelCanvas").transform.Find("SkillsPanel").transform.Find("SkillB2").GetComponent<MSSingleSkillButton>().SkillText)
            GameObject.Find("MonsterPanelCanvas").transform.Find("SkillsPanel").transform.Find("SkillImage").GetComponent<MSSkillImage>().SkillIndex = 2;
       else  if (this.SkillText == GameObject.Find("MonsterPanelCanvas").transform.Find("SkillsPanel").transform.Find("SkillB3").GetComponent<MSSingleSkillButton>().SkillText)
            GameObject.Find("MonsterPanelCanvas").transform.Find("SkillsPanel").transform.Find("SkillImage").GetComponent<MSSkillImage>().SkillIndex = 3;
        else if (this.SkillText == GameObject.Find("MonsterPanelCanvas").transform.Find("SkillsPanel").transform.Find("SkillB4").GetComponent<MSSingleSkillButton>().SkillText)
            GameObject.Find("MonsterPanelCanvas").transform.Find("SkillsPanel").transform.Find("SkillImage").GetComponent<MSSkillImage>().SkillIndex = 4;

    }

    private void FillSkillDescriptionInfo(string SkillD)
    {
        if (GameObject.Find("MonsterPanelCanvas").transform.Find("SkillsPanel").transform.Find("SkillImage").GetComponent<MSSkillImage>().SkillIndex == 1 && this.SkillText == GameObject.Find("MonsterPanelCanvas").transform.Find("SkillsPanel").transform.Find("SkillB1").GetComponent<MSSingleSkillButton>().SkillText)
            GameObject.Find("MonsterPanelCanvas").transform.Find("SkillsPanel").transform.Find("SkillImage").transform.Find("SkillText").GetComponent<TextMeshProUGUI>().text = SkillText;
        else if (GameObject.Find("MonsterPanelCanvas").transform.Find("SkillsPanel").transform.Find("SkillImage").GetComponent<MSSkillImage>().SkillIndex == 2 && this.SkillText == GameObject.Find("MonsterPanelCanvas").transform.Find("SkillsPanel").transform.Find("SkillB2").GetComponent<MSSingleSkillButton>().SkillText)
            GameObject.Find("MonsterPanelCanvas").transform.Find("SkillsPanel").transform.Find("SkillImage").transform.Find("SkillText").GetComponent<TextMeshProUGUI>().text = SkillText;
        else if (GameObject.Find("MonsterPanelCanvas").transform.Find("SkillsPanel").transform.Find("SkillImage").GetComponent<MSSkillImage>().SkillIndex == 3 && this.SkillText == GameObject.Find("MonsterPanelCanvas").transform.Find("SkillsPanel").transform.Find("SkillB3").GetComponent<MSSingleSkillButton>().SkillText)
            GameObject.Find("MonsterPanelCanvas").transform.Find("SkillsPanel").transform.Find("SkillImage").transform.Find("SkillText").GetComponent<TextMeshProUGUI>().text = SkillText;
       else  if (GameObject.Find("MonsterPanelCanvas").transform.Find("SkillsPanel").transform.Find("SkillImage").GetComponent<MSSkillImage>().SkillIndex == 4 && this.SkillText == GameObject.Find("MonsterPanelCanvas").transform.Find("SkillsPanel").transform.Find("SkillB4").GetComponent<MSSingleSkillButton>().SkillText)
            GameObject.Find("MonsterPanelCanvas").transform.Find("SkillsPanel").transform.Find("SkillImage").transform.Find("SkillText").GetComponent<TextMeshProUGUI>().text = SkillText;

    }
}
