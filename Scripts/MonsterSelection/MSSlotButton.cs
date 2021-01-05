using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MSSlotButton : MonoBehaviour
{
    public GameObject CurrentMonster1;
    public BaseMonster CurrentMonster;

    private void Update()
    {
        if(CurrentMonster == null)
        this.GetComponent<Button>().interactable = false;

        else if (CurrentMonster != null)
            this.GetComponent<Button>().interactable = true;
    }
    public void RemoveMonster()
    {
        Debug.Log("Removing Monster");
        //button only active if slot full
        BaseMonster x = CurrentMonster;
        CurrentMonster = null;
        this.GetComponent<Image>().sprite = GameObject.Find("Main Camera").transform.Find("EmptySlotImage").GetComponent<SpriteRenderer>().sprite;

        GameObject MonsterScrollable = GameObject.Find("MonsterPanelCanvas").transform.Find("MonsterScrollList").transform.Find("ViewPort").transform.Find("Content").gameObject;
        foreach (Transform ScrollButton in MonsterScrollable.transform)
        {
            if (ScrollButton.GetComponent<MonsterSelectButton>().Monster == x)
                ScrollButton.GetComponent<Image>().color = new Color(ScrollButton.GetComponent<Image>().color.r, ScrollButton.GetComponent<Image>().color.g, ScrollButton.GetComponent<Image>().color.b, 1);
        }
    }

}


/*
 * clicking on monster in scrollable panel puts monster in FIRST available slot
 * clicking monster in slot removes monster from slot
 * holding shows stats on both 
 *
 */