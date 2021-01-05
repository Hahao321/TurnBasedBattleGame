using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MSAddButton : MonoBehaviour
{
    public GameObject Monster1;
    public BaseMonster Monster;
    // Start is called before the first frame update

    private void Update()
    {
        Monster = GameObject.Find("MonsterPanelCanvas").transform.Find("MonsterOptionsPanel").GetComponent<MSOptionsPanel>().Monster;
    }
    public void SetMonster()
    {
        Transform MonsterSlotContent = GameObject.Find("MonsterPanelCanvas").transform.Find("MonsterSlotsContent");
        GameObject MonsterSlotContentGO = GameObject.Find("MonsterPanelCanvas").transform.Find("MonsterSlotsContent").gameObject;

        foreach (Transform SlotButton in MonsterSlotContent)
        {
            if (SlotButton.GetComponent<MSSlotButton>().CurrentMonster == Monster)
                return;
        }

        if (MonsterSlotContentGO.transform.Find("Slot1").GetComponent<MSSlotButton>().CurrentMonster ==  null)
        {
            MonsterSlotContentGO.transform.Find("Slot1").GetComponent<MSSlotButton>().CurrentMonster = Monster;
            MonsterSlotContentGO.transform.Find("Slot1").GetComponent<Image>().sprite = Monster.MonsterSprite;
            this.GetComponent<Image>().color = new Color(this.GetComponent<Image>().color.r, this.GetComponent<Image>().color.g, this.GetComponent<Image>().color.b, 0.7f);

        }
        else if (MonsterSlotContentGO.transform.Find("Slot2").GetComponent<MSSlotButton>().CurrentMonster == null)
        {
            MonsterSlotContentGO.transform.Find("Slot2").GetComponent<MSSlotButton>().CurrentMonster = Monster;
            MonsterSlotContentGO.transform.Find("Slot2").GetComponent<Image>().sprite = Monster.MonsterSprite;
            this.GetComponent<Image>().color = new Color(this.GetComponent<Image>().color.r, this.GetComponent<Image>().color.g, this.GetComponent<Image>().color.b, 0.7f);
        }
        else if (MonsterSlotContentGO.transform.Find("Slot3").GetComponent<MSSlotButton>().CurrentMonster == null)
        {
            MonsterSlotContentGO.transform.Find("Slot3").GetComponent<MSSlotButton>().CurrentMonster = Monster;
            MonsterSlotContentGO.transform.Find("Slot3").GetComponent<Image>().sprite = Monster.MonsterSprite;
            this.GetComponent<Image>().color = new Color(this.GetComponent<Image>().color.r, this.GetComponent<Image>().color.g, this.GetComponent<Image>().color.b, 0.7f);
        }
        else if (MonsterSlotContentGO.transform.Find("Slot4").GetComponent<MSSlotButton>().CurrentMonster == null)
        {
            MonsterSlotContentGO.transform.Find("Slot4").GetComponent<MSSlotButton>().CurrentMonster = Monster;
            MonsterSlotContentGO.transform.Find("Slot4").GetComponent<Image>().sprite = Monster.MonsterSprite;
            this.GetComponent<Image>().color = new Color(this.GetComponent<Image>().color.r, this.GetComponent<Image>().color.g, this.GetComponent<Image>().color.b, 0.7f);
        }

        GameObject.Find("MonsterPanelCanvas").transform.Find("MonsterOptionsPanel").gameObject.SetActive(false);

        return;
    }
}
