using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// controls the spawning of falling objects during time
/// </summary>
public class FallingPointsManager : MonoBehaviour
{

    [SerializeField] GameObject prize;
    [SerializeField] Transform PrizesParent;

    [Header("Coordinates of the limits")]
    [SerializeField] float xLimits;
    [SerializeField] float yCoord;
    float counter;
    float nextPrizeTime;



    private void Start()
    {
        nextPrizeTime = Random.Range(5f, 15f);
    }


    void Update()
    {
        if (GameStateManager.Instance.currentState != GameState.Playing) return;
        if (PrizesParent.childCount > 0) return;
        counter += Time.deltaTime;
        if (counter > nextPrizeTime) SpawnPrize();
    }

    void SpawnPrize()
    {
        GameObject prize = Instantiate(this.prize);
        float coordX = Random.Range(-xLimits, xLimits);        
        prize.transform.position = new Vector2(coordX, yCoord);
        prize.transform.parent = PrizesParent;

        StartCoroutine(KeepBallStill(3, prize.GetComponent<Rigidbody2D>() ));

        RainingPoint prizeComponent = prize.GetComponent<RainingPoint>();
        prizeComponent.pointsToGive = Random.Range(5, 25);
        prizeComponent.maxSpeed = CalculateSpeed();

        RestartCounter();
    }

    IEnumerator KeepBallStill(float seconds , Rigidbody2D rb) {
        rb.isKinematic = true;
        yield return new WaitForSeconds(seconds);
        if (GameStateManager.Instance.currentState != GameState.Playing) yield break;
        rb.isKinematic = false;

    }


    void RestartCounter()
    {
        nextPrizeTime = Random.Range(5f, 15f);
        counter = 0;
    }

    float CalculateSpeed() {
        int amount = ScoreManager.Instance.score;
        if (amount < 100) return 1.5f;
        if (amount < 200) return 3;
        if (amount < 400) return 5;
        if (amount < 800) return 8;
        return 10;

    
    }
}
