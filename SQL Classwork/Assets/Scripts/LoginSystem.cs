using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginSystem : MonoBehaviour
{

    public string username, email, password, repeatPassword;
    //public bool errorGUI;
    public float scrW, scrH;
    public string errorMessage;

    public bool mainMenu = true;
    public bool login, newUser;



    private void Start()
    {
        mainMenu = true;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Return))
        {
            errorMessage = "";
            Debug.Log("Sending...");
            StartCoroutine(CreateAccount(username, email, password));
        }
        */
    }

    private void OnGUI()
    {
        scrW = Screen.width / 16;
        scrH = Screen.height / 9;

        // main background
        GUI.Box(new Rect(scrW, scrH, scrW * 14, scrH * 7), "");

        if (mainMenu)
        {
            if (GUI.Button(new Rect(scrW * 2, scrH * 2, scrW * 5, scrH * 5), "Login"))
            {
                login = true;
                mainMenu = false;
            }

            if (GUI.Button(new Rect(scrW * 9, scrH * 2, scrW * 5, scrH * 5), "Create User"))
            {
                newUser = true;
                mainMenu = false;
            }

        }

        if (login)
        {
            GUI.Box(new Rect(scrW * 6.5f, scrH * 1.5f, scrW * 3, scrH), "Login");

            GUI.Box(new Rect(scrW * 4, scrH * 3.5f, scrW, scrH), "Username");
            username = GUI.TextField(new Rect(scrW * 5.5f, scrH * 3.5f, scrW * 5, scrH), username);

            GUI.Box(new Rect(scrW * 4, scrH * 5.5f, scrW, scrH), "Password");
            password = GUI.TextField(new Rect(scrW * 5.5f, scrH * 5.5f, scrW * 5, scrH), password);

            if (GUI.Button(new Rect(scrW * 12, scrH * 4, scrW * 1.5f, scrH), "=>"))
            {
                errorMessage = "";
                Debug.Log("Sending...");
                StartCoroutine(Login(username, password));
            }

            if (GUI.Button(new Rect(scrW * 12, scrH * 6, scrW * 1.5f, scrH), "Menu"))
            {
                login = false;
                mainMenu = true;
                username = password = "";
            }

        }

        if (newUser)
        {
            GUI.Box(new Rect(scrW * 6.5f, scrH * 1.5f, scrW * 3, scrH), "Create User");

            GUI.Box(new Rect(scrW * 3, scrH * 3f, scrW * 2, scrH * 0.75f), "Username");
            username = GUI.TextField(new Rect(scrW * 5.5f, scrH * 3f, scrW * 5, scrH * 0.75f), username);

            GUI.Box(new Rect(scrW * 3, scrH * 4.25f, scrW * 2, scrH * 0.75f), "Email");
            email = GUI.TextField(new Rect(scrW * 5.5f, scrH * 4.25f, scrW * 5, scrH * 0.75f), email);

            GUI.Box(new Rect(scrW * 3, scrH * 5.5f, scrW * 2, scrH * 0.75f), "Password");
            password = GUI.TextField(new Rect(scrW * 5.5f, scrH * 5.5f, scrW * 5, scrH * 0.75f), password);

            GUI.Box(new Rect(scrW * 3, scrH * 6.75f, scrW * 2, scrH * 0.75f), "Repeat Password");
            repeatPassword = GUI.TextField(new Rect(scrW * 5.5f, scrH * 6.75f, scrW * 5, scrH * 0.75f), repeatPassword);

            if (GUI.Button(new Rect(scrW * 12, scrH * 4, scrW * 1.5f, scrH), "=>"))
            {
                if (password != repeatPassword)
                {
                    errorMessage = "Passwords do not match";
                }
                else
                {
                    errorMessage = "";
                    Debug.Log("Sending...");
                    StartCoroutine(CreateAccount(username, email, password));
                }
            }

            if (GUI.Button(new Rect(scrW * 12, scrH * 6, scrW * 1.5f, scrH), "Menu"))
            {
                newUser = false;
                mainMenu = true;
                username = email = password = repeatPassword = "";
            }

        }



        if (errorMessage != "")
        {
            GUI.Box(new Rect(scrW * 5, scrH * 4, scrW * 6, scrH * 1), errorMessage);

            username = "";
            password = "";
            email = "";
            repeatPassword = "";

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

    // TO DO
    IEnumerator Login(string username, string password)
    {
        string loginURL = "http://localhost/sqlsystem/login.php";
        WWWForm user = new WWWForm();
        user.AddField("username_Post", username);
        user.AddField("password_Post", password);
        WWW www = new WWW(loginURL, user);
        yield return www;
        Debug.Log(www.text);
        if (www.text != "Login")
        {
            errorMessage = www.text;
            // errorGUI = true;
        }
    }










}
