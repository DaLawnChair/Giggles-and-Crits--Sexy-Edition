using Godot;
using System;

public partial class Shotgun : Weapon
{
	Node3D raycasts;
	int[,] sprayPattern = new int[,] {{15,15},{15,0},{15,-15},{0,15},{0,0},{0,-15}, {-15,15},{-15,0},{-15,-15}};

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		raycasts = GetNode<Node3D>("RayCasts");
		base._Ready("Shotgun", "hitscan", new int[]{6,36}, new int[]{6,36}, 1, 0.5f, 0.5f, 0.5f);
		int counter=0;
		foreach (RayCast3D rayCast in raycasts.GetChildren())
		{			
			Vector3 rotation = new Vector3(sprayPattern[counter, 0], sprayPattern[counter, 1], 0);
			rayCast.Rotation = rotation;
			counter+=1;
			counter = counter % 3;
		}
	}
}
