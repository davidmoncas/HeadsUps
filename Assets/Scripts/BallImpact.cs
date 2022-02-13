using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallImpact : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float impactForce, wallImpactForce;
    Vector2 velocityBeforePhisicsUpdate;

    // Combo
    int comboPoints = 0;
    public bool perfectHit = false;
    int[] comboPointPrizes = { 2, 5, 10, 15, 20 };

    public float maxSpeed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            if (rb.isKinematic) rb.isKinematic = false;
            this.GetComponent<CircleCollider2D>().isTrigger = false;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("colision");
        if (collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            


            // The Y velocity of the player changes the impact 
            PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();

            float playerVelocity = playerMovement.velocityY;
            float impactMultiplier;

            impactMultiplier = 1 + playerVelocity * 10;
            impactMultiplier = Mathf.Clamp(impactMultiplier, 0, 2.5f);


            rb.AddForce(Vector2.up * impactForce * impactMultiplier);

            //Add some random force to the sides
            float randomX = Random.Range(-1f, 1f);
            rb.AddForce(Vector2.right * randomX * impactForce * 0.05f);

            float relativeImpact = Mathf.Abs(playerVelocity - velocityBeforePhisicsUpdate.y); //goes from 1 to 10 approx
            relativeImpact /= 20;

            //print("----" + relativeImpact);

            Vector2 localUp = Vector2.one - (Vector2)transform.up.normalized * relativeImpact;
            transform.localScale = (Vector3)Vector2.Scale(transform.localScale, localUp);


            if (perfectHit) IncreaseCombo();

            else
            {
                ScoreManager.Instance.ChangeScore(1);
                RestartCombo();
            }

            EventManager.TriggerEvent("Play Hit Particle", collision.contacts[0].point);
            EventManager.TriggerEvent("Ball Hit");
        }

        else if (collision.gameObject.GetComponent<Barrier>() != null)
        {
            Barrier barrier = collision.gameObject.GetComponent<Barrier>();

            BarrierType type = barrier.type;
            float bounceForce = 0;
            if (type == BarrierType.normal) bounceForce = 1;
            else if (type == BarrierType.bouncier) bounceForce = 1.5f;
            else if (type == BarrierType.flat) bounceForce = 0.1f;

            rb.AddForce(velocityBeforePhisicsUpdate.normalized * -1 * bounceForce * wallImpactForce);

        }


    }

    void IncreaseCombo()
    {

        print("won" + comboPointPrizes[comboPoints] + "   " + comboPoints);
        ScoreManager.Instance.ChangeScore(comboPointPrizes[comboPoints]);
        comboPoints++;
        if (comboPoints >= 5) comboPoints = 0;

        perfectHit = false;
    }

    void RestartCombo()
    {
        comboPoints = 0;
        perfectHit = false;
    }


    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();

    }

    private void FixedUpdate()
    {
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        velocityBeforePhisicsUpdate = rb.velocity;

    }

    void Update()
    {


        transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(0.5f, 0.5f), 0.05f);
    }
}
