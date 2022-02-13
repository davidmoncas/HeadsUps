using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the state of the game (starting, paused, no more lifes)
/// </summary>
/// 

public enum GameState {Starting , Playing , Paused , NeedMoreLifes }

public class GameStateManager : Singleton<GameStateManager>
{
    public GameState currentState;

    [SerializeField] GameObject StartCanvas , MoreLifesCanvas , loseCanvas;
    [SerializeField] GameObject ball;
    [SerializeField] GameObject player;
    [SerializeField] Transform fallingPrizesParent;

    void SetStateToStarting() {
        currentState = GameState.Starting;
        player.GetComponent<PlayerMovement>().enabled = false;
        ball.GetComponent<Rigidbody2D>().isKinematic = true;
        StartCanvas.SetActive(true);
    
    }

    public void SetStateToPlaying() {
        currentState = GameState.Playing;
        player.GetComponent<PlayerMovement>().enabled = true;
       // ball.GetComponent<Rigidbody2D>().isKinematic = false;
        StartCanvas.SetActive(false);
        EventManager.TriggerEvent("Start Playing");
    }

    void SetStateToPause() {
        currentState = GameState.Paused;
        player.GetComponent<PlayerMovement>().enabled = false;
        ball.GetComponent<Rigidbody2D>().isKinematic = true;
    }

    void SetStateToMoreLifes()
    {
        currentState = GameState.NeedMoreLifes;
        player.GetComponent<PlayerMovement>().enabled = false;
        ball.GetComponent<Rigidbody2D>().isKinematic = true;
        MoreLifesCanvas.SetActive(false);
    }


    public void SetStateToLose() {
        player.GetComponent<PlayerMovement>().enabled = false;
        ball.GetComponent<Rigidbody2D>().isKinematic = true;
        PauseFallingPrizes(true);
        loseCanvas.SetActive(true);
    }

    private void Awake()
    {
        GameStateManager instance = GameStateManager.Instance;  //to create the instance
        SetStateToStarting();
    }

    private void OnEnable()
    {
        base.OnEnable();
        EventManager.StartListening("Player Has No More Lifes" , SetStateToMoreLifes);
    }   
    
    private void OnDisable()
    {
        EventManager.StopListening("Player Has No More Lifes" , SetStateToMoreLifes);
    }

    void PauseFallingPrizes(bool flag) {
        foreach (Transform child in fallingPrizesParent) {
            child.GetComponent<Rigidbody2D>().isKinematic = flag;
        }
    }

}
