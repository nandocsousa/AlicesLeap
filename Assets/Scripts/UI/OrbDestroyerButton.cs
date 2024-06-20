using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbDestroyerButton : MonoBehaviour
{
	public void StartDestroyingOrbs()
	{
		if (!OrbMenuManager.isPlacing)
		{
			if (OrbMenuManager.isDeleting == false)
			{
				OrbMenuManager.isDeleting = true;
				OrbMenuManager.UsingOrbSpawner();
			}
			else
			{
				OrbMenuManager.isDeleting = false;
				OrbMenuManager.UsingOrbSpawner();
			}
		}
	}
}
