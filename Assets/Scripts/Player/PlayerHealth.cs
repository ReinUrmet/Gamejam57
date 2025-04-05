using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    public GameObject Player;
    public GameObject[] fullHearts;
    public int health;
    public string RestartScene = "RestartTseen";
    private bool isDead = false;

    public void UpdateHearts()
    {
        for (int i = 0; i < fullHearts.Length; i++)
        {
            fullHearts[i].SetActive(i < health);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Health -" + damage);
        UpdateHearts();

        if (!isDead && health <= 0)
        {
            isDead = true;
            Destroy(Player);
            Invoke("Restart", 1f);
            Debug.Log("Restart");
        }
    }
    void Restart()
    {
        SceneManager.LoadScene(RestartScene);
    }
}
