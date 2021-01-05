using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ESFilterButton : MonoBehaviour
{
    public GameObject FilterButton;
    public GameObject SortButton;
    public GameObject FilterImage;
    public GameObject SortImage;
    public GameObject FilterOk;


    public bool FilterDroppingDown = false;
    public bool FilterShowing = false;

    

    // Update is called once per frame
    void Update()
    {
        if (FilterDroppingDown)
        {
            if (FilterImage.transform.localPosition.y > 47)
            {
                FilterButton.GetComponent<Button>().interactable = false;
                SortButton.GetComponent<Button>().interactable = false;
                FilterOk.transform.Find("FilterOkButton").GetComponent<Button>().interactable = false;
                FilterImage.transform.localPosition = new Vector3(FilterImage.transform.localPosition.x, FilterImage.transform.localPosition.y - 2, FilterImage.transform.localPosition.z);
                FilterOk.transform.localPosition = new Vector3(FilterOk.transform.localPosition.x, FilterOk.transform.localPosition.y - 2, FilterOk.transform.localPosition.z);

            }
            else
            {
                FilterDroppingDown = false;
                FilterShowing = true;
                FilterButton.GetComponent<Button>().interactable = true;
                SortButton.GetComponent<Button>().interactable = true;
                FilterOk.transform.Find("FilterOkButton").GetComponent<Button>().interactable = true;

            }
        }
    }
    public void FilterClicked()
    {
        
        if (FilterShowing)
        {

            FilterImage.transform.localPosition = new Vector3(FilterImage.transform.localPosition.x, 336, FilterImage.transform.localPosition.z);
            FilterOk.transform.localPosition = new Vector3(FilterOk.transform.localPosition.x, 248, FilterOk.transform.localPosition.z);
            FilterImage.SetActive(false);
            FilterOk.SetActive(false);
            FilterShowing = false;
            FilterDroppingDown = false;
            GameObject.Find("OwnedRunesCanvas").transform.Find("Mask").gameObject.SetActive(false);
            return;
        }

        GameObject.Find("OwnedRunesCanvas").transform.Find("Mask").gameObject.SetActive(true);

        if (SortButton.GetComponent<ESSortButton>().SortShowing)
            SortButton.GetComponent<ESSortButton>().SortClicked();

        FilterImage.SetActive(true);
        FilterOk.SetActive(true);
        FilterDroppingDown = true;
    }

}
