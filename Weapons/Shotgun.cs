using Godot;
using System;

public partial class Shotgun : Weapon
{
	Node3D rayCasts;
	int[,] sprayPattern = new int[,] {{15,15},{15,0},{15,-15},{0,15},{0,0},{0,-15}, {-15,15},{-15,0},{-15,-15}};

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		rayCasts = GetNode<Node3D>("RayCasts");
		base._Ready("Shotgun", "hitscan", new int[] {6,12}, new int[] {6,36}, 3, 1, 1, 0.5f, 0.5f, 0.5f);
		int counter=0;
		foreach (RayCast3D rayCast in rayCasts.GetChildren())
		{			
			//rayCast.Rotation = new Vector3(sprayPattern[counter, 0], sprayPattern[counter, 1], 0);
			GD.Print(rayCast.Rotation);
			counter+=1;
			//counter = counter % 3;
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
				GD.Print($"{rayCast.Name}");
				rayCast.GetCollider().CallDeferred("takeDamage",damage);
			}
		}
	}
}
