using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GoalSystem
{
	public class Player : MonoBehaviour
	{
		private Rigidbody2D myrigidbody;
		[SerializeField]
		private float movementspeed = 10f;
		[SerializeField]
		private float jumpheigt = 10f;
		private bool isGround;
		private bool facing = true;
		private float horizontal;
		public Transform groundCheck;

		public LayerMask whatIsGround;
		public static Action playerLost;
		public static Action playerWon;
		void Start()
		{
			myrigidbody = GetComponent<Rigidbody2D>();
		}
		void FixedUpdate()
		{
			horizontal = Input.GetAxis("Horizontal");

			isGround = Physics2D.OverlapCircle(groundCheck.position, 0.2f, whatIsGround);

			myrigidbody.velocity = new Vector2(horizontal * movementspeed, myrigidbody.velocity.y);
			if (Input.GetKeyDown(KeyCode.Space) && isGround == true)
			{
				myrigidbody.AddForce(transform.up * jumpheigt, ForceMode2D.Impulse);
			}
			if (horizontal > 0 && !facing)
			{
				Flip();
			}
			else if (horizontal < 0 && facing)
			{
				Flip();
			}
		}
		private void Flip()
		{
			facing = !facing;
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}
		void Lose()
		{
			myrigidbody.bodyType = RigidbodyType2D.Static;
			playerLost?.Invoke();
			Destroy(gameObject);
		}
		void Finish()
		{
			myrigidbody.bodyType = RigidbodyType2D.Static;
			playerWon?.Invoke();
			Destroy(gameObject);
		}
		void OnTriggerEnter2D(Collider2D col)
		{
			if (col.tag == "Saw")
			{
				Lose();
			}
			if (col.tag == "Death")
			{
				Lose();
			}
			if (col.tag == "Portal")
			{
				if (GoalSystem.GoalManager.Instance.IsCompletedGoals)
					Finish();
			}
		}
	}
}