using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESRerollButton : MonoBehaviour
{
    public GameObject RerollImage;
    public GameObject RerollBlank;
    public ESEquipCanvas EquipImage;
    public BaseEqupment CurrentEquip;

    public void ReRollButtonClicked()
    {
        if (CurrentEquip.level == 10)
        {

            RerollBlank.SetActive(true);
            RerollImage.SetActive(true);

            RerollImage.GetComponent<ESRerollImage>().CurrentEquip = CurrentEquip;
            RerollImage.GetComponent<ESRerollImage>().UpdateRerollImageStats();
        }
        else
        {
            //pop up level not max text
            return;
        }
        
    }

    private void Update()
    {
        CurrentEquip = EquipImage.CurrentEquipment;
    }



}
