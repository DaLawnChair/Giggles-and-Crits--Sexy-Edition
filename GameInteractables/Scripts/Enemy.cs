using Godot;
using System;

public partial class Enemy : MobEntity
{
	public override void _Ready()
	{
		currHealth = 100;
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Vector3.Zero;
		grounded = IsOnFloor();

		//Handling gravity
		if(grounded==false)
		{
			y_velocity += Gravity;
		}	
		else
		{
			y_velocity = 0;
		}
		if(y_velocity <= MaxFallSpeed)
		{
			y_velocity = MaxFallSpeed;
		}

		velocity.Y = y_velocity;
		Velocity = velocity;
		MoveAndSlide();  
	}
}
