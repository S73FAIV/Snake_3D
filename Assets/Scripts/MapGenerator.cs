using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public Transform tilePrefab;
    public Transform foodPrefab;
    public Vector2 mapSize = new Vector2(11, 11);

    [Range(0, 1)] public float outlinePercent;

    private List<Vector2> allTileCoords;
    public List<Vector2> _occupiedCoords = new List<Vector2>();
    private Queue<Vector2> shuffledTileCoords;
    public int seed;

    private Transform mapHolder;
    private Transform food;
    [SerializeField]private Vector2 foodCoord;

    private void Start()
    {
        mapSize = new Vector2(PlayerPrefs.GetInt("Mapsize"), PlayerPrefs.GetInt("Mapsize"));;
        seed = new System.Random().Next();
        
        GenerateMap();
        SpawnNewFood();
    }

    public void GenerateMap()
    {
        
        allTileCoords = new List<Vector2>();
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                allTileCoords.Add(new Vector2(x, y));
            }
        }

        shuffledTileCoords = new Queue<Vector2>(Utility.ShuffleArray(allTileCoords.ToArray(), seed));

        string holderName = "Generated Map";
        if (transform.Find(holderName))
        {
            DestroyImmediate(transform.Find(holderName).gameObject);
        }

        mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = transform;

        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                Vector3 tilePosition = CoordToPosition(x, y);
                Transform newTile = Instantiate(tilePrefab, tilePosition, Quaternion.Euler(Vector3.right * 90));
                newTile.localScale = Vector3.one * (1 - outlinePercent);
                newTile.parent = mapHolder;
            }
        }
    }
    
    public bool isCoordOnMap(Vector2 coord)
    {
        return allTileCoords.Contains(coord);
    }

    public Vector3 CoordToPosition(int x = 0, int y = 0)
    {
        return new Vector3(-mapSize.x / 2 + 0.5f + x, 0, -mapSize.y / 2 + 0.5f + y);
    }

    public Vector2 GetRandomCoord()
    {
        Vector2 randomCoord = shuffledTileCoords.Dequeue();
        shuffledTileCoords.Enqueue(randomCoord);
        return randomCoord;
    }

    public Vector2 GetRandomFreeCoord()
    {
        Vector2 firstRandomCoord = GetRandomCoord();
        Vector2 randomCoord = firstRandomCoord;
        while (_occupiedCoords.Contains(randomCoord))
        {
            randomCoord = GetRandomCoord();
            Debug.Log("GetNewRandomCoord");
            if (randomCoord == firstRandomCoord)
            {
                Debug.Log("No Free Random Coords");
                return firstRandomCoord;
            }
        }

        return randomCoord;
    }

    public void SpawnNewFood()
    {
        Vector2 randomCoord = GetRandomFreeCoord();
        Vector3 foodPosition = CoordToPosition((int) randomCoord.x, (int) randomCoord.y);
        if (food == null)
        {
            food = Instantiate(foodPrefab, foodPosition + Vector3.up * .36f, Quaternion.identity);
        }
        else
        {
            food.position = foodPosition;
        }

        food.parent = mapHolder;
        foodCoord = randomCoord;
    }

    public Vector2 GetFoodCoord()
    {
        return foodCoord;
    }
    
}