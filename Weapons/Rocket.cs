using Godot;
using System;

public partial class Rocket : RigidBody3D
{
	// Called when the node enters the scene tree for the first time.
	const float Speed = 100f;
	Area3D damageArea;
	public Boolean flying=false;
	public int damage;
	int rot;
	public override void _Ready()
	{
		TopLevel = true;
		damageArea = GetNode<Area3D>("DamageArea");
		damageArea.Monitoring = false; //Doesn't explode at start
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (flying)
		{
			ApplyForce(-Transform.Basis.Y*Speed);
		}
		else
		{
			Sleeping = true;
		}
		//rot += 1;

		// foreach(RayCast3D ray in damageArea.GetNode<Node3D>("RayCasts").GetChildren())
		// {
		// 	ray.Rotation+= new Vector3(0.01f,0f,0f);
		// }

	}

	void OnHitDetectionBodyEntered(Node3D body)
	{
		explode();
	}

	void explode()
	{
		flying = false;
		damageArea.Monitoring = true;
	}

	void canSee(Node3D body)
	{
		RayCast3D ray = new RayCast3D();
		//Make ray collide with the player layer and the ground layer
		ray.CollideWithAreas = true;
		ray.CollideWithBodies = true;
		ray.SetCollisionMaskValue(1,true);
		ray.SetCollisionMaskValue(2,true);
		
		//// Previous attempt at it, using globalPositions that didn’t work
		// Vector3 direction = body.GlobalPosition - ray.GlobalPosition;
		// Vector3 angleBetween = new Vector3(ray.Transform.Basis.X.AngleTo(direction) - 90*(float)(Math.PI/180f),
		// 									ray.Transform.Basis.Y.AngleTo(direction),
		// 									ray.Transform.Basis.Z.AngleTo(direction)
		// 									);
		// ray.Rotation = angleBetween;
		// GD.Print( angleBetween * (float)(180f/Math.PI));
		
		
		ray.TargetPosition = new Vector3(0,-0.5f,0); //Length of the ray

		//Forward is bad
		//Back is okay
		//Up is bad
		//Down is bad
		//Right is bad
		//Left is bad
		ray.LookAtFromPosition(ray.Position, body.Position, Vector3.Back); 
		damageArea.GetNode<Node3D>("RayCasts").AddChild(ray);
		GD.Print($"{ray.Position},{ray.RotationDegrees}");
		
		if(ray.GetCollider()==body)
		{
			if(body.HasMethod("takeDamage"))
			{
				((MobEntity)body).takeDamage(damage);
			}	
		}
	}
	void OnDamageAreaBodyEntered(Node3D body)
	{
		canSee(body);
	}
}
