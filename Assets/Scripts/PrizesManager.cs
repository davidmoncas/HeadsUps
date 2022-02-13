using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// controls the spawning of prizes during time
/// </summary>
public class PrizesManager : MonoBehaviour
{
    [SerializeField] GameObject prize;
    [SerializeField] Transform PrizesParent;

    [Header("Coordinates of the limits")]
    [SerializeField] float xLimits;
    [SerializeField] float ymax , ymin;
    float counter;
    float nextPrizeTime;

    private void Start()
    {
        nextPrizeTime = Random.Range(5f, 15f);
    }


    // Update is called once per frame
    void Update()
    {
        if (GameStateManager.Instance.currentState != GameState.Playing) return;
        counter += Time.deltaTime;
        if (counter > nextPrizeTime) SpawnPrize();
    }

    void SpawnPrize() {
        GameObject prize = Instantiate(this.prize);
        float coordX = Random.Range(-xLimits, xLimits);
        float coordY = Random.Range(ymin, ymax);
        prize.transform.position = new Vector2(coordX, coordY);

        prize.transform.parent = PrizesParent;

        Prize prizeComponent = prize.GetComponent<Prize>();
        prizeComponent.prizeAmount = Random.Range(1, 10);


        RestartCounter();
    }

    void RestartCounter() {
        nextPrizeTime = Random.Range(5f, 15f);
        counter = 0;
    }
}
