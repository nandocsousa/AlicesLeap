using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEventsScript : MonoBehaviour
{
	public static event Action E_DollyEndReached;

	public void RaiseDollyEndReached()
	{
		E_DollyEndReached?.Invoke();
	}
}
