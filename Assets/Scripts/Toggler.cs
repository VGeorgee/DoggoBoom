
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class Toggler : MonoBehaviour {

    private bool active;
    public void Start(){
        active = false;
        this.gameObject.SetActive(active);
    }

    public void Toggle(){
        this.gameObject.SetActive(active = !active);
    }

}