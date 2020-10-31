using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	public float speed = 5f;

	private Rigidbody2D myBody;
	private Animator anim;

	public Transform groundCheckPosition;

	public LayerMask groundLayer;

	private bool isGrounded;
	private bool jumped;

	private float jumpPower = 12f;

	void Awake()
	{
		myBody = GetComponent<Rigidbody2D>();

		anim = GetComponent<Animator>();
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		CheckIfGrounded();
		PlayerJump();
	}

	void FixedUpdate()
	{
		PlayerWalk();
	}

	void PlayerWalk()
	{
		float h = Input.GetAxisRaw("Horizontal");
		
		if (h > 0)
		{
			myBody.velocity = new Vector2(speed, myBody.velocity.y);

			ChangeDirection(1);
		}
		else if (h < 0)
		{
			myBody.velocity = new Vector2(-speed, myBody.velocity.y);

			ChangeDirection(-1);

		} else
		{
			myBody.velocity = new Vector2(0f, myBody.velocity.y);
		}

		anim.SetInteger("Speed", Mathf.Abs((int)myBody.velocity.x));
	}

	void ChangeDirection(int direction)
	{
		Vector3 tempScale = transform.localScale;

		tempScale.x = direction;

		transform.localScale = tempScale;
	}

	void CheckIfGrounded()
	{
		isGrounded = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, 0.1f, groundLayer);

		if (isGrounded)
		{
			if (jumped)
			{
				jumped = false;

				anim.SetBool("Jump", false);
			}
		}
	}

	void PlayerJump()
	{
		if(isGrounded)
		{
			if(Input.GetKey(KeyCode.Space))
			{
				jumped = true;

				myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);

				anim.SetBool("Jump", true);
			}
		}
	}
}
