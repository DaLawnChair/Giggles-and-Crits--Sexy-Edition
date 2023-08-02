using Godot;
using System;

public partial class Player : CharacterBody3D
{
	public const float JumpForce = 100f;
	public const float Sensitivity = 0.01f;
	

	public const float MoveSpeedGround = 20.0f;
	public const float MoveSpeedAir = 18.0f;
	
	public const float Gravity = -4.9f;
	public const float MaxFallSpeed = -20f;
	public Node3D cameraBase;
	public WeaponHolder weaponHolder;
	public Camera3D camera;

	public int health=100;

	float y_velocity=0;
	Boolean grounded;
	public override void _Ready()
	{
		cameraBase = GetNode<Node3D>("CameraBase");	
		camera = GetNode<Camera3D>("Camera");
		weaponHolder = GetNode<WeaponHolder>("Camera/WeaponHolder");
		Input.MouseMode = Input.MouseModeEnum.Captured; // Keeps the mouse inside of the window

	} 
	public override void _Input(InputEvent @event)
	{
		if(@event is InputEventMouseMotion)
		{
			InputEventMouseMotion mouseMotion = @event as InputEventMouseMotion;
			Vector3 cameraRot = camera.Rotation;
			cameraRot.Y += (-mouseMotion.Relative.X * Sensitivity);
			cameraRot.X += (-mouseMotion.Relative.Y * Sensitivity);
			cameraRot.X = Mathf.Clamp(cameraRot.X, Mathf.DegToRad(-80f), Mathf.DegToRad(80f));
			camera.Rotation = cameraRot;

		}
		if(@event is InputEventKey eventKey)
		{
			if (eventKey.IsActionPressed("exit"))
			{
				GetTree().Quit();
			}
		}

	}
	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle(); // 9.8 by default

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Vector3.Zero;
		grounded = IsOnFloor();
		
		Vector3 forward_velocity =  Transform.Basis.Z * (-Input.GetActionStrength("move_forward")+Input.GetActionStrength("move_backward")); 
		Vector3 right_velocity = Transform.Basis.X * (Input.GetActionStrength("move_right")-Input.GetActionStrength("move_left")); 

		velocity = forward_velocity + right_velocity;
		velocity = velocity.Normalized();
		if(grounded)
		{
			velocity *= MoveSpeedGround;
		}
		else
		{
			velocity *= MoveSpeedAir;
		}

		//Handling gravity
		if(grounded==false)
		{
			y_velocity += Gravity;
		}	
		else
		{
			y_velocity = 0;
			if(Input.IsActionJustPressed("jump"))
			{
				y_velocity = JumpForce;
			}
		}
		if(y_velocity <= MaxFallSpeed)
		{
			y_velocity = MaxFallSpeed;
		}

		velocity.Y = y_velocity;
		velocity = velocity.Rotated(Vector3.Up, camera.Rotation.Y);
		Velocity = velocity;
		MoveAndSlide();  
		updateGunPlayerAnimationVars();
	}

	public void updateGunPlayerAnimationVars()
	{
		//Give access to the current weapon the velocity and groundness of the player.
		weaponHolder.weaponList[weaponHolder.currWeaponIndex].playerVelocity = Velocity;
		weaponHolder.weaponList[weaponHolder.currWeaponIndex].playerGrounded = grounded;
	}
}
