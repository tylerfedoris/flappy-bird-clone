using System;
using System.Diagnostics.CodeAnalysis;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FlappyBirdClone.Scripts
{
    public class GameEvents : MonoBehaviour
    {
        [SerializeField] private TMP_Text ScoreText;
        [SerializeField] private TMP_Text BestScoreText;
        
        public enum Season
        {
            Summer,
            Winter
        }

        public enum TimeOfDay
        {
            Day,
            Sunset,
            Evening,
            Night,
            Twilight
        }
        
        private const string bestScoreKey = "BestScore";

        private Season[] seasons;
        private TimeOfDay[] timesOfDay;
        private Camera mainCamera;
        private int currentScore = 0;
        private int bestScore = 0;

        public static GameEvents Current;
        public bool GameOver = false;
        public Season CurrentSeason = Season.Summer;
        public TimeOfDay CurrentTimeOfDay = TimeOfDay.Day;
        public Vector3 ScreenBounds;

        private void Awake()
        {
            bestScore = PlayerPrefs.GetInt(bestScoreKey, 0);
            mainCamera = Camera.main;
            ScreenBounds =
                mainCamera
                    ? mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z)) 
                    : Vector3.zero;
            Current = this;
        }

        private void Start()
        {
            if (ScoreText)
            {
                ScoreText.text = currentScore.ToString();
            }

            if (BestScoreText)
            {
                BestScoreText.text = bestScore.ToString();
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown("r"))
            {
                SceneManager.LoadScene("MainScene");
            }
        }

        public event Action onTriggerGameOver;

        public void TriggerGameOver()
        {
            Debug.Log("GAME OVER!");
            GameOver = true;
            onTriggerGameOver?.Invoke();
        }

        [SuppressMessage("ReSharper", "InvertIf")]
        public void IncreaseScore()
        {
            currentScore++;
            if (ScoreText)
            {
                ScoreText.text = currentScore.ToString();
            }

            if (currentScore > bestScore)
            {
                bestScore = currentScore;
                PlayerPrefs.SetInt(bestScoreKey, bestScore);
                if (BestScoreText)
                {
                    BestScoreText.text = bestScore.ToString();
                }
            }
        }

        public void IncrementSeason()
        {
            CurrentSeason = CurrentSeason switch
            {
                Season.Summer => Season.Winter,
                Season.Winter => Season.Summer,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
        public void IncrementTimeOfDay()
        {
            CurrentTimeOfDay = CurrentTimeOfDay switch
            {
                TimeOfDay.Day => TimeOfDay.Sunset,
                TimeOfDay.Sunset => TimeOfDay.Evening,
                TimeOfDay.Evening => TimeOfDay.Night,
                TimeOfDay.Night => TimeOfDay.Twilight,
                TimeOfDay.Twilight => TimeOfDay.Day,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
