using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FlappyBirdClone.Scripts
{
    public class GameEvents : MonoBehaviour
    {
        public static GameEvents Current;
        public bool GameOver = false;

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
    }
}
