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

    public GameObject coinPrefab;

    public HealthUI healthUI;
    public bool isPlayer = false;

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
            }
            else
            {
                SceneManager.LoadScene(0);
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
}
