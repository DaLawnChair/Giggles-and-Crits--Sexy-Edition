using Godot;
using System;

public partial class Rocket : RigidBody3D
{
	// Called when the node enters the scene tree for the first time.
	const float Speed = 100f;
	Area3D damageArea;
	public Boolean flying=false;
	public int damage;
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
	void OnDamageAreaBodyEntered(Node3D body)
	{
		if(body.HasMethod("takeDamage"))
		{
			((MobEntity)body).takeDamage(damage);
		}	
	}
}
