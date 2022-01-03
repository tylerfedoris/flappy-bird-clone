using System;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

namespace FlappyBirdClone.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float jumpStrength = 20f;
        [SerializeField] private float maxVelocity = 10.0f;
        [SerializeField] private TMP_Text textComponent;

        private PlayerControls controls;
        private new Rigidbody2D rigidbody2D;
        private Camera mainCamera;

        private const float yMin = 0.07f;
        private const float yMax = 0.93f;

        private void Awake()
        {
            mainCamera = Camera.main;
            
            controls = new PlayerControls();
            controls.PlayerActions.Jump.performed += ctx => Jump();
        }

        // Start is called before the first frame update
        private void Start()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (GameEvents.Current.GameOver)
            {
                return;
            }
            
            if (textComponent)
            {
                textComponent.text = $"Jump Velocity:\n{rigidbody2D.velocity.magnitude}";
            }

            var viewportPoint = mainCamera.WorldToViewportPoint(transform.position);
            
            if (viewportPoint.y > yMax)
            {
                rigidbody2D.velocity = Vector2.zero;
            }

            viewportPoint.y = Mathf.Clamp(viewportPoint.y, yMin, yMax);
            transform.position = mainCamera.ViewportToWorldPoint(viewportPoint);
        }

        private void Jump()
        {
            if (GameEvents.Current.GameOver)
            {
                return;
            }
            
            var viewportPoint = mainCamera.WorldToViewportPoint(transform.position);
            
            if (viewportPoint.y <= yMin || viewportPoint.y >= yMax)
            {
                rigidbody2D.velocity = Vector2.zero;
            }
            
            if (rigidbody2D.velocity.y < maxVelocity)
            {
                rigidbody2D.AddForce(transform.up * jumpStrength, ForceMode2D.Impulse);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            GameEvents.Current.TriggerGameOver();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Obstacle"))
            {
                GameEvents.Current.IncreaseScore();
            }
        }

        private void OnEnable()
        {
            controls.PlayerActions.Enable();
        }
        
        private void OnDisable()
        {
            controls.PlayerActions.Disable();
        }
    }
}
