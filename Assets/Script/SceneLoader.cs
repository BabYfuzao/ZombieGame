using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        
    }

    public void StartButton()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(0);
    }

    public void BackButton()
    {
        SceneManager.LoadScene(0);
    }
}
