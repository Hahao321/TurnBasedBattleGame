using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioInfo : MonoBehaviour
{
    public string SceneNameToLoad;
    public string TextName;
    public ScenarioManager Manager;
    public bool selected = false;


    private void Update()
    {
        selectedScenario();
    }


    void selectedScenario()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                foreach (GameObject scen in Manager.ScenariosObjects)
                {
                    if (hit.transform.position == scen.transform.position)
                    {
                        scen.GetComponent<ScenarioInfo>().selected = true;
                    }
                }
            }

        }
    }


}
