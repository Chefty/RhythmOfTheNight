using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickHit : MonoBehaviour
{
    [SerializeField]
    private GameObject hitFX;
    [SerializeField]
    AudioClip[] hitAudioClips;

    private ObjectPooler objectPooler;
    private int poIndex;
    private AudioSource audioSource;
    private GameObject lastCollidedObject;

    // Start is called before the first frame update
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
}
