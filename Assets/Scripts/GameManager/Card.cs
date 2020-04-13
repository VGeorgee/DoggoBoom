
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Proyecto26;    


public class Card{
    
    public string name;
    
    public bool isLifeCard;
    
    public bool isAttackCard;

    public bool isBombCard;

    public bool isUselessCard;

    public Card(string line){
        string[] splitter = line.Split(',');
        //Debug.Log(splitter.Length);
        this.name = splitter[0];
        switch (splitter[1])
        {
            case "LIFE": {isLifeCard = true; break;}
            case "BOMB": {isBombCard = true; break;}
            case "ATTACK": {isAttackCard = true;  break;}   
            default: {isUselessCard = true; break; }
        }
    }

    public Card(){

    }

    public override string ToString(){
        return name + " islife:" + isLifeCard + "  isBomb:" + isBombCard + "  isAttack:" + isAttackCard +  "  isUseless:" + isUselessCard;
    }
}