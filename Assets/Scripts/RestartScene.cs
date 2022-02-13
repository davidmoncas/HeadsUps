using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour
{
    [SerializeField] int sceneNumber;

    public void ResetScene() {
        SceneManager.LoadScene(sceneNumber);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(sceneNumber);
    }
}
