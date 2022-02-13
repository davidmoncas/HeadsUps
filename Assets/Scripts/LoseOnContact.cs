using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseOnContact : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        BallImpact ball = collision.gameObject.GetComponent<BallImpact>();
        if (ball != null)
        {
            ScoreManager.Instance.ChangeLifes(-1);

            if (ScoreManager.Instance.lifes > 0)
                GameStateManager.Instance.SetStateToLose();
        }
    }

}
