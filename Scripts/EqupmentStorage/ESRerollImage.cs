using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ESRerollImage : MonoBehaviour
{
    public TextMeshProUGUI ReRollText;
    public TextMeshProUGUI Sub1;
    public TextMeshProUGUI Sub2;
    public TextMeshProUGUI Sub3;
    public BaseEqupment CurrentEquip;
    public ESEquipCanvas EquipImage;
    public GameObject Blank;
    public void ReRollCancelButton()
    {
        this.gameObject.SetActive(false);
        Blank.SetActive(false);
    }

    private void Update()
    {
       

    }

    public void UpdateRerollImageStats()
    {
        Sub1.text = CurrentEquip.SS1stat + CurrentEquip.SS1;
        if (!CurrentEquip.IsSS1Flat)
            Sub1.text += "%";

        Sub2.text = CurrentEquip.SS2stat + CurrentEquip.SS2;
        if (!CurrentEquip.IsSS2Flat)
            Sub2.text += "%";

        Sub3.text = CurrentEquip.SS3stat + CurrentEquip.SS3;
        if (!CurrentEquip.IsSS3Flat)
            Sub3.text += "%";

        ReRollText.text = "Reroll " + CurrentEquip.CalcualteUpgradePrice(); //replace with calculate reroll price when prices designed
    }
    

    public void ConfirmRerollButton()
    {
         //number order indexing at 0 atk hp def, atkp hpp defp, spd, crit, cd, acc, res        
        int a = UnityEngine.Random.Range(0, 10);



        UpdateRerollImageStats();
    }

}
