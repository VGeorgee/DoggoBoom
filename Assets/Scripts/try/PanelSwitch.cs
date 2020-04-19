
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Proyecto26;    


public class PanelSwitch : MonoBehaviour {
    Vector3 [] move;
    int PanelPosition;
    RectTransform rt;
    public void Start(){
        rt = this.GetComponent<RectTransform>();
        move = new Vector3[3];
        PanelPosition = 0;
        move[0] = new Vector3(-2880, 0, 0);
        move[1] = new Vector3(-1440, 0, 0);
        move[2] = new Vector3(0, 0, 0);
        rt.position = move[1];
    }


    public void SwitchToMainPanel(){
        rt.position = move[1];
    }
    public void SwitchToRightPanel(){
        rt.position = move[0];
    }
    public void SwitchToLeftPanel(){
        rt.position = move[2];
    }
}