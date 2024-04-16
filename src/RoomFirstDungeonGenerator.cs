using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField]
    private int minRoomWidth = 4, minRoomHeight = 4;
    [SerializeField]
    private int dungeonWidth =20, dungeonHeight = 20;
    [SerializeField]
    [Range(0, 10)]
    private int offset = 1;
    [SerializeField]
    private bool randomWalkRooms = false;

    private Vector2Int spawnPoint;

    public GameObject player;
    public GameObject enemyPrefab;
    public GameObject jarPrefab;
    public GameObject barrelPrefab;
    public GameObject exitPrefab;

    public int numberOfEnemies = 5;
    public int numberOfJars = 10;
    public int numberOfBarrels = 10;
    public float minDistance = 5f;
    private List<GameObject> spawnedEnemies = new List<GameObject>();
    private List<GameObject> spawnedJars = new List<GameObject>();
    private List<GameObject> spawnedBarrels = new List<GameObject>();


    public void GenDungeon()
    {
        RunProceduralGeneration(); 
    }

    protected override void RunProceduralGeneration()
    {
        tilemapVisualizer.Clear();
        CreateRooms();
    }

    public void CreateRooms()
    {
        var roomsList = ProceduralGenerationAlgorithms.BinarySpacePartitioning(new BoundsInt((Vector3Int)startPosition,
            new Vector3Int(dungeonWidth, dungeonHeight, 0)), minRoomWidth, minRoomHeight);

        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

        if(randomWalkRooms)
        {
            floor = CreateRoomsRandomly(roomsList);
        }
        else
        {
            floor = CreateSimpleRooms(roomsList);
        }


        SetSpawnPoint(floor);
        SpawnEnemies(floor);
        SpawnJars(floor);
        SpawnBarrels(floor);
        SpawnExit(floor, minDistance);


        List<Vector2Int> roomCenters = new List<Vector2Int>();
        foreach (var room in roomsList)
        {
            roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        }

        HashSet<Vector2Int> corridors = ConnectRooms(roomCenters);
        floor.UnionWith(corridors);

        tilemapVisualizer.PaintFloorTiles(floor);
        WallGenerator.CreateWalls(floor, tilemapVisualizer);
    }


    private void SetSpawnPoint(HashSet<Vector2Int> floor)
    {
        List<Vector2Int> floorList = new List<Vector2Int>(floor);
        spawnPoint = floorList[Random.Range(0, floorList.Count)];

        player.transform.position = new Vector3(spawnPoint.x, spawnPoint.y, 0);
    }

    private void SpawnEnemies(HashSet<Vector2Int> floorPositions)
    {
        // Destroy previous enemies
        foreach (var enemy in spawnedEnemies)
        {
            if (enemy != null)
            {
                //DestroyImmediate(enemy);
                Destroy(enemy);
            }
        }
        spawnedEnemies.Clear();

        List<Vector2Int> floorList = new List<Vector2Int>(floorPositions);

        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector2Int randomPosition = floorList[Random.Range(0, floorList.Count)];

            // Check if the random position is not too close to the player's spawn point
            if (Vector2.Distance(randomPosition, spawnPoint) > 5) // Adjust the distance as needed
            {
                GameObject newEnemy = Instantiate(enemyPrefab, new Vector3(randomPosition.x, randomPosition.y, 0), Quaternion.identity);
                spawnedEnemies.Add(newEnemy);
            }
        }
    }

    private void SpawnExit(HashSet<Vector2Int> floorPositions, float minDistance)
    {
        List<Vector2Int> floorList = new List<Vector2Int>(floorPositions);
        Vector2 exitSpawnPoint;

        do
        {
            exitSpawnPoint = floorList[Random.Range(0, floorList.Count)];
        } while (Vector2.Distance(new Vector2(exitSpawnPoint.x, exitSpawnPoint.y), new Vector2(spawnPoint.x, spawnPoint.y)) < minDistance);

        exitPrefab.transform.position = new Vector3(exitSpawnPoint.x, exitSpawnPoint.y, 0);

    }

    private void SpawnJars(HashSet<Vector2Int> floorPositions)
    {
        // Destroy previous enemies
        foreach (var jar in spawnedJars)
        {
            if (jar != null)
            {
                //DestroyImmediate(jar);
                Destroy(jar);
            }
        }
        spawnedJars.Clear();

        List<Vector2Int> floorList = new List<Vector2Int>(floorPositions);

        for (int i = 0; i < numberOfJars; i++)
        {
            Vector2Int randomPosition = floorList[Random.Range(0, floorList.Count)];

            // Check if the random position is not too close to the player's spawn point
            if (Vector2.Distance(randomPosition, spawnPoint) > 5) // Adjust the distance as needed
            {
                GameObject newJar = Instantiate(jarPrefab, new Vector3(randomPosition.x, randomPosition.y, 0), Quaternion.identity);
                spawnedEnemies.Add(newJar);
            }
        }
    }

    private void SpawnBarrels(HashSet<Vector2Int> floorPositions)
    {
        // Destroy previous enemies
        foreach (var barrel in spawnedBarrels)
        {
            if (barrel != null)
            {
                //DestroyImmediate(barrel);
                Destroy(barrel);
            }
        }
        spawnedBarrels.Clear();

        List<Vector2Int> floorList = new List<Vector2Int>(floorPositions);

        for (int i = 0; i < numberOfBarrels; i++)
        {
            Vector2Int randomPosition = floorList[Random.Range(0, floorList.Count)];

            // Check if the random position is not too close to the player's spawn point
            if (Vector2.Distance(randomPosition, spawnPoint) > 5) // Adjust the distance as needed
            {
                GameObject newBarrel = Instantiate(barrelPrefab, new Vector3(randomPosition.x, randomPosition.y, 0), Quaternion.identity);
                spawnedEnemies.Add(newBarrel);
            }
        }
    }


    private HashSet<Vector2Int> CreateRoomsRandomly(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        for(int i = 0; i < roomsList.Count; i++)
        {
            var roomBounds = roomsList[i];
            var roomCenter = new Vector2Int(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y));
            var roomFloor = RunRandomWalk(randomWalkParameters, roomCenter);
            foreach (var position in roomFloor)
            {
                if(position.x >= (roomBounds.xMin + offset) && position.x <= (roomBounds.xMax - offset) 
                    && position.y >= (roomBounds.yMin - offset) && position.y <= (roomBounds.yMax - offset))
                {
                    floor.Add(position);
                }
            }
        }
        return floor;
    }


    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
    {
        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
        var currentRoomCenter = roomCenters[Random.Range(0, roomCenters.Count)];
        roomCenters.Remove(currentRoomCenter);

        while(roomCenters.Count > 0)
        {
            Vector2Int closest = FindClosestPointTo(currentRoomCenter, roomCenters);
            roomCenters.Remove(closest);
            HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter, closest);
            currentRoomCenter = closest;
            corridors.UnionWith(newCorridor);
        }
        return corridors;
    }

    private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoomCenter, Vector2Int destination)
    {
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
        var position = currentRoomCenter;
        corridor.Add(position);
        while (position.y != destination.y)
        {
            if(destination.y > position.y) 
            {
                position += Vector2Int.up;
            }
            else if(destination.y < position.y) 
            {
                position += Vector2Int.down;
            }
            corridor.Add(position);
        }
        while(position.x != destination.x)
        {
            if(destination.x > position.x)
            {
                position += Vector2Int.right;
            }
            else if(destination.x < position.x)
            {
                position += Vector2Int.left;
            }
            corridor.Add(position);
        }
        return corridor;
    }

    private Vector2Int FindClosestPointTo(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters)
    {
        Vector2Int closest = Vector2Int.zero;
        float distance = float.MaxValue;
        foreach (var position in roomCenters)
        {
            float currentDistance = Vector2.Distance(position, currentRoomCenter);
            if(currentDistance < distance) 
            { 
                distance = currentDistance;
                closest = position;
            }
        }
        return closest;
    }


    private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        foreach(var room in roomsList)
        {
            for(int col=offset; col < room.size.x - offset; col++)
            {
                for(int row = offset; row <room.size.y - offset; row++)
                {
                    Vector2Int position = (Vector2Int)room.min + new Vector2Int(col, row);
                    floor.Add(position);
                }
            }
        }
        return floor;
    }
}
