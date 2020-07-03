using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDisable : MonoBehaviour
{
	public float lifeTime = 2f;

	private WaitForSeconds delay;

    private void Awake()
    {
		delay = new WaitForSeconds(lifeTime);
	}

    // Use this for initialization
    void OnEnable()
	{
		StartCoroutine(Disabler());
	}

	private IEnumerator Disabler()
	{
		yield return delay;
		gameObject.SetActive(false);
	}
}
