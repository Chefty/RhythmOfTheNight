using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmInterpreter : MonoBehaviour
{
    [SerializeField]
    private float rhythmDelay = 1f;
    private string rhythm = "xx.x.x.xxx.x.xxxx.xx..x.x.xxx...xx.x...xx..x.x.x.x";
    RhythmObjectSpawner rhythmObjectSpawner;

    private char[] rhythmArray;
    private WaitForSeconds rhythmDelayCo;
    private bool isParsingDone = false;

    void Start()
    {
        rhythmObjectSpawner = GetComponent<RhythmObjectSpawner>();
        rhythmArray = rhythm.ToCharArray();
        rhythmDelayCo = new WaitForSeconds(rhythmDelay);
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
