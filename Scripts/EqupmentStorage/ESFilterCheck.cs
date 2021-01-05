using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ESFilterCheck : MonoBehaviour
{
    public Sprite check;
    public Sprite x;
    public GameObject Manager;
    public bool IsFiltering = false;

    public List<GameObject> CurrentFilter = new List<GameObject>();
    public void EditFilterList()
    {
        ESManager esManager = Manager.GetComponent<ESManager>();
        List<GameObject> OwnedEquipment = esManager.OwnedEquipment;
        List<GameObject> ReturnFilterList = new List<GameObject>();

        if (this.transform.parent.gameObject.transform.Find("FilterText").GetComponent<TextMeshProUGUI>().text == "6 Star")
        {
            foreach(GameObject equipment in OwnedEquipment)
            {
                if (equipment.GetComponent<BaseEqupment>().Stars == 6)
                    ReturnFilterList.Add(equipment);
            }
        }
        else if (this.transform.parent.gameObject.transform.Find("FilterText").GetComponent<TextMeshProUGUI>().text == "5 Star")
        {
            foreach (GameObject equipment in OwnedEquipment)
            {
                if (equipment.GetComponent<BaseEqupment>().Stars == 5)
                    ReturnFilterList.Add(equipment);
            }
        }
        else if (this.transform.parent.gameObject.transform.Find("FilterText").GetComponent<TextMeshProUGUI>().text == "Legend")
        {
            foreach (GameObject equipment in OwnedEquipment)
            {
                if (equipment.GetComponent<BaseEqupment>().grade == "Legend")
                    ReturnFilterList.Add(equipment);
            }
        }

        CurrentFilter = ReturnFilterList;

        if (IsFiltering)
        {
            CurrentFilter.Clear();
        }

        if (IsFiltering)
        {
            GetComponent<Image>().sprite = x;
            IsFiltering = false;
        }

        else
        {
            GetComponent<Image>().sprite = check;
            IsFiltering = true;
        }

        
    }

   


}
