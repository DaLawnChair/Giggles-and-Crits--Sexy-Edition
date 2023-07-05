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
        GD.Print("BBB");
		value = resourceTypes[type];
		GD.Print(value);
    }

	public override void _on_player_body_entered(Player player)
    {
		base._on_player_body_entered(player);
        player.currentWeapon.curAmmo[1] = Math.Min(player.currentWeapon.maxAmmo[1], player.currentWeapon.curAmmo[1] + (int) (value*player.currentWeapon.maxAmmo[1]));
		GD.Print(player.currentWeapon.curAmmo[1]);
    }
}
