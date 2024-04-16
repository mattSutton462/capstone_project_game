using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinScript : MonoBehaviour
{
    public int coinValue = 1; // The value of the coin

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding GameObject is the player
        if (other.CompareTag("Player"))
        {
            // Collect the coin
            CollectCoin();

            // Destroy the coin GameObject
            Destroy(gameObject);
        }
    }

    void CollectCoin()
    {
        //Debug.Log("COIN");
        CoinCounterUI coinCounterUI = FindObjectOfType<CoinCounterUI>();
        if (coinCounterUI != null)
        {
            coinCounterUI.UpdateCoinCount(coinValue); // Increment the coin count by 1
        }
        else
        {
            Debug.LogWarning("CoinCounterUI not found in the scene.");
        }
    }

}
