using System;
using System.Collections;
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

        public void SetSpritesToNextDay()
        {
            backgroundIndex = GameEvents.Current.CurrentTimeOfDay == GameEvents.TimeOfDay.Twilight 
                ? 0 
                : (int)GameEvents.Current.CurrentTimeOfDay + 1;
            
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
        
        public void TransitionAlpha(float endAlpha, float duration)
        {
            foreach (var spriteRenderer in SectionA.top.GetComponentsInChildren<SpriteRenderer>())
            {
                StartCoroutine(LerpAlpha(spriteRenderer, endAlpha, duration));
            }
            foreach (var spriteRenderer in SectionA.mid.GetComponentsInChildren<SpriteRenderer>())
            {
                StartCoroutine(LerpAlpha(spriteRenderer, endAlpha, duration));
            }
            foreach (var spriteRenderer in SectionA.bottom.GetComponentsInChildren<SpriteRenderer>())
            {
                StartCoroutine(LerpAlpha(spriteRenderer, endAlpha, duration));
            }
            
            // Section B Sprites
            foreach (var spriteRenderer in SectionB.top.GetComponentsInChildren<SpriteRenderer>())
            {
                StartCoroutine(LerpAlpha(spriteRenderer, endAlpha, duration));
            }
            foreach (var spriteRenderer in SectionB.mid.GetComponentsInChildren<SpriteRenderer>())
            {
                StartCoroutine(LerpAlpha(spriteRenderer, endAlpha, duration));
            }
            foreach (var spriteRenderer in SectionB.bottom.GetComponentsInChildren<SpriteRenderer>())
            {
                StartCoroutine(LerpAlpha(spriteRenderer, endAlpha, duration));
            }
            
            // Section C Sprites
            foreach (var spriteRenderer in SectionC.top.GetComponentsInChildren<SpriteRenderer>())
            {
                StartCoroutine(LerpAlpha(spriteRenderer, endAlpha, duration));
            }
            foreach (var spriteRenderer in SectionC.mid.GetComponentsInChildren<SpriteRenderer>())
            {
                StartCoroutine(LerpAlpha(spriteRenderer, endAlpha, duration));
            }
            foreach (var spriteRenderer in SectionC.bottom.GetComponentsInChildren<SpriteRenderer>())
            {
                StartCoroutine(LerpAlpha(spriteRenderer, endAlpha, duration));
            }
        }

        private IEnumerator LerpAlpha(SpriteRenderer spriteToTransition, float endAlpha, float duration)
        {
            float elapsedTime = 0;
            var startValue = spriteToTransition.color;
            var endValue = new Color(startValue.r, startValue.b, startValue.g, endAlpha);

            while (elapsedTime < duration)
            {
                spriteToTransition.color = Color.Lerp(startValue, endValue, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            spriteToTransition.color = endValue;
        }
    }
}
