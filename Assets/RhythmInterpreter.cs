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
    private string lastRhythmPattern;

    void Awake()
    {
        rhythmObjectSpawner = GetComponent<RhythmObjectSpawner>();
        rhythmDelayCo = new WaitForSeconds(rhythmDelay);
    }

    public void SetRhythmPattern(string rhythmPattern)
    {
        Debug.LogError("SetRhythmPattern - " + rhythmPattern);
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
        Debug.LogError("ParseRhythm");
        for (int i = 0; i < rhythmArray.Length; i++)
        {
            yield return rhythmDelayCo; // Delay spawning objects

            if (rhythmArray[i] == 'x')
                rhythmObjectSpawner.SpawnRhythmNote(true, rhythmArray[i]);
            else if (rhythmArray[i] == '.')
                rhythmObjectSpawner.SpawnRhythmNote(false, rhythmArray[i]);
            else
                break;
        }

        isParsingDone = true;
    }
}
