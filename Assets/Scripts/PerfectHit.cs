using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerfectHit : MonoBehaviour
{

    [SerializeField] Transform ball;
    BallImpact ballImpact;
    Vector2 offset;


    void Start()
    {
        offset = this.transform.position - ball.position;
        ballImpact = ball.gameObject.GetComponent<BallImpact>();
    }


    void Update()
    {
        transform.position = (Vector2)ball.position + offset;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PerfectHit")) {
            EventManager.TriggerEvent("Play Combo Particle", (Vector3)transform.position);

            ballImpact.perfectHit = true;
        }
    }

}
