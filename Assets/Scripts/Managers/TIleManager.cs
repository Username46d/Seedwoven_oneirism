using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using DG.Tweening;

public class TIleManager : MonoBehaviour
{
    public static TIleManager Instance;

    [Header("���������")]
    public Tilemap tilesMap;
    public Tilemap floverMap;
    public TypesPlant[] tileTypes;

    [Header("�������� ����")]
    public List<Vector3Int> activeTiles;

    private Vector3Int lastHovPos = new Vector3Int(int.MaxValue, int.MaxValue, 0);

    private Dictionary<Vector3Int, GameTile> tileDataMap = new Dictionary<Vector3Int, GameTile>();  
    Vector3Int positionsInVoid = new Vector3Int(0, 0, 0);

    public static event System.Action<EventsData> tileManagerEvents;
    public class GameTile
    {
        public Plants PlantedSeed;
        public TypesPlant soilData;
        public bool isOpened;
        public Vector3Int Position;
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
                    tileDataMap[posit].isOpened = true;
                    tilesMap.SetTileFlags(posit, TileFlags.None);
                    tilesMap.SetColor(posit, Color.white);
                }
                else
                { 
                    tilesMap.SetTileFlags(posit, TileFlags.None);
                    tilesMap.SetColor(posit, Color.gray);
                    tilesMap.RefreshTile(posit);
                }

                RuleTile ruleTileFromMap = tilesMap.GetTile(posit) as RuleTile;
                foreach (var types in tileTypes)
                {
                    if (types.ruleTile == ruleTileFromMap)
                    {
                        tileDataMap[posit].soilData = types;
                    }
                }
            }
        }
        Debug.Log(tileDataMap.Count);
    }
    public GameTile CreateNewTile()
    {
        GameTile data = new GameTile
        {
        };
        return data;
    }
    public GameTile CreateNewTile(Plants plant, Vector3Int position)
    {
        GameTile data = new GameTile
        {
            PlantedSeed = plant,
            Position = position
        };
        return data;
    }
    public bool IsCanPlant(Vector3Int position)
    {
        if (tileDataMap.ContainsKey(position))
        {
            if (tileDataMap[position].isOpened == true && tileDataMap[position].PlantedSeed == null && tileDataMap[position].soilData.isPlantable)
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
    public void OpenTiles(Vector3Int CenralPos, int radiuses)
    {
        for (int i = -radiuses; i <= radiuses; i++)
        {
            Debug.Log("������� i " + i);
            for (int j = -radiuses; j <= radiuses; j++)
            {
                Debug.Log("������� j " + j + " radius " + radiuses);
                positionsInVoid.x = CenralPos.x + i;
                positionsInVoid.y = CenralPos.y + j;
                if (tileDataMap.ContainsKey(positionsInVoid))
                {
                    tilesMap.SetColor(positionsInVoid, Color.white);
                    tileDataMap[positionsInVoid].isOpened = true;
                }
            }
        }
    }
    public void AddTile(Vector3Int position)
    {
        var time = tileDataMap[position].PlantedSeed.growtTime;
        tileDataMap[position].PlantedSeed.growtTile.transform.localScale = new Vector3(0.2f, 0.2f, 1f);
        //tileDataMap[position].PlantedSeed.growtTile.transform.rotation.z = Random.RandomRange(0f, 360f);
        tileDataMap[position].PlantedSeed.growtTile.transform.DOScale(new Vector3 (1f, 1f, 1f), time / 1.5f).SetEase(Ease.InOutQuad);
        tileDataMap[position].PlantedSeed.growtTile.transform.DORotate(new Vector3 (0f, 0f, Random.RandomRange(0f, 360f)), time / 1.5f).SetEase(Ease.Linear);

        tileManagerEvents.Invoke(new EventsData(transform.TransformPoint(position), 1, 1, 2));
    }
    public void AddPlant(Plants plant, Vector3Int position)
    {
        tileDataMap[position].PlantedSeed = plant;
    }
    public Plants getPlant(Vector3Int position) { return tileDataMap[position].PlantedSeed; }
    public Vector3 getPosition(Vector3Int position) { return tilesMap.GetCellCenterWorld(position); }
    void Update()
    {
        // �������
        //Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //mouseWorldPos.z = 0;
        //Vector3Int hoveredTile = tilesMap.WorldToCell(mouseWorldPos);
        //if (hoveredTile != lastHovPos)
        //{
        //    if (tileDataMap.ContainsKey(hoveredTile)) { Debug.Log($"��� �����: {tileDataMap[hoveredTile].soilData.growtMultiplier}"); }
        //    lastHovPos = hoveredTile;
        //}
        // ����� �������
    }
}
