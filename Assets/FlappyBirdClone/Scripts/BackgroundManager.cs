using System;
using UnityEngine;

namespace FlappyBirdClone.Scripts
{
    public class BackgroundManager : MonoBehaviour
    {
        [SerializeField] private Background backgroundA;
        [SerializeField] private Background backgroundB;
        [SerializeField] private float transitionDuration = 10f;

        private bool transitionFromAtoB = true;

        // Start is called before the first frame update
        private void Start()
        {
            GameEvents.Current.onIncrementTimeOfDay += OnIncrementTimeOfDay;
        }

        // Update is called once per frame
        private void Update()
        {
        }

        private void OnIncrementTimeOfDay()
        {
            if (transitionFromAtoB)
            {
                backgroundB.SetSpritesToNextDay();
                backgroundB.TransitionAlpha(1f, transitionDuration);
                transitionFromAtoB = false;
            }
            else if (!transitionFromAtoB)
            {
                backgroundA.SetSpritesToNextDay();
                backgroundB.TransitionAlpha(0f, transitionDuration);
                transitionFromAtoB = true;
            }
        }

        private void OnDestroy()
        {
            GameEvents.Current.onIncrementTimeOfDay -= OnIncrementTimeOfDay;
        }
    }
}
