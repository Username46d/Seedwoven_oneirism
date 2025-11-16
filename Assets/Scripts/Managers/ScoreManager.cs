    using System;
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using DG.Tweening;

    public class ScoreManager : MonoBehaviour
    {
        public int totalScore { get; private set; }
        public int currentScore { get; private set; }
        public TextMeshProUGUI scoreText;
        public event Action<int> OnScoreChanged;
        private Vector3 originalPos;
        public AudioSource audioSource;
        public float minPitch = 0.9f;
        public float maxPitch = 1.1f;

        void Awake()
        {
            totalScore = 0;
            currentScore = 999;
        }

        void Start()
        {
            if (scoreText != null)
            {
                scoreText.text = "Points: " + currentScore.ToString();
                originalPos = scoreText.transform.localPosition;
            }
            else
                Debug.LogWarning("ScoreManager: scoreText reference not set!");
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                AddScore(10);
            }
        }

        public void AddScore(int points)
        {
            totalScore += points;
            currentScore += points;
            OnScoreChanged?.Invoke(points);
            Debug.Log($"Added {points} points. Current: {currentScore}. Total: {totalScore}");

            if (scoreText != null)
                scoreText.text = "Points: " + currentScore.ToString();

            scoreText.transform.DOKill();
            scoreText.transform.localPosition = originalPos;

            scoreText.transform.DOLocalMoveY(originalPos.y - 4f, 0.3f)
                .SetEase(Ease.OutExpo)
                .OnComplete(() =>
                {
                    scoreText.transform.DOLocalMoveY(originalPos.y, 0.5f)
                        .SetEase(Ease.OutExpo);
                });

            audioSource.pitch = UnityEngine.Random.Range(minPitch, maxPitch);
            audioSource.Play();
        }
    }
