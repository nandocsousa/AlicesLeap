using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretTriggerBlocker : MonoBehaviour
{
	private void OnEnable()
	{
		PlayerController.E_SecretTrigger += HandleSecretTrigger;
	}

	private void OnDisable()
	{
		PlayerController.E_SecretTrigger -= HandleSecretTrigger;
	}

	private void HandleSecretTrigger()
	{
		gameObject.SetActive(false);
	}
}
