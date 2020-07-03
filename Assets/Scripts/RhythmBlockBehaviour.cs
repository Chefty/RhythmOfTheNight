using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RhythmBlockBehaviour : MonoBehaviour
{
    #region Variables
    // Public fields
    [SerializeField]
    private float moveSpeed = 15f;
    [SerializeField]
    private Material disabledMaterial;
    [SerializeField]
    private Material enabledMaterial;

    // Private fields
    private Collider blockCollider;
    private bool lastBlock = false; 
    #endregion

    // Getter/Setter
    public bool LastBlock { get => lastBlock; set => lastBlock = value; }

    #region Unity Callbacks
    private void Awake()
    {
        blockCollider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        // Reset default material and collider state
        gameObject.GetComponent<Renderer>().sharedMaterial = enabledMaterial;
        blockCollider.enabled = true;
    }

    void Update()
    {
        // Moving block forward
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("OutArea"))
        {
            // Set block color to disabled state before being auto disabled
            gameObject.GetComponent<Renderer>().sharedMaterial = disabledMaterial;
            gameObject.GetComponent<AutoDisable>().enabled = true;
            // Disable collision, block is out of range
            blockCollider.enabled = false;
            // Player missed a drum head, so he won't have his firework 
            GameManager.instance.IsPlayerWin = false;
        }
    }

    private void OnDisable()
    {
        // If last block disabled it's endgame
        if (LastBlock)
        {
            lastBlock = false; // Reset variable for next round
            GameManager.instance.IsEndGame = true;
        }
    }
    #endregion
}
