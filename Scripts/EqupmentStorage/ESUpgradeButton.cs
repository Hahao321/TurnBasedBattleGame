using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESUpgradeButton : MonoBehaviour
{
    public GameObject UpgradeImage;

    public void UpgradeButton()
    {
        UpgradeImage.SetActive(true);
        GameObject.Find("EquipmentCanvas").transform.Find("EquipmentImage").transform.Find("BlankImageSell").gameObject.SetActive(true);
    }
}
