using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour 
{

    private Vector2 direction = Vector2.zero;
	public Vector2 Direction { get { return direction; } }

	public float moveMax = 10000f;
	public float moveForceOnGround = 15.0f;
	public float moveForceOnAir = 0.5f;
	public float jumpForce = 80f;
	public float jumpSidewaysForce = 15f;

	public Transform groundCheck;
    private bool grounded = false;
	public bool Grounded { get { return grounded; } }
    private float groundRadius = 0.2f;

	private AnimationController anim;

    private Vector2 rightNormal = Vector2.zero;

	private bool jumpActionDown = false;

	private Quaternion lastGroundRotation = Quaternion.identity;
	private Vector2 lastNormal = Vector2.up;

	private Transform visuals;

	public Rigidbody2D rbody2d;

	public float groundRotateDistance = 1.5f;



	void Start () 
    {
		visuals = this.transform.FindChild("Visuals");
		anim = visuals.GetComponent<AnimationController>();
		rbody2d = this.GetComponent<Rigidbody2D> ();
	}

	void Update () 
    {
		

	}
    void FixedUpdate()
    {
		int groundLayer = LayerMask.GetMask ("Ground");
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

		if (grounded)
		{
			if (direction.x != 0)
			{
				if (direction.x > 0)
				{
					rbody2d.AddForce(rightNormal * this.moveForceOnGround);
				}
				else if (direction.x < 0)
				{
					rbody2d.AddForce(rightNormal * -1 * this.moveForceOnGround);
				}
			}
		}
		else
		{
			if (direction.x > 0)
			{
				rbody2d.AddForce(rightNormal * this.moveForceOnAir);
			}
			else if (direction.x < 0)
			{
				rbody2d.AddForce(rightNormal * -1 * this.moveForceOnAir);
			}

		}

		RaycastHit2D hit = Physics2D.Raycast(transform.position, lastNormal * -1, groundRotateDistance, groundLayer);
        Debug.DrawRay(transform.position, lastNormal * -1 * groundRotateDistance, Color.blue);

		if (hit.collider != null)
		{
			Debug.DrawRay(hit.point, hit.normal, Color.magenta);

			float angle = Mathf.Deg2Rad * 90f;
			float ax = hit.normal.x * Mathf.Cos(angle) + hit.normal.y * Mathf.Sin(angle);
			float ay = -hit.normal.x * Mathf.Sin(angle) + hit.normal.y * Mathf.Cos(angle);
	
			rightNormal = new Vector2(ax, ay);
			Debug.DrawRay(hit.point, rightNormal, Color.white);


			Vector2 v1 = Vector2.up;
			Vector2 v2 = hit.normal;

			float turnAngle = Mathf.Acos(Vector2.Dot(v1,v2));
			turnAngle = turnAngle * Mathf.Rad2Deg;
			if (v2.x > 0)
				turnAngle= 360.0f - turnAngle;
			//this.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, turnAngle));
			lastGroundRotation = Quaternion.Euler(new Vector3(0, 0, turnAngle));
			lastNormal = hit.normal;
		}
		else
		{
			lastGroundRotation = Quaternion.identity;
            lastNormal = Vector2.up;
		}

		/*
		if(!this.grounded && lastHook!=null)
		{
			Vector2 v1 = Vector2.up;
			Vector2 v2 = lastHook.GetRopeDir();

			float turnAngle = Mathf.Acos(Vector2.Dot(v1, v2));
			turnAngle = turnAngle * Mathf.Rad2Deg;
			if (v2.x > 0)
				turnAngle = 360.0f - turnAngle;
			visuals.localRotation = Quaternion.Euler(new Vector3(0, 0, turnAngle));
		}
		else
		{
			visuals.localRotation = lastGroundRotation;
		}
		*/
    }


	public void JumpActionEnd()
	{
		jumpActionDown = false;
	}


    public void UpAction()
    {
        if (direction.y == 0)
            direction.y = 1;
    }

    public void DownAction()
    {
        if (direction.y == 0)
            direction.y = -1;
    }

    public void LeftAction()
    {
        if (direction.x == 0)
            direction.x = -1;
    }
    public void StopVerticalMovement()
    {
        direction.x = 0;
    }
    public void StopHorizontalMovement()
    {
        direction.y = 0;
    }

    public void RightAction()
    {
        if(direction.x == 0)
            direction.x = 1;
    }
		
    public void JumpAction()
    {
		if (jumpActionDown == false)
		{
			jumpActionDown = true;
			
			if (grounded)
			{

				Vector2 sideForce = Vector2.zero;
				if (direction.x > 0)
				{
					sideForce = rightNormal * this.jumpSidewaysForce;
				}
				else if (direction.x < 0)
				{
                    sideForce = rightNormal * this.jumpSidewaysForce * -1;
				}
				rbody2d.AddForce(Vector2.up * jumpForce + sideForce);
			}
		}
		
    }
}
