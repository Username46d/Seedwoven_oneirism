using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using static TIleManager;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("Менеджеры")]
    public GameObject Flowers;
    public GrowthManager growthManager;
    public TIleManager tileManager;
    public List<Plants> plantsData;

    public ScoreManager scoreManager = new ScoreManager();

    private SortedDictionary<int, List<Vector3Int>> growtQueue = new SortedDictionary<int, List<Vector3Int>>();

    public float currentGameTime = 0f;
    // Дополнительно
    private bool isMouseHeld = false;
    Vector3Int lastCell = new Vector3Int(999, 999, 0);


    public static event System.Action<EventsData> gameManagerEvents;

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
        if (growtQueue.Count > 0)
        {
            if (growtQueue.First().Key <= currentGameTime)
            {
                foreach (var seed in growtQueue.First().Value)
                {
                    growthManager.Register(seed);
                }
                growtQueue.Remove(growtQueue.First().Key);
            }
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            scoreManager.AddScore(10);
        }
        HangleInput();
    }
    void HangleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); mousePos.z = 0;
            PlantAtCell(mousePos);
            gameManagerEvents.Invoke(new EventsData(mousePos, 0, 0, 2));
        }
        if (Input.GetMouseButtonUp(0))
        {
            isMouseHeld = false;
        }
        if (isMouseHeld && Input.GetMouseButton(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); mousePos.z = 0;
            PlantAtCell(mousePos);
        }
    }
    //private void PlantAtCell(Vector3 mousePos)
    //{
    //    isMouseHeld = true;
    //    Vector3Int tilePos = tileManager.tilesMap.WorldToCell(mousePos);
    //    if (tileManager.IsCanPlant(tilePos))
    //    {
    //        var randomDara = plantsData[Random.RandomRange(0, plantsData.Count)];
    //        Plants randPlant = new Plants();
    //        randPlant.sized = randomDara.sized; randPlant.growtTime = randomDara.growtTime; randPlant.position = tilePos;  // randPlant.position = tilePos; randPlant.growtTiles = randomDara.growtTiles;
    //        GameObject plant = Instantiate(randomDara.growtTile, TIleManager.Instance.getPosition(tilePos), Quaternion.identity);
    //        plant.transform.SetParent(Flowers.transform);
    //        randPlant.growtTile = plant;
    //        tileManager.AddPlant(randPlant, tilePos);
    //        growthManager.Register(randPlant.position);
    //    }
    //}

    private void PlantAtCell(Vector3 mousePos)
    {
        isMouseHeld = true;
        Vector3Int tilePos = tileManager.tilesMap.WorldToCell(mousePos);
        if (tileManager.IsCanPlant(tilePos))
        {
            var randomDara = plantsData[Random.RandomRange(0, plantsData.Count)];
            Plants randPlant = Instantiate(randomDara);
            randPlant.sized = randomDara.sized; randPlant.growtTime = randomDara.growtTime; randPlant.position = tilePos;  // randPlant.position = tilePos; randPlant.growtTiles = randomDara.growtTiles;
            GameObject plant = Instantiate(randomDara.growtTile, TIleManager.Instance.getPosition(tilePos), Quaternion.identity);
            plant.transform.SetParent(Flowers.transform);
            randPlant.growtTile = plant;
            tileManager.AddPlant(randPlant, tilePos);
            growthManager.Register(randPlant.position);
        }
    }
    public void RegisterSeed(Vector3Int position, int growtTime)   
    {
        // Переделать таймер
        //Debug.Log(")
        int timeKey = (int)currentGameTime + growtTime;
        if (!growtQueue.ContainsKey(timeKey))  
        {
            growtQueue[timeKey] = new List<Vector3Int>();
        }
        growtQueue[timeKey].Add(position);
    }
}


public struct EventsData
{
    public Vector3 position;
    public int effectIndex;
    public int sfxIndex;
    public int audioSource;

    public EventsData(Vector3 pos, int e, int s, int a) => (position, effectIndex, sfxIndex, audioSource) = (pos, e, s, a);
    public EventsData(Vector3 pos, int e) => (position, effectIndex, sfxIndex, audioSource) = (pos, e, 0, 0);
    public EventsData(int s, int a) => (position, effectIndex, sfxIndex, audioSource) = (Vector3.zero, 0, s, a);
}