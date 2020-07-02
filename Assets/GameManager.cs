using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private RhythmObjectSpawner rhythmObjectSpawner;
    private RhythmInterpreter rhythmInterpreter;
    private string rhythmPattern;
    public string RhythmPattern { 
        get => rhythmPattern;
        set {
            if (value != rhythmPattern)
            {
                rhythmObjectSpawner.ClearSpawnableList();
                rhythmInterpreter.SetRhythmPattern(value);
                rhythmPattern = value;
            }
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            rhythmObjectSpawner = FindObjectOfType<RhythmObjectSpawner>();
            rhythmInterpreter = FindObjectOfType<RhythmInterpreter>();
        }
        else
            Destroy(this);
    }
}
