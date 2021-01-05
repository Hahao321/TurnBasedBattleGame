using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MonsterSelectionManager : MonoBehaviour
{

    public GameObject ButtonReference;
    public GameObject MonsterSelectionCanvasGO; 
    public GameObject MonsterContentGO;

    public BigData locationData;

    private void Awake()
    {

        locationData = GameObject.FindGameObjectWithTag("LocationData").GetComponent<BigData>();


        //destroy all children left under content to reset
        foreach (Transform child in MonsterContentGO.transform) 
        {
            GameObject.Destroy(child.gameObject);
        }

        MonsterSelectionCanvasGO.SetActive(true);

        PopulateContent();

        //call function that fills slots with previous used monsters if they exist
        

    }

    //loop thru list of monsters that we own
    //create button
    //set sprite
    //set function to onClick add to thingy  set reference to said monster

    private void PopulateContent()
    {


        foreach(BaseMonster Monster in locationData.VisableHeroListm)
        {
            //create button
            GameObject x = Instantiate(ButtonReference, MonsterContentGO.transform, false);
            x.transform.SetParent(MonsterContentGO.transform);

            //reference monster sprite
            Sprite MonsterSprite = Monster.MonsterSprite;

            //set button sprite to monster sprite
            x.GetComponent<Image>().sprite = MonsterSprite;

            //make func that sets reference to current monster
            x.GetComponent<MonsterSelectButton>().Monster = Monster;

            //add that function to the button
        }
    }


    private List<BaseMonster> SelectedMonsters()
    {
        List<BaseMonster> x = new List<BaseMonster>();
        GameObject a = GameObject.Find("MonsterPanelCanvas").transform.Find("MonsterSlotsContent").transform.Find("Slot1").gameObject;
        if (a.GetComponent<MSSlotButton>().CurrentMonster != null)
            x.Add(a.GetComponent<MSSlotButton>().CurrentMonster);
        a = GameObject.Find("MonsterPanelCanvas").transform.Find("MonsterSlotsContent").transform.Find("Slot2").gameObject;
        if (a.GetComponent<MSSlotButton>().CurrentMonster != null)
            x.Add(a.GetComponent<MSSlotButton>().CurrentMonster);
        a = GameObject.Find("MonsterPanelCanvas").transform.Find("MonsterSlotsContent").transform.Find("Slot3").gameObject;
        if (a.GetComponent<MSSlotButton>().CurrentMonster != null)
            x.Add(a.GetComponent<MSSlotButton>().CurrentMonster);
        a = GameObject.Find("MonsterPanelCanvas").transform.Find("MonsterSlotsContent").transform.Find("Slot4").gameObject;
        if (a.GetComponent<MSSlotButton>().CurrentMonster != null)
            x.Add(a.GetComponent<MSSlotButton>().CurrentMonster);

        return x;
    }

    public void StartFight()
    { //clear battleData lsit
        locationData.BattleDataList.Clear();

        List<BaseMonster> HeroMonsters = SelectedMonsters(); //change

        if (HeroMonsters.Count == 0)
        {
            Debug.Log("No Monsters Selected!");
            //some kinda text popup that says add monsters
            return;
        }


        BigData.ChosenHeroList = SelectedMonsters(); //change


        //make new battleData and add to list for each stage
        string ScenarioName = locationData.ScenarioName;
        int SubNum = locationData.SubNumber;

       
        int level = locationData.LevelsForSubScenario(ScenarioName, SubNum);

        locationData.FillBattleDataList(ScenarioName, SubNum, "easy", level);   //adds everything to battledatalist
        Debug.Log("Ran fill from selection");


        SceneManager.LoadScene("BattleSceneCopy");

    }
}
