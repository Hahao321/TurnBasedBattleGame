using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MSScrollEquipImage : MonoBehaviour
{
    public BaseEqupment CurrentEquip;

 

    public GameObject FilterImage;
    public Button FilterButton;
    public GameObject SortImage;
    public Button SortButton;



    public void EquipFilterClicked()
    {
        if (FilterImage.active)
        {
            FilterImage.SetActive(false);
            return;
        }
        FilterImage.SetActive(true);
        SortImage.SetActive(false);

    }
    public void EquipSortClicked()
    {
        if (SortImage.active)
        {
            SortImage.SetActive(false);
            return;
        }
        SortImage.SetActive(true);
        FilterImage.SetActive(false);

    }
}
