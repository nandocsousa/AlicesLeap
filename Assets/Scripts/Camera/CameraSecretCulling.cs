using UnityEngine;

public class CameraLayerToggle : MonoBehaviour
{
	public Camera targetCamera;
	private string layerToToggle = "Secret";

	private int layerMask;

	void Start()
	{
		// Convert the layer name to a layer mask
		layerMask = 1 << LayerMask.NameToLayer(layerToToggle);
	}

	void Update()
	{
		if (OrbMenuManager.isDeleting || OrbMenuManager.isPlacing)
		{
			targetCamera.cullingMask &= ~layerMask;
		}
		else
		{
			targetCamera.cullingMask |= layerMask;
		}
	}
}
