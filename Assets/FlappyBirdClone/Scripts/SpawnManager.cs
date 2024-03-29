using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FlappyBirdClone.Scripts
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] private float obstacleMoveSpeed = 3.0f;
        [SerializeField] private List<GameObject> aboveObstaclePrefabs;
        [SerializeField] private List<GameObject> belowObstaclePrefabs;
        [SerializeField] private int maxObstacleSize = 3;
        [SerializeField] private float spawnInterval = 2f;

        private Vector3 spawnPosition;
        private const float spawnPositionOffset = 3f;
        private float elapsedTime = 0.0f;

        // Start is called before the first frame update
        private void Start()
        {
            spawnPosition = new Vector3(GameEvents.Current.ScreenBounds.x + spawnPositionOffset, 0f, 0f);
            SpawnObstaclePair();
        }

        // Update is called once per frame
        private void Update()
        {
            if (!GameEvents.Current.GameStart || GameEvents.Current.GameOver)
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

            var aboveObstaclePrefab = aboveObstaclePrefabs[Random.Range(0, aboveObstaclePrefabs.Count)];
            var aboveObstacle = Instantiate(aboveObstaclePrefab, spawnPosition, parentRotation,
                parentObject.transform);
            int aboveObstacleSize = Random.Range(0, maxObstacleSize + 1);
            aboveObstacle.GetComponent<Obstacle>().AddBodyLayers(aboveObstacleSize);

            var belowObstaclePrefab = belowObstaclePrefabs[Random.Range(0, belowObstaclePrefabs.Count)];
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
