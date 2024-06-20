using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	public PlayerInformation Information;

	#region EVENTS
	public static event Action<int> E_PlayerDead;
	public static event Action<int> E_UpdateCheckpoint;
	public static event Action<int> E_ReachedEnd;
	public static event Action E_SecretCollected;
	#endregion

	#region COMPONENTS
	public Rigidbody2D RB { get; private set; }
	#endregion

	#region STATE PARAMETERS
	public bool IsFacingRight { get; private set; }
	public bool IsJumping { get; private set; }
	public bool IsDashing { get; private set; }

	public float LastOnGroundTime { get; private set; }

	//Jump
	private bool _isJumpCut;
	private bool _isJumpFalling;

	//Dash
	private int _dashesLeft;
	private bool _dashRefilling;
	private Vector2 _lastDashDir;
	private bool _isDashAttacking;
	#endregion

	#region INPUT PARAMETERS
	private Vector2 _moveInput;

	public float LastPressedJumpTime { get; private set; }
	public float LastPressedDashTime { get; private set; }
	#endregion

	#region CHECK PARAMETERS
	[Header("Checks")]
	[SerializeField] private Transform _groundCheckPoint;
	[SerializeField] private Vector2 _groundCheckSize = new Vector2(0.49f, 0.03f);
	#endregion

	#region LAYERS & TAGS
	[Header("Layers & Tags")]
	[SerializeField] private LayerMask _groundLayer;
	#endregion

	private bool isPaused = false;
	public int currentCheckpoint = 0;

	private void OnEnable()
	{
		
	}

	private void OnDisable()
	{
		
	}

	private void Awake()
	{
		RB = GetComponent<Rigidbody2D>();
	}

	private void Start()
	{
		SetGravityScale(Information.gravityScale);
		IsFacingRight = true;
		_dashesLeft = 0;
	}

	private void Update()
	{
		if (!isPaused)
		{
			#region TIMERS
			LastOnGroundTime -= Time.deltaTime;

			LastPressedJumpTime -= Time.deltaTime;
			LastPressedDashTime -= Time.deltaTime;
			#endregion

			#region INPUT HANDLER
			_moveInput.x = Input.GetAxisRaw("Horizontal");
			_moveInput.y = Input.GetAxisRaw("Vertical");

			if (_moveInput.x != 0)
				CheckDirectionToFace(_moveInput.x > 0);

			if (Input.GetKeyDown(KeyCode.Space))
			{
				OnJumpInput();
			}

			if (Input.GetKeyUp(KeyCode.Space))
			{
				OnJumpUpInput();
			}

			if (Input.GetKeyDown(KeyCode.LeftShift))
			{
				OnDashInput();
			}
			#endregion

			#region COLLISION CHECKS
			if (!IsDashing && !IsJumping)
			{
				//Ground Check
				if (Physics2D.OverlapBox(_groundCheckPoint.position, _groundCheckSize, 0, _groundLayer))
				{
					LastOnGroundTime = Information.coyoteTime;
				}
			}
			#endregion

			#region JUMP CHECKS
			if (IsJumping && RB.velocity.y < 0)
			{
				IsJumping = false;

				_isJumpFalling = true;
			}

			if (LastOnGroundTime > 0 && !IsJumping)
			{
				_isJumpCut = false;

				_isJumpFalling = false;
			}

			if (!IsDashing)
			{
				//Jump
				if (CanJump() && LastPressedJumpTime > 0)
				{
					IsJumping = true;
					_isJumpCut = false;
					_isJumpFalling = false;
					Jump();
				}
			}
			#endregion

			#region DASH CHECKS
			if (CanDash() && LastPressedDashTime > 0)
			{
				Sleep(Information.dashSleepTime);

				if (_moveInput != Vector2.zero)
					_lastDashDir = _moveInput;
				else
					_lastDashDir = IsFacingRight ? Vector2.right : Vector2.left;

				IsDashing = true;
				IsJumping = false;
				_isJumpCut = false;

				StartCoroutine(nameof(StartDash), _lastDashDir);
			}
			#endregion

			#region GRAVITY
			if (!_isDashAttacking)
			{
				if (RB.velocity.y < 0 && _moveInput.y < 0)
				{
					RB.velocity = new Vector2(RB.velocity.x, Mathf.Max(RB.velocity.y, -Information.maxFallSpeed));
				}
				else if (_isJumpCut)
				{
					SetGravityScale(Information.gravityScale * Information.jumpCutGravityMult);
					RB.velocity = new Vector2(RB.velocity.x, Mathf.Max(RB.velocity.y, -Information.maxFallSpeed));
				}
				else if ((IsJumping || _isJumpFalling) && Mathf.Abs(RB.velocity.y) < Information.jumpHangTimeThreshold)
				{
					SetGravityScale(Information.gravityScale * Information.jumpHangGravityMult);
				}
				else if (RB.velocity.y < 0)
				{
					SetGravityScale(Information.gravityScale * Information.fallGravityMult);
					RB.velocity = new Vector2(RB.velocity.x, Mathf.Max(RB.velocity.y, -Information.maxFallSpeed));
				}
				else
				{
					SetGravityScale(Information.gravityScale);
				}
			}
			else
			{
				SetGravityScale(0);
			}
			#endregion
		}
	}

	private void FixedUpdate()
	{
		if(!isPaused)
		{
			if (!IsDashing)
			{
				Run(1);
			}
			else if (_isDashAttacking)
			{
				Run(Information.dashEndRunLerp);
			}
		}
	}

	private void HandleGamePaused()
	{
		if (isPaused)
		{
			isPaused = false;
		}
		else
		{
			isPaused = true;
		}
	}

	#region TRIGGERS/COLLISIONS
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(!isPaused)
		{
			if (collision.tag == "Orb")
			{
				_dashesLeft = 1;
			}

			if (collision.tag == "Kill")
			{
				E_PlayerDead?.Invoke(currentCheckpoint);
				Destroy(gameObject);
			}

			if (collision.tag == "Checkpoint")
			{
				Checkpoint checkpoint = collision.GetComponent<Checkpoint>();
				UpdateCheckpoint(checkpoint.checkpointID);
				E_UpdateCheckpoint?.Invoke(currentCheckpoint);
			}

			if (collision.tag == "Finish")
			{
				Scene currentScene = SceneManager.GetActiveScene();
				int currentLevelID = currentScene.buildIndex;
				E_ReachedEnd?.Invoke(currentLevelID);
			}

			if (collision.tag == "Secret")
			{
				E_SecretCollected?.Invoke();
				Destroy(collision.gameObject);
			}
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (!isPaused)
		{
			if (collision.gameObject.CompareTag("Ground"))
			{
				_dashesLeft = 0;
			}

			if (collision.gameObject.CompareTag("Kill"))
			{
				E_PlayerDead?.Invoke(currentCheckpoint);
				Destroy(gameObject);
			}
		}
	}
	#endregion

	#region INPUT CALLBACKS
	public void OnJumpInput()
	{
		LastPressedJumpTime = Information.jumpInputBufferTime;
	}

	public void OnJumpUpInput()
	{
		if (CanJumpCut())
			_isJumpCut = true;
	}

	public void OnDashInput()
	{
		LastPressedDashTime = Information.dashInputBufferTime;
	}
	#endregion

	#region GENERAL METHODS
	public void UpdateCheckpoint(int newCheckpointNumber)
	{
		if (newCheckpointNumber > currentCheckpoint)
		{
			currentCheckpoint = newCheckpointNumber;
		}
		else
		{
			Debug.Log("checkpoint");
		}
	}

	public void SetGravityScale(float scale)
	{
		RB.gravityScale = scale;
	}

	private void Sleep(float duration)
	{
		StartCoroutine(nameof(PerformSleep), duration);
	}

	private IEnumerator PerformSleep(float duration)
	{
		Time.timeScale = 0;
		yield return new WaitForSecondsRealtime(duration);
		Time.timeScale = 1;
	}
	#endregion

	#region RUN METHODS
	private void Run(float lerpAmount)
	{
		float targetSpeed = _moveInput.x * Information.runMaxSpeed;
		targetSpeed = Mathf.Lerp(RB.velocity.x, targetSpeed, lerpAmount);

		#region Calculate AccelRate
		float accelRate;

		if (LastOnGroundTime > 0)
			accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Information.runAccelAmount : Information.runDeccelAmount;
		else
			accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Information.runAccelAmount * Information.accelInAir : Information.runDeccelAmount * Information.deccelInAir;
		#endregion

		#region Add Bonus Jump Apex Acceleration
		if ((IsJumping || _isJumpFalling) && Mathf.Abs(RB.velocity.y) < Information.jumpHangTimeThreshold)
		{
			accelRate *= Information.jumpHangAccelerationMult;
			targetSpeed *= Information.jumpHangMaxSpeedMult;
		}
		#endregion

		#region Conserve Momentum
		if (Information.doConserveMomentum && Mathf.Abs(RB.velocity.x) > Mathf.Abs(targetSpeed) && Mathf.Sign(RB.velocity.x) == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f && LastOnGroundTime < 0)
		{
			accelRate = 0;
		}
		#endregion

		float speedDif = targetSpeed - RB.velocity.x;

		float movement = speedDif * accelRate;

		RB.AddForce(movement * Vector2.right, ForceMode2D.Force);
	}

	private void Turn()
	{
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;

		IsFacingRight = !IsFacingRight;
	}
	#endregion

	#region JUMP METHODS
	private void Jump()
	{
		LastPressedJumpTime = 0;
		LastOnGroundTime = 0;

		#region Perform Jump
		float force = Information.jumpForce;
		if (RB.velocity.y < 0)
			force -= RB.velocity.y;

		RB.AddForce(Vector2.up * force, ForceMode2D.Impulse);
		#endregion
	}
	#endregion

	#region DASH METHODS
	//Dash Coroutine
	private IEnumerator StartDash(Vector2 dir)
	{
		LastOnGroundTime = 0;
		LastPressedDashTime = 0;

		float startTime = Time.time;

		_dashesLeft--;
		_isDashAttacking = true;

		SetGravityScale(0);

		while (Time.time - startTime <= Information.dashAttackTime)
		{
			RB.velocity = dir.normalized * Information.dashSpeed;
			yield return null;
		}

		startTime = Time.time;

		_isDashAttacking = false;

		SetGravityScale(Information.gravityScale);
		RB.velocity = Information.dashEndSpeed * dir.normalized;

		while (Time.time - startTime <= Information.dashEndTime)
		{
			yield return null;
		}

		//Dash over
		IsDashing = false;
	}
	#endregion

	#region CHECK METHODS
	public void CheckDirectionToFace(bool isMovingRight)
	{
		if (isMovingRight != IsFacingRight)
			Turn();
	}

	private bool CanJump()
	{
		return LastOnGroundTime > 0 && !IsJumping;
	}

	private bool CanJumpCut()
	{
		return IsJumping && RB.velocity.y > 0;
	}

	private bool CanDash()
	{
		return _dashesLeft > 0 && !IsDashing;
	}
	#endregion

	#region EDITOR METHODS
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(_groundCheckPoint.position, _groundCheckSize);
	}
	#endregion
}
