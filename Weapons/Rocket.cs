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
		damageArea.Monitoring = false; //Doesn't exlode when it doesn't need to
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
		GD.Print(LinearVelocity);

		if(Mathf.IsEqualApprox(LinearVelocity.LengthSquared(),0))
		{
			explode();
		}

	}

	void OnHitDetectionBodyEntered(Node3D body){}
	// {
	// 	explode();
	// 	GD.Print("stopped");
	// }

	void explode()
	{
		flying = false;

		damageArea.Monitoring = true;
		GD.Print("monitoring on");
		//CallDeferred("QueueFree");
	}
	void OnDamageAreaBodyEntered(Node3D body)
	{

		if(body.IsClass("Enemy"))
		{
			((Enemy)(body)).takeDamage(damage);
			GD.Print("tookdamage");
		}
		
	}
}
