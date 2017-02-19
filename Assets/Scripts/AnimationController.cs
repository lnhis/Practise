using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour 
{
	private bool facingRight = true;
	private Animator anim;
	private Character controller;

	public bool FacingRight { get { return facingRight; } }

	void Start () 
	{
		anim = this.GetComponent<Animator>();
		controller = this.transform.parent.GetComponent<Character>();
	}
	public void ActivateSpin()
	{
		anim.SetTrigger ("Spin");
	}

	void Update () 
	{
		anim.SetFloat("Speed", controller.rbody2d.velocity.magnitude);
		//Debug.LogWarning ("Flag 1: " + controller.rbody2d.velocity + " vs " + controller.Direction.x);
		bool brakes = controller.rbody2d.velocity.x > 0 && controller.Direction.x < 0 || controller.rbody2d.velocity.x < 0 && controller.Direction.x > 0;
		anim.SetBool ("Brake", brakes);
		anim.SetBool ("OnGround", controller.Grounded);
		//anim.SetBool("Ground", controller.Grounded);

		if(controller.rbody2d.velocity.x > 0 && !facingRight)
			Flip();
		else if (controller.rbody2d.velocity.x < 0 && facingRight)
			Flip();
	}

	private void Flip()
	{
		facingRight = !facingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
