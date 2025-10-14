using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SoilManager : MonoBehaviour
{
    public Tilemap soilMap;

    [System.Serializable]
    public class SoilTileType
    {
        public RuleTile activeTile;
        public RuleTile deactiveTile;
        public float growTime;
        public int pointBonus;
    }
    [Header("List of tiles")]
    public List<SoilTileType> soilTile = new List<SoilTileType>();

    private Dictionary<RuleTile, SoilTileType> soilsDiction = new Dictionary<RuleTile, SoilTileType>();

    [Header("HighlightColors")]
    public Color normalColor = new Color(1, 1, 1, 0.7f);
    public Color highColor = new Color(1, 1, 1, 0.7f);

    void Start()
    {
        InitializeDiction();
    }

    void Update()
    {
        
    }
    void InitializeDiction()
    {
        foreach (var soil in soilTile)
        {
            soilsDiction[soil.activeTile] = soil;
            soilsDiction[soil.deactiveTile] = soil;
        }
    }
}
