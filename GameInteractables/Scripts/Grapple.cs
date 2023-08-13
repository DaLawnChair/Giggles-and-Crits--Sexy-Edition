using Godot;
using System;

public partial class Grapple : RigidBody3D
{
	public const float Speed = 20f;
	public Boolean flying = false;
	Node3D grappleBase;
	Curve3D rope;
	GrappleLauncher grappleLauncher;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		TopLevel = true; //Prevents Grapple from moving with camera
		grappleBase = (Node3D) GetParent();
		grappleLauncher = (GrappleLauncher) GetParent().GetParent();
		rope = GetNode<Path3D>("Rope").Curve;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(flying)
		{			
			ApplyForce(Transform.Basis.Y*Speed); //We rotated the object 90 in the x axis, so we move based on the Basis of the Y-axis to go in the Global Z direction
			drawRope();
		}
		else
		{
			Sleeping=true;

		}
	}

	public void drawRope()
	{
		rope.SetPointPosition(1,grappleBase.Position);
		rope.SetPointPosition(0,Position);
		// GD.Print(rope.GetPointPosition(0));
		// GD.Print(rope.GetPointPosition(0)-grappleBase.Position);
	}
	public void _on_area_body_3d_body_entered(Node3D body)
	{
		flying=false;
	}
	
}