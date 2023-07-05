using Godot;
using System;

public partial class HealthPack : Pickupable
{
	string resource = "Health";
	[Export]
	string type = "Small";
	float value;

	// Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
		base._Ready();
        GD.Print("BBB");
		value = resourceTypes[type];
		GD.Print(value);
    }

		public override void _on_player_body_entered(Player player)
    {
		base._on_player_body_entered(player);
		player.health += 100;
		GD.Print(player.health);
    }
}
