using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmObjectSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnable;
    [SerializeField]
    private int spawnableLimit;
    [SerializeField]
    private List<Transform> spawnPoints;

    private bool lastNoteState = false;
    private Transform lastSpawnPoint = null;
    private ObjectPooler objectPooler;
    private int poIndex;

    private void Start()
    {
        objectPooler = ObjectPooler.SharedInstance;
        poIndex = objectPooler.AddObject(spawnable, 1, true);
    }

    /// <summary>
    /// Spawning rhythm objects depending on if there is a note or a rest time
    /// </summary>
    /// <param name="isNote"></param>
    public void SpawnRhythmNote(bool isNote, bool lastCall)
    {
        if (!spawnable || spawnPoints.Count <= 0) //Error check
            return;

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
    /// Reset the game
    /// </summary>
    public void ClearSpawnableList()
    {
        foreach (var spawnable in objectPooler.GetAllPooledObjects(poIndex))
        {
            spawnable.SetActive(false);
        }
    }
}
