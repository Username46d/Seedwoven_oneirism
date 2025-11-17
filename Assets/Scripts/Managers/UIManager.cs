using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public TextMeshProUGUI scoreText;
    private void Start()
    {
        Instance = this;
    }
    public void UpdateScore(int score)
    {
        scoreText.text = $"Current score: {score}";
    }
}
