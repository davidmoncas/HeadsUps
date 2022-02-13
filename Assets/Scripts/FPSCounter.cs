using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    public int avgFrameRate;
    public Text UpdateText, FixedUpdateText;

    public void Update()
    {
        float current = 0;
        current = Time.frameCount / Time.time;
        avgFrameRate = (int)current;
        UpdateText.text = avgFrameRate.ToString() + " FPS";
    }

    private void FixedUpdate()
    {
        FixedUpdateText.text = Time.fixedDeltaTime + "";
    }

}