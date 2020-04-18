using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AI {

    protected Cards deck;
    protected int lives;
    protected int numberOfMoves;
    protected int numberOfAttackCards;
    public AI (Cards deck){
        this.deck = deck;
    }

    public abstract bool ShouldAttack();
    public void UpdateAI(int lives = 1, int numberOfMoves = 1, int numberOfAttackCards = 0){
        this.lives = lives;
        this.numberOfMoves = numberOfMoves;
        this.numberOfAttackCards = numberOfAttackCards;
    }

}