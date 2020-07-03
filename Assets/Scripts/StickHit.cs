using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickHit : MonoBehaviour
{
    #region Variables
    // Public fields
    [SerializeField]
    private GameObject hitFX;
    [SerializeField]
    private AudioClip[] hitAudioClips;

    // Private fields
    private ObjectPooler objectPooler;
    private int poIndex;
    private AudioSource audioSource;
    private GameObject lastCollidedObject;
    #endregion

    // Start is called before the first frame update
    #region Unity Callbacks
    void Start()
    {
        objectPooler = ObjectPooler.SharedInstance;
        poIndex = objectPooler.AddObject(hitFX, 1, true);
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RhythmObject") && other.gameObject != lastCollidedObject) //Avoid trigger twice with same GO
        {
            lastCollidedObject = other.gameObject;
            StartCoroutine(DrumHitCO(lastCollidedObject));
        }
    }
    #endregion

    #region Helper Methods
    private IEnumerator DrumHitCO(GameObject other)
    {
        // Play audio feedback
        float pitch = Random.Range(.2f, 1.5f);
        audioSource.pitch = pitch;
        audioSource.Play();
        // Spawning particle effect FX pooled
        GameObject spawnedGO = objectPooler.GetPooledObject(poIndex);
        spawnedGO.transform.position = transform.position;
        spawnedGO.SetActive(true);
        // Disable drum head
        other.SetActive(false);
        yield return null;
    } 
    #endregion
}
