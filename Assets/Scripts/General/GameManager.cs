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
	}

	private void OnDisable()
	{
		PauseMenuController.E_PauseGame -= HandleGamePaused;
		PlayerController.E_PlayerDead -= RespawnPlayer;
		PlayerController.E_ReachedEnd -= HandleLevelFinished;
	}
	void Start()
    {
        
    }

    void Update()
    {
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
}
