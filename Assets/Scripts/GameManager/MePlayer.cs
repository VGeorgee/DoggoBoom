
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Proyecto26;    


public class MePlayer : Player {

    protected override void Start(){
        base.Start();
        
        Debug.Log("ME PLAYER IS INSTANTIATED!!!");
    }


    public void SetPlayerPutCard(){
        if(activeCard != null && !activeCard.isLifeCard){
            this.activateCard = true;
        }
    }
    public void SetPlayerDrawCard(){
        this.finishedDrawCardTurn = true;
    }
    
    public void SetPlayerKillTurn(){
        this.finishedKillTurn = true;
    }

    public override string NotifyUser(){
        Debug.Log("ME PLAYER");
        return userName;
    }


    public Card ChangeSelectedCard(int change){
        if(GetNumberOfCards() > 0){
            activeCardIndex += change;
            if(activeCardIndex < 0){
                activeCardIndex = GetNumberOfCards() - 1;
            }
            if(activeCardIndex >= GetNumberOfCards()){
                activeCardIndex = 0;
            }
            activeCard = cards[activeCardIndex];
        } else {
            activeCard = null;
        }
        return activeCard;
    }

}