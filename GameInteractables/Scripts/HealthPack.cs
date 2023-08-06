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
		value = resourceTypes[type];
    }

	Boolean IsFull(int curr, int max)
	{
		return curr==max;
	}
	public override void _on_player_body_entered(Player player)
    {
		if(player.currHealth!=player.maxHealth)
		{
			isFull = false;
			player.currHealth = Math.Min(player.maxHealth, player.currHealth + (int) (value*player.maxHealth));
			base._on_player_body_entered(player);
			GD.Print(player.currHealth);
		}
		
    }
}
