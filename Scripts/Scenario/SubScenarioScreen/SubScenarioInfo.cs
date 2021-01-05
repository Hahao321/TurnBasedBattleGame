using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubScenarioInfo : MonoBehaviour
{
    public bool selected;
    public Manager manager;
    public int SubNumber;

    private void Update()
    {
        selectedSubScenario();
    }
    void selectedSubScenario()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                foreach (GameObject scen in manager.SubScenariosObjects)
                {
                    if (hit.transform.position == scen.transform.position)
                    {
                        scen.GetComponent<SubScenarioInfo>().selected = true;
                    }
                }
            }

        }
    }
}
