using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    private GameObject drumStick;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Animator animator;
    private Collider drumStickCollider;
    
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        drumStickCollider = drumStick.GetComponent<Collider>();
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        float HorizontalAxis = Input.GetAxis("Horizontal");
        Vector2 move = new Vector2(HorizontalAxis, 0);

        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector2.zero)
        {
            gameObject.transform.position = move;
        }

        // Hit with the stick
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            animator.SetTrigger("StickHit");
        }

        // Apply gravity to the player
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }


    #region AnimationEvent
    
    public void EnableStickCollider()
    {
        drumStickCollider.enabled = true;
    }

    public void DisableStickCollider()
    {
        drumStickCollider.enabled = false;
    }

    #endregion
}
