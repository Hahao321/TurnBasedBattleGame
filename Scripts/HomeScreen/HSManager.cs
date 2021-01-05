using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HSManager : MonoBehaviour
{
    // Start is called before the first frame update
   public void ScenarioScreen()
    {
        SceneManager.LoadScene("ScenarioScreen");
    }
    public void StorageScreen()
    {
        SceneManager.LoadScene("EquipmentStorageScreen");

    }
    public void MonsterStorageScreen()
    {
        SceneManager.LoadScene("MonsterStorageScreen");

    }
}
