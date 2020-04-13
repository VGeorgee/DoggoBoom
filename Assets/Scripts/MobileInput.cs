using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MobileInput : MonoBehaviour
{


    public RectTransform panel;
    void Start(){
        if(panel){
            Debug.Log("Panel activated");
            panel.gameObject.SetActive(true);
        }
    }

    
    private TouchScreenKeyboard keyboard;
    public void OpenKeyboard(){
        Debug.Log("KEYBOARD OPENING!"); 
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, true, true, true);
    }
    
}
