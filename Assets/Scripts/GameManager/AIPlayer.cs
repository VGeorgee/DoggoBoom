
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Proyecto26;    


public class AIPlayer : Player{


    private Cards deck;
    int numberOfMovesLeft;

    protected override void Start(){
        base.Start();
        Debug.Log("AI IS INSTANTIATED!!!");
    }

    public override IEnumerator DrawCardFromDeck(){
        finishedDrawCardTurn = true;
        yield break;
    }

    private bool GetAIDecision(){
        return Random.Range(0.0f, 100.0f) < 50.0f;  
    }
    public IEnumerator LetActivationOfSelectedCard2(){

        if(GetNumberOfCards() == 0){
            yield break;
        }
        
        yield return new WaitForSeconds(Random.Range(0.5f, 3.5f));



        activateCard = true;
        //finishedDrawCardTurn = true;
    }

    public override IEnumerator LetActivationOfSelectedCard(){
        
        yield return new WaitForSeconds(Random.Range(0.5f, 3.5f));

        if(activeCard == null){
            SetLastCardActive();
        }

        if(activeCard != null && !activeCard.isLifeCard)
            activateCard = true;

        finishedDrawCardTurn = true;
    }
   protected override IEnumerator OnKillPlayer(bool isKilled){
       this.finishedKillTurn = true;
        yield break;
    }

    public bool HaveAttackCard(){
        return cards.Find(x => x.isAttackCard == true) != null;
    }

    public void SetActiveCardAttackCard(){
        activeCardIndex = cards.FindIndex(x => x.isAttackCard == true);
        Debug.Log("INDEX OF ATTACK CARD FOUND:::: " + activeCardIndex);
        activeCard = cards[activeCardIndex];
    }


    public override string NotifyUser(){
        Debug.Log("I AM AI PLAYER!!!!!!!+++++++_____");
        return userName;
    }

    public void InitializeAI(Cards deck){
        this.deck = deck;
    }

    public void UpdateAI(int numberOfMovesLeft){
        this.numberOfMovesLeft = numberOfMovesLeft;
    }


}