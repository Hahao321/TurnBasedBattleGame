using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESSellButton : MonoBehaviour
{
    public GameObject Blank;
    public GameObject SellImage;

    public void SellButtonClicked()
    {
        SellImage.SetActive(true);
        Blank.SetActive(true);
    }
}
