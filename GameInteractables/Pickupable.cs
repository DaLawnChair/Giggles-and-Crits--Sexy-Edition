using Godot;
using System;
using System.Collections.Generic; 

public partial class Pickupable : StaticBody3D
{
    Area3D collision;
    MeshInstance3D mesh;
    Timer spawnTimer;
    protected readonly Dictionary<String, float> resourceTypes = new Dictionary<String, float>(){{"Small", 0.25f}, {"Medium", 0.5f}, {"Large", 1.0f}};
    float respawnTime = 2f;


    public override void _Ready()
    {
        collision = GetNode<Area3D>("Area3D");
        mesh = GetNode<MeshInstance3D>("MeshInstance3D");
        spawnTimer = GetNode<Timer>("Timer");
        spawnTimer.WaitTime = respawnTime;
        GD.Print("AAA");
    }

    public void _on_area_3d_body_entered(Player player)
    {
        GD.Print(player.Name);
        collision.SetDeferred("monitoring",false);
        mesh.Visible = false;
        player.health += 100;
        spawnTimer.Start();
    }

    public void _on_timer_timeout()
    {
        mesh.Visible = true;
        collision.SetDeferred("monitoring",true);
    }
}