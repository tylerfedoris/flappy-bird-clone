using System;
using UnityEngine;

namespace FlappyBirdClone.Scripts
{
    public class ContinuousMoveLeft : MonoBehaviour
    {
        public float speed;
        
        private Rigidbody2D rigidBody2D;

        private void Awake()
        {
            
        }

        // Start is called before the first frame update
        private void Start()
        {
            rigidBody2D = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        private void Update()
        {
            
        }

        private void FixedUpdate()
        {
            if (!GameEvents.Current.GameStart || GameEvents.Current.GameOver)
            {
                return;
            }
            
            var thisTransform = transform;
            rigidBody2D.MovePosition(thisTransform.position + (-thisTransform.right * (Time.deltaTime * speed)));
        }
    }
}
