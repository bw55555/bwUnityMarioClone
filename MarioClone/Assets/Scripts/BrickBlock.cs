using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class BrickBlock : MonoBehaviour
{

	// Used to change the sprite
	private SpriteRenderer sr;
	private Collider2D collider2d;
	// The sprite to change into
	public Sprite explodedBlock;

	// Wait time before switching sprites
	public float secBeforeSpriteChange = .2f;

	void Awake()
	{
		sr = GetComponent<SpriteRenderer>();
		collider2d = GetComponent<Collider2D>();
	}

	// Called when something hits the BrickBlock
	void OnCollisionEnter2D(Collision2D col)
	{

		// Check if the collision hit the bottom of the block

		// Mathf.Max(col.GetContact(0).point.y, col.GetContact(1).point.y) < transform.position.y
		if (col.contacts[0].point.y <= collider2d.bounds.min.y)
		{

			// Play sound
			SoundManager.Instance.PlayOneShot(SoundManager.Instance.rockSmash);

			// Change the Block sprite
			sr.sprite = explodedBlock;

			// Wait a fraction of a second and then destroy the BrickBlock
			DestroyObject(gameObject, secBeforeSpriteChange);

		}

	}

}
