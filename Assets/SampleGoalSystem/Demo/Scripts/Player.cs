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
		private bool jump = false;
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

        private void Update()
        {
			jump = Input.GetButtonDown("Jump");
			horizontal = Input.GetAxis("Horizontal");
		}

        void FixedUpdate()
		{

			isGround = Physics2D.OverlapCircle(groundCheck.position, 0.4f, whatIsGround);

			if (jump && isGround)
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
			myrigidbody.velocity = new Vector2(horizontal * movementspeed, myrigidbody.velocity.y);
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
        private void OnDrawGizmosSelected()
        {
			Gizmos.color = Color.red;
			if (isGround)
            {
				Gizmos.color = Color.green;
			}
			
			Gizmos.DrawWireSphere(groundCheck.position, 0.4f);
		}
    }
}