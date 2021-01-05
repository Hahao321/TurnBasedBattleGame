using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESCancelButton : MonoBehaviour
{
    public GameObject EquipmentPanel;

    public void TurnOffEquipmentPanel()
    {
        EquipmentPanel.SetActive(false);
    }
}
