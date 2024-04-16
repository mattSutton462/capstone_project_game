using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCounterUI : MonoBehaviour
{
    public TMP_Text coinCountText; // Reference to the TextMeshPro Text component

    private int coinCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the text with the starting coin count
        UpdateCoinCountText();
    }

    // Method to update the coin count
    public void UpdateCoinCount(int count)
    {
        coinCount += count;
        UpdateCoinCountText();
    }

    // Method to update the text displayed on the UI
    void UpdateCoinCountText()
    {
        coinCountText.text = coinCount.ToString();
    }
}
