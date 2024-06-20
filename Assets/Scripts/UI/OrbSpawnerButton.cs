using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class OrbSpawnerButton : MonoBehaviour
{
    [SerializeField] GameObject orb;

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (OrbMenuManager.isPlacing)
			{
				OrbMenuManager.isPlacing = false;
				OrbMenuManager.UsingOrbSpawner();
			}
		}
	}

	public void SpawnOrb()
    {
        if (!OrbMenuManager.isDeleting)
        {
			if (OrbMenuManager.orbAmount > 0)
			{
				OrbMenuManager.isPlacing = true;
				OrbMenuManager.UsingOrbSpawner();
				Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				Instantiate(orb, mousePosition, Quaternion.identity);
				--OrbMenuManager.orbAmount;
			}
		}
    }
}
