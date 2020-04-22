using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HardAI : AI {
   
    private bool attackedbool;
    public HardAI (Cards deck) : base(deck){
        Debug.Log("HARD AI CREATED");
    }

    public override bool ShouldAttack(){
        bool winnable = CalculateMiniMax(true, this.numberOfMoves, this.numberOfAttackCards, 1, this.lives, 1, 0);
        Debug.Log("ability to win:" + winnable + " have to attack: " + attackedbool);
        return (this.lives == 1 && GetCardAtIndex(0).isBombCard) || attackedbool;
    }

    public Card GetCardAtIndex(int index){
        index = index >= deck.GetCards().Count ? deck.GetCards().Count - 1 : index;
        return deck.GetCards()[index];
    }

    private bool GenerateWinner(bool AIMoves, int movesLeft, int AIattacksLeft, int HUAttacksLeft, int AIHealth, int HUHealth){
       
        int attacks = AIattacksLeft + HUAttacksLeft + movesLeft;
        if(AIMoves){
            if(AIattacksLeft > HUAttacksLeft) {
                HUHealth -= attacks;
            }
            else {
                AIHealth -= attacks;
            }
        }
        else{
            if(AIattacksLeft < HUAttacksLeft) {
                AIHealth -= attacks;
            }
            else {
                HUHealth -= attacks;
            }
        }
        return AIHealth > HUHealth;
    }

   
    private bool CalculateMiniMax(bool AIMoves, int movesLeft, int AIattacksLeft, int HUAttacksLeft, int AIHealth, int HUHealth, int index){

        if(AIHealth < 1){
            return false;
        }
        if(HUHealth < 1){
            return true;
        }
    
        if(index  >= deck.GetCards().Count - 1){
            attackedbool = true;
            return GenerateWinner(AIMoves, movesLeft, AIattacksLeft, HUAttacksLeft, AIHealth, HUHealth);
        }
        
        if(AIMoves){
            bool winAtAttack = false;
            if(AIattacksLeft > 0){
                winAtAttack = CalculateMiniMax(!AIMoves, movesLeft + 1,  AIattacksLeft - 1, HUAttacksLeft, AIHealth, HUHealth, index);
            }
            
            if(GetCardAtIndex(index).isAttackCard){
                AIattacksLeft++;
            }
            if(GetCardAtIndex(index).isLifeCard){
                AIHealth++;
            }
            if(GetCardAtIndex(index).isBombCard){
                AIHealth--;
            }
           
            bool winAtDraw = CalculateMiniMax(movesLeft == 1 ? !AIMoves : AIMoves, movesLeft == 1 ? 1 : movesLeft - 1 ,  AIattacksLeft, HUAttacksLeft, AIHealth, HUHealth, index + 1);
            attackedbool = winAtDraw ? false : winAtAttack;
            return winAtAttack || winAtDraw;
            
        } else {
            
            bool winAtAttack = true; 
            if(HUAttacksLeft > 0){
                winAtAttack = CalculateMiniMax(!AIMoves, movesLeft + 1,  AIattacksLeft, HUAttacksLeft - 1, AIHealth, HUHealth, index);
            }
            
            if(GetCardAtIndex(index).isAttackCard){
                HUAttacksLeft++;
            }
            if(GetCardAtIndex(index).isLifeCard){
                HUHealth++;
            }
            if(GetCardAtIndex(index).isBombCard){
                HUHealth--;
            }
            
            bool winAtDraw = CalculateMiniMax(movesLeft == 1 ? !AIMoves : AIMoves, movesLeft == 1 ? 1 : movesLeft - 1 ,  AIattacksLeft, HUAttacksLeft, AIHealth, HUHealth, index + 1);   
            return !(winAtAttack == false || winAtDraw == false);
        }
    }

}