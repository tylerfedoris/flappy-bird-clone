using System;
using UnityEngine;

namespace FlappyBirdClone.Scripts
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private GameObject bodyLayerPrefab;
        [SerializeField] private bool isAbove = false;
        [SerializeField] private int bodyLayers = 3;
        
        private Transform thisTransform;
        private const float aboveYPosition = 3.5f;
        private const float belowYPosition = -3.5f;
        private Vector3 position;
        private Camera mainCamera;
        private Vector3 screenBounds;
        private const float outOfBoundsThreshold = 3f;

        private void Awake()
        {
            thisTransform = transform;
            position = thisTransform.position;
            
            var yPosition = isAbove ? aboveYPosition : belowYPosition;
            position = new Vector3(position.x, yPosition, position.z);
            thisTransform.position = position;
        }

        private void Start()
        {
            mainCamera = Camera.main;
            screenBounds =
                mainCamera
                    ? mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z)) 
                    : Vector3.zero;
        }

        private void Update()
        {
            if (transform.position.x < -screenBounds.x - outOfBoundsThreshold)
            {
                Destroy(gameObject);
            }
        }

        public void AddBodyLayers(int numLayers)
        {
            var offsetSign = isAbove ? 1 : -1;
            var yOffset = isAbove ? 2.0f : -2.0f;
            for (var layer = 0; layer < numLayers; layer++)
            {
                var bodyLayer = Instantiate(bodyLayerPrefab, thisTransform);
                bodyLayer.transform.localPosition = new Vector3(0.0f, yOffset + (layer * offsetSign), 0.0f);
            }

            thisTransform.position += new Vector3(0f, -offsetSign * numLayers, 0f);
        }
    }
}
