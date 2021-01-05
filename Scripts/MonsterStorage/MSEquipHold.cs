using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.UI;

public class MSEquipHold : MonoBehaviour
{
    // Start is called before the first frame update
    public BaseEqupment CurrentEquip;
    public Image monster;
    public Image EquipSprite;
    public GameObject blank;
    public MSMonEqpImage MonsEquipPanel;
    public GameObject EquipmentSwitchPanel;
    public MSManager msManager;
    public GameObject EquipmentRemovePanel;
    public GameObject SwitchRemoveBlank;
    public Button OptionsButton;
    public Button RemoveButton;
    public Button EquipButton;
    private void Awake()
    {
        msManager = GameObject.Find("Main Camera").GetComponent<MSManager>();

    }
    private void Update()
    {
        if (CurrentEquip.MonsterIndex != 0)
        {
            RemoveButton.interactable = true;
        }
        else
            RemoveButton.interactable = false;

        if (MonsEquipPanel.CurrentMonster.MonsterIndex == CurrentEquip.MonsterIndex)
            EquipButton.interactable = false;
        else
            EquipButton.interactable = true;

    }
    public void CloseButton()
    {
        this.gameObject.SetActive(false);
        blank.SetActive(false);
        OptionsButton.interactable = true;
    }

    public void SingleClick()   //initial equip panel. Equips if both runes empty, if slot full offers switch, if selected equipment taken offers remove
    {
        bool switched = false;
        if(CurrentEquip.MonsterIndex != 0)
        {
            SwitchRemoveBlank.SetActive(true);
            EquipmentRemovePanel.SetActive(true);
            //fill out price info in new panel
            return;
        }
     
        if (CurrentEquip.slot == 1)
        {
            if (MonsEquipPanel.CurrentMonster.Equipment1 == null)
            {
                MonsEquipPanel.CurrentMonster.Equipment1 = CurrentEquip;
                CurrentEquip.MonsterIndex = MonsEquipPanel.CurrentMonster.MonsterIndex;
                CloseButton();
            }
            else
            {
                switched = true;
                //set up switch cost
                EquipmentSwitchPanel.SetActive(true);
                SwitchRemoveBlank.SetActive(true);
            }
        }
        if (CurrentEquip.slot == 2)
        {
            if (MonsEquipPanel.CurrentMonster.Equipment2 == null)
            {
                MonsEquipPanel.CurrentMonster.Equipment2 = CurrentEquip;
                CurrentEquip.MonsterIndex = MonsEquipPanel.CurrentMonster.MonsterIndex;
                CloseButton();
            }
            else
            {
                switched = true;
                //set up switch cost
                EquipmentSwitchPanel.SetActive(true);
                SwitchRemoveBlank.SetActive(true);
            }
        }
        if (CurrentEquip.slot == 3)
        {
            if (MonsEquipPanel.CurrentMonster.Equipment3 == null)
            {
                MonsEquipPanel.CurrentMonster.Equipment3 = CurrentEquip;
                CurrentEquip.MonsterIndex = MonsEquipPanel.CurrentMonster.MonsterIndex;
                CloseButton();
            }
            else
            {
                switched = true;
                //set up switch cost
                EquipmentSwitchPanel.SetActive(true);
                SwitchRemoveBlank.SetActive(true);
            }
        }
        if (CurrentEquip.slot == 4)
        {
            if (MonsEquipPanel.CurrentMonster.Equipment4 == null)
            {
                MonsEquipPanel.CurrentMonster.Equipment4 = CurrentEquip;
                CurrentEquip.MonsterIndex = MonsEquipPanel.CurrentMonster.MonsterIndex;
                CloseButton();
            }
            else
            {
                switched = true;
                //set up switch cost
                EquipmentSwitchPanel.SetActive(true);
                SwitchRemoveBlank.SetActive(true);
            }
        }

        
        if(switched == false)
        {
            MSManager x = GameObject.Find("Main Camera").GetComponent<MSManager>();
            x.PerformUpdate(x.CurrentMonster, x.PanelIndex);
            MonsEquipPanel.UpdateMonsterEquipPanel();
            blank.SetActive(false);

        }
        Debug.Log(CurrentEquip.MonsterIndex);
    }

    public void YesSwitch() //if currnet slot is full offers to switch equipment
    {
        if(CurrentEquip.slot == 1)
        {
            MonsEquipPanel.CurrentMonster.Equipment1.MonsterIndex = 0; //the currently equipped equipment has its index set to 0 to imply its unequipped
            CurrentEquip.MonsterIndex = MonsEquipPanel.CurrentMonster.MonsterIndex; //"CurrentEquip" the equipment we are trying to equip has index set to the monster
            MonsEquipPanel.CurrentMonster.Equipment1 = CurrentEquip; //monsters equipment set 
        }
        if (CurrentEquip.slot == 2)
        {
            MonsEquipPanel.CurrentMonster.Equipment2.MonsterIndex = 0; //the currently equipped equipment has its index set to 0 to imply its unequipped
            CurrentEquip.MonsterIndex = MonsEquipPanel.CurrentMonster.MonsterIndex; //"CurrentEquip" the equipment we are trying to equip has index set to the monster
            MonsEquipPanel.CurrentMonster.Equipment2 = CurrentEquip; //monsters equipment set 
        }
        if (CurrentEquip.slot == 3)
        {
            MonsEquipPanel.CurrentMonster.Equipment3.MonsterIndex = 0; //the currently equipped equipment has its index set to 0 to imply its unequipped
            CurrentEquip.MonsterIndex = MonsEquipPanel.CurrentMonster.MonsterIndex; //"CurrentEquip" the equipment we are trying to equip has index set to the monster
            MonsEquipPanel.CurrentMonster.Equipment3 = CurrentEquip; //monsters equipment set 
        }
        if (CurrentEquip.slot == 4)
        {
            MonsEquipPanel.CurrentMonster.Equipment4.MonsterIndex = 0; //the currently equipped equipment has its index set to 0 to imply its unequipped
            CurrentEquip.MonsterIndex = MonsEquipPanel.CurrentMonster.MonsterIndex; //"CurrentEquip" the equipment we are trying to equip has index set to the monster
            MonsEquipPanel.CurrentMonster.Equipment4 = CurrentEquip; //monsters equipment set 
        }
        //money - cost


        SwitchRemoveBlank.SetActive(false);
        EquipmentSwitchPanel.SetActive(false);
        this.gameObject.SetActive(false);
        blank.SetActive(false);
        OptionsButton.interactable = true;
        MonsEquipPanel.UpdateMonsterEquipPanel();
        msManager.PerformUpdate(msManager.CurrentMonster, msManager.PanelIndex);
    }
    public void NoSwitch()
    {
        EquipmentSwitchPanel.SetActive(false);
        SwitchRemoveBlank.SetActive(false);
    }

    public void YesRemove()
    {
        foreach(BaseMonster x in msManager.Monsters)
        {
            if(x.MonsterIndex == CurrentEquip.MonsterIndex)
            {
                if (CurrentEquip.slot == 1)
                    x.Equipment1 = null;
                if (CurrentEquip.slot == 2)
                    x.Equipment2 = null;
                if (CurrentEquip.slot == 3)
                    x.Equipment3 = null;
                if (CurrentEquip.slot == 4)
                    x.Equipment4 = null;
            }
        }
        CurrentEquip.MonsterIndex = 0;

        monster.color = new Color(monster.color.r, monster.color.g, monster.color.b, 0);
        MonsEquipPanel.UpdateMonsterEquipPanel();
        msManager.PerformUpdate(msManager.CurrentMonster, msManager.PanelIndex);
        NoRemove(); //close
    }
    public void NoRemove()
    {
        EquipmentRemovePanel.SetActive(false);
        SwitchRemoveBlank.SetActive(false);
    }
    



}
