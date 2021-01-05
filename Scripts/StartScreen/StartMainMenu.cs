using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class StartMainMenu : MonoBehaviour
{

    public GameObject RegisterImage;
    public GameObject LoginImage;

    public int currentMoney;
    public string currentUser;

    public TextMeshProUGUI usernametext;
    private void Update()
    {
        currentMoney = GameData.Money;
        currentUser = GameData.Username;



        if (GameData.Username == null)
        {
            usernametext.text = "";
        }
        else
        {
            usernametext.text = GameData.Username;
        }
    }
    public void GoToRegister()
    {
        RegisterImage.SetActive(true);
        LoginImage.SetActive(false);
    }
    public void GoToLogin()
    {
        RegisterImage.SetActive(false);
        LoginImage.SetActive(true);
    }
}
