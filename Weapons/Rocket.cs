using Godot;
using System;

public partial class Rocket : RigidBody3D
{
	// Called when the node enters the scene tree for the first time.
	const float Speed = 100f;
	public Boolean flying=false;
	public override void _Ready()
	{
		TopLevel = true;

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

	void OnAreaBody3DBodyEntered(Node3D body)
	{
		flying = false;

	}
}
