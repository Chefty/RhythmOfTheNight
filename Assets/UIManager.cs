using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField rhythmEditor;
    [SerializeField]
    private Button start;

    private GameObject settings;
    private bool isPause = true;

    private void Awake()
    {
        settings = transform.GetChild(0).gameObject;
        settings.SetActive(true); //Settings enabled on start
        start.onClick.AddListener(delegate { StartRhythmInterpreter(rhythmEditor.text); });
        PauseGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
            isPause = !isPause;
        
        if (isPause)
            PauseGame();
        else
            ResumeGame();
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        settings.SetActive(true);
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
        settings.SetActive(false);
    }

    private void StartRhythmInterpreter(string rhythmPattern)
    {
        Debug.LogError("TEST: " + rhythmPattern);
        if (rhythmPattern != "")
        {
            GameManager.instance.RhythmPattern = rhythmPattern;
            isPause = false;
        }
        else
            Debug.Log("Missing rhythm pattern");
    }
}
