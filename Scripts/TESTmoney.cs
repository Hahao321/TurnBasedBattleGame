using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TESTmoney : MonoBehaviour
{
    public TextMeshProUGUI Ctext;
    public GameObject CtextGo;
    public string CurrentUsername;
    public int CurrentMoney;



    private void Awake()
    {
        Ctext = GameObject.Find("Main Camera").transform.Find("Canvas").transform.Find("Text1").GetComponent<TextMeshProUGUI>();

        CurrentMoney = GameData.Money;
        CurrentUsername = GameData.Username;

        if (GameData.Username != null)
        {
            Ctext.text = GameData.Username + "has " + GameData.Money + "money";
        }
        else
            Ctext.text = "";
    }

    private void Update()
    {
        if (GameData.Username != null)
            Ctext.text = GameData.Username + "has " + GameData.Money + "money";
        else
            Ctext.text = "";

        CurrentMoney = GameData.Money;
        CurrentUsername = GameData.Username;

    }

    public void callSaveData()
    {
        StartCoroutine(SavePlayerData());
    }

    IEnumerator SavePlayerData()
    {
        WWWForm form = new WWWForm();

        form.AddField("name", GameData.Username);
        form.AddField("money", GameData.Money);

        WWW www = new WWW("http://localhost/sqlconnect/savedata.php", form);
        yield return www;

        if(www.text == "0")
        {
            Debug.Log("Game Saved");
        }
        else
        {
            Debug.Log("Save Failed, Error #" + www.text);
        }

        GameData.LogOut();
        SceneManager.LoadScene("StartScreen");
    }


    public void MakeMonay()
    {
        GameData.Money += 1000;
    }
}
