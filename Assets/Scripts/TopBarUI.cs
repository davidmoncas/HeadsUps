using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopBarUI : MonoBehaviour
{

    [SerializeField] Text scoreText, lifesText , highestScoreText;


    public void SetLifes(int amount) => lifesText.text ="x" + amount ;

    public void SetScore(int amount) => scoreText.text =  amount+"";

    public void SetHighScore(int amount) => highestScoreText.text = "x" + amount;
}
