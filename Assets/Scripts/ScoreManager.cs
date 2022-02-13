using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{

    [HideInInspector] public int score;
    [HideInInspector] public int lifes;
    [HideInInspector] public int highestScore;

    [SerializeField] TopBarUI scoreUI;

    public void ChangeLifes(int amount = 1) {

        if (lifes + amount <= 0) {
            EventManager.TriggerEvent("Player Has No More Lifes");
            return;
        }

        lifes += amount;
        scoreUI.SetLifes(lifes);

        SaveManager.Instance.Save();
    }

    public void ChangeScore(int amount = 1) {


        score += amount;
        if (score > highestScore) { 
            highestScore = score;
            scoreUI.SetHighScore(highestScore);
        }

        scoreUI.SetScore(score);
    }

    void InitializeCounters() {
        scoreUI.SetScore(score);
        scoreUI.SetLifes(lifes);
        scoreUI.SetHighScore(highestScore);
    }

    private void OnEnable()
    {
        base.OnEnable();
        EventManager.StartListening("Everything Loaded", InitializeCounters);
    }   
    
    private void OnDisable()
    {
        EventManager.StopListening("Everything Loaded", InitializeCounters);
    }

}
