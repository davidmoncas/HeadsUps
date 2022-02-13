using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_test : MonoBehaviour
{
    float lastXpos;
    float XSpeed;

    public bool inertia;
    public float inertiaTime = 1;
    public float timeCounter;

    public float maxSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            if (lastXpos == 0) lastXpos = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        }
        if (Input.GetMouseButton(0))
        {
            float newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;

            float delta = newPos - lastXpos;
            this.transform.position += Vector3.right * delta;
            lastXpos = newPos;

            XSpeed = delta / Time.deltaTime;
            XSpeed *= maxSpeed;
        }   

        if (Input.GetMouseButtonUp(0)) { 
            lastXpos = 0;
            inertia = true;
            timeCounter = 0;
            print(XSpeed);
        }


        if (inertia)
        {
            float factoredSpeed = XSpeed * (1 - Mathf.Sqrt(timeCounter / inertiaTime));          
            if (timeCounter > inertiaTime) inertia = false;
            this.transform.position += Vector3.right * factoredSpeed;
            timeCounter += Time.deltaTime;
            print(timeCounter + "   " + (1 - Mathf.Sqrt(timeCounter / inertiaTime)));
        }


        //clamp the movement
        Vector2 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -2.35f, 2.35f);
        transform.position = pos;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(Camera.main.ScreenToWorldPoint(Input.mousePosition) , 0.2f);
    }
}
