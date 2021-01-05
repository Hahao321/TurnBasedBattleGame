using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MSMonEqpImage : MonoBehaviour
{
    public BaseMonster CurrentMonster;


    public BaseEqupment HighlightEquipment;

    public Image MonsterSprite;
    public TextMeshProUGUI Set1T;
    public TextMeshProUGUI Set2T;

    public Button ESlot1;
    public Button ESlot2;
    public Button ESlot3;
    public Button ESlot4;

    public BaseEqupment E1;
    public BaseEqupment E2;
    public BaseEqupment E3;
    public BaseEqupment E4;


    public void UpdateMonsterEquipPanel()   //called on equiping rune, taking off runes
    {
        MonsterSprite.sprite = CurrentMonster.MonsterSprite;

        if(CurrentMonster.Equipment1 != null)
             E1 = CurrentMonster.Equipment1;
        if (CurrentMonster.Equipment2 != null)
            E2 = CurrentMonster.Equipment2;
        if (CurrentMonster.Equipment3 != null)
            E3 = CurrentMonster.Equipment3;
        if (CurrentMonster.Equipment4 != null)
            E4 = CurrentMonster.Equipment4;

        if (CurrentMonster.Equipment1 == null)
            E1 = null;
        if (CurrentMonster.Equipment2 == null)
            E2 = null;
        if (CurrentMonster.Equipment3 == null)
            E3 = null;
        if (CurrentMonster.Equipment4 == null)
            E4 = null;


        if (E1!=null)
            ESlot1.GetComponent<Image>().sprite = E1.EquipmentSprite;
        if (E2 != null)
            ESlot2.GetComponent<Image>().sprite = E2.EquipmentSprite;
        if (E3 != null)
            ESlot3.GetComponent<Image>().sprite = E3.EquipmentSprite;
        if (E4 != null)
            ESlot4.GetComponent<Image>().sprite = E4.EquipmentSprite;

        if (E1 == null)
            ESlot1.GetComponent<Image>().sprite = null;
        if (E2 == null)
            ESlot2.GetComponent<Image>().sprite = null;
        if (E3 == null)
            ESlot3.GetComponent<Image>().sprite = null;
        if (E4 == null)
            ESlot4.GetComponent<Image>().sprite = null;



        SetText();
    }

    public void SetText()
    {
        Set1T.text = "";
        Set2T.text = "";

        //for now energy / fatal but both 2/3/4  15% 30% 50%

        int[] setCounter = { 0, 0 };

        List<BaseEqupment> EquipL = new List<BaseEqupment>();

        if (E1 != null)
            EquipL.Add(E1);
        if (E2 != null)
            EquipL.Add(E2);
        if (E3 != null)
            EquipL.Add(E3);
        if (E4 != null)
            EquipL.Add(E4);


        foreach(BaseEqupment x in EquipL)
        {
            if (x.Set == "Energy")
                setCounter[0]++;

            if (x.Set == "Fatal")
                setCounter[1]++;
        }

        if (setCounter[0] > 1 && Set1T.text =="")
        {
            if (setCounter[0] == 2)
                Set1T.text = "Energy X2 => 15% Hp";
            if (setCounter[0] == 3)
                Set1T.text = "Energy X3 => 30% Hp";
            if (setCounter[0] == 4)
                Set1T.text = "Energy X4 => 50% Hp";
        }
        if (setCounter[0] > 1 && Set1T.text != "")
        {
            if (setCounter[0] == 2)
                Set2T.text = "Energy X2 => 15% Hp";
            if (setCounter[0] == 3)
                Set2T.text = "Energy X3 => 30% Hp";
            if (setCounter[0] == 4)
                Set2T.text = "Energy X4 => 50% Hp";
        }

        if (setCounter[1] > 1 && Set1T.text == "")
        {
            if (setCounter[1] == 2)
                Set1T.text = "Fatal X2 => 15% Atk";
            if (setCounter[1] == 3)
                Set1T.text = "Fatal X3 => 30% Atk";
            if (setCounter[1] == 4)
                Set1T.text = "Fatal X4 => 50% Atk";
        }
        if (setCounter[1] > 1 && Set1T.text != "")
        {
            if (setCounter[1] == 2)
                Set2T.text = "Fatal X2 => 15% Atk";
            if (setCounter[1] == 3)
                Set2T.text = "Fatal X3 => 30% Atk";
            if (setCounter[1] == 4)
                Set2T.text = "Fatal X4 => 50% Atk";
        }

        if (Set1T.text == Set2T.text)
            Set2T.text = "";

    }

}
