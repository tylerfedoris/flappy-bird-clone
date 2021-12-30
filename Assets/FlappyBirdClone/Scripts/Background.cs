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
        [SerializeField] private int backgroundIndex = 0;

        [SerializeField] private BackgroundObject SectionA;
        [SerializeField] private BackgroundObject SectionB;
        [SerializeField] private BackgroundObject SectionC;

        private Camera mainCamera;
        private Vector3 screenBounds;

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
            
            // Section C Sprites
            foreach (var spriteRenderer in SectionC.top.GetComponentsInChildren<SpriteRenderer>())
            {
                spriteRenderer.sprite = backgroundSprites[backgroundIndex].top;
            }
            foreach (var spriteRenderer in SectionC.mid.GetComponentsInChildren<SpriteRenderer>())
            {
                spriteRenderer.sprite = backgroundSprites[backgroundIndex].mid;
            }
            foreach (var spriteRenderer in SectionC.bottom.GetComponentsInChildren<SpriteRenderer>())
            {
                spriteRenderer.sprite = backgroundSprites[backgroundIndex].bottom;
            }
        }
    }
}
