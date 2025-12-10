using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [Header("Setting")]
    public TextMeshProUGUI scoreText;
    public GameObject[] gameObjects;

    [Header("ChallengePanelSetting")]
    public TMP_Text[] textMeshPros;
    public Image[] images;

    [Header("ShopPanelSetting")]
    public TMP_Text[] shopText;
    public Image[] shopImages;
    public GameObject[] shopObjects;
    public TMP_Text[] shopButtonText;
    public Button[] shopButton;

    [Header("ChoicePanel")]
    public GameObject transformButtons;
    public Image[] choiceImage;

    public TextMeshProUGUI timer;
    private List<Challenge> challengees;
    private List<Plants> plantes;

    private int expandPlantIndex = -1;
    private int scorePlantInddex = -1;
    int tscore = 0;
    private Button[] choiceButtons;
    private void Start()
    {
        Instance = this;
        choiceButtons = transformButtons.transform.GetComponentsInChildren<Button>(true);
        choiceButtons = choiceButtons.OrderBy(btn => btn.name).ToArray();
        challengees = new List<Challenge>();
        plantes = new List<Plants>();
    }
    public void UpdateScore(int score, int fine)
    {
        scoreText.text = $"Current score: {score} / {fine}";
        tscore = score;
    }
    public void UpdateTime(int time)
    {
        timer.text = $"{time}";
    }
    public void Open(int index)
    {
        gameObjects[index].SetActive(true);
        Time.timeScale = 0f;
    }
    public void Close(int index)
    {
        gameObjects[index].SetActive(false);
        Time.timeScale = 1f;
    }
    public void SceneLoad(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Open(0);
        }
    } 
    public void ChangeChallengePalen(Challenge challenge1, Challenge challenge2)
    {
        images[0].sprite = challenge1._sprite;
        textMeshPros[0].text = challenge1._text;
        images[1].sprite = challenge2._sprite;
        textMeshPros[1].text = challenge2._text;
        challengees.Clear();
        challengees.Add(challenge1); challengees.Add(challenge2);
    }
    public void Shop()
    {
        for (int i = 0; i < 3; i++)
        {
            shopObjects[i].SetActive(false);
            shopButton[i].interactable = true; shopButton[i].GetComponent<Image>().color = Color.white;
        }
        List<Plants> plants = GameManager.Instance.RandomPlants();
        if (plants == null)
        {
            return;
        }
        else
        {
            plantes.Clear();
            for (int i = 0; i < plants.Count; i++)
            {
                shopImages[i].sprite = plants[i].growtTile.GetComponent<SpriteRenderer>().sprite;
                shopText[i].text = plants[i].descript;
                shopButtonText[i].text = $"{plants[i].price}";
                shopObjects[i].SetActive(true);
                plantes.Add(plants[i]);
            }
        }
        shopText[3].text = $"{tscore}";
        Open(3);
    }
    public void OnChallengSelected(int i)
    {
        Debug.Log(challengees.Count);
        challengees[i].Apply();
        Close(2);
    }
    public void Continue()
    {
        if (expandPlantIndex != -1 && scorePlantInddex != -1)
        {
            GameManager.Instance.AddPlant(GameManager.Instance.unUsedPlants[scorePlantInddex]); GameManager.Instance.AddPlant(GameManager.Instance.unUsedPlants[expandPlantIndex]);
            Close(4);
        }
    }
    public void BuyPlant(int i)
    {
        if (GameManager.Instance.BuyThisPlant(plantes[i]))
        {
            shopButton[i].interactable = false;
            shopButton[i].GetComponent<Image>().color = Color.gray;
        }
        Close(3);
    }
    public void ChoicePanel()
    {
        Plants[] plants = GameManager.Instance.unUsedPlants.ToArray();
        for (int i = 0; i < plants.Length; i++)
        {
            choiceButtons[i].GetComponent<Image>().sprite = plants[i].growtTile.GetComponent<SpriteRenderer>().sprite;
        }
        Open(4);
    }
    public void ChoiceThisPlant(int i)
    {
        if (GameManager.Instance.unUsedPlants[i].varietyFlower == VarietyFlower.ExpandedFlower)
        {
            expandPlantIndex = i;
            choiceImage[0].sprite = GameManager.Instance.unUsedPlants[i].growtTile.GetComponent<SpriteRenderer>().sprite;
        }
        else if(GameManager.Instance.unUsedPlants[i].varietyFlower == VarietyFlower.ScoreFlower)
        {
            scorePlantInddex = i;
            choiceImage[1].sprite = GameManager.Instance.unUsedPlants[i].growtTile.GetComponent<SpriteRenderer>().sprite;
        }
    }
}
