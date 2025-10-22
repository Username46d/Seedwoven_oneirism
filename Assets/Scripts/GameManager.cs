using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("���������")]
    public TIleManager tileManager;
    public GameObject[] seedPrefabs;

    public float currentGameTime = 0f;
    private SortedDictionary<int, List<Vector3Int>> growtQueue = new SortedDictionary<int, List<Vector3Int>>();  // !!!!! ��������������, ������� ��� �������
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
    }

    void Update()
    {
        currentGameTime = Time.time;
        //Debug.Log("�����" + currentGameTime);
        if (growtQueue.Count > 0)
        {
            if (growtQueue.First().Key <= currentGameTime)
            {
                foreach (var seed in growtQueue.First().Value)
                {
                    tileManager.GetSeed(seed).Register();
                }
                growtQueue.Remove(growtQueue.First().Key);
            }
        }
        HangleInput();
    }
    void HangleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            Vector3Int tilePos = tileManager.tilesMap.WorldToCell(mousePos);
            if (tileManager.IsCanPlant(tilePos)){
                PlantSeed(tilePos);
            }
        }
    }
    public void RegisterSeed(int growtTime, Vector3Int position)   
    {
        // ���������� ������
        int timeKey = (int)currentGameTime + growtTime;
        if (!growtQueue.ContainsKey(timeKey))  
        {
            growtQueue[timeKey] = new List<Vector3Int>();
        }
        growtQueue[timeKey].Add(position);

    }
    void PlantSeed(Vector3Int posit)
    {
        SeedOne seed = new SeedOne();
        seed.InitializeSeed(posit);
        tileManager.PlantedSeed(posit, seed);
    }
}
