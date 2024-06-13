using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerTrackSetter : MonoBehaviour
{
	private GameObject player;
	private CinemachineVirtualCamera vCamera;

	private void OnEnable()
	{
		GameManager.E_PlayerRespawned += StartHandlePlayerRespawned;
	}

	private void OnDisable()
	{
		GameManager.E_PlayerRespawned -= StartHandlePlayerRespawned;
	}

	private void Start()
	{
		player = GameObject.FindWithTag("Player");
		vCamera = gameObject.GetComponent<CinemachineVirtualCamera>();
		vCamera.Follow = player.transform;
		vCamera.LookAt = player.transform;
	}

	private void StartHandlePlayerRespawned()
	{
		StartCoroutine(DelayedHandlePlayerRespawned());
	}

	private IEnumerator DelayedHandlePlayerRespawned()
	{
		yield return new WaitForSeconds(0.2f);
		HandlePlayerRespawned();
	}

	private void HandlePlayerRespawned()
	{
		player = GameObject.FindWithTag("Player");
		vCamera.Follow = player.transform;
		vCamera.LookAt = player.transform;
	}
}
