using UnityEngine;
using System.Collections;

public delegate void JumpDelegate ();

public class PlayerController : MonoBehaviour
{
		public Rigidbody target;
		public Collider collider;
		// The object we're steering
		 float speed = 4.0f, walkSpeedDownscale = 2.0f, turnSpeed = 2.0f, mouseTurnSpeed = 1f, jumpSpeed = 4.0f;
		// Tweak to ajust character responsiveness
		public LayerMask groundLayers = -1;
		// Which layers should be walkable?
		// NOTICE: Make sure that the target collider is not in any of these layers!

		public bool
				showGizmos = false,
			// Turn this off to reduce gizmo clutter if needed
				requireLock = false,
			// Turn this off if the camera should be controllable even without cursor lock
				controlLock = false;
		// Turn this on if you want mouse lock controlled by this script
		public JumpDelegate onJump = null;
		// Assign to this delegate to respond to the controller jumping
	
	
		private const float inputThreshold = 0.01f,directionalJumpFactor = 0.7f;

	 float groundDrag = 20.0f;
		// Tweak these to adjust behaviour relative to speed
		 float groundedCheckOffset = -0.1f;
		// Tweak so check starts from just within target footing
		private const float groundedDistance = 0.25f;
		// Tweak if character lands too soon or gets stuck "in air" often
		
	
		private bool grounded, walking;
		private bool isRemotePlayer = false;
	
		public bool Grounded {
	// Make our grounded status available for other components
				get {
						return grounded;
				}
		}

		public void SetIsRemotePlayer (bool val)
		{
				isRemotePlayer = val;
		}

		void Reset ()
	// Run setup on component attach, so it is visually more clear which references are used
		{
				Setup ();
		}
	
		void Setup ()
	// If target is not set, try using fallbacks
		{
				if (target == null) {
						target = GetComponent<Rigidbody> ();
				}
				if (collider == null) {
						collider = GetComponent<Collider> ();
				}
		}
	
		void Start ()
	// Verify setup, configure rigidbody
		{
				Setup ();
				// Retry setup if references were cleared post-add
		
				if (target == null) {
						Debug.LogError ("No target assigned. Please correct and restart.");
						enabled = false;
						return;
				}

				target.freezeRotation = true;
				// We will be controlling the rotation of the target, so we tell the physics system to leave it be
				walking = false;
		}
	
		void Update ()
	// Handle rotation here to ensure smooth application.
		{
				if (isRemotePlayer)
						return;

				if (GameManager.isOculus ())
						ApplyOculusRotation ();
				else
						StandardRotation ();

				Walking ();
			
		}

		void StandardRotation ()
		{
				float rotationAmount;
				if (Input.GetMouseButton (1) && (!requireLock || controlLock || Screen.lockCursor)) {
						// If the right mouse button is held, rotation is locked to the mouse
						if (controlLock) {
								Screen.lockCursor = true;
						}
		
						rotationAmount = Input.GetAxis ("Mouse X") * mouseTurnSpeed * Time.deltaTime;
				} else {
						if (controlLock) {
								Screen.lockCursor = false;
						}
			
						rotationAmount = Input.GetAxis ("Horizontal") * turnSpeed * Time.deltaTime;
				}
				target.transform.RotateAround (target.transform.up, rotationAmount);
		}

		void Walking ()
		{
				if (Input.GetKeyDown (KeyCode.Backslash) || Input.GetKeyDown (KeyCode.Plus)) {
						walking = !walking;
				}
		}
		//apply oculus Y rotation to the character
		void ApplyOculusRotation ()
		{
				Quaternion q = Quaternion.identity;
				OVRDevice.GetOrientation (0, ref q);
				Vector3 eulerAngles = target.transform.rotation.eulerAngles;
				eulerAngles.y = q.eulerAngles.y;
				transform.rotation = Quaternion.Euler (eulerAngles);
		}
	
		float SidestepAxisInput {
	// If the right mouse button is held, the horizontal axis also turns into sidestep handling
				get {
						if (Input.GetMouseButton (1)) {
								float sidestep = -(Input.GetKey (KeyCode.Q) ? 1 : 0) + (Input.GetKey (KeyCode.E) ? 1 : 0);
								float horizontal = Input.GetAxis ("Horizontal");
				
								return Mathf.Abs (sidestep) > Mathf.Abs (horizontal) ? sidestep : horizontal;
						} else {
								//float sidestep = -(Input.GetKey (KeyCode.Q) ? 1 : 0) + (Input.GetKey (KeyCode.E) ? 1 : 0);
				float sidestep = Input.GetAxis ("Horizontal");
				return sidestep;
						}
				}
		}
	
		void FixedUpdate ()
	// Handle movement here since physics will only be calculated in fixed frames anyway
		{

   
				grounded = Physics.Raycast (
			collider.transform.position + collider.transform.up * -groundedCheckOffset,
			collider.transform.up * -1,
			groundedDistance,
			groundLayers
				);
				// Shoot a ray downward to see if we're touching the ground

				if (isRemotePlayer)
						return;

		target.drag = 0;
				

				if (grounded) {
			target.drag = groundDrag;
						if (Input.GetButton ("Jump")) {

								// Handle jumping
								
				target.AddForce (jumpSpeed * target.transform.up + target.velocity.normalized * directionalJumpFactor, ForceMode.VelocityChange);
								// When jumping, we set the velocity upward with our jump speed
								// plus some application of directional movement
				
								if (onJump != null) {
										onJump ();
								}
						} else {

						}
				} else {
						//target.drag = 0.0f;
						// If we're airborne, we should have no drag
				}


				// Only allow movement controls if we did not just jump
				Vector3 movement = Input.GetAxis ("Vertical") * target.transform.forward +SidestepAxisInput * target.transform.right;
		
				float appliedSpeed = walking ? speed / walkSpeedDownscale : speed;
				// Scale down applied speed if in walk mode
		
				if (Input.GetAxis ("Vertical") < 0.0f) {
						// Scale down applied speed if walking backwards
						appliedSpeed /= walkSpeedDownscale;
				}
		
				if (movement.magnitude > inputThreshold) {
						// Only apply movement if we have sufficient input

			if(grounded)
					target.AddForce (movement.normalized * appliedSpeed, ForceMode.VelocityChange);//VelocityChange
			//else
			//	target.AddForce (movement.normalized * appliedSpeed/15, ForceMode.VelocityChange);//VelocityChange

				} else {
						// If we are grounded and don't have significant input, just stop horizontal movement
						target.velocity = new Vector3 (0.0f, target.velocity.y, 0.0f);
						//	return;
				}

		}
	
		void OnDrawGizmos ()
		// Use gizmos to gain information about the state of your setup
		{
				if (!showGizmos || target == null) {
						return;
				}
		
				Gizmos.color = grounded ? Color.blue : Color.red;
				Gizmos.DrawLine (target.transform.position + target.transform.up * -groundedCheckOffset,
			target.transform.position + target.transform.up * -(groundedCheckOffset + groundedDistance));
		}

	void OnCollisionEnter(Collision col)
	{
		Debug.Log ("kolizja!");

	}
}
