using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScorePanelUI : MonoBehaviour
{
    [SerializeField] Text highScoreText;

    private void OnEnable()
    {
        highScoreText.text = ScoreManager.Instance.highestScore + "";
    }

}
