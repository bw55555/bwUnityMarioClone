using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : MonoBehaviour
{

	// Snail speed
	public float speed = 2;
	private Collider2D collider2d;
	private Rigidbody2D rb;

	// Snail Direction
	Vector2 direction = Vector2.right;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		collider2d = GetComponent<Collider2D>();
	}

	void FixedUpdate()
	{

		// Move the snail
		rb.velocity = direction * speed;

	}

	void OnTriggerEnter2D(Collider2D col)
	{

		// If you hit SnailStart & SnailEnd flip direction
		transform.localScale = new Vector2(-1 * transform.localScale.x,
			transform.localScale.y);

		direction = new Vector2(-1 * direction.x, direction.y);

	}

	// If Manny hits the top of the snail kill the snail
	// and otherwise kill Manny
	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.name == "Manny")
		{

			// If hit from above play dead animation, remove
			// the collider so the snail falls off the screen
			// and kill Snail after 3 seconds
			if (col.contacts[0].point.y >= collider2d.bounds.max.y)
			{

				GetComponent<Animator>().SetTrigger("Dead");
				GetComponent<Collider2D>().enabled = false;
				direction = new Vector2(direction.x, -1);
				DestroyObject(gameObject, 3);

			}
			else
			{
				// Kill manny

				SoundManager.Instance.PlayOneShot(SoundManager.Instance.mannyDies);

				DestroyObject(col.gameObject, .5f);
			}
		}
	}

}
