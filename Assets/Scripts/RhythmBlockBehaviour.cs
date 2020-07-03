using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RhythmBlockBehaviour : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 15f;
    [SerializeField]
    private Material disabledMaterial;
    [SerializeField]
    private Material enabledMaterial;

    private Collider collider;
    private bool lastBlock = false;

    public bool LastBlock { get => lastBlock; set => lastBlock = value; }

    private void Awake()
    {
        collider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        // Reset default material when block is enabled again
        gameObject.GetComponent<Renderer>().sharedMaterial = enabledMaterial;
        collider.enabled = true;
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
            collider.enabled = false;
            // Player missed a drum head, so he won't have firework 
            GameManager.instance.IsPlayerWin = false;
        }
    }

    private void OnDisable()
    {
        // If last block disabled it's endgame
        if (LastBlock)
            GameManager.instance.IsEndGame = true;
    }
}
