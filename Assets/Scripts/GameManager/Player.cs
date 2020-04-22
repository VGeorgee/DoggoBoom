
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Proyecto26;    


public abstract class Player : MonoBehaviour {
    public string userName;

    public List<Card> cards;

    public bool finishedDrawCardTurn;
    public bool activateCard;
    public bool finishedKillTurn;
    public Card activeCard;
    public int activeCardIndex;

    public bool isAlive;

    public bool isActivaPlayer;

    protected virtual void Start(){
        cards = new List<Card>();
        isAlive = true;
        activeCardIndex = 0;
        Debug.Log("PLAYER IS INSTANTIATED!!!");
    }
    
    public virtual IEnumerator DrawCardFromDeck(){
        yield return new WaitUntil(() => this.finishedDrawCardTurn == true);
    }
    
    public virtual IEnumerator LetActivationOfSelectedCard(){
        
        yield return new WaitUntil(() => this.activateCard == true || this.finishedDrawCardTurn == true);
    }

    public Card RemoveActiveCard(){
        if(activeCard != null){
            Debug.Log("ACTIVATED CARD HERE:"+ userName + " " + activeCard);
            Card toRemove = activeCard;
            cards.RemoveAt(activeCardIndex);
            SetLastCardActive();
            return toRemove;
        }
        return null;
    }
    
    public IEnumerator KillPlayer(){
        Card lifeCard = cards.Find(element => element.isLifeCard == true);
        if(lifeCard != null){
            cards.Remove(lifeCard);
        }
        else {
            isAlive = false;
        }
        SetLastCardActive();
        StartCoroutine(OnKillPlayer(lifeCard == null));
        yield break;
    }

    protected abstract IEnumerator OnKillPlayer(bool isKilled);

    public void StartTurn(){
        finishedDrawCardTurn = false;
        activateCard = false;
        finishedKillTurn = false;
    }

    public void AddCard(Card card){
        if(card != null){
            cards.Add(card);
        }
    }

    public int GetNumberOfCards(){
        return cards.Count;
    }

    public void SetLastCardActive(){
        if(GetNumberOfCards() > 0){
            activeCardIndex = GetNumberOfCards() - 1;
            activeCard = cards[activeCardIndex];
        }
        else{
            activeCardIndex = 0;
            activeCard = null;
        }
    }

    public int GetNumberOfAttackCards(){
        int numberOfAttackCards = 0;
        for(int i = 0; i < cards.Count; i++){
            if(cards[i].isAttackCard){
                numberOfAttackCards++;
            }
        }
        return numberOfAttackCards;
    }


    public abstract string NotifyUser();
}