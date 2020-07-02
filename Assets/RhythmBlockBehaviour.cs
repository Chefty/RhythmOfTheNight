using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmBlockBehaviour : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 2f;

    void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Play mistake animation
            Destroy(gameObject);
        }
        else if (other.CompareTag("DrumStick"))
        {
            //Play success animation
            Destroy(gameObject);
        }
    }
}
