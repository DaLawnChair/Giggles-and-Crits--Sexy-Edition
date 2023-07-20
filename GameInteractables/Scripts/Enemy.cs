using Godot;
using System;

public partial class Enemy : CharacterBody3D
{
	public const float JumpForce = 100f;
	public const float Sensitivity = 0.01f;
	

	public const float MoveSpeedGround = 20.0f;
	public const float MoveSpeedAir = 18.0f;
	
	public const float Gravity = -4.9f;
	public const float MaxFallSpeed = -20f;
	public Weapon currentWeapon;

	public int health=100;

	float y_velocity=0;
	Boolean grounded;

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Vector3.Zero;
		grounded = IsOnFloor();
		
		// Vector3 forward_velocity =  Transform.Basis.Z * (-Input.GetActionStrength("move_forward")+Input.GetActionStrength("move_backward")); 
		// Vector3 right_velocity = Transform.Basis.X * (Input.GetActionStrength("move_right")-Input.GetActionStrength("move_left")); 

		// velocity = forward_velocity + right_velocity;
		// velocity = velocity.Normalized();
		// if(grounded)
		// {
		// 	velocity *= MoveSpeedGround;
		// }
		// else
		// {
		// 	velocity *= MoveSpeedAir;
		// }

		//Handling gravity
		if(grounded==false)
		{
			y_velocity += Gravity;
		}	
		else
		{
			y_velocity = 0;
			// if(Input.IsActionJustPressed("jump"))
			// {
			// 	y_velocity = JumpForce;
			// }
		}
		if(y_velocity <= MaxFallSpeed)
		{
			y_velocity = MaxFallSpeed;
		}

		velocity.Y = y_velocity;
		//velocity = velocity.Rotated(Vector3.Up, camera.Rotation.Y);
		Velocity = velocity;
		MoveAndSlide();  
		//updateGunPlayerAnimationVars();
	}

	public void takeDamage(int damage)
	{
		health-= damage;
		GD.Print(health);
	}
}
