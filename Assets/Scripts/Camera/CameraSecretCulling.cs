using UnityEngine;

public class CameraSecretCulling : MonoBehaviour
{
	public Camera targetCamera;
	private string layerToToggle = "Secret";
	private bool canActivate = true;
	private int layerMask;

	void Start()
	{
		// Convert the layer name to a layer mask
		layerMask = 1 << LayerMask.NameToLayer(layerToToggle);
	}

	void Update()
	{
		if (canActivate)
		{
			targetCamera.cullingMask &= ~layerMask;
		}
		else
		{
			targetCamera.cullingMask |= layerMask;
		}

		if (OrbMenuManager.isDeleting || OrbMenuManager.isPlacing)
		{
			canActivate = true;
		}
		else
		{
			canActivate = false;
		}
	}


}
