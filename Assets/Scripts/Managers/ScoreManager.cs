using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class ScoreManager 
{
    int totalScore = 0;
    int currentScore = 0;

    public ScoreManager() => (totalScore, currentScore) = (0, 0);
    public ScoreManager(int total, int current) => (totalScore, currentScore) = (total, current);

    public void AddScore(int points)
    {
        totalScore += points;
        currentScore += points;
        UIManager.Instance.UpdateScore(currentScore);
    }
}
