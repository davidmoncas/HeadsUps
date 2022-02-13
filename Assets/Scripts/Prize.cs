using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prize : MonoBehaviour
{
    public int prizeAmount=1;
    public float duration = 10;
    float timer;
    bool disappearing = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BallImpact ball = collision.GetComponent<BallImpact>();
        if (ball != null) {

            EventManager.TriggerEvent("Play Prize Particles", this.transform.position);
            ScoreManager.Instance.ChangeScore(prizeAmount);
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        duration = Random.Range(5, 10);
    }
    private void Update()
    {

        timer += Time.deltaTime;
        if (duration - timer < 2 && !disappearing) {
            disappearing = true;
            this.GetComponent<Animator>().SetTrigger("Disappear");
        }


        if (timer > duration) Destroy(this.gameObject);
    }
}
