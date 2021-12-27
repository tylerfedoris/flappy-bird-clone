using System;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBirdClone.Scripts
{
    public class Background : MonoBehaviour
    {
        [SerializeField] private List<GameObject> backgroundPrefabs;
        [SerializeField] private Vector3 startingPosition = Vector3.zero;
        [SerializeField] private float xOffset;
        [SerializeField] private int sections = 3;
        
        private GameObject[] backgroundSections;

        private void Awake()
        {
            backgroundSections = new GameObject[sections];
        }

        // Start is called before the first frame update
        private void Start()
        {
            var thisTransform = transform;
            var thisRotation = thisTransform.rotation;

            for (var i = 0; i < backgroundSections.Length; ++i)
            {
                backgroundSections[i] = Instantiate(backgroundPrefabs[0], startingPosition + new Vector3(xOffset * i, 0f, 0f), thisRotation, thisTransform);
            }
        }

        // Update is called once per frame
        private void Update()
        {
        
        }
    }
}
