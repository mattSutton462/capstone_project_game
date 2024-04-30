using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealOrb : MonoBehaviour
{
    public int healVal = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding GameObject is the player
        if (other.CompareTag("Player"))
        {
            // Collect the Heal
            CollectHeal(other.gameObject);

            // Destroy the HealOrb GameObject
            Destroy(gameObject);
        }
    }

    void CollectHeal(GameObject player)
    {
        Debug.Log("heal");

        HealthScript playerHealth = player.GetComponent<HealthScript>();

        if (playerHealth != null)
        {
            playerHealth.Heal(healVal);
        }
        else
        {
            Debug.LogWarning("HealthScript component not found!");
        }

    }
}
