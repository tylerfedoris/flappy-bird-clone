using System;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBirdClone.Scripts
{
    public class Background : MonoBehaviour
    {
        [System.Serializable]
        private struct BackgroundSpriteObject
        {
            public Sprite top;
            public Sprite mid;
            public Sprite bottom;
        }

        [System.Serializable]
        private struct BackgroundObject
        {
            public GameObject top;
            public GameObject mid;
            public GameObject bottom;
        }

        [SerializeField] private List<BackgroundSpriteObject> backgroundSprites;
        [SerializeField] private float xOffset = 10.5f;
        [SerializeField] private int backgroundIndex = 0;

        [SerializeField] private BackgroundObject SectionA;
        [SerializeField] private BackgroundObject SectionB;

        private void Awake()
        {
            if (backgroundIndex > backgroundSprites.Count)
            {
                return;
            }
            
            // Section A Sprites
            foreach (var spriteRenderer in SectionA.top.GetComponentsInChildren<SpriteRenderer>())
            {
                spriteRenderer.sprite = backgroundSprites[backgroundIndex].top;
            }
            foreach (var spriteRenderer in SectionA.mid.GetComponentsInChildren<SpriteRenderer>())
            {
                spriteRenderer.sprite = backgroundSprites[backgroundIndex].mid;
            }
            foreach (var spriteRenderer in SectionA.bottom.GetComponentsInChildren<SpriteRenderer>())
            {
                spriteRenderer.sprite = backgroundSprites[backgroundIndex].bottom;
            }
            
            // Section B Sprites
            foreach (var spriteRenderer in SectionB.top.GetComponentsInChildren<SpriteRenderer>())
            {
                spriteRenderer.sprite = backgroundSprites[backgroundIndex].top;
            }
            foreach (var spriteRenderer in SectionB.mid.GetComponentsInChildren<SpriteRenderer>())
            {
                spriteRenderer.sprite = backgroundSprites[backgroundIndex].mid;
            }
            foreach (var spriteRenderer in SectionB.bottom.GetComponentsInChildren<SpriteRenderer>())
            {
                spriteRenderer.sprite = backgroundSprites[backgroundIndex].bottom;
            }
        }

        // Start is called before the first frame update
        private void Start()
        {
            var thisTransform = transform;
            var thisRotation = thisTransform.rotation;
        }

        // Update is called once per frame
        private void Update()
        {
        
        }
    }
}
