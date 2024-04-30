using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class HealthScript : MonoBehaviour
{
    [SerializeField]
    private int currentHealth, maxHealth;

    public UnityEvent<GameObject> OnHitWithReference, OnDeathWithReference;

    [SerializeField]
    public bool isDead = false;

    public GameObject coinPrefab, healPrefab;

    public HealthUI healthUI;
    public bool isPlayer = false;
    public bool isEnemy = false;

    public void IntitializeHealth( int healthValue)
    {
        currentHealth = healthValue;
        maxHealth = healthValue;
        isDead = false;

        if (healthUI != null)
        {
            healthUI.InitializeHearts(maxHealth); // Initialize the heart images
            healthUI.UpdateHearts(currentHealth); // Update the heart images based on current health
        }
    }

    public void GetHit (int amount, GameObject sender)
    {
        if (isDead)
            return;
        if (sender.layer == gameObject.layer)
            return;

        currentHealth -= amount;

        if (healthUI != null)
        {
            if (isPlayer)
            {
                healthUI.UpdateHearts(currentHealth); // Update the heart images based on current health
            }
        }

        if (currentHealth > 0)
        {
            OnHitWithReference?.Invoke(sender);
        }
        else
        {
            OnDeathWithReference?.Invoke(sender);
            isDead = true;
            Destroy(gameObject);

            if(isPlayer == false)
            {
                DropCoins();
                if (isEnemy)
                {
                    DropHeals();
                }
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    public void Heal(int amount)
    {
        if (!isDead)
        {
            currentHealth += amount;
            currentHealth = Mathf.Min(currentHealth, maxHealth); // Ensure health doesn't exceed max health

            if (healthUI != null && isPlayer) // Check if it's the player to update UI
            {
                healthUI.UpdateHearts(currentHealth); // Update the heart images based on current health
            }
        }
    }

    void DropCoins()
    {
        int numberOfCoins = Random.Range(0, 4);

        for (int i = 0; i < numberOfCoins; i++)
        {
            // Instantiate the coin prefab at the jar's position with a slight random offset
            Vector2 spawnPosition = new Vector2(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f));
            Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
        }
    }

    void DropHeals()
    {
        int chanceToDrop = Random.Range(0, 101);

        if(chanceToDrop > 75) 
        {
            Vector2 spawnPosition = new Vector2(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f));
            Instantiate(healPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
