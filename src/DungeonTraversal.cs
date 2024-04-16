using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DungeonTraversal: MonoBehaviour
{
    [SerializeField]
    private RoomFirstDungeonGenerator dungeonGenerator;



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dungeonGenerator.GenDungeon();
            FindObjectOfType<FloorCounter>().UpdateFloorCount(1);
        }
    }
}
