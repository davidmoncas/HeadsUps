using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Shows the position of the ball when is out of the screen
/// </summary>
public class OffScreenIndicator : MonoBehaviour
{
    [SerializeField] Transform ball;
    [SerializeField] float topLimit;
    [SerializeField] Transform indicatorImage;


    void Update()
    {
        if (ball.position.y > topLimit)
        {
            indicatorImage.gameObject.SetActive(true);

            Vector2 indicatorPosition = indicatorImage.position;
            indicatorPosition.x = ball.position.x;
            indicatorImage.position = indicatorPosition;

        }
        else {
            indicatorImage.gameObject.SetActive(false);
        }
    }
}
