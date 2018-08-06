using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginSystem : MonoBehaviour
{

    public string username, email, password;
    //public bool errorGUI;
    public float scrW, scrH;
    public string errorMessage;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            errorMessage = "";
            Debug.Log("Sending...");
            StartCoroutine(CreateAccount(username, email, password));
        }
    }

    private void OnGUI()
    {
        scrW = Screen.width / 16;
        scrH = Screen.height / 9;
        if (errorMessage != "")
        {
            GUI.Box(new Rect(scrW * 5, scrH * 4, scrW * 6, scrH * 1), errorMessage);

            if (GUI.Button(new Rect(scrW * 10, scrH * 4, scrW * 1, scrH * 1), "X"))
            {
                //errorGUI = false;
                errorMessage = "";
            }
        }
    }

    IEnumerator CreateAccount(string username, string email, string password)
    {
        string createUserURL = "http://localhost/sqlsystem/createuser.php";
        WWWForm user = new WWWForm();
        user.AddField("username_Post", username);
        user.AddField("email_Post", email);
        user.AddField("password_Post", password);
        WWW www = new WWW(createUserURL, user);
        yield return www;
        Debug.Log(www.text);
        if (www.text != "Create User")
        {
            errorMessage = www.text;
           // errorGUI = true;
        }
    }












}
