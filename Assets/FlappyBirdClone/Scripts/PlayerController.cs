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

        private void Awake()
        {
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
            if (textComponent)
            {
                textComponent.text = $"Jump Velocity:\n{rigidbody2D.velocity.magnitude}";
            }
        }

        private void Jump()
        {
            if (rigidbody2D.velocity.y < maxVelocity)
            {
                rigidbody2D.AddForce(transform.up * jumpStrength, ForceMode2D.Impulse);   
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
