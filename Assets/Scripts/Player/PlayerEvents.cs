using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents : MonoBehaviour
{
	public static event Action E_PlayerSpawned;
    public GameObject player;
    public Transform spawnArea;

	private void OnEnable()
	{
		CameraEventsScript.E_DollyEndReached += SpawnPlayer;
	}

	private void OnDisable()
	{
		CameraEventsScript.E_DollyEndReached -= SpawnPlayer;
	}

	//initial spawn
	private void SpawnPlayer()
	{
		E_PlayerSpawned?.Invoke();
		Instantiate(player, spawnArea.position, Quaternion.identity);
	}

	//respawn at checkpoint
	private void RespawnPlayer()
	{

	}
}
