using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FlappyBirdClone.Scripts
{
    public class GameEvents : MonoBehaviour
    {
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

        public static GameEvents Current;
        public bool GameOver = false;
        public Season CurrentSeason = Season.Summer;
        public TimeOfDay CurrentTimeOfDay = TimeOfDay.Day;

        private void Awake()
        {
            Current = this;
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
