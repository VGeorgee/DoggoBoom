using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Proyecto26;    


public class AIPlayer : Player{

    private Cards deck;
    int numberOfMovesLeft;
    private AI AIInstance;

    protected override void Start(){
        base.Start();
        Debug.Log("AI IS INSTANTIATED!!!");

    }

    public void InitiateAI(Cards deck){
        this.deck = deck;
        switch(StaticData.GetInstance().AI){
            case 1: { AIInstance = new EasyAI(deck); break; }
            case 2: { AIInstance = new MediumAI(deck); break; }
            case 3: { AIInstance = new HardAI(deck); break; }
            default: { break; }
        }
    }
   
    public void UpdateAI(int numberOfMoves = 1){
        AIInstance.UpdateAI(GetNumberOfLifeCards() + 1, numberOfMoves, GetNumberOfAttackCards());
    }

    public override IEnumerator DrawCardFromDeck(){
        finishedDrawCardTurn = true;
        yield break;
    }

    public override IEnumerator LetActivationOfSelectedCard(){

        yield return new WaitForSeconds(Random.Range(0.5f, 3.5f));
        if(HaveAttackCard()){
            if(AIInstance.ShouldAttack()){
                SetActiveCardAttackCard();
                if(activeCard != null && !activeCard.isLifeCard){
                    activateCard = true;
                    Debug.Log("AI GONNA ATTACK!");
                }
                
            } else {
                    Debug.Log("AI'nt GONNA ATTACK!");
            }
        }
        finishedDrawCardTurn = true;
        yield break;
    }

   protected override IEnumerator OnKillPlayer(bool isKilled){
       this.finishedKillTurn = true;
        yield break;
    }

    private bool HaveAttackCard(){
        return cards.Find(x => x.isAttackCard == true) != null;
    }
    private void SetActiveCardAttackCard(){
        activeCardIndex = cards.FindIndex(x => x.isAttackCard == true);
        activeCard = cards[activeCardIndex];
    }

    private int GetNumberOfLifeCards(){
        int numberOfLifeCards = 0;
        for(int i = 0; i < deck.GetCards().Count; i++){
            if(deck.GetCards()[i].isLifeCard){
                numberOfLifeCards++;
            }
        }
        return numberOfLifeCards;
    }


    public override string NotifyUser(){
        Debug.Log("AI PLAYER");
        return userName;
    }

}