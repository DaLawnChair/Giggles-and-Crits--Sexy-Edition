using Godot;
using System;

public partial class GrappleLauncher : Node3D
{
	PackedScene grappleScene;
	Grapple grapple;
	float grappleSpeed=Grapple.Speed;
	Node3D grappleBase;
	RayCast3D rope;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		grappleScene =  (PackedScene) GD.Load("res://GameInteractables/Grapple.tscn");
		grappleBase = GetNode<Node3D>("GrappleBase");
		rope = GetNode<RayCast3D>("Rope");
		Visible = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//GD.Print(rope.TargetPosition);
		if(Input.IsActionJustPressed("fire_grapple"))
		{
			fireGrapple();
		}
		if(Input.IsActionJustReleased("fire_grapple"))
		{
			retractGrapple();
		}

		if(grappleBase.HasNode("Grapple"))
		{
			rope.TargetPosition = grapple.Position - Position; 
			//rope.LookAtFromPosition(rope.Position,grapple.Position);
			//GD.Print(rope.TargetPosition);
			GD.Print(rope.TargetPosition);
		}
		else
		{
			rope.TargetPosition = Vector3.Zero;	
		}
	}
	void fireGrapple()
	{
		grapple = (Grapple) grappleScene.Instantiate();
		grappleBase.AddChild(grapple);
		Visible = true;
		grapple.flying = true;	
	}
	void retractGrapple()
	{
		Visible = false;
		grapple.QueueFree();
		grapple = null;
	}

}