using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class Request : MonoBehaviour
{

    public static bool local = true;

    public static string domain = "http://localhost";
    public static string api = "/api";

    public InputField usernameField;
    public InputField passwordField;

    public static string login = domain + api + "/login";
    public static string leaderboard = domain + api + "/leaderboard";


    void Start() {
        StartCoroutine(LoginRequest("username", "password"));
    }
 
    public void Login(){
        Debug.Log("fields: " + usernameField.text + " " + passwordField.text);
        StartCoroutine(LoginRequest(usernameField.text, passwordField.text));
    }

    IEnumerator GetText() {
        UnityWebRequest www = UnityWebRequest.Get(leaderboard);
        yield return www.SendWebRequest();
 
        if(www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
        }
        else {
            Debug.Log(www.downloadHandler.text);
        }
    }



    IEnumerator LoginRequest(string username, string password) {
        WWWForm form = new WWWForm();
        form.AddField( "username", username);
        form.AddField( "password", password);
 
        UnityWebRequest www = UnityWebRequest.Post(login, form);
        yield return www.SendWebRequest();

        if(www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
        }
        else {
            Debug.Log(www.downloadHandler.text);
        }
    }

}
