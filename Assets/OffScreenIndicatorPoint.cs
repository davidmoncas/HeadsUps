using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Shows the position of the falling points when they are outside of the screen
public class OffScreenIndicatorPoint : MonoBehaviour
{
    [SerializeField] Transform fallingPointsParent;
    Transform ball;
    [SerializeField] float topLimit;
    [SerializeField] Transform indicatorImage;


    void Update()
    {
        if (fallingPointsParent.childCount == 0) {
            indicatorImage.gameObject.SetActive(false);
            return;
            
        } 
        
        ball = fallingPointsParent.GetChild(0);

        if (ball.position.y > topLimit)
        {
            indicatorImage.gameObject.SetActive(true);

            Vector2 indicatorPosition = indicatorImage.position;
            indicatorPosition.x = ball.position.x;
            indicatorImage.position = indicatorPosition;

        }
        else
        {
            indicatorImage.gameObject.SetActive(false);
        }
    }
}
