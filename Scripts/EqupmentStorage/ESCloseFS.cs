using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESCloseFS : MonoBehaviour
{
    public GameObject FilterButton;
    public GameObject SortButton;
    public GameObject FilterImage;
    public GameObject SortImage;

   

    public void CloseDropDowns()
    {

        if (FilterButton.GetComponent<ESFilterButton>().FilterShowing)
        {
            FilterButton.GetComponent<ESFilterButton>().FilterClicked();
        }


        if (SortButton.GetComponent<ESSortButton>().SortShowing)
            SortButton.GetComponent<ESSortButton>().SortClicked();
    }
}
