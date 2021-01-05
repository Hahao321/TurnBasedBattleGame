using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ESUpgradeImage : MonoBehaviour
{
    public GameObject Blank;
    public ESEquipCanvas EquipImage;
    public GameObject UpgradeImage;
    public BaseEqupment CurrentEquipment;

    public Button UpgradeConfirmButtonB;

    private void OnEnable()
    {
        UpdateUpgradeInfo();
    }

    private void Update()
    {
        if (CurrentEquipment.level == 10)
        {
            UpgradeConfirmButtonB.transform.Find("UpgradeCostText").GetComponent<TextMeshProUGUI>().text = "Max Level";
            UpgradeConfirmButtonB.interactable = false;
        }
        else
        {
            UpgradeConfirmButtonB.transform.Find("UpgradeCostText").GetComponent<TextMeshProUGUI>().text = "Upgrade " + CurrentEquipment.CalcualteUpgradePrice();

            UpgradeConfirmButtonB.interactable = true;
        }
    }

    public void UpdateUpgradeInfo()
    {
        this.transform.Find("UpgradeEquipImage").GetComponent<Image>().sprite = EquipImage.transform.Find("EquipmentSingleImage").GetComponent<Image>().sprite;
        CurrentEquipment = EquipImage.CurrentEquipment;
        string a = "";
        string b = "";
        string c = " > ";
        string d = "";

        a = CurrentEquipment.BaseStat;
        b = b+ CurrentEquipment.BaseStatValue;
        int e = CurrentEquipment.BaseStatValue + 3;
        d = d + e; //value calcualtion
        if (CurrentEquipment.level < 10)
            this.transform.Find("UpgradeMainStatText").GetComponent<TextMeshProUGUI>().text = a + b + c + d;
        else
            this.transform.Find("UpgradeMainStatText").GetComponent<TextMeshProUGUI>().text = a + b;
        a = "";
        b = "";
        d = "";
        if (CurrentEquipment.level < 2)
        {
            this.transform.Find("UpgradeSubStat1").GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 255);
            this.transform.Find("UpgradeSubStat1").GetComponent<TextMeshProUGUI>().text = "???";
        }
        if(CurrentEquipment.level == 2)
        {
            this.transform.Find("UpgradeSubStat1").GetComponent<TextMeshProUGUI>().text = "???";
            this.transform.Find("UpgradeSubStat1").GetComponent<TextMeshProUGUI>().color = new Color(0, 174, 0, 255);
        }
        if (CurrentEquipment.level > 2)
        {
            this.transform.Find("UpgradeSubStat1").GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 255);
            this.transform.Find("UpgradeSubStat1").GetComponent<TextMeshProUGUI>().text = CurrentEquipment.SS1stat + " " + CurrentEquipment.SS1;
            if (!CurrentEquipment.IsSS1Flat)
            {
                this.transform.Find("UpgradeSubStat1").GetComponent<TextMeshProUGUI>().text += "%";

            }
        }

        if (CurrentEquipment.level < 5)
        {
            this.transform.Find("UpgradeSubStat2").GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 255);
            this.transform.Find("UpgradeSubStat2").GetComponent<TextMeshProUGUI>().text = "???";

        }
        
        if (CurrentEquipment.level == 5)
        {
            this.transform.Find("UpgradeSubStat2").GetComponent<TextMeshProUGUI>().text = "???";
            this.transform.Find("UpgradeSubStat2").GetComponent<TextMeshProUGUI>().color = new Color(0, 174, 0, 255);
        }
        if (CurrentEquipment.level > 5)
        {
            this.transform.Find("UpgradeSubStat2").GetComponent<TextMeshProUGUI>().text += "%";
            this.transform.Find("UpgradeSubStat2").GetComponent<TextMeshProUGUI>().text = CurrentEquipment.SS2stat + " " + CurrentEquipment.SS2;
            if (!CurrentEquipment.IsSS2Flat)
            {
                this.transform.Find("UpgradeSubStat2").GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 255);

            }
        }

        if (CurrentEquipment.level < 9)
        {
            this.transform.Find("UpgradeSubStat3").GetComponent<TextMeshProUGUI>().text = "???";
            this.transform.Find("UpgradeSubStat3").GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 255);
        }
        if (CurrentEquipment.level == 9)
        {
            this.transform.Find("UpgradeSubStat3").GetComponent<TextMeshProUGUI>().text = "???";
            this.transform.Find("UpgradeSubStat3").GetComponent<TextMeshProUGUI>().color = new Color(0, 174, 0, 255);
        }
        if (CurrentEquipment.level > 9)
        {
            this.transform.Find("UpgradeSubStat3").GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, 255);

            this.transform.Find("UpgradeSubStat3").GetComponent<TextMeshProUGUI>().text = CurrentEquipment.SS3stat + " " + CurrentEquipment.SS3;
            if (!CurrentEquipment.IsSS1Flat)
            {
                this.transform.Find("UpgradeSubStat3").GetComponent<TextMeshProUGUI>().text += "%";
            }
        }

        UpgradeImage.transform.Find("UpgradeConfirmButton").transform.Find("UpgradeCostText").GetComponent<TextMeshProUGUI>().text = "Upgrade " + CurrentEquipment.CalcualteUpgradePrice();
        if(CurrentEquipment.level == 10)
        UpgradeImage.transform.Find("LevelText").GetComponent<TextMeshProUGUI>().text = "Level " + CurrentEquipment.level;
        else
        {
            int f = CurrentEquipment.level + 1;
            UpgradeImage.transform.Find("LevelText").GetComponent<TextMeshProUGUI>().text = "Level " + CurrentEquipment.level + " > " + f;

        }

    }

    public void UpgradeCancelButton()
    {
        Blank.SetActive(false);
        UpdateUpgradeInfo();
        UpgradeImage.SetActive(false);
    }

    public void UpgradeConfirmButton()
    {
        CurrentEquipment.level++;
        //upgrade equipment stats
        Blank.SetActive(true);
        //check if upgrade worked
        //play corresponding animation
        UpdateUpgradeInfo();

    }
}
