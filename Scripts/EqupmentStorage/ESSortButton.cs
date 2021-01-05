using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using UnityEngine;
using UnityEngine.UI;

public class ESSortButton : MonoBehaviour
{
    public GameObject FilterButton;
    public GameObject SortButton;
    public GameObject FilterImage;
    public GameObject SortImage;
    public GameObject FilterOk;


    public bool SortDroppingDown = false;
    public bool SortShowing = false;



    // Update is called once per frame
    void Update()
    {
        if (SortDroppingDown)
        {
            if (SortImage.transform.localPosition.y > 16)
            {
                FilterButton.GetComponent<Button>().interactable = false;
                SortButton.GetComponent<Button>().interactable = false;
                FilterOk.transform.Find("FilterOkButton").GetComponent<Button>().interactable = false;

                SortImage.transform.localPosition = new Vector3(SortImage.transform.localPosition.x, SortImage.transform.localPosition.y - 2, SortImage.transform.localPosition.z);

            }
            else
            {
                SortDroppingDown = false;
                SortShowing = true;
                FilterButton.GetComponent<Button>().interactable = true;
                SortButton.GetComponent<Button>().interactable = true;
                FilterOk.transform.Find("FilterOkButton").GetComponent<Button>().interactable = true;

            }
        }
    }
    public void SortClicked()
    {

        if (SortShowing)
        {
            SortImage.transform.localPosition = new Vector3(SortImage.transform.localPosition.x, 362, SortImage.transform.localPosition.z);
            SortImage.SetActive(false);
        
            SortShowing = false;
            SortDroppingDown = false;

            GameObject.Find("OwnedRunesCanvas").transform.Find("Mask").gameObject.SetActive(false);
            return;
        }
        GameObject.Find("OwnedRunesCanvas").transform.Find("Mask").gameObject.SetActive(true);

        if (FilterButton.GetComponent<ESFilterButton>().FilterShowing)
            FilterButton.GetComponent<ESFilterButton>().FilterClicked();

        SortImage.SetActive(true);
        SortDroppingDown = true;
    }
}
