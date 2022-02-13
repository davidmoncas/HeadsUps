using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Points that fall from the top and have to be gathered by the player
/// </summary>
public class RainingPoint : MonoBehaviour
{

    public float maxSpeed = 5;
    public int pointsToGive = 10;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if (player != null) {
            ScoreManager.Instance.ChangeScore(pointsToGive);
            //TODO: add particles
            Destroy(this.gameObject);

        }
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);

    }
}
