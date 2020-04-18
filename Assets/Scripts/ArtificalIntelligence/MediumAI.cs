using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MediumAI : AI {
   
    public MediumAI (Cards deck) : base(deck){
        Debug.Log("MEDIUM AI CREATED");
    }

    public override bool ShouldAttack(){
        if(deck.GetCards()[0].isBombCard){
            return true;
        }
        return Random.Range(0.0f, 1.0f) < deck.GetChanceOfBomb(); 
    }
    
}