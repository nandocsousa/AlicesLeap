using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSecretsActive : MonoBehaviour
{
	public GameObject secrets;
	private void OnEnable()
	{
		CameraEventsScript.E_DollyEndReached += HandleLevelStart;
	}

	private void OnDisable()
	{
		CameraEventsScript.E_DollyEndReached -= HandleLevelStart;
	}

	private void HandleLevelStart()
	{
		secrets.SetActive(true);
	}
}
