using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image[] heartImages; // Array of heart images
    public Sprite fullHeartSprite; // Sprite for a full heart
    public Sprite emptyHeartSprite; // Sprite for an empty heart

    private int maxHealth; // Maximum health
    private int currentHealth; // Current health

    // Initialize the heart images based on the player's max health
    public void InitializeHearts(int maxHealth)
    {
        this.maxHealth = maxHealth;
        currentHealth = maxHealth;

        // Enable heart images based on max health
        for (int i = 0; i < heartImages.Length; i++)
        {
            heartImages[i].enabled = (i < maxHealth);
            heartImages[i].sprite = fullHeartSprite;
        }
    }

    // Update the heart images based on current health
    public void UpdateHearts(int health)
    {
        currentHealth = health;

        // Update heart images based on current health
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < currentHealth)
            {
                heartImages[i].sprite = fullHeartSprite;
            }
            else
            {
                heartImages[i].sprite = emptyHeartSprite;
            }
        }
    }
}
