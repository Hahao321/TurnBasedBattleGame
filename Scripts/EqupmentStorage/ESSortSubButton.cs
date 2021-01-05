using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ESSortSubButton : MonoBehaviour
{
    public ESManager Manager;
    public ESSortButton SortButton;
    public void SortFilteredList()
    {
        List<GameObject> newSortList = new List<GameObject>();
        if(this.transform.Find("SortText").gameObject.GetComponent<TextMeshProUGUI>().text == "Grade")
        {
            for (int i = 0; i < 5; i++)
            {
                foreach (GameObject Equipment in Manager.FilteredEquipmentList)
                {
                    if(i == 0)
                    {
                        if (Equipment.GetComponent<BaseEqupment>().grade == "Legend")
                            newSortList.Add(Equipment);
                    }
                    if (i == 1)
                    {
                        if (Equipment.GetComponent<BaseEqupment>().grade == "SuperRare")
                            newSortList.Add(Equipment);
                    }
                    if (i == 2)
                    {
                        if (Equipment.GetComponent<BaseEqupment>().grade == "VeryRare")
                            newSortList.Add(Equipment);
                    }
                    if (i == 3)
                    {
                        if (Equipment.GetComponent<BaseEqupment>().grade == "Rare")
                            newSortList.Add(Equipment);
                    }
                    if (i == 4)
                    {
                        if (Equipment.GetComponent<BaseEqupment>().grade == "Common")
                            newSortList.Add(Equipment);
                    }
                }

            }
            
        }
        if (this.transform.Find("SortText").gameObject.GetComponent<TextMeshProUGUI>().text == "Stars")
        {

            for (int i = 0; i < 6; i++)
            {
                foreach (GameObject Equipment in Manager.FilteredEquipmentList)
                {
                    Debug.Log("i for stars = " + i);
                    if (i == 0)
                    {
                        if (Equipment.GetComponent<BaseEqupment>().Stars == 6)
                        {
                            Debug.Log(Equipment);
                            newSortList.Add(Equipment);
                        }
                    }
                    if (i == 1)
                    {
                        if (Equipment.GetComponent<BaseEqupment>().Stars == 5)
                            newSortList.Add(Equipment);
                    }
                    if (i == 2)
                    {
                        if (Equipment.GetComponent<BaseEqupment>().Stars == 4)
                            newSortList.Add(Equipment);
                    }
                    if (i == 3)
                    {
                        if (Equipment.GetComponent<BaseEqupment>().Stars == 3)
                            newSortList.Add(Equipment);
                    }
                    if (i == 4)
                    {
                        if (Equipment.GetComponent<BaseEqupment>().Stars == 2)
                            newSortList.Add(Equipment);
                    }
                    if (i == 5)
                    {
                        if (Equipment.GetComponent<BaseEqupment>().Stars == 1)
                            newSortList.Add(Equipment);
                    }
                }

            }
        }
        if (this.transform.Find("SortText").gameObject.GetComponent<TextMeshProUGUI>().text == "Level")
        {
            newSortList = Manager.OwnedEquipment;
        }


        Manager.FilteredEquipmentList = newSortList;

        Manager.UpdateEquipmentButtons();
        SortButton.SortClicked();
    }

}
