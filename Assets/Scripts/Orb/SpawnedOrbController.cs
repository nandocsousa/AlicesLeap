using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedOrbController : MonoBehaviour
{
    private bool isDragged = true;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragged = false;
            Destroy(gameObject.GetComponent<SpawnedOrbController>());
        }

        if (isDragged)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            gameObject.transform.position = mousePosition;
        }
        else
        {
            gameObject.transform.position = gameObject.transform.position;
        }
    }
}
