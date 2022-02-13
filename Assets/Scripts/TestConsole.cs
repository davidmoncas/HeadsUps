using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestConsole : MonoBehaviour
{

    [SerializeField] PlayerMovement playermovement;
    [SerializeField] Slider movementX, movementY, medianSpeed;
    [SerializeField] Text movementXText, movementYText, medianSpeedText;

    

    void ChangeSpeedX() {
        movementXText.text = movementX.value+"";
        playermovement.speedX = movementX.value;
        SaveManager.Instance.Save();
    }

    void ChangeSpeedY()
    {
        movementYText.text = movementY.value + "";
        playermovement.speedY = movementY.value;
        SaveManager.Instance.Save();
    }

    void ChangeMedianSpeed()
    {
        medianSpeedText.text = medianSpeed.value + "";
        playermovement.medianSpeedX = medianSpeed.value;
        SaveManager.Instance.Save();
    }



    private void OnEnable()
    {
        EventManager.StartListening("Everything Loaded", Init);
    }

    void Init() {
        movementX.value = playermovement.speedX;
        movementY.value = playermovement.speedY;
        medianSpeed.value = playermovement.medianSpeedX;

        playermovement.medianSpeedX = medianSpeed.value;
        playermovement.speedY = movementY.value;
        playermovement.speedX = movementX.value;

        movementX.onValueChanged.AddListener(delegate { ChangeSpeedX(); });
        movementY.onValueChanged.AddListener(delegate { ChangeSpeedY(); });
        medianSpeed.onValueChanged.AddListener(delegate { ChangeMedianSpeed(); });
    }

}
