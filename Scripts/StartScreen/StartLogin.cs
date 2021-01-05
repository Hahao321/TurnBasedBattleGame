using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StartLogin : MonoBehaviour
{
    public InputField nameField;
    public InputField passwordField;

    public Button SubmitLoginButton;

    public StartMainMenu Manager;

    public void CallLogin()
    {
        StartCoroutine(LoginPlayer());
    }

    IEnumerator LoginPlayer()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", nameField.text);
        form.AddField("password", passwordField.text);

        WWW www = new WWW("http://localhost/sqlconnect/login.php", form);
        yield return www; //will wait until we get info back from www, this is why we using coroutine

        if (www.text[0] == '0') //we pass 0 if no error. single quotes for char
        {
            Debug.Log("user set to " + nameField.text);
            GameData.Username = nameField.text;
            GameData.Money = int.Parse(www.text.Split('\t')[1]);    //takes second string in feedback from login php, turns it to int, stores it in money variable

            Debug.Log("user is " + GameData.Username);
            //load game scene
            GameData.Username = nameField.text;

            //load in character info

            SceneManager.LoadScene("MoneyTestScreen");

        }
        else
        {
            Debug.Log("User Login Failed, Error #" + www.text);
        }


        yield return www; //will wait until we get info back from www, this is why we using coroutine
    }
    public void VerifyInputs()
    {
        SubmitLoginButton.interactable = (nameField.text.Length > 7 && nameField.text.Length < 17 && passwordField.text.Length > 7 && passwordField.text.Length < 17);
    }

}
