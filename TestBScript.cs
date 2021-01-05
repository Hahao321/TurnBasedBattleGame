using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TestBScript : MonoBehaviour
{
    
    public void NextScene()
    {
        Debug.Log("next scene func called");
        SceneManager.LoadScene("BattleScene");
    }
    public void ToSpawn()
    {
        BigData bigD = GameObject.Find("LocData").GetComponent<BigData>();
        bigD.FillBattleDataList("Scenario 1", 3, "efews", 10);
    }
}
