using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDisable : MonoBehaviour
{
    #region Variables
    public float lifeTime = 2f;

    private WaitForSeconds delay;

    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        delay = new WaitForSeconds(lifeTime);
    }

    void OnEnable()
    {
        StartCoroutine(DisablerCO());
    }
    #endregion

    #region Helper Methods
    /// <summary>
    /// Auto disabler coroutine based on GO lifetime setup
    /// </summary>
    /// <returns></returns>
    private IEnumerator DisablerCO()
    {
        yield return delay;
        gameObject.SetActive(false);
    } 
    #endregion
}
