using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESEquipmentButton : MonoBehaviour
{
    public GameObject CurrentEquipment;

    public void EquipmentClicked()
    {
        BaseEqupment CurrentEquipmentBE = CurrentEquipment.GetComponent<BaseEqupment>();
        GameObject.Find("EquipmentCanvas").transform.Find("EquipmentImage").GetComponent<ESEquipCanvas>().CurrentEquipment = CurrentEquipmentBE;
        GameObject.Find("EquipmentCanvas").transform.Find("EquipmentImage").gameObject.SetActive(true);
    }

}
