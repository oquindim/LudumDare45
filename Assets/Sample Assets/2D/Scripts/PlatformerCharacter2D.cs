using UnityEngine;

public class PlatformerCharacter2D : MonoBehaviour 
{
	bool facingRight = true;							// For determining which way the player is currently facing.

	[SerializeField] float maxSpeed = 10f;				// The fastest the player can travel in the x axis.
	[SerializeField] float jumpForce = 400f;			// Amount of force added when the player jumps.	

	[Range(0, 1)]
	[SerializeField] float crouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	
	[SerializeField] bool airControl = false;			// Whether or not a player can steer while jumping;
	[SerializeField] LayerMask whatIsGround;			// A mask determining what is ground to the character
	
	Transform groundCheck;								// A position marking where to check if the player is grounded.
	float groundedRadius = .2f;							// Radius of the overlap circle to determine if grounded
	bool grounded = true;								// Whether or not the player is grounded.
	Transform ceilingCheck;								// A position marking where to check for ceilings
	float ceilingRadius = .01f;							// Radius of the overlap circle to determine if the player can stand up
	Animator anim;										// Reference to the player's animator component.
	
	float gravity;
	float weigth;

	Transform playerGraphics;							// Reference to the graphics so we can change direction

    void Awake()
	{
		// Setting up references.
		groundCheck = transform.Find("GroundCheck");
		ceilingCheck = transform.Find("CeilingCheck");
		anim = GetComponent<Animator>();
		playerGraphics = transform.Find("Graphics");
		weigth = GetComponent<Rigidbody2D>().mass;
		gravity = GetComponent<Rigidbody2D>().gravityScale;
		if(playerGraphics == null)
			Debug.LogError ("Graphics not found!");
	}


	void FixedUpdate()
	{
		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);
		anim.SetBool("Ground", grounded);
		// Set the vertical animation
		anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);
	}

	public float getGravity(){ return gravity; }
	public float setGravity(float grav){
		gravity = grav;
		return gravity; 
	}

	public float getWeigth(){ return weigth; }
	public float setWeigth(float wei){
		weigth = wei;
		return weigth; 
	}


	public void Move(float move, bool crouch, bool jump, bool fall)
	{
		if(!crouch && anim.GetBool("Crouch"))
		{
			if( Physics2D.OverlapCircle(ceilingCheck.position, ceilingRadius, whatIsGround))
				crouch = true;
		}

		// Set whether or not the character is crouching in the animator
		anim.SetBool("Crouch", crouch);

		if(grounded || airControl)
		{
			move = (crouch ? move * crouchSpeed : move);

			anim.SetFloat("Speed", Mathf.Abs(move));

			GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
			
		}

        if (jump) {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
        }
		
		if (fall && gravity == 0) {
			GetComponent<Rigidbody2D>().gravityScale = 10f;
		} else {
			GetComponent<Rigidbody2D>().gravityScale = 0f;
		}
	}

}
