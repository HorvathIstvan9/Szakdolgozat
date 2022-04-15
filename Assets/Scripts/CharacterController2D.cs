using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;                          
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;          
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  
	[SerializeField] private bool m_AirControl = false;                         
	[SerializeField] private LayerMask m_WhatIsGround;                          
	[SerializeField] private Transform m_GroundCheck;                           
	[SerializeField] private Transform m_CeilingCheck;                          
	[SerializeField] private Collider2D m_CrouchDisableCollider;
	[SerializeField] private Collider2D m_DeathDisableCollider;
	[SerializeField] private LayerMask m_WhatIsClimbableWall;
	[SerializeField] private Transform m_WallCheck;
	[SerializeField] private Animator fadeAnimator;

	const float k_GroundedRadius = .2f; 
	bool m_Grounded;            
	const float k_CeilingRadius = .2f; 
	const float k_WallRadius = .01f;
	Rigidbody2D m_Rigidbody2D;
	Animator animator;
	bool m_FacingRight = true;  
	Vector2 m_Velocity = Vector2.zero;
	float gravity;
	float horizontalMove = 0f;
	public float runSpeed = 40f;
	bool isJumping = false;
	bool isCrouching = false;
	bool isFlying = false;
	bool onWall=false;
	bool isDying = false;
	Vector3 respawnPoint;
	public float flyTime = 0f;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		gravity = m_Rigidbody2D.gravityScale;
		respawnPoint = transform.position;
	}
    private void Update()
    {
		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
		if (Input.GetButtonDown("Jump"))
		{
            if (onWall)
            {
				WallJump();
            }
            else if (isJumping&&flyTime>0)
            {
				isFlying = true;
            }
            else
            {
				isJumping = true;
				Jump();
			}
		}
        if (Input.GetButtonUp("Jump")||flyTime<=0)
        {
			isFlying = false;
        }
		if (Input.GetButtonDown("Crouch"))
		{
			isCrouching = true;
		}
		else if (Input.GetButtonUp("Crouch"))
		{
			isCrouching = false;
		}
		animator.SetFloat("jumpVelocity", m_Rigidbody2D.velocity.y);
		animator.SetBool("isFlying", isFlying);
	}

    void Fly(float move, float vertical)
    {
		isJumping = false;
		isFlying = true;
		m_Rigidbody2D.gravityScale = 0.5f;
		m_Rigidbody2D.velocity = new Vector2(move * runSpeed, vertical + 3);
		animator.SetBool("isJumping", isJumping);
		animator.SetBool("isFlying", isFlying);
		flyTime -= Time.deltaTime;
	}
    private void FixedUpdate()
	{
		GroundCheck();
		WallCheck();
		Move(horizontalMove * Time.fixedDeltaTime, isCrouching);
        if (isFlying&&!onWall)
        {
			Fly(horizontalMove * Time.fixedDeltaTime, m_Rigidbody2D.velocity.y * Time.fixedDeltaTime);
        }
	}
	void Move(float move, bool crouch)
	{
        if (!crouch&&!isDying)
		{
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				crouch = true;
			}
		}
		if (m_Grounded || m_AirControl)
		{
			if (crouch)
			{
				move *= m_CrouchSpeed;
				m_CrouchDisableCollider.enabled = false;
			}
			else if(!isDying)
			{
				m_CrouchDisableCollider.enabled = true;
			}
			Vector2 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			m_Rigidbody2D.velocity = Vector2.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			if (move > 0 && !m_FacingRight)
			{
				Flip();
			}
			else if (move < 0 && m_FacingRight)
			{
				
				Flip();
			}
			animator.SetFloat("speed", Mathf.Abs(horizontalMove));
		}
		animator.SetBool("isCrouching", crouch);
	}
	void Jump()
    {
		if (m_Grounded && m_CrouchDisableCollider.enabled)
		{
			m_Grounded = false;
			isJumping = true;
			m_Rigidbody2D.velocity = Vector2.up * m_JumpForce;
			animator.SetBool("isJumping", isJumping);
		}
	}
	void WallJump()
    {
		onWall = false;
		m_Grounded = false;
		isJumping = true;
		Vector2 wallDirection = m_FacingRight ? Vector2.left : Vector2.right;
		m_Rigidbody2D.velocity = new Vector2(wallDirection.x , 1f) * m_JumpForce;
		m_Rigidbody2D.gravityScale = gravity;
		animator.SetBool("isJumping", isJumping);
		animator.SetBool("onWall", onWall);
		Flip();

	}
    void GroundCheck()
    {
		m_Grounded = false;
        if (!isDying)
        {
			isJumping = true;
		}	
        if (Physics2D.OverlapCircle(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround))
        {
			m_Grounded = true;
			isJumping = false;
        }
		animator.SetBool("isJumping", isJumping);
	}
	void WallCheck()
	{
		if (Physics2D.OverlapCircle(m_WallCheck.position, k_WallRadius, m_WhatIsClimbableWall) && !m_Grounded)
		{
			m_Rigidbody2D.gravityScale = 0;
			m_Rigidbody2D.velocity = Vector2.zero;
			onWall = true;
			isJumping = false;
			
			animator.SetBool("isJumping", isJumping);
			animator.SetBool("onWall", onWall);
			if (Mathf.Abs(horizontalMove) > 0)
			{
				m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, runSpeed * Time.fixedDeltaTime * gravity);
			}
		}
		else if(!Physics2D.OverlapCircle(m_WallCheck.position, k_WallRadius, m_WhatIsClimbableWall))
		{
			m_Rigidbody2D.gravityScale = gravity;
			onWall = false;
			animator.SetBool("onWall",onWall);
		}
	}
	private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spike")
        {
			isDying = true;
			StartCoroutine(Respawn(respawnPoint));
        }
	}
    private void OnCollisionStay2D(Collision2D collision)
    {
		if (collision.gameObject.tag == "Platform")
		{
			this.transform.parent = collision.gameObject.transform;
		}
	}
    private void OnCollisionExit2D(Collision2D collision)
    {
		this.gameObject.transform.parent = null;
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.gameObject.tag == "Spike")
		{
			isDying = true;
			StartCoroutine(Respawn(respawnPoint));
		}
		if (collision.tag == "CheckPoint")
        {
			respawnPoint = collision.transform.position;
        }
		if (collision.gameObject.tag == "Collectible")
		{
			flyTime+=5f;
			collision.gameObject.SetActive(false);
		}
	}
	IEnumerator Respawn(Vector3 respawn)
    {
		isJumping = false;
		Vector2 direction = m_FacingRight ? Vector2.left : Vector2.right;
		m_Rigidbody2D.velocity = new Vector2(direction.x * 3, 1f) * m_JumpForce;
		m_DeathDisableCollider.enabled = false;
		m_CrouchDisableCollider.enabled = false;
		animator.SetBool("isJumping", isJumping);
		animator.SetBool("death",isDying);
		yield return new WaitForSeconds(2);
		transform.position = respawn;
		isDying = false;
		m_CrouchDisableCollider.enabled = true;
		m_DeathDisableCollider.enabled = true;
		animator.SetBool("death", isDying);
	}
    void Flip()
	{
		m_FacingRight = !m_FacingRight;
		transform.Rotate(0f,180f,0f);
	}
}