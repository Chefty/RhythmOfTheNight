using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float gravityValue = -10f;
    [SerializeField]
    private GameObject drumStick;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Animator animator;
    private Collider drumStickCollider;
    #endregion

    #region Unity Callbacks
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        drumStickCollider = drumStick.GetComponent<Collider>();
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        // Round up velocity value
        if (groundedPlayer && playerVelocity.y < 0)
            playerVelocity.y = 0f;

        // Player movement (horizontal only)
        float HorizontalAxis = Input.GetAxis("Horizontal");
        Vector2 move = new Vector2(HorizontalAxis, 0);
        controller.Move(move * Time.deltaTime * playerSpeed);
        if (move != Vector2.zero)
            gameObject.transform.position = move;

        // Hit with the stick
        if (Input.GetButtonDown("Jump") && groundedPlayer)
            animator.SetTrigger("StickHit");

        // Apply gravity to the player
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    } 
    #endregion

    // Enabling/Disabling stick collider (through animation) only when player strikes
    #region Animation Events
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
