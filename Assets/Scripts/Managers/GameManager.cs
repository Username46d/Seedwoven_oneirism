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
    public List<Plants> unUsedPlants;
    public List<Plants> plantsData;
    public ScoreManager scoreManager;

    private SortedDictionary<int, List<Vector3Int>> growtQueue = new SortedDictionary<int, List<Vector3Int>>();

    public float currentGameTime = 0f;
    private float timer = 0f;

    public float chall = 1f;
    // Дополнительно
    private bool isMouseHeld = false;
    Vector3Int lastCell = new Vector3Int(999, 999, 0);
    private void Awake()
    {
        Instance = this;
        Time.timeScale = 1f;
    }
    void Start()
    {
        scoreManager = new ScoreManager();
    }
    void Update()
    {
        currentGameTime = Time.time;
        timer += Time.deltaTime;
        //Debug.Log(timer);
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
        if (timer >= 60f)
        {
            timer = 0f;
            Checked(scoreManager.CheckPoints());
        }
        HangleInput();
        if (Input.GetKeyDown(KeyCode.U))
        {
            AddNewPlant();
        }
    }
    void HangleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); mousePos.z = 0;
            PlantAtCell(mousePos);
            EventsManager.Instance.DoAudioEvents(new AudioEvent(0, 2)); EventsManager.Instance.DoParticleEvents(new ParticleEvent(mousePos, 0));
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
    private void PlantAtCell(Vector3 mousePos)
    {
        isMouseHeld = true;
        Vector3Int tilePos = tileManager.tilesMap.WorldToCell(mousePos);
        if (tileManager.isContained(tilePos))
        {
            if (tileManager.IsSpecialPlant(tilePos))
            {
                tileManager.getPlant(tilePos).Apply();
                Debug.Log("Выполнено в GameManager");
                return;
            }
            if (tileManager.IsCanPlant(tilePos) && plantsData.Count != 0)
            {
                var randomDara = plantsData[Random.RandomRange(0, plantsData.Count)];
                Plants randPlant = Instantiate(randomDara);
                randPlant.sized = randomDara.sized; randPlant.growtTime = randomDara.growtTime; randPlant.position = tilePos;  // randPlant.position = tilePos; randPlant.growtTiles = randomDara.growtTiles;
                GameObject plant = Instantiate(randomDara.growtTile, TIleManager.Instance.getPosition(tilePos), Quaternion.identity);
                plant.transform.SetParent(Flowers.transform);
                randPlant.growtTile = plant;
                tileManager.AddPlant(randPlant, tilePos);
                growthManager.Register(randPlant.position);
                return;
            }
        }
    }
    public void RegisterSeed(Vector3Int position, int growtTime)   
    {
        int timeKey = (int)(currentGameTime + (growtTime * chall));
        if (!growtQueue.ContainsKey(timeKey))  
        {
            growtQueue[timeKey] = new List<Vector3Int>();
        }
        growtQueue[timeKey].Add(position);
    }
    public void Checked(bool isDefeat)
    {
        if (isDefeat)
        {
            UIManager.Instance.Open(1);
        }
        else
        {
            NewChallenge();
        }
    }
    public void NewChallenge()
    {
        ChallengeManager.Instance.DoChallenge();
    }
    public void SetChall(float newChall)
    {
        chall = newChall;
    }
    public void AddNewPlant()
    {
        if (unUsedPlants.Count == 0)
        {
            return;
        }
        var newPlant = unUsedPlants[Random.RandomRange(0, unUsedPlants.Count)];
        plantsData.Add(newPlant);
        unUsedPlants.Remove(newPlant);
    }
    public List<Plants> RandomPlants()
    {
        List<Plants> plants = new List<Plants>();
        if (unUsedPlants.Count == 0)
        {
            return null;
        }
        if (unUsedPlants.Count <= 3)
        {
            for (int i = 0; i < unUsedPlants.Count; i++)
            {
                plants.Add(unUsedPlants[i]);
            }
            return plants;
        }
        if (unUsedPlants.Count > 3)
        {
            return unUsedPlants.OrderBy(x => Random.value).Take(3).ToList();
        }
        return null;
    }
    public bool BuyThisPlant(Plants plant)
    {
        if (scoreManager.DeleteScore(plant.price))
        {
            unUsedPlants.Remove(plant);
            plantsData.Add(plant);
            return true;
        }
        return false;
    }
}


