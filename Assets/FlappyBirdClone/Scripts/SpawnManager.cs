using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FlappyBirdClone.Scripts
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] private float obstacleMoveSpeed = 3.0f;
        [SerializeField] private GameObject aboveObstaclePrefab;
        [SerializeField] private GameObject belowObstaclePrefab;
        [SerializeField] private int maxObstacleSize = 3;
        [SerializeField] private float spawnInterval = 2f;

        private Camera mainCamera;
        private Vector3 screenBounds;
        private Vector3 spawnPosition;
        private const float spawnPositionOffset = 3f;
        private float elapsedTime = 0.0f;

        // Start is called before the first frame update
        private void Start()
        {
            mainCamera = Camera.main;
            screenBounds =
                mainCamera
                    ? mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z)) 
                    : Vector3.zero;
            spawnPosition = new Vector3(screenBounds.x + spawnPositionOffset, 0f, 0f);
            SpawnObstaclePair();
        }

        // Update is called once per frame
        private void Update()
        {
            if (GameEvents.Current.GameOver)
            {
                return;
            }
            
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= spawnInterval)
            {
                SpawnObstaclePair();
                elapsedTime = 0f;
            }
        }

        private void SpawnObstaclePair()
        {
            var parentObject = GameObject.Find("_Dynamic");
            var parentRotation = parentObject.transform.rotation;
            
            var aboveObstacle = Instantiate(aboveObstaclePrefab, spawnPosition, parentRotation,
                parentObject.transform);
            var aboveObstacleSize = Random.Range(0, maxObstacleSize + 1);
            aboveObstacle.GetComponent<Obstacle>().AddBodyLayers(aboveObstacleSize);

            var belowObstacle = Instantiate(belowObstaclePrefab, spawnPosition, parentRotation,
                parentObject.transform);
            belowObstacle.GetComponent<Obstacle>().AddBodyLayers(maxObstacleSize - aboveObstacleSize);

            var aboveMovementScript = aboveObstacle.GetComponent<ContinuousMoveLeft>();
            aboveMovementScript.speed = obstacleMoveSpeed;
            var belowMovementScript = belowObstacle.GetComponent<ContinuousMoveLeft>();
            belowMovementScript.speed = obstacleMoveSpeed;
        }
    }
}
