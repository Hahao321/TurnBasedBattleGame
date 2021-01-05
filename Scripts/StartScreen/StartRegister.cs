using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class StartRegister : MonoBehaviour
{
    public InputField nameField;
    public InputField passwordField;

    public Button SubmitRegisterButton;

    public StartMainMenu Manager;



    public void CallRegister()
    {
        StartCoroutine(Register());
    }
    IEnumerator Register()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", nameField.text);
        form.AddField("password", passwordField.text);

        WWW www = new WWW("http://localhost/sqlconnect/register.php", form);


        yield return www; //will wait until we get info back from www, this is why we using coroutine
        if(www.text == "0")
        {
            Debug.Log("User Created");
            Manager.RegisterImage.SetActive(false);

            //load game scene
            GameData.Username = nameField.text;
            GameData.Money = int.Parse(www.text.Split('\t')[1]);    //takes second string in feedback from login php, turns it to int, stores it in money variable

            //load in character info

            SceneManager.LoadScene("MoneyTestScreen");
        }
        else
        {
            Debug.Log("User creation Failed, Error # " + www.text);
        }
    }

    public void VerifyInputs()
    {
        SubmitRegisterButton.interactable = (nameField.text.Length > 7 && nameField.text.Length < 17 && passwordField.text.Length > 7 && passwordField.text.Length < 17);
    }

}
