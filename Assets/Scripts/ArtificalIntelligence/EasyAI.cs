using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EasyAI : AI {
    public EasyAI (Cards deck) : base(deck){
        Debug.Log("EASY AI CREATED");
    }
    public override bool ShouldAttack(){
        return Random.Range(0.0f, 1.0f) < deck.GetChanceOfBomb();
    }
}