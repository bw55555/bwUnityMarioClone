using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manny : MonoBehaviour
{

	// Defines Mannys speed horizontally
	public float speed = 5;

	// Defines Mannys facing direction
	public bool facingRight = true;

	// Jump speed
	public float jumpSpeed = 5f;

	// Mannys components
	// private SpriteRenderer sr;
	private Rigidbody2D rb;
	private Animator animator;

	// Will flip depending if on ground
	bool isJumping = false;

	// Used to check if Manny is on the ground
	// You draw a line out away from the sprite a defined
	// distance and check if it collides with something
	// If it hits nothing Raycast() returns null
	private float rayCastLength = 0.005f;

	// Sprite width and height
	private float width;
	private float height;

	// How long is the jump button held
	private float jumpButtonPressTime;

	// Max jump amount
	public float maxJumpTime = 0.2f;

	// ----- 1. NEW STUFF -----

	// Special Y velocity value used when jumping off of a wall
	public float wallJumpY = 10f;

	// ----- END OF NEW STUFF -----

	void FixedUpdate()
	{

		// Get horizontal movement -1 Left, or 1 Right
		float horzMove = Input.GetAxisRaw("Horizontal");

		// Need to get Mannys y
		Vector2 vect = rb.velocity;

		// Change x and keep y as is
		rb.velocity = new Vector2(horzMove * speed, vect.y);

		// ----- 1. NEW STUFF -----

		// If Manny is jumping next to a wall his velocity will
		// go up in the Y direction
		if (IsWallOnLeftOrRight() && !IsOnGround() && horzMove == 1)
		{

			rb.velocity = new Vector2(-GetWallDirection() * speed * -.75f,
				wallJumpY);
		}

		// ----- END OF NEW STUFF -----

		// Set the speed so the right Animation is played
		animator.SetFloat("Speed", Mathf.Abs(horzMove));

		// Makes sure Manny is facing the right direction
		if (horzMove > 0 && !facingRight)
		{
			FlipManny();
		}
		else if (horzMove < 0 && facingRight)
		{
			FlipManny();
		}

		// Get vertical movement
		float vertMove = Input.GetAxis("Jump");

		if (IsOnGround() && isJumping == false)
		{
			if (vertMove > 0f)
			{
				isJumping = true;
				// ----- NEW STUFF -----
				SoundManager.Instance.PlayOneShot(SoundManager.Instance.jump);
				// ----- END OF NEW STUFF -----
			}
		}

		// If button is held pass max time set 
		// vertical move to 0
		if (jumpButtonPressTime > maxJumpTime)
		{
			vertMove = 0f;
		}

		// If is jumping and we have a valid jump press length
		// make Manny jump
		if (isJumping && (jumpButtonPressTime < maxJumpTime))
		{
			rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
		}

		// If we have moved high enough make Manny fall
		// Set Mannys Rigidbody 2d Gravity Scale to 2
		if (vertMove >= 1f)
		{
			jumpButtonPressTime += Time.deltaTime;
		}
		else
		{
			isJumping = false;
			jumpButtonPressTime = 0f;
		}

	}

	// Makes sure components have been created when the 
	// game starts
	void Awake()
	{
		// sr = GetComponent<SpriteRenderer> ();
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();

		// Gets Mannys collider width and height and
		// then adds more to it. Used to raycast to see
		// if Manny is colliding with anything so we
		// can jump
		width = GetComponent<Collider2D>().bounds.extents.x + 0.1f;
		height = GetComponent<Collider2D>().bounds.extents.y + 0.2f;
	}

	// When moving in a direction face in that direction
	void FlipManny()
	{

		// Flip the facing value
		facingRight = !facingRight;

		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

	public bool IsOnGround()
	{

		// Check if contacting the ground straight down
		bool groundCheck1 = Physics2D.Raycast(new Vector2(
								transform.position.x,
								transform.position.y - height),
								-Vector2.up, rayCastLength);

		// Check if contacting ground to the right
		bool groundCheck2 = Physics2D.Raycast(new Vector2(
			transform.position.x + (width - 0.2f),
			transform.position.y - height),
			-Vector2.up, rayCastLength);

		// Check if contacting ground to the left
		bool groundCheck3 = Physics2D.Raycast(new Vector2(
			transform.position.x - (width - 0.2f),
			transform.position.y - height),
			-Vector2.up, rayCastLength);

		if (groundCheck1 || groundCheck2 || groundCheck3)
			return true;

		return false;

	}

	// If Manny falls off the screen destroy the game object
	void OnBecameInvisible()
	{
		Debug.Log("Manny Destroyed");
		Destroy(gameObject);
	}

	// ----- 1. NEW STUFF -----

	public bool IsWallOnLeft()
	{

		// -Vector2.right checks to the left with a raycast 
		// for a wall
		return Physics2D.Raycast(new Vector2(transform.position.x - width,
			transform.position.y),
			-Vector2.right,
			rayCastLength);
	}

	public bool IsWallOnRight()
	{

		// Vector2.right checks to the left with a raycast 
		// for a wall
		return Physics2D.Raycast(new Vector2(transform.position.x + width,
			transform.position.y),
			Vector2.right,
			rayCastLength);
	}

	// Verifies if walls are on left or right for wall jumping
	public bool IsWallOnLeftOrRight()
	{

		if (IsWallOnLeft() || IsWallOnRight())
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	// Gets the wall direction if it exists
	// Multiply the results against Manny's X velocity
	public int GetWallDirection()
	{
		if (IsWallOnLeft())
		{
			return -1;
		}
		else if (IsWallOnRight())
		{
			return 1;
		}
		else
		{
			return 0;
		}
	}

	// ----- END OF NEW STUFF -----

}
