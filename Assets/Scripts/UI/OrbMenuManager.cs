using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbMenuManager : MonoBehaviour
{
    public GameObject spawnerButton, destroyerButton;
    public static event Action E_UsingOrbSpawner;
    public static int orbAmount = 30;
    public static bool isDeleting = false;
    public static bool isPlacing = false;

	private void OnEnable()
	{
        CameraEventsScript.E_DollyEndReached += HandleShowcaseFinish;
	}

	private void OnDisable()
	{
		CameraEventsScript.E_DollyEndReached -= HandleShowcaseFinish;
	}

	private void Update()
	{
        if (isDeleting || isPlacing)
        {

        }
	}

	public static void UsingOrbSpawner()
    {
        E_UsingOrbSpawner?.Invoke();
    }

    private void HandleShowcaseFinish()
    {
        spawnerButton.SetActive(true);
        destroyerButton.SetActive(true);
    }
}
