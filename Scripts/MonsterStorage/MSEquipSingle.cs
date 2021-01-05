using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MSEquipSingle : MonoBehaviour
{
    public BaseEqupment CurrentEquip;
    public MSMonEqpImage MonsEquipPanel;
    public GameObject HoldPanel;
    public GameObject blank;
    public Button OptionsButton;
    public MSManager msManager;


    


    private void Awake()
    {
        MonsEquipPanel = GameObject.Find("OptionsCanvas").transform.Find("EquipImage").transform.Find("MonsterEquipImage").GetComponent<MSMonEqpImage>();
        blank = GameObject.Find("OptionsCanvas").transform.Find("EquipImage").transform.Find("EquipBlank").gameObject;
        HoldPanel = GameObject.Find("OptionsCanvas").transform.Find("EquipImage").transform.Find("EquipHoldImage").gameObject;
        OptionsButton = GameObject.Find("OptionsCanvas").transform.Find("MenuImage").transform.Find("MenuButton").GetComponent<Button>();
        msManager = GameObject.Find("Main Camera").GetComponent<MSManager>();
    }


   
    public void OnClick()
    {

        HoldPanel.GetComponent<MSEquipHold>().CurrentEquip = CurrentEquip;
        HoldPanel.SetActive(true);
        blank.SetActive(true);
        OptionsButton.interactable = false;
        HoldPanel.GetComponent<MSEquipHold>().EquipSprite.sprite = CurrentEquip.EquipmentSprite;

        if (CurrentEquip.MonsterIndex != 0)
       {
           HoldPanel.GetComponent<MSEquipHold>().monster.color = new Color(HoldPanel.GetComponent<MSEquipHold>().monster.color.r, HoldPanel.GetComponent<MSEquipHold>().monster.color.g, HoldPanel.GetComponent<MSEquipHold>().monster.color.b, 255);
           
            foreach(BaseMonster x in msManager.Monsters)
            {
                if(x.MonsterIndex == CurrentEquip.MonsterIndex)
                    HoldPanel.GetComponent<MSEquipHold>().monster.sprite = x.MonsterSprite;
            }
            
        }
      else
        {
            HoldPanel.GetComponent<MSEquipHold>().monster.color = new Color(HoldPanel.GetComponent<MSEquipHold>().monster.color.r, HoldPanel.GetComponent<MSEquipHold>().monster.color.g, HoldPanel.GetComponent<MSEquipHold>().monster.color.b, 0);

        }
    }
}
