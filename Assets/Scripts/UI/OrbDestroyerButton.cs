using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbDestroyerButton : MonoBehaviour
{
	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (OrbMenuManager.isDeleting)
			{
				OrbMenuManager.isDeleting = false;
				OrbMenuManager.UsingOrbSpawner();
			}
		}
	}

	public void DestroyOrb()
	{
		if (!OrbMenuManager.isPlacing)
		{
			OrbMenuManager.isDeleting = true;
			OrbMenuManager.UsingOrbSpawner();
			/*if (OrbMenuManager.isDeleting == false)
			{
				OrbMenuManager.isDeleting = true;
				OrbMenuManager.UsingOrbSpawner();
			}
			else
			{
				OrbMenuManager.isDeleting = false;
				OrbMenuManager.UsingOrbSpawner();
			}*/
		}
	}
}
