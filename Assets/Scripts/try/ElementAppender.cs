using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Proyecto26;    


public class ElementAppender : MonoBehaviour {

    public static string domain = "http://192.168.1.2:80";
    public static string api = "/api";
    public static string leaderboard = domain + api + "/leaderboard";
    public List<Sprite> Sprites = new List<Sprite>(); //List of Sprites added from the Editor to be created as GameObjects at runtime
    public GameObject ParentPanel; //Parent Panel you want the new Images to be children of
    public GameObject imageToUse;

    public GameObject lastElement;

    public void AddNewElement(){
        var newobj = Instantiate(imageToUse, lastElement.transform, lastElement.transform);
        newobj.transform.SetParent(ParentPanel.transform);
        
        newobj.transform.parent = lastElement.transform;
        newobj.transform.position = lastElement.transform.position;
        RectTransform rt = (RectTransform)newobj.transform;
        rt.position +=  new Vector3(0, -rt.rect.height, 0);
        lastElement = newobj;
    }
    
    public void AddNewElement(LeaderboardData data){
        var newobj = Instantiate(imageToUse, lastElement.transform, lastElement.transform);
        newobj.transform.SetParent(ParentPanel.transform);
        
        newobj.transform.parent = lastElement.transform;
        newobj.transform.position = lastElement.transform.position;
        RectTransform rt = (RectTransform)newobj.transform;
        rt.position +=  new Vector3(0, -rt.rect.height, 0);
        lastElement = newobj;

        Text []texts = newobj.GetComponentsInChildren<Text> (); 

        texts[0].text = data.username;
        texts[1].text = data.points + "";


    }

    public void setInitialElement(LeaderboardData data){
        var newobj = Instantiate(imageToUse, ParentPanel.transform, ParentPanel.transform);
        newobj.transform.SetParent(ParentPanel.transform);
        
        newobj.transform.parent = ParentPanel.transform;
        newobj.transform.position = ParentPanel.transform.position;
        RectTransform rt = (RectTransform)newobj.transform;
        rt.position +=  new Vector3(0, -rt.rect.height/2, 0);

        lastElement = newobj;

        Text []texts = newobj.GetComponentsInChildren<Text> (); 

        texts[0].text = data.username;
        texts[1].text = data.points + "";
    }

    public void LoadElements(){
        RestClient.GetArray<LeaderboardData>(leaderboard)
        .Then(response => {
            
            if(response == null) 
                return;
            setInitialElement(response[0]);
            for (int i = 1; i < response.Length; i++){
                AddNewElement(response[i]);
            }
            
        }).Catch(err => {
            Debug.Log(err.Message);
        });
    }


    void Start () {
        List<LeaderboardData> list = new List<LeaderboardData>();
        list.Add(new LeaderboardData() {
            username = "Henlo",
            points = 1234
        });

        //LoadElements(list);
        /*
        var newobj = Instantiate(imageToUse, ParentPanel.transform, ParentPanel.transform);
        newobj.transform.SetParent(ParentPanel.transform);
        
        newobj.transform.parent = ParentPanel.transform;
        newobj.transform.position = ParentPanel.transform.position;
        RectTransform rt = (RectTransform)newobj.transform;
        rt.position +=  new Vector3(0, -rt.rect.height/2, 0);

        lastElement = newobj;
        */
    }
}