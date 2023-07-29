using Godot;
using System;

public partial class GrappleLauncher : Node3D
{
	PackedScene grappleScene;
	Grapple grapple;
	Node3D grappleBase;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		grappleScene =  (PackedScene) GD.Load("res://GameInteractables/Grapple.tscn");
		grappleBase = GetNode<Node3D>("GrappleBase");
		Visible = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("fire_grapple"))
		{
			fireGrapple();
		}
		if(Input.IsActionJustReleased("fire_grapple"))
		{
			retractGrapple();
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