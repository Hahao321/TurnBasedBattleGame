using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MSManager : MonoBehaviour
{
    public List<GameObject> MonstersGO = new List<GameObject>();
    public List<BaseMonster> Monsters = new List<BaseMonster>();

    public List<GameObject> SortedMonstersGO = new List<GameObject>();
    public List<BaseMonster> SortedMonsters = new List<BaseMonster>();

    public GameObject MonsterContent;
    public GameObject MonsterButton;
    public BigData bigData;

    public GameObject ViewMonsterImage;
    public GameObject MonsterAwakenImage;
    public GameObject MonsterEquipImage;

    public List<GameObject> OwnedEquipGO;
    public List<BaseEqupment> OwnedEquip;

    public List<BaseEqupment> SortedEquip;
    public GameObject EquipContent;
    public GameObject EquipmentSingleButtonReferece;
    public int PanelIndex; //1 2 3 4

    public MSMonEqpImage MonsterEquipSlotPanel;
    public BaseMonster CurrentMonster;
    private void Awake()
    {
        bigData = GameObject.Find("LocData").GetComponent<BigData>();
       // MonstersGO = bigData.VisableHeroList;
        OwnedEquipGO = bigData.OwnedEquipment;

        foreach(GameObject x in OwnedEquipGO)
        {
            OwnedEquip.Add(x.GetComponent<BaseEqupment>());
        }
        SortedEquip = OwnedEquip;

        //  foreach (GameObject x in MonstersGO)
        //  {
        //      Monsters.Add(x.GetComponent<HeroStateMachine>().hero);
        //  }

        Monsters = bigData.VisableHeroListm;
        

        //SortedMonstersGO = MonstersGO;
        SortedMonsters = Monsters;

        UpdateMonsterContent();

        //set first panel for first monster

        PanelIndex = 1;
        CurrentMonster = SortedMonsters[0];

        PerformUpdate(CurrentMonster, PanelIndex);

    }

    public void UpdateMonsterContent()
    {
        foreach(Transform x in MonsterContent.transform)
        {
            Destroy(x.gameObject);
        }


        foreach (BaseMonster x in SortedMonsters)
        {
            //create button
            GameObject y = Instantiate(MonsterButton, MonsterContent.transform, false);
            y.transform.SetParent(MonsterContent.transform);

            //reference monster sprite
            y.GetComponent<Image>().sprite = x.MonsterSprite;

            //make func that sets reference to current monster
            y.GetComponent<MSMonsterButton>().Monster = x;

        }
    }

    public void PerformUpdate(BaseMonster Monster, int PanelIndex)
    {
        if (PanelIndex == 1)
            UpdateViewMonster(Monster);

        if (PanelIndex == 2)
            UpdateEquipMonster(Monster);

        if (PanelIndex == 3)
            UpdateAwakenMonster(Monster);
    }


    public void UpdateViewMonster(BaseMonster x)
    {
        //turn off all other panels 
        ViewMonsterImage.SetActive(true);
       // MonsterAwakenImage.SetActive(false);
        MonsterEquipImage.SetActive(false);

        ViewMonsterImage.transform.Find("VMMonsterSprite").GetComponent<Image>().sprite = x.MonsterSprite;


    }
    public void UpdateAwakenMonster(BaseMonster x)
    {
        ViewMonsterImage.SetActive(false);
        MonsterAwakenImage.SetActive(true);
        MonsterEquipImage.SetActive(false);
    }
    public void UpdateEquipMonster(BaseMonster x)
    {
        ViewMonsterImage.SetActive(false);
       // MonsterAwakenImage.SetActive(false);
        MonsterEquipImage.SetActive(true);

        MonsterEquipSlotPanel.CurrentMonster = CurrentMonster;
        MonsterEquipSlotPanel.UpdateMonsterEquipPanel();
        UpdateEquipmentPanel();
    }

    public void UpdateEquipmentPanel()
    {
        foreach (Transform child in EquipContent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach(BaseEqupment x in SortedEquip)
        {
            GameObject y = Instantiate(EquipmentSingleButtonReferece, EquipContent.transform, false);
            y.transform.SetParent(EquipContent.transform);

            //reference monster sprite
            y.GetComponent<Image>().sprite = x.EquipmentSprite;

            //make func that sets reference to current monster
            y.GetComponent<MSEquipSingle>().CurrentEquip = x.GetComponent<BaseEqupment>();


            Image w = y.transform.Find("Image").GetComponent<Image>();
            if (x.MonsterIndex != 0)
            {
                w.color = new Color(w.color.r, w.color.g, w.color.b, 255);
                foreach(BaseMonster mon in Monsters)
                {
                    if(mon.MonsterIndex == x.MonsterIndex)
                        w.sprite = mon.MonsterSprite;
                }
                
            }
            else
           {
                w.color = new Color(w.color.r, w.color.g, w.color.b, 0);
          }
        }
    }

    public void HomeScreen()
    {
        SceneManager.LoadScene("HomeScreen");
    }

}
