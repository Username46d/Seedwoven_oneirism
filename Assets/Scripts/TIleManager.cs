using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using Unity.VisualScripting;

public class TIleManager : MonoBehaviour
{
    public static TIleManager Instance;
    [Header("Настройки")]
    public Tilemap tilesMap;

    [Header("Активные поля")]
    public List<Vector3Int> activeTiles;

    // Отладка
    private Vector3Int lastHovPos = new Vector3Int(int.MaxValue, int.MaxValue, 0);
    // Конец отладки

    private Dictionary<Vector3Int, GameTile> tileDataMap = new Dictionary<Vector3Int, GameTile>();  // потом сделать через геттер
    Vector3Int positionsInVoid = new Vector3Int(0, 0, 0);
    public class GameTile
    {
        public SeedOne PlantedSeed;
        public bool IsOpened = false;
    }
    void Start()
    {
        Instance = this;
        InitializeTiles();
    }

    void InitializeTiles()
    {
        BoundsInt bounds = tilesMap.cellBounds;
        foreach (var posit in bounds.allPositionsWithin)
        {
            if (tilesMap.HasTile(posit))
            {
                Debug.Log(posit);
                TileBase tile = tilesMap.GetTile(posit);
                tileDataMap[posit] = CreateNewTile();
                if (activeTiles.Contains(posit))
                {
                    tileDataMap[posit].IsOpened = true;
                    tilesMap.SetTileFlags(posit, TileFlags.None);
                    tilesMap.SetColor(posit, Color.white);
                }
                else
                {
                    tilesMap.SetTileFlags(posit, TileFlags.None);
                    tilesMap.SetColor(posit, Color.gray);
                    tilesMap.RefreshTile(posit);
                }
            }
        }
        Debug.Log(tileDataMap.Count);
    }
    GameTile CreateNewTile()
    {
        GameTile data = new GameTile
        {
        };
        return data;
    }
    public bool IsCanPlant(Vector3Int position)
    {
        if (tileDataMap.ContainsKey(position))
        {
            if (tileDataMap[position].IsOpened == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    public void PlantedSeed(Vector3Int position, SeedOne seed)
    {
        tileDataMap[position].PlantedSeed = seed;
    }
    public SeedOne GetSeed(Vector3Int position)
    {
        return tileDataMap[position].PlantedSeed;
    }
    public void OpenTiles(Vector3Int CenralPos, int radiuses)
    {
        int maxOp = 25;
        Debug.Log(CenralPos);
        //for (int i = -radiuses; i < radiuses; i++)
        //{
        //    Debug.Log("Позиция i " + i);
        //    for (int j = -radiuses; i < radiuses; j++)
        //    {
        //        Debug.Log("Позиция j " + j + " radius " + radiuses);
        //        positionsInVoid.x = CenralPos.x + i;
        //        positionsInVoid.y = CenralPos.y + j;
        //        if (tileDataMap.ContainsKey(positionsInVoid)){
        //            tilesMap.SetColor(positionsInVoid, Color.white);
        //            tileDataMap[positionsInVoid].IsOpened = true;
        //        }
        //        maxOp -= 1;
        //        Debug.Log(positionsInVoid);
        //        if (maxOp <= 0){
        //            return;
        //        }
        //    }
        //}
        int i = -radiuses;
        while (i < radiuses+1)
        {
            int j = -radiuses;
            Debug.Log("Позиция i " + i);
            while (j < radiuses+1)
            {
                Debug.Log("Позиция j " + j + " radius " + radiuses);
                positionsInVoid.x = CenralPos.x + i;
                positionsInVoid.y = CenralPos.y + j;
                if (tileDataMap.ContainsKey(positionsInVoid))
                {
                    tilesMap.SetColor(positionsInVoid, Color.white);
                    tileDataMap[positionsInVoid].IsOpened = true;
                }
                maxOp -= 1;
                Debug.Log(positionsInVoid);
                if (maxOp <= 0)
                {
                    return;
                }
                j++;
            }
            i++;
        }
    }
    void Update()
    {
        // Отладка
        //Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //mouseWorldPos.z = 0;
        //Vector3Int hoveredTile = tilesMap.WorldToCell(mouseWorldPos);
        //if (hoveredTile != lastHovPos)
        //{
        //    Debug.Log($"Позиция тайла: {hoveredTile.x} и {hoveredTile.y}");
        //    lastHovPos = hoveredTile;
        //}
        // Конец отладки
    }
}
