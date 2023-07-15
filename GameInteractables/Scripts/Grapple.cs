using Godot;
using System;

public partial class Grapple : RigidBody3D
{
	const float Speed = 0.1f;
	public Boolean flying = false;
	public Vector3 hookPoint;
	GrappleLauncher grappleLauncher;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		TopLevel = true; //Prevents Grapple from moving with camera
		grappleLauncher = (GrappleLauncher) GetParent().GetParent();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(flying)
		{			
			ApplyImpulse(-Transform.Basis.Y*Speed);
			grappleLauncher.drawRope();
		}
		else
		{
			Sleeping=true;

		}
		hookPoint = Position;
	}

	public void _on_area_body_3d_body_entered(Node3D body)
	{
		flying=false;
	}
	
}