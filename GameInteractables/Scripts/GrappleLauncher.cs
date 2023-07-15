using Godot;
using System;

public partial class GrappleLauncher : Node3D
{
	PackedScene grappleScene;
	Grapple grapple;
	MeshInstance3D rope;
	MeshInstance3D ropeDefault;
	Node3D grappleBase;
	Camera3D camera;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		grappleScene =  (PackedScene) GD.Load("res://GameInteractables/Grapple.tscn");
		grappleBase = GetNode<Node3D>("GrappleBase");
		camera = GetParent<Camera3D>();
		rope = GetNode<MeshInstance3D>("GrappleBase/Rope");
		rope.TopLevel = false;
		Visible = false;
		//ropeDefault = (MeshInstance3D) rope.Duplicate();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//grappleBase.Rotation = camera.Rotation;
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
		resetRope();
		Visible = false;
		grapple.QueueFree();
	}

	public void drawRope()
	{
		float deltaPositionZ = grapple.Position.Z - grappleBase.Position.Z; 
		rope.Scale = new Vector3(rope.Scale.X, rope.Scale.Y, Mathf.Abs(deltaPositionZ)); // Absolute value so scaling isn't affected by direction
		rope.Position = new Vector3(rope.Position.X, rope.Position.Y, deltaPositionZ/2);
		rope.LookAtFromPosition(grappleBase.Position,grapple.Position);
	}
 	void resetRope()
	{
		rope.Scale = new Vector3(0.75f,0.75f,0.75f);
		rope.Position = grappleBase.Position;
	}
}
