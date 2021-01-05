using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public BigData locationData;
    public string ScenarioName;
    public List<SubScenarioInfo> SubScenarios = new List<SubScenarioInfo>();
    public List<GameObject> SubScenariosObjects = new List<GameObject>();

    private void Awake()
    {
        TurnColidersOn();
        locationData = GameObject.FindGameObjectWithTag("LocationData").GetComponent<BigData>();
    }


    private void Update()
    {
        CheckIfSelected();
    }

    public string[] CheckIfSelected()
    {
        string[] returnArray = { "", "" };
        string x = "";

        foreach (GameObject Scenario in SubScenariosObjects)
        {
            if (Scenario.GetComponent<SubScenarioInfo>().selected)
            {
                Debug.Log("sub scenario was selected");

                locationData.SubNumber = Scenario.GetComponent<SubScenarioInfo>().SubNumber;
                locationData.ScenarioName = ScenarioName;
                SceneManager.LoadScene("MonsterSelectionScene");

            }

        }
        return returnArray;
    }
    public void TurnColidersOff()
    {
        foreach (GameObject Scenario in SubScenariosObjects)
        {
            Scenario.GetComponent<PolygonCollider2D>().enabled = false;
        }
    }
    public void TurnColidersOn()
    {
        foreach (GameObject Scenario in SubScenariosObjects)
        {
            Scenario.GetComponent<PolygonCollider2D>().enabled = true;
        }
    }
}
