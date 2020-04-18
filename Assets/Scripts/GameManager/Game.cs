using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Proyecto26;    


public class Game : MonoBehaviour{
    private int movingPlayerIndex;
    private int numberOfLivingPlayers;
    public Player [] players;

    public MePlayer mePlayer;
    public Cards deck;
    public GameObject mep;
    public GameObject AI;
    public Text winnerName;
    public Text numberOfCardsInHand;
    public Text activeCardNumber;
    public Text activatedCardName;
    public Text turnOfPlayer;
    public GameObject endOfGamePanel;
    public GameObject bombPanel;
    public Text chanceForBomb;
    public Image cardInHandImage;
    /// IMAGES
    public Sprite[] lifeCardImage;
    public Sprite[] UselessCardImage;
    public Sprite[] AttackCardImage;
    ///___IMAGES

    void Start(){
        InitScene();
        StartDesiredGame();
    }


    void StartDesiredGame(){
        switch (StaticData.gameplayMode)
        {
            case 0: { StartAIGame();  break; }
            default: break;
        }
    }




    void InitScene(){
        cardInHandImage.sprite = lifeCardImage[0];
        endOfGamePanel.gameObject.SetActive(false);
        bombPanel.gameObject.SetActive(false);
    }

    public void StartAIGame(){       

        players = new Player[2];
        players[0] = mep.GetComponent<MePlayer>();
        players[0].userName = StaticData.username;
        players[1] = AI.GetComponent<AIPlayer>();
        players[1].userName = "AI";

        deck = new Cards(3, 1);
        deck.Shuffle();
        Debug.Log("Finished SHUFFLE!");


        mePlayer = (MePlayer)players[0];

        Debug.Log(players[0].userName);
        movingPlayerIndex = 0;
        numberOfLivingPlayers = 2;
        StartCoroutine(StartGame());
    }

    private void SetChanceForBomb(){
        int percent = (int)( deck.GetChanceOfBomb() * 100);
        chanceForBomb.text = percent + "%";

        if(percent < 30){
            chanceForBomb.color = Color.white;
        }
        else if(percent < 80){
            chanceForBomb.color = Color.yellow;
        }
        else{
            chanceForBomb.color = Color.red;
        }
    }

    private void NextPlayer(){
        movingPlayerIndex++;
        movingPlayerIndex %= players.Length;        
    }

    Player  movingPlayer;

    IEnumerator StartGame(){

        yield return new WaitForSeconds(0.1f);


        for(int i = 0; i < 2; i++){
            Card lifeCard = deck.GetLifeCard();
            if(lifeCard != null){
                players[i].AddCard(lifeCard);
            } else {
                Debug.Log("LIFE CARD NOT FOUND _:_:_:__:_::_:_:_:_:_:_:_:_:_:_:_:_:_:_:_:_:_:_:_:_:_");
            }
        }
        
        
        for(int i = 0; i < 2; i++){
            players[0].AddCard(deck.GetNonBombCard());
            players[1].AddCard(deck.GetNonBombCard());
        }

        players[0].SetLastCardActive();
        players[1].SetLastCardActive();


        ((AIPlayer) players[1]).InitiateAI(deck);
        //SetPlayerCardCounter();
        movingPlayer = players[movingPlayerIndex];
        ShowLastCard();

        int numberOfMoves = 1;
        while(numberOfLivingPlayers > 1){

            movingPlayer = players[movingPlayerIndex];



            while(numberOfMoves > 0 && numberOfLivingPlayers > 1){
                
                
                turnOfPlayer.text = movingPlayer.NotifyUser();
                movingPlayer.StartTurn();
                SetChanceForBomb();
                SetPlayerCardCounter();
                
                if(movingPlayer is AIPlayer){
                    ((AIPlayer)movingPlayer).UpdateAI(numberOfMoves);
                }

                Debug.Log("NUMBER OF MOVES:::::" + numberOfMoves);
                StartCoroutine(movingPlayer.LetActivationOfSelectedCard());
                yield return new WaitUntil(() => movingPlayer.activateCard == true || movingPlayer.finishedDrawCardTurn == true);



                //ChangeSelectedCard(0);
                if(movingPlayer.activateCard == true ){
                    Card activatedCard = movingPlayer.RemoveActiveCard();
                    Debug.Log("ACTIVATED CARD" + movingPlayer.userName + "::: :::: :::: :::: " + activatedCard);

                    if(activatedCard != null){
                        ShowLastCard();
                        SetPlayerCardCounter();

                        if(activatedCard.isAttackCard){
                            Debug.Log("ACTIVATED AN ATTACK CARD, TURN IS OVER!!!");
                            break;
                        }

                    }
                }

                Card drawn = deck.DrawCard();
                StartCoroutine(movingPlayer.DrawCardFromDeck());

                yield return new WaitUntil(() => movingPlayer.finishedDrawCardTurn == true);

                if(drawn.isBombCard){
                    BombExploded();
                    StartCoroutine(movingPlayer.KillPlayer());
                    yield return new WaitUntil(() => movingPlayer.finishedKillTurn == true);
                    
                    if(movingPlayer.isAlive){
                        deck.PutDrawnCardBack(drawn);
                        Debug.Log("PLAYER DRAWN BOMB BUT ALIVE::: " + movingPlayer.userName);
                    } else {
                        numberOfLivingPlayers--;
                        Debug.Log("PLAYER KILLED::: " + movingPlayer.userName);
                    }
                } else {
                    movingPlayer.AddCard(drawn);
                }

                ShowLastCard();
                //ChangeSelectedCard(0);
                numberOfMoves--;
            }

            numberOfMoves++;
            NextPlayer();
        }

        SetWinner(players[movingPlayerIndex].userName);
        endOfGamePanel.gameObject.SetActive(true);
        Request.UpdateLeaderboardData(players[movingPlayerIndex].userName, 100);
        yield break;
    }

    private void SetWinner(string username){
        winnerName.text = username;
    }

    public void PlayerDrawCard(){
        mePlayer.SetPlayerDrawCard();
    }

    private void BombExploded(){
        if(movingPlayer == mePlayer)
            bombPanel.gameObject.SetActive(true);
    }

    public void BombPanelTouched(){
        mePlayer.SetPlayerKillTurn();
        bombPanel.gameObject.SetActive(false);
    }

    public void ChangeSelectedCard(int i){
        //if(movingPlayer != mePlayer) return;

        Card selectedCard = mePlayer.ChangeSelectedCard(i);
        cardInHandImage.gameObject.SetActive(selectedCard != null);
        if(selectedCard != null){
            if(selectedCard.isLifeCard){
                cardInHandImage.sprite = GetRandomSpriteFromArray(lifeCardImage);
                activatedCardName.text = "Life";
            }
            else if(selectedCard.isUselessCard){
                cardInHandImage.sprite = GetRandomSpriteFromArray(UselessCardImage);
                activatedCardName.text = "Useless";
            }
            else if(selectedCard.isAttackCard){
                cardInHandImage.sprite = GetRandomSpriteFromArray(AttackCardImage);
                activatedCardName.text = "Attack";
            }
        }
        SetPlayerCardCounter();
    }

    public void SetPlayerCardCounter(){
        numberOfCardsInHand.text = "Cards: " + mePlayer.GetNumberOfCards();
        activeCardNumber.text = "Active: " + (mePlayer.activeCardIndex + 1);
    }

    //called on screen
    public void SetPlayerPutCard(){
        mePlayer.SetPlayerPutCard();
    }

    private void ShowLastCard(){
        movingPlayer.SetLastCardActive();
        if(movingPlayer != mePlayer) return;
        ChangeSelectedCard(0);
    }
    private Sprite GetRandomSpriteFromArray(Sprite[] array){
        return array[((int)Random.Range(0.0f, 100.0f)) % array.Length];
    }

}