using UnityEngine;

public class CursorTracker : MonoBehaviour
{
	public float minX = -10f;
	public float maxX = 10f;
	public float minY = -5f;
	public float maxY = 5f;

	void LateUpdate()
	{
		Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		cursorPosition.z = 0f;

		cursorPosition.x = Mathf.Clamp(cursorPosition.x, minX, maxX);
		cursorPosition.y = Mathf.Clamp(cursorPosition.y, minY, maxY);

		transform.position = cursorPosition;
		Debug.Log(cursorPosition);
	}
}
