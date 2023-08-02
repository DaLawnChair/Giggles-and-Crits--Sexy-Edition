using Godot;
using System;

public partial class AmmoPack : Pickupable
{
	string resource = "Ammo";
	[Export]
	string type = "Small";
	float value;

	// Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
		base._Ready();
		value = resourceTypes[type];
    }

	public override void _on_player_body_entered(Player player)
    {
		base._on_player_body_entered(player);
		player.weaponHolder.addAmmo(value);
    }
}
