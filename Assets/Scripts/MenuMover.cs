using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MenuMover : MonoBehaviour
{
    
    public Text buttonText;
    public InputField usernameField;
    public InputField passwordField;
    public GameObject panel;
    public bool pageLoaded;
    RectTransform rt;
    Vector3 left;
    Vector3 right;
    public bool isLoading;

    float speed = 4.0f;
    
    void Start(){
        rt = panel.GetComponent<RectTransform>();
        left = rt.position;
        right = new Vector3(0.0f, rt.position.y);
        pageLoaded = true;
        isLoading = false;
    }

    public void EmptyInputFields(){
        if(usernameField != null)
            usernameField.text = "";
        if(passwordField != null)
            passwordField.text = "";
    }

    public void LoadLoginPage(){
        buttonText.text = "Login";
        EmptyInputFields();
        MovePanel();
    }
    public void LoadRegisterPage(){
        buttonText.text = "Register";
        EmptyInputFields();
        MovePanel();
    }

    public void LoadOriginalPage(){
        MovePanel();
    }

    public void MovePanel(){     
        isLoading = true;
        pageLoaded = !pageLoaded;
    }

    void Update () {
        if(isLoading){
            float step = (speed * Time.deltaTime);
            if(!pageLoaded){
                rt.position = Vector3.Lerp(rt.position, right, step);
                if(rt.position == right){
                    isLoading = false;
                }
            } else{
                rt.position = Vector3.Lerp(rt.position, left, step); 
                if(rt.position == left){
                    isLoading = false;
                }
            }
        }
    }
}
