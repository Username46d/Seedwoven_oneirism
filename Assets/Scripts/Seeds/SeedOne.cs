using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedOne : MonoBehaviour
{
    [Header("Параметры")]
    public int growthTime = 5;
    public int maxStadied = 3;
    public int growtRadius = 1;

    private int currentStadia = 0;

    public Vector3Int growtPosition;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void InitializeSeed(Vector3Int grid)
    {
        growtPosition = grid;
        Register();
    }
    public void Register()
    {
        if (currentStadia <= maxStadied)
        {
            currentStadia += 1;
            Debug.Log("Растение выросло на стадию " + currentStadia);
            GameManager.Instance?.RegisterSeed(growthTime, growtPosition);
        }
        else
        {
            TIleManager.Instance?.OpenTiles(growtPosition, growtRadius);
        }
    }
}

