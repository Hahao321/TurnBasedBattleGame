using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ESManager : MonoBehaviour
{
    public GameObject EquipmentButtonReference;
    public BigData locationData;
    public List<GameObject> OwnedEquipment = new List<GameObject>();
    public List<GameObject> FilteredEquipmentList = new List<GameObject>();

    private void Awake()
    {

       locationData = GameObject.Find("LocData").GetComponent<BigData>();
        OwnedEquipment = locationData.OwnedEquipment;
       FilteredEquipmentList = OwnedEquipment;

        UpdateEquipmentButtons();


    }

    public void UpdateEquipmentButtons()
    {
        DeleteAllEquipmentButtons();
        GameObject EquipmentContent = GameObject.Find("OwnedRunesCanvas").transform.Find("EquipmentScrollableList").transform.Find("EquipmentViewPort").transform.Find("EquipmentContent").gameObject;
        foreach (GameObject Equipment in FilteredEquipmentList)
        {
            GameObject x = Instantiate(EquipmentButtonReference, EquipmentContent.transform, false);
            x.transform.SetParent(EquipmentContent.transform);

            //reference equipment sprite
            Sprite EquipmentSprite = Equipment.GetComponent<BaseEqupment>().EquipmentSprite;

            //set button sprite to monster sprite
            x.GetComponent<Image>().sprite = EquipmentSprite;

            //make func that sets reference to current monster
            x.GetComponent<ESEquipmentButton>().CurrentEquipment = Equipment;
        }
    }

    public void DeleteAllEquipmentButtons()
    {
        GameObject x = GameObject.Find("OwnedRunesCanvas").transform.Find("EquipmentScrollableList").transform.Find("EquipmentViewPort").transform.Find("EquipmentContent").gameObject;
        foreach (Transform child in x.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void SetFilteredListNoOpen()
    {
        List<GameObject> MasterFilterList = new List<GameObject>();

        Transform x = GameObject.Find("OwnedRunesCanvas").transform.Find("Mask").transform.Find("FilterImage").transform;
        bool added = false;

        foreach (Transform child in x)
        {
            GameObject y = child.Find("FilterCheckButton").gameObject;

            if (y.GetComponent<ESFilterCheck>().IsFiltering)
            {
                foreach (GameObject z in y.GetComponent<ESFilterCheck>().CurrentFilter)
                {
                    added = false;

                    foreach (GameObject w in MasterFilterList)
                    {

                        //use index for now we using name
                        if (z.name == w.name)
                            added = true;

                    }
                    if (!added)
                        MasterFilterList.Add(z);
                }
            }
        }

        FilteredEquipmentList = MasterFilterList;
        if (FilteredEquipmentList.Count == 0)
            FilteredEquipmentList = OwnedEquipment;

        UpdateEquipmentButtons();
    }
    public void SetFilteredList()
    {
        List<GameObject> MasterFilterList = new List<GameObject>();

        Transform x = GameObject.Find("OwnedRunesCanvas").transform.Find("Mask").transform.Find("FilterImage").transform;
        bool added = false;

        foreach (Transform child in x)
        {
            GameObject y = child.Find("FilterCheckButton").gameObject;

            if (y.GetComponent<ESFilterCheck>().IsFiltering)
            {
                foreach (GameObject z in y.GetComponent<ESFilterCheck>().CurrentFilter)
                {
                    added = false;

                    foreach (GameObject w in MasterFilterList)
                    {

                        //use index for now we using name
                        if (z.name == w.name)
                            added = true;
                        
                    }
                    if (!added)
                        MasterFilterList.Add(z);
                }
            }
        }

        FilteredEquipmentList = MasterFilterList;
        if (FilteredEquipmentList.Count == 0)
            FilteredEquipmentList = OwnedEquipment;

        UpdateEquipmentButtons();

        GameObject.Find("OwnedRunesCanvas").transform.Find("FilterButton").gameObject.GetComponent<ESFilterButton>().FilterClicked();
    }

    public void HomeScreenButton()
    {
        SceneManager.LoadScene("HomeScreen");
        
    }
}

