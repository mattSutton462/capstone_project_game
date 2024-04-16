using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloorCounter : MonoBehaviour
{
    public TextMeshProUGUI floorCountText; // Reference to the TextMeshPro Text component

    private int floorCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the text with the starting floor count
        UpdateFloorCountText();
    }

    // Method to update the floor count
    public void UpdateFloorCount(int count)
    {
        floorCount += count;
        UpdateFloorCountText();
    }

    // Method to update the text displayed on the UI
    void UpdateFloorCountText()
    {
        floorCountText.text = floorCount.ToString();
    }
}
