using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmObjectSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnable;
    [SerializeField]
    private List<Transform> spawnPoints;

    private bool lastNoteState = false;
    Transform lastSpawnPoint = null;
    private List<GameObject> spawnableList = new List<GameObject>();
    /// <summary>
    /// Spawning rhythm objects depending on if there is a note or a rest time
    /// </summary>
    /// <param name="isNote"></param>
    public void SpawnRhythmNote(bool isNote, char note)
    {
        Debug.Log(note);
        if (!spawnable || spawnPoints.Count <= 0) //Error check
            return;
        if (isNote && !lastNoteState) // Changing spawn points only if not consecutive notes
            lastSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
        
        // Caching new note
        lastNoteState = isNote;
        if (isNote) // If there is a note then we spawn a rhythm object
            spawnableList.Add(Instantiate(spawnable, lastSpawnPoint.position, spawnable.transform.rotation));
    }

    /// <summary>
    /// Reset the game
    /// </summary>
    public void ClearSpawnableList()
    {
        if (spawnableList.Count <= 0)
            return;

        foreach (var spawnable in spawnableList)
        {
            Destroy(spawnable);
        }
        spawnableList.Clear();
    }
}
