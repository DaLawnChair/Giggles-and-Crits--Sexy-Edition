using Godot;
using System;

public partial class Shotgun : Weapon
{
	Node3D rayCasts;
	int[,] sprayPattern = new int[,] {{7,7},{7,0},{7,-7},{0,7},{0,0},{0,-7}, {-7,7},{-7,0},{-7,-7}};

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		rayCasts = GetNode<Node3D>("RayCasts");
		base._Ready("Shotgun", "hitscan", new int[] {6,12}, new int[] {6,36}, 10, 1, 1, 0.5f, 0.5f, 0.5f);
		int counter=0;
		foreach (RayCast3D rayCast in rayCasts.GetChildren())
		{			
			rayCast.RotationDegrees = new Vector3(sprayPattern[counter, 0], sprayPattern[counter, 1], 0);
			counter+=1;
		}
	}

	public override void fireBullet()
	{
		foreach (RayCast3D rayCast in rayCasts.GetChildren())
		{
			if(!rayCast.IsColliding())
			{
				continue;
			}
			if(rayCast.GetCollider().HasMethod("takeDamage"))
			{
				rayCast.GetCollider().CallDeferred("takeDamage",damage);
			}
		}
	}
}
