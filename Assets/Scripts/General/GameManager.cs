using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public GameObject player;
	public GameObject[] checkpoints;
	private bool isPaused = false;
	public static event Action E_PlayerRespawned;
	private void OnEnable()
	{
		PauseMenuController.E_PauseGame += HandleGamePaused;
		PlayerController.E_PlayerDead += RespawnPlayer;
		PlayerController.E_ReachedEnd += HandleLevelFinished;
		OrbMenuManager.E_UsingOrbSpawner += HandleUsingOrbSpawner;
	}

	private void OnDisable()
	{
		PauseMenuController.E_PauseGame -= HandleGamePaused;
		PlayerController.E_PlayerDead -= RespawnPlayer;
		PlayerController.E_ReachedEnd -= HandleLevelFinished;
		OrbMenuManager.E_UsingOrbSpawner -= HandleUsingOrbSpawner;
	}
	void Start()
    {
        
    }

    void Update()
    {
		Debug.Log(Time.timeScale);
		if (isPaused)
		{
			Time.timeScale = 0f;
		}
		else
		{
			Time.timeScale = 1.0f;
		}
    }

	private void HandleGamePaused()
	{
		if (!isPaused)
		{
			isPaused = true;
		}
		else
		{
			isPaused = false;
		}
	}

	private void RespawnPlayer(int checkpointID)
	{
		Instantiate(player, checkpoints[checkpointID].transform.position, Quaternion.identity);
		E_PlayerRespawned?.Invoke();
	}

	private void HandleLevelFinished(int i)
	{
		isPaused = true;
	}

	private void HandleUsingOrbSpawner()
	{
		if (isPaused)
		{
			isPaused = false;
		}
		else
		{
			isPaused = true;
		}
	}
}
