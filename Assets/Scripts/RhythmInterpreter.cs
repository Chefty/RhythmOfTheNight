using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmInterpreter : MonoBehaviour
{
    #region Variables
    // Public fields
    [SerializeField]
    private float rhythmDelay = 1f;

    // Private fields
    private char[] rhythmArray;
    private WaitForSeconds rhythmDelayCo;
    #endregion

    #region Unity Callbacks
    void Awake()
    {
        rhythmDelayCo = new WaitForSeconds(rhythmDelay);
    }
    #endregion

    #region Helper Methods
    /// <summary>
    /// If not empty - Set rhythm pattern and start parsing
    /// </summary>
    /// <param name="rhythmPattern"></param>
    public void SetRhythmPattern(string rhythmPattern)
    {
        if (rhythmPattern == "")
        {
            Debug.LogError("Empty rhythm pattern, can't start the game");
            return;
        }
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

            bool lastCall = (i == rhythmArray.Length - 1); // Last rhythm note in pattern - end of parsing

            if (rhythmArray[i] == 'x')
                GameManager.instance.rhythmObjectSpawner.SpawnRhythmNote(true, lastCall);
            else if (rhythmArray[i] == '.')
                GameManager.instance.rhythmObjectSpawner.SpawnRhythmNote(false, lastCall);
            else
                break;
        }
    } 
    #endregion
}
