using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ESEquipCanvas : MonoBehaviour
{
    public GameObject SellImage;
    public GameObject Blank;
    public BaseEqupment CurrentEquipment;
    //on image not canvas oops
    private void Update()
    {
        if(CurrentEquipment!= null)
         SetUpEquipmentCanvas(CurrentEquipment);    
    }
    public void SetUpEquipmentCanvas(BaseEqupment x)
    {
        this.transform.Find("EquipmentSingleImage").GetComponent<Image>().sprite = x.EquipmentSprite;
        string a = "";
        string b = " ";
        string c = "";
        a = x.BaseStat;
        c = "";
        c = c + x.BaseStatValue;
        this.transform.Find("MainStatText").GetComponent<TextMeshProUGUI>().text = a + b + c;

        if(x.level >= 3)
        {
            c = "";

            a = x.SS1stat;
            c = "" + x.BaseStatValue + x.SS1;
            this.transform.Find("SubStat1").GetComponent<TextMeshProUGUI>().text = a + b + c;
        }
        else
        {
            c = "";

            this.transform.Find("SubStat1").GetComponent<TextMeshProUGUI>().text = "???";

        }

        if (x.level >= 6)
        {
            c = "";

            a = x.SS2stat;
            c = c + x.SS2;
            this.transform.Find("SubStat2").GetComponent<TextMeshProUGUI>().text = a + b + c;
        }
        else
        {
            c = "";

            this.transform.Find("SubStat2").GetComponent<TextMeshProUGUI>().text = "???";

        }

        if (x.level >= 10)
        {
            c = "";

            a = x.SS3stat;
            c = c + x.SS3;
            this.transform.Find("SubStat3").GetComponent<TextMeshProUGUI>().text = a + b + c;
        }
        else
        {
            c = "";

            this.transform.Find("SubStat3").GetComponent<TextMeshProUGUI>().text = "???";

        }

        string SellString = "Sell ";
        this.transform.Find("SellButton").transform.Find("SellText").GetComponent<TextMeshProUGUI>().text =SellString + x.calculateCurrentSellingPrice();
        this.transform.Find("SellImage").transform.Find("SellPriceText").GetComponent<TextMeshProUGUI>().text = "" + x.calculateCurrentSellingPrice();

        this.transform.Find("LevelText").GetComponent<TextMeshProUGUI>().text = "Level "+CurrentEquipment.level;
    }

    public void CloseSellImage()
    {
        SellImage.SetActive(false);
        Blank.SetActive(false);
    }
    public void SellEquipmentConfirm()
    {

        BigData.Money += CurrentEquipment.SellingValue;
        Debug.Log("Sold for " + CurrentEquipment.SellingValue + "  your total money is " + BigData.Money);
  
      

        BigData x = GameObject.Find("LocData").GetComponent<BigData>();
        foreach(GameObject equipment in x.OwnedEquipment)
        {
            if(equipment.name == CurrentEquipment.name)
            {
                x.OwnedEquipment.Remove(CurrentEquipment.gameObject);
                GameObject.Find("ESManager").GetComponent<ESManager>().FilteredEquipmentList.Remove(CurrentEquipment.gameObject);
                break;
            }
               
        }

        GameObject a = GameObject.Find("OwnedRunesCanvas").transform.Find("Mask").transform.Find("FilterImage").gameObject;
        foreach (Transform child in a.transform)
        {
            ESFilterCheck z=  child.Find("FilterCheckButton").GetComponent<ESFilterCheck>();
            foreach(GameObject equip in z.CurrentFilter)
            {
                if (equip.name == CurrentEquipment.name)
                    z.CurrentFilter.Remove(equip);
                break;
            }
        }

        SellImage.SetActive(false);
        Blank.SetActive(false);
        GameObject.Find("ESManager").GetComponent<ESManager>().SetFilteredListNoOpen();

        this.gameObject.SetActive(false);
    }
}
