using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Proyecto26;    
using RSG;
using System.Net;

    [Serializable]
    public class User {
        public string username;
        public string password;

        public override String ToString(){
            return username + password;
        }
    }

    [Serializable]
    public class MessageResponse {
        public string message;

        public override String ToString(){
            return message;
        }
    }

    [Serializable]
    public class LeaderboardData {
        public string username;
        public int points;

        public override String ToString(){
            return "{" + username + " " + points + "}";
        }
    }

    [Serializable]
    public class MultiplayerData {
        public string username;
        public string ip;

        public override String ToString(){
            return "{" + username + " " + ip + "}";
        }
    }

public class Request : MonoBehaviour
{

    public bool local = true;

    //"http://localhost";
    public static string domain = "http://192.168.1.2:80";
    public static string api = "/api";

    public InputField usernameField;
    public InputField passwordField;
    public InputField debugField;
    public Text buttonText;

    public static string login = domain + api + "/login";
    public static string register = domain + api + "/register";
    public static string leaderboard = domain + api + "/leaderboard";


    public void DoIt(){
        if(buttonText.text == "Register"){
            Debug.Log("Register");
            Register();
        }
        else if(buttonText.text == "Login"){
            Debug.Log("Register");
            Login();
        }
    }
    public void Login(){
        //Debug.Log("fields: " + usernameField.text + " " + passwordField.text);
        //StartCoroutine(LoginRequest(usernameField.text ?? "asd", passwordField.text ?? "bsd"));
        LoginRequest(usernameField.text, passwordField.text);
    }

    public void Register(){
        //Debug.Log("fields: " + usernameField.text + " " + passwordField.text);
        RegisterRequest(usernameField.text, passwordField.text);
    }

    public void DebugMe(){
        LoginRequest(usernameField.text, passwordField.text);
        /*
        Debug.Log("send");
        RestClient.Post<RegistrationResponse>(login, new User {
            username = "asd",
            password = "asd"
        }).Then(response => {
            Debug.Log(response.message);
            debugField.text = response.message;
        }).Catch(err => {
           debugField.text = err.Message;
        });
        */
    }
    public void RegisterRequest(string username, string password) {
        RestClient.Post<MessageResponse>(register, new User {
            username = username,
            password = password
        }).Then(response => {
            Debug.Log(response.message);
            debugField.text = response.message;
        }).Catch(err => {
           debugField.text = err.Message;
        });
    }
    public void LoginRequest(string username, string password) {
        RestClient.Post<MessageResponse>(login, new User {
            username = username,
            password = password
        }).Then(response => {
            Debug.Log(response.message);
            debugField.text = response.message;

            if(response.message == "SUCCESS"){
                StaticData.username = username;
                SceneLoaderScript.loadlevel(1);
            }

        }).Catch(err => {
           debugField.text = err.Message;
        });
    }
    public void GetLeaderboardData(){
        RestClient.GetArray<LeaderboardData>(leaderboard)
        .Then(response => {
            string s = "arr: ";
            foreach(LeaderboardData t in response ){
                s += t.ToString();
            }
            debugField.text = JsonHelper.ArrayToJsonString<LeaderboardData>(response);
            return response;
        }).Catch(err => {
            Debug.Log(err.Message);
           debugField.text = err.Message;
        });
    }

    public void UpdateLeaderboard(string username, int points){
        RestClient.Post<MessageResponse>(leaderboard, new LeaderboardData{
            username = username,
            points = points
        })
        .Then(response => {
            Debug.Log(response.message);
            //Debug.Log(JsonHelper.ArrayToJsonString<LeaderboardData>(response));
            debugField.text = response.message;
            return response;
        }).Catch(err => {
            Debug.Log(err.Message);
           debugField.text = err.Message;
        });
    }
        public static void UpdateLeaderboardData(string username, int points){
        RestClient.Post<MessageResponse>(leaderboard, new LeaderboardData{
            username = username,
            points = points
        })
        .Then(response => {
            Debug.Log(response.message);
        }).Catch(err => {
            Debug.Log(err.Message);
        });
    }
}
