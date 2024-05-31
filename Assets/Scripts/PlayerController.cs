using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerInformation Information;

    #region COMPONENTS
    public Rigidbody2D rb {  get; private set; }
    #endregion

    #region STATE PARAMETERS
    public bool IsFacingRight { get; private set; }
    public bool IsJumping { get; private set; }
    //timers
    public float LastOnGroundTime { get; private set; }
    //jump
    //Jump
    private bool _isJumpCut;
    private bool _isJumpFalling;
    #endregion

    #region INPUT
    private Vector2 _moveInput;
    public float LastPressedJumpTime { get; private set; }
    #endregion

    #region CHECK PARAMETERS
    //Set all of these up in the inspector
    [Header("Checks")]
    [SerializeField] private Transform _groundCheckPoint;
    //Size of groundCheck depends on the size of your character generally you want them slightly small than width (for ground) and height (for the wall check)
    [SerializeField] private Vector2 _groundCheckSize = new Vector2(0.49f, 0.03f);
    #endregion

    #region LAYERS & TAGS
    [Header("Layers & Tags")]
    [SerializeField] private LayerMask _groundLayer;
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        SetGravityScale(Information.gravityScale);
        IsFacingRight = true;
    }

    void Update()
    {
        #region TIMERS

        #endregion
    }

    #region GENERAL METHODS
    public void SetGravityScale(float scale)
    {
        rb.gravityScale = scale;
    }
    #endregion
}
