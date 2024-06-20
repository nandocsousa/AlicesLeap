using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedOrbController : MonoBehaviour
{
    private bool isPlaced = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
			isPlaced = true;
        }

        if (!isPlaced)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            gameObject.transform.position = mousePosition;
        }
        else
        {
            gameObject.transform.position = gameObject.transform.position;
        }
    }

	private void OnMouseDown()
	{
        if (OrbMenuManager.isDeleting)
        {
            Destroy(gameObject);
        }
	}
}
