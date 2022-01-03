using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FlappyBirdClone.Scripts
{
    public class GameEvents : MonoBehaviour
    {
        [SerializeField] private TMP_Text ScoreText;
        
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

        private Season[] seasons;
        private TimeOfDay[] timesOfDay;
        private Camera mainCamera;
        private int currentScore = 0;

        public static GameEvents Current;
        public bool GameOver = false;
        public Season CurrentSeason = Season.Summer;
        public TimeOfDay CurrentTimeOfDay = TimeOfDay.Day;
        public Vector3 ScreenBounds;

        private void Awake()
        {
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

        public void IncreaseScore()
        {
            currentScore++;
            if (ScoreText)
            {
                ScoreText.text = currentScore.ToString();
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
