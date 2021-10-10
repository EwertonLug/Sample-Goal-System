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
		
		private bool facing = true;
		private float horizontal;
		public BoxCollider2D boxCollider;
		

		public LayerMask whatIsGround;
		public static Action playerLost;
		public static Action playerWon;
		void Start()
		{
			myrigidbody = GetComponent<Rigidbody2D>();
			boxCollider = GetComponent<BoxCollider2D>();
		}

        private void Update()
        {
			
			if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
			{
				jump = true;
            }
            
			
			horizontal = Input.GetAxis("Horizontal");
		}

        void FixedUpdate()
		{

			
			
			if (jump)
			{
				jump = false;
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
		private bool IsGrounded()
        {
			RaycastHit2D ground =  Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size,0, Vector2.down, 0.1f, whatIsGround);
			return ground.collider != null;
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

			if (IsGrounded())
            {
				Gizmos.color = Color.green;
			}
			var posY = boxCollider.bounds.center.y - (boxCollider.bounds.size.y / 2);
			var posX = boxCollider.bounds.center.x;

			var pos = new Vector2(posX, posY);

			Gizmos.DrawWireSphere(pos, boxCollider.bounds.size.x/2);
		}
    }
}