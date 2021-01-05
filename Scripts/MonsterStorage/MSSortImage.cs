using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MSSortImage : MonoBehaviour
{
    public GameObject SortImage;
    public Button SortButton;
    public bool SortShowing;
    public bool SortMovingDown;
    public bool SortMovingUp;
    public GameObject SortBlank;
    public MSManager msManager;

   


    private void Update()
    {
        if (SortMovingDown && SortImage.transform.localPosition.y > 125)
        {
            SortImage.transform.localPosition = new Vector3(SortImage.transform.localPosition.x, SortImage.transform.localPosition.y-2, SortImage.transform.localPosition.z);
        }
        else if (SortMovingDown && SortImage.transform.localPosition.y <= 125)
        {
            SortMovingDown = false;
            SortShowing = true;
        }
        if (SortMovingUp && SortImage.transform.localPosition.y < 350)
        {
            SortImage.transform.localPosition = new Vector3(SortImage.transform.localPosition.x , SortImage.transform.localPosition.y+4, SortImage.transform.localPosition.z);
        }
        else if (SortMovingUp && SortImage.transform.localPosition.y >= 350 )
        {
            SortMovingUp = false;
            SortButton.image.color = new Color(SortButton.image.color.r, SortButton.image.color.g, SortButton.image.color.g, 255);
            SortButton.interactable = true;
            SortBlank.SetActive(false);

        }

    }
    public void SortButtonClicked()
    {
        SortMovingDown = true;
        SortButton.interactable = false;
        SortButton.image.color = new Color(SortButton.image.color.r, SortButton.image.color.g, SortButton.image.color.g, 0);
        SortBlank.SetActive(true);

    }

    public void MoveSortImageUp()
    {
        if (!SortShowing)
            return;

        SortMovingUp = true;

    }


}//312 87
