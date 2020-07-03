using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmInterpreter : MonoBehaviour
{
    [SerializeField]
    private float rhythmDelay = 1f;
    RhythmObjectSpawner rhythmObjectSpawner;

    private char[] rhythmArray;
    private WaitForSeconds rhythmDelayCo;
    private bool isParsingDone = false;
    public bool IsParsingDone { get => isParsingDone; set => isParsingDone = value; }

    void Awake()
    {
        rhythmObjectSpawner = GetComponent<RhythmObjectSpawner>();
        rhythmDelayCo = new WaitForSeconds(rhythmDelay);
    }

    public void SetRhythmPattern(string rhythmPattern)
    {
        if (rhythmPattern == "")
            return;
        rhythmArray = rhythmPattern.ToCharArray();
        StartCoroutine(ParseRhythm());
    }

    /// <summary>
    /// Parsing rhythm string through an array of char spawning based on the char value 'x' being a note '.' being a rest
    /// </summary>
    /// <returns></returns>
    private IEnumerator ParseRhythm()
    {
        for (int i = 0; i < rhythmArray.Length; i++)
        {
            yield return rhythmDelayCo; // Delay spawning objects

            bool lastCall = (i == rhythmArray.Length - 1); // Last rhythm note in pattern

            if (rhythmArray[i] == 'x')
                rhythmObjectSpawner.SpawnRhythmNote(true, lastCall);
            else if (rhythmArray[i] == '.')
                rhythmObjectSpawner.SpawnRhythmNote(false, lastCall);
            else
                break;
        }
        isParsingDone = true;
    }
}
