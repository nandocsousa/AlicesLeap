using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCameras : MonoBehaviour
{
    public GameObject dollyCam;
    public GameObject playerCam;

	private void OnEnable()
	{
        PlayerEvents.E_PlayerSpawned += HandleCameraChanger;
	}

	private void OnDisable()
	{
		PlayerEvents.E_PlayerSpawned -= HandleCameraChanger;
	}

	void Start()
    {
        dollyCam.SetActive(true);
        playerCam.SetActive(false);
    }

    void Update()
    {
        
    }

    public void HandleCameraChanger()
    {
        dollyCam.SetActive(false);
        playerCam.SetActive(true);
    }
}
