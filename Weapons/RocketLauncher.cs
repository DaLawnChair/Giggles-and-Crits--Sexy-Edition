using Godot;
using System;

public partial class RocketLauncher : Weapon
{
	PackedScene rocketScene;
	Rocket rocket;

	Node3D rocketBase;
	public override void _Ready()
	{
		base._Ready("RocketLauncher", "projectile", new int[] {4,8}, new int[] {4,8}, 100, 1, 1, 0.5f, 0.5f, 0.5f);
		rocketScene = (PackedScene) GD.Load("res://Weapons/Rocket.tscn");
		rocketBase = GetNode<Node3D>("RocketBase");
	}

    public override void fireProjectile()
    {
		rocket = (Rocket) rocketScene.Instantiate();
        rocketBase.AddChild(rocket);
		rocket.flying = true;
    }

	
}