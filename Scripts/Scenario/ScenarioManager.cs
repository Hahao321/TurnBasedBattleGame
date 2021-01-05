using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScenarioManager : MonoBehaviour
{
    // Start is called before the first frame update
   public List<ScenarioInfo> Scenarios = new List<ScenarioInfo>();
   public List<GameObject> ScenariosObjects = new List<GameObject>();
    public Canvas Panel;
    public string ScenetoLoad = "";
  

    private void Awake()
    {
        TurnColidersOn();
        Panel.enabled = false;
    }

    private void Update()
    {
        CheckIfSelected();

      
    }
    public void TurnColidersOff()
    {
        foreach (GameObject Scenario in ScenariosObjects)
        {
            Scenario.GetComponent<PolygonCollider2D>().enabled = false;
        }
    }
    public void TurnColidersOn()
    {
        foreach (GameObject Scenario in ScenariosObjects)
        {
            Scenario.GetComponent<PolygonCollider2D>().enabled = true;
        }
    }

    public string[] CheckIfSelected()
    {
        string[] returnArray = {"","" };
        string x = "";

        foreach(GameObject Scenario in ScenariosObjects)
        {
            if (Scenario.GetComponent<ScenarioInfo>().selected)
            {
                Scenario.GetComponent<ScenarioInfo>().selected = false;
                x = Scenario.GetComponent<ScenarioInfo>().SceneNameToLoad;
                returnArray[0] = x;
                returnArray[1] = Scenario.GetComponent<ScenarioInfo>().TextName;
                TurnColidersOff();

                //call function that turns on panel
                TurnPanelOn(returnArray);
            }
            
        }
        return returnArray;
    }

    private void TurnPanelOn(string[] x)
    {
        ScenetoLoad = x[0];
        string a = "Do you want to travel to ";
        // in between add textName
        string b = "?";

        string PanelText = a + x[1] + b;

        Panel.transform.Find("AreYouSurePanel").transform.Find("PanelText").GetComponent<TextMeshProUGUI>().text = PanelText;

        Panel.enabled = true;
    }

    public void ConfirmButton()
    {
        //play zoom animation
        //couroutine wait or w.e
        SceneManager.LoadScene(ScenetoLoad);
       
    }
    public void CancelButton()
    {
        TurnColidersOn();
        Panel.enabled = false;
    }

    public void BackButtonHomeScreen()
    {
        SceneManager.LoadScene("HomeScreen");
    }
}
