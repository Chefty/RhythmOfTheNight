using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmObjectSpawner : MonoBehaviour
{
    #region Variables
    // Public fields
    [SerializeField]
    private GameObject spawnable;
    [SerializeField]
    private int spawnableLimit;
    [SerializeField]
    private List<Transform> spawnPoints;

    // Private fields
    private bool lastNoteState = false;
    private Transform lastSpawnPoint = null;
    private ObjectPooler objectPooler;
    private int poIndex;
    #endregion

    #region Unity Callbacks
    private void Start()
    {
        // Init objects to pool
        objectPooler = ObjectPooler.SharedInstance;
        poIndex = objectPooler.AddObject(spawnable, 1, true);
    }
    #endregion

    #region Helper Methods
    /// <summary>
    /// Spawning rhythm objects depending on if there is a note or a rest time
    /// </summary>
    /// <param name="isNote"></param>
    public void SpawnRhythmNote(bool isNote, bool lastCall)
    {
        if (!spawnable || spawnPoints.Count <= 0) // Error check
        {
            Debug.LogError("Something went wrong while trying to spawn rhythm objects. Please, check the inspector.");
            return;
        }

        if (isNote && !lastNoteState) // Changing spawn points only if not consecutive notes
            lastSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

        // Caching new note
        lastNoteState = isNote;
        if (isNote) // If there is a note then we spawn a rhythm object from the pool
        {
            GameObject spawnedGO = objectPooler.GetPooledObject(poIndex);
            spawnedGO.transform.position = lastSpawnPoint.position;
            spawnedGO.GetComponent<AutoDisable>().enabled = false;
            spawnedGO.SetActive(true);
            if (lastCall)
                spawnedGO.GetComponent<RhythmBlockBehaviour>().LastBlock = true;
        }
    }

    /// <summary>
    /// Reset pooled rhythm blocks when game's start
    /// </summary>
    public void ClearSpawnableList()
    {
        foreach (var spawnable in objectPooler.GetAllPooledObjects(poIndex))
        {
            spawnable.SetActive(false);
        }
    } 
    #endregion
}
