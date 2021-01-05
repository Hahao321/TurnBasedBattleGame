using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterSelectButton : MonoBehaviour
{
    public GameObject Monster1; //delete
    public BaseMonster Monster;

    public void OpenOptions()
    {
        //highlight monster button
        GameObject x = GameObject.Find("MonsterPanelCanvas").transform.Find("MonsterOptionsPanel").gameObject;
        x.SetActive(true);
        x.GetComponent<MSOptionsPanel>().Monster = Monster;
        //update panel func

    }
   

    
    


}
