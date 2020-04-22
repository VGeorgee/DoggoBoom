
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Proyecto26;    
using System.IO;

public class Cards{

    List <Card> cards;
    int numberOfLifeCards;
    int numberOfBombCards;
    int numberOfAttackCards;
    
    public Cards(int maxLifeCards, int maxBombCards, int maxAttackCards = 100){
        cards = new List<Card>();
        numberOfLifeCards = 0;
        numberOfBombCards = 0;
        numberOfAttackCards = 0;

        //string path = "Assets/Resources/cards.txt";
        var textFile = Resources.Load<TextAsset>("cards");

        string[] fileLines = textFile.text.Split('\n');

        for(int i = 0; i < fileLines.Length; i++){
            string toUse = fileLines[i].Trim();
            Card c = new Card(toUse);
            if(c.isLifeCard && numberOfLifeCards < maxLifeCards){
                cards.Add(c);
                numberOfLifeCards++;
            }
            else if(c.isBombCard && numberOfBombCards < maxBombCards){
                cards.Add(c);
                numberOfBombCards++;
            }
            else if(c.isAttackCard && numberOfAttackCards < maxAttackCards){
                cards.Add(c);
                numberOfAttackCards++;
            }
            else if(c.isUselessCard){
                cards.Add(c);
            }
            //Debug.Log(c);
        }
        Debug.Log("Finished reading CARDS!");
    }

    public Card GetLifeCard(){
        Card card = null;
        for(int i = 0; i < cards.Count; i++){
            if(cards[i].isLifeCard){
                card = cards[i];
                cards.RemoveAt(i);
                numberOfLifeCards--;
                break;
            }
        }
        return card;
    }

    public Card DrawCard(){
        Card c = null;
        if(cards.Count > 0){
            c = cards[0];
            if(c.isBombCard){
                numberOfBombCards--;
            }
            else if(c.isLifeCard){
                numberOfLifeCards--;
            }
            else if(c.isAttackCard){
                numberOfAttackCards--;
            }
            else if(c.isUselessCard){
                ;
            }
            cards.RemoveAt(0);
        }
        return c;
    }

    public List<Card> GetCards(){
        return cards;
    }

    public Card GetNonBombCard(){
        Card c = null;
        for(int i = 0; i < cards.Count; i++){
            if(!cards[i].isBombCard){
                c = cards[i];
                cards.RemoveAt(i);
                break;
            }
        }
        return c;
    }

    public float GetChanceOfBomb(){
        return numberOfBombCards / (float)cards.Count;
    }
    
    public float GetChanceOfAttackCard(){
        return numberOfAttackCards / (float)cards.Count;
    }


    public void PutDrawnCardBackAtPosition(Card card, int position){
        //TODO;
        return;
    }

    public void PutDrawnCardBack(Card c){
        if(c != null){
            if(cards.Count > 0) {
                int indexForBomb = ((int)(Random.Range(0.0f, 4000.0f))) % cards.Count;  
                Card tmp = cards[indexForBomb];
                cards[indexForBomb] = c;
                cards.Add(tmp);
            }
            else {
                cards.Add(c);
            }
            numberOfBombCards++;
        }
    }

    public void Shuffle(){
        int numbers = cards.Count;
        int [] indexes = new int[numbers];
        for(int i = 0; i < numbers; i++){
            int rnd = ((int)(Random.Range(0.0f, 4000.0f))) % numbers;  
            indexes[i] = rnd;
        }

        for(int i = 0; i < numbers; i++){
            Card tmp;
            tmp = cards[i];
            cards[i] = cards[indexes[i]];
            cards[indexes[i]] = tmp;
        }
    }
}