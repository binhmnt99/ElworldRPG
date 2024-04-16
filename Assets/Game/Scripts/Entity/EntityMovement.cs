using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Entity
{
    [RequireComponent(typeof(CharacterController))]
    public class EntityMovement : MonoBehaviour
    {
        private CharacterController characterController;
        private Vector3 velocity;
        private bool isGrounded;
        private float gravity = -9.81f;
        public float MovementSpeed = 2f;

        private PlayerController playerController;

        private void Awake() {
            characterController = gameObject.GetComponent<CharacterController>();
            playerController = new PlayerController();
        }

        private void OnEnable()
        {
            playerController?.Enable();
        }

        private void OnDisable()
        {
            playerController?.Disable();
        }

        private void Update()
        {
            isGrounded = characterController.isGrounded;
            if (isGrounded && velocity.y < 0f)
            {
                velocity.y = 0f;
            }

            float x = playerController.PlayerAction.Movement.ReadValue<Vector2>().x;
            float y = playerController.PlayerAction.Movement.ReadValue<Vector2>().y;

            Vector3 move = new(x,0,y);
            characterController.Move(move * Time.deltaTime * MovementSpeed);

            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
            }

            velocity.y += gravity * Time.deltaTime;
            characterController.Move(velocity * Time.deltaTime);

        }
    }
}
