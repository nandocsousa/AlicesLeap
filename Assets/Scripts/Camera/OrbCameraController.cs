using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbCameraController : MonoBehaviour
{
	public GameObject playerCam, orbCam, cursorTracker;
	private bool showcaseFinished = false;

	private void OnEnable()
	{
		CameraEventsScript.E_DollyEndReached += HandleShowcaseFinished;
	}

	private void OnDisable()
	{
		CameraEventsScript.E_DollyEndReached -= HandleShowcaseFinished;
	}

	private void Update()
	{
		if (showcaseFinished)
		{
			if (OrbMenuManager.isPlacing || OrbMenuManager.isDeleting)
			{
				playerCam.SetActive(false);
				orbCam.SetActive(true);
				cursorTracker.SetActive(true);
			}
			else
			{
				playerCam.SetActive(true);
				orbCam.SetActive(false);
				cursorTracker.SetActive(false);
			}
		}
	}

	private void HandleShowcaseFinished()
	{
		showcaseFinished = true;
	}
}
