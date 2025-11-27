using System.Collections;
using System.Collections.Generic;
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
    public TMP_Text[] textMeshPros;
    public Image[] images;
    public Button[] buttons;

    private List<Challenge> challengees;
    private void Start()
    {
        Instance = this;
        challengees = new List<Challenge>();
    }
    public void UpdateScore(int score)
    {
        scoreText.text = $"Current score: {score}";
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
        Debug.Log("Выполнилось");
        if (challenge1._sprite == null || challenge2._sprite == null || images[0] == null || images[1] == null)
        {
            Debug.Log("абдябдя");
        }
        images[0].sprite = challenge1._sprite;
        Debug.Log("Выполнилось");
        textMeshPros[0].text = challenge1._text;
        Debug.Log("Выполнилось");
        images[1].sprite = challenge2._sprite;
        Debug.Log("Выполнилось");
        textMeshPros[1].text = challenge2._text;
        Debug.Log("Выполнилось");
        challengees.Clear();
        challengees.Add(challenge1); challengees.Add(challenge2);
    }
    public void OnChallengSelected(int i)
    {
        Debug.Log(challengees.Count);
        challengees[i].Apply();
        Close(2);
    }
}
