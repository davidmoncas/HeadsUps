using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the movement of the player with the touch / mouse
/// </summary>
public class PlayerMovement : Singleton<PlayerMovement>
{

    float lastXpos, lastYpos;
    [SerializeField] public float speedX, speedY;

    [SerializeField] float xLimit; //x coordinate of the limit of the screen

    [SerializeField] float yLimitTop, yLimitBottom;

    [SerializeField] public float medianSpeedX , topSpeedX , minSpeedX;

    [SerializeField] Transform body;
    [SerializeField] Transform neckBone;

    float bodyYCoord;

    Rigidbody2D rb;
    Vector2 initialPos;

    private float latePositionY;
    [HideInInspector]public float velocityY;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialPos = this.transform.position;
        bodyYCoord = body.position.y;

    }

    void Update()
    {
        bool conditionForMovement;
        bool conditionForStopMovement;
        Vector2 touchPosition;


#if UNITY_EDITOR
        conditionForMovement = Input.GetMouseButton(0);
        conditionForStopMovement = Input.GetMouseButtonUp(0);
        touchPosition = Input.mousePosition;
#endif
#if UNITY_ANDROID && !UNITY_EDITOR
        conditionForMovement = Input.touchCount > 0;
        conditionForStopMovement = Input.touchCount == 0;
        Touch touch = Input.GetTouch(0);
        touchPosition = touch.position;
#endif


        if (conditionForMovement)
        {

            if (lastXpos == 0) lastXpos = touchPosition.x;
            if (lastYpos == 0) lastYpos = touchPosition.y;

            float deltaX = (touchPosition.x - lastXpos) / Screen.width * Time.deltaTime;
            float deltaY = (touchPosition.y - lastYpos) / Screen.height * Time.deltaTime;


            initialPos += new Vector2(deltaX * speedX * FactoredSpeed(deltaX), deltaY * speedY);
            initialPos.x = Mathf.Clamp(initialPos.x, -xLimit, xLimit);
            initialPos.y = Mathf.Clamp(initialPos.y, yLimitBottom, yLimitTop);

            lastXpos = touchPosition.x;
            lastYpos = touchPosition.y;
        }


        if (conditionForStopMovement) //restart the positions when the touch is over
        {   
            lastXpos = lastYpos = 0;
        }

    }

    private void FixedUpdate()
    {
        // Calculate the velocity
        velocityY = (transform.position.y - latePositionY) / Time.fixedTime;
        latePositionY = transform.position.y;


        rb.MovePosition(initialPos);
        

        Vector2 bodyPosition = initialPos;
        bodyPosition.y = bodyYCoord;
        body.transform.position = bodyPosition;
        neckBone.position = this.transform.position - Vector3.up*0.4f;

    }


    float FactoredSpeed(float delta) {


        float minX = 0.0001f;
        float maxX = 0.005f;

        delta = Mathf.Clamp( Mathf.Abs(delta),minX,maxX);

        if (delta < medianSpeedX) return minSpeedX + (medianSpeedX - minX) / (1 - minSpeedX) * delta;
        else return 1 + (maxX - medianSpeedX) / (topSpeedX - 1) * delta;

        //float normalizedSpeed = Mathf.Abs(delta) / medianSpeedX;



        //normalizedSpeed = Mathf.Clamp(normalizedSpeed, minSpeedX, topSpeedX);

        //return normalizedSpeed;
    }
}
