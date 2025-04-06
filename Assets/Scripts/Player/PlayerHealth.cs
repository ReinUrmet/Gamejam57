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

    [Header("Invulnerability Settings")]
    public float invulnDuration = 1f;
    private bool isInvulnerable = false;

    public void UpdateHearts()
    {
        for (int i = 0; i < fullHearts.Length; i++)
        {
            fullHearts[i].SetActive(i < health);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isInvulnerable || isDead) return; // ✅ Ignore damage if invulnerable or dead

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
        else
        {
            StartCoroutine(StartInvulnerability());
        }
    }

    System.Collections.IEnumerator StartInvulnerability()
    {
        isInvulnerable = true;

        // Optional: add a flashing effect here

        yield return new WaitForSeconds(invulnDuration);

        isInvulnerable = false;
    }

    void Restart()
    {
        SceneManager.LoadScene(RestartScene);
    }
}
