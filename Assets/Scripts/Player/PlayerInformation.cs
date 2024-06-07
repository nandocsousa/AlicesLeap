using UnityEngine;

[CreateAssetMenu(menuName = "Player Information")]
public class PlayerInformation : ScriptableObject
{
	[Header("Gravity")]
	[HideInInspector] public float gravityStrength;
	[HideInInspector] public float gravityScale;

	[Space(5)]
	public float fallGravityMult;
	public float maxFallSpeed;

	[Space(20)]

	[Header("Run")]
	public float runMaxSpeed;
	public float runAcceleration;
	[HideInInspector] public float runAccelAmount;
	public float runDecceleration;
	[HideInInspector] public float runDeccelAmount;
	[Space(5)]
	[Range(0f, 1)] public float accelInAir;
	[Range(0f, 1)] public float deccelInAir;
	[Space(5)]
	public bool doConserveMomentum = true;

	[Space(20)]

	[Header("Jump")]
	public float jumpHeight;
	public float jumpTimeToApex;
	public float jumpForce;

	[Header("Both Jumps")]
	public float jumpCutGravityMult;
	[Range(0f, 1)] public float jumpHangGravityMult;
	public float jumpHangTimeThreshold;
	[Space(0.5f)]
	public float jumpHangAccelerationMult;
	public float jumpHangMaxSpeedMult;

	[Space(20)]

	[Header("Assists")]
	[Range(0.01f, 0.5f)] public float coyoteTime;
	[Range(0.01f, 0.5f)] public float jumpInputBufferTime;

	[Space(20)]

	[Header("Dash")]
	public int dashAmount;
	public float dashSpeed;
	public float dashSleepTime;
	[Space(5)]
	public float dashAttackTime;
	[Space(5)]
	public float dashEndTime;
	public Vector2 dashEndSpeed;
	[Range(0f, 1f)] public float dashEndRunLerp;
	[Space(5)]
	public float dashRefillTime;
	[Space(5)]
	[Range(0.01f, 0.5f)] public float dashInputBufferTime;

	private void OnValidate()
	{
		gravityStrength = -(2 * jumpHeight) / (jumpTimeToApex * jumpTimeToApex);

		gravityScale = gravityStrength / Physics2D.gravity.y;

		runAccelAmount = (50 * runAcceleration) / runMaxSpeed;
		runDeccelAmount = (50 * runDecceleration) / runMaxSpeed;

		jumpForce = Mathf.Abs(gravityStrength) * jumpTimeToApex;

		#region Variable Ranges
		runAcceleration = Mathf.Clamp(runAcceleration, 0.01f, runMaxSpeed);
		runDecceleration = Mathf.Clamp(runDecceleration, 0.01f, runMaxSpeed);
		#endregion
	}
}