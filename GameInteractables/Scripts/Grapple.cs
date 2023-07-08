using Godot;
using System;

public partial class Grapple : RigidBody3D
{
	const float Speed = 0.25f;
	public Boolean flying = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		TopLevel = true; //Prevents Grapple from moving with camera
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(flying)
		{			
			ApplyImpulse(-Transform.Basis.Y,Transform.Basis.Y*Speed);
		}
	}
}
