using System;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

namespace FlappyBirdClone.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float jumpStrength = 20f;
        [SerializeField] private float minVelocity = -10.0f;
        [SerializeField] private float maxVelocity = 10.0f;
        [SerializeField] private float positiveVelocityRotation = 40.0f;
        [SerializeField] private float negativeVelocityRotation = -40.0f;
        [SerializeField] private TMP_Text textComponent;
        [SerializeField] private AudioClip jumpSoundEffect;
        [SerializeField] private AudioClip collisionSoundEffect;
        [SerializeField] private AudioClip pointSoundEffect;
        [SerializeField] private bool godMode = false;

        private PlayerControls controls;
        private new Rigidbody2D rigidbody2D;
        private Camera mainCamera;
        private AudioSource audioSource;
        private Animator animator;

        private const float yMin = 0.07f;
        private const float yMax = 0.93f;
        
        private float scoredDelay = 1f;
        private float timeSinceScored = 0f; 

        private void Awake()
        {
            mainCamera = Camera.main;
            
            controls = new PlayerControls();
            controls.PlayerActions.Jump.performed += ctx => Jump();
            controls.PlayerActions.Quit.performed += ctx => Quit();

            audioSource = GetComponent<AudioSource>();
            animator = GetComponent<Animator>();
        }

        // Start is called before the first frame update
        private void Start()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (!GameEvents.Current.GameStart)
            {
                if (rigidbody2D.bodyType != RigidbodyType2D.Static)
                {
                    rigidbody2D.bodyType = RigidbodyType2D.Static;
                }
                return;
            }

            if (GameEvents.Current.GameOver)
            {
                return;
            }

            if (textComponent)
            {
                textComponent.text = $"Jump Velocity:\n{rigidbody2D.velocity.y}";
            }

            var viewportPoint = mainCamera.WorldToViewportPoint(transform.position);
            
            if (viewportPoint.y > yMax)
            {
                rigidbody2D.velocity = Vector2.zero;
            }

            viewportPoint.y = Mathf.Clamp(viewportPoint.y, yMin, yMax);
            transform.position = mainCamera.ViewportToWorldPoint(viewportPoint);

            float t = (rigidbody2D.velocity.y - minVelocity) / (maxVelocity - minVelocity);
            var newRotation = Quaternion.Lerp(Quaternion.Euler(0f, 0f, negativeVelocityRotation), 
                Quaternion.Euler(0f, 0f, positiveVelocityRotation), t);
            rigidbody2D.MoveRotation(newRotation);
        }

        private void Jump()
        {
            if (GameEvents.Current.GameOver)
            {
                return;
            }
            
            if (!GameEvents.Current.GameStart)
            {
                GameEvents.Current.GameStart = true;
                rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            }
            
            audioSource.PlayOneShot(jumpSoundEffect);
            
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
        
        private void Quit() {
            #if UNITY_STANDALONE
                Application.Quit();
            #endif
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (GameEvents.Current.GameOver || godMode) return;
            
            rigidbody2D.constraints = RigidbodyConstraints2D.None;
            
            // how much the player should be knocked back
            var magnitude = 1f;
            
            // calculate force vector
            var force = transform.position - other.transform.position;
            
            // normalize force vector to get direction only and trim magnitude
            force.Normalize();
            
            rigidbody2D.AddForce(force * magnitude, ForceMode2D.Impulse);
            animator.speed = 0;
            audioSource.PlayOneShot(collisionSoundEffect);
            GameEvents.Current.TriggerGameOver();
        }

        private void OnTriggerExit2D(Collider2D other)
        { 
            if (GameEvents.Current.GameOver) return;
            
            timeSinceScored += Time.deltaTime;

            if (!(timeSinceScored > -scoredDelay) || !other.gameObject.CompareTag("Obstacle")) return;
            
            GameEvents.Current.IncreaseScore();
            audioSource.PlayOneShot(pointSoundEffect);
            timeSinceScored = 0f;
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
