using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables
    // Public fields
    public static GameManager instance; // Singleton accessor instance
    [SerializeField]
    private GameObject fireworkParticle;
    public RhythmObjectSpawner rhythmObjectSpawner { get; private set; }
    public RhythmInterpreter rhythmInterpreter { get; private set; }

    // Private fields
    private ObjectPooler objectPooler;
    private int poIndex;
    private UIManager uiManager;
    private string rhythmPattern;
    private bool isEndGame = false;
    private bool isPlayerWin = true;
    private Color failedSkyColor = new Color(244f / 255f, 67f / 255f, 54f / 255f, 0f);
    private Color defaultSkyColor = new Color(255f / 255f, 255f / 255f, 255f / 255f, 0f);

    // Getter/Setter
    public string RhythmPattern
    {
        get => rhythmPattern;
        set
        {
            rhythmObjectSpawner.ClearSpawnableList();
            rhythmInterpreter.SetRhythmPattern(value);
            rhythmPattern = value;
        }
    }
    public bool IsEndGame { get => isEndGame; set => isEndGame = value; }
    public bool IsPlayerWin { get => isPlayerWin; set => isPlayerWin = value; }
    #endregion

    #region Unity Callbacks
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            rhythmObjectSpawner = FindObjectOfType<RhythmObjectSpawner>();
            rhythmInterpreter = FindObjectOfType<RhythmInterpreter>();
            uiManager = FindObjectOfType<UIManager>();
        }
        else
            Destroy(this);
    }

    private void Start()
    {
        // Init objects to pool
        objectPooler = ObjectPooler.SharedInstance;
        poIndex = objectPooler.AddObject(fireworkParticle, 1, true);
    }

    private void Update()
    {
        // When boolean endgame is true we start end game coroutine method
        if (isEndGame == true)
        {
            isEndGame = false;
            StartCoroutine(EndGameCO());
        }
    }
    #endregion

    #region Helper Methods
    /// <summary>
    /// Couroutine triggered game is over
    /// Launching firework + heading back to main interface
    /// </summary>
    /// <returns></returns>
    public IEnumerator EndGameCO()
    {
        if (isPlayerWin)
        {
            // Reset default sky color when play win
            Camera.main.backgroundColor = defaultSkyColor;
            // Spawning Firework particle effect FX pooled
            GameObject spawnedGO = objectPooler.GetPooledObject(poIndex);
            spawnedGO.transform.position = transform.position;
            spawnedGO.SetActive(true);
        }
        else
            Camera.main.backgroundColor = failedSkyColor; // Red sky if player failed to match all rhythm pattern
        yield return null;
    } 
    #endregion
}
