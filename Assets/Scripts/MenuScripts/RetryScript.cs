using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryScript : MonoBehaviour
{
    public String sceneName;


    public void Retry()
    {
        Debug.Log("Vajutus");
        SceneManager.LoadScene(sceneName);
    }
}
