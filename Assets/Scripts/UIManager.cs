using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Variables
    // Public fields
    [SerializeField]
    private TMP_InputField rhythmEditor;
    [SerializeField]
    private Button start;

    // Private fields
    private GameObject settings;
    private bool isPause = true;

    // Getter/Setter
    public bool IsPause { get => isPause; }
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        settings = transform.GetChild(0).gameObject;
        settings.SetActive(true); //Settings enabled on start
        start.onClick.AddListener(delegate { StartRhythmInterpreter(rhythmEditor.text); });
        PauseGame(); // Game's paused by defaut to display info and set rhythm pattern
    }

    private void Update()
    {
        // Pause option to display menu
        if (Input.GetKeyDown("escape"))
            isPause = !isPause;

        if (isPause)
            PauseGame();
        else
            ResumeGame();
    }
    #endregion

    #region Helper Methods
    public void PauseGame()
    {
        isPause = true;
        Time.timeScale = 0f;
        settings.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
        isPause = false;
        Time.timeScale = 1f;
        settings.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// Start to parse the rhythm and reset games values
    /// </summary>
    /// <param name="rhythmPattern"></param>
    private void StartRhythmInterpreter(string rhythmPattern)
    {
        if (rhythmPattern != "")
        {
            GameManager.instance.RhythmPattern = rhythmPattern; // Set rhythmPattern and trigger methods
            GameManager.instance.IsPlayerWin = true; // Reset win state
            isPause = false;
        }
        else
            Debug.Log("Missing rhythm pattern");
    } 
    #endregion
}
