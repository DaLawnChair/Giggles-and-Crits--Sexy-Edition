using Godot;
using System;

public partial class HUD : CanvasLayer
{
	Label health;
	Label curAmmo;
	Label resAmmo;

	Timer healthFlickerTimer;

	Color ammoBlue = Color.Color8(50,130,240,255);
	Color ammoRed = Color.Color8(230,50,100,255);
	Color ammoWhite = Color.Color8(255,255,255,255);

	Player player;	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		health = GetNode<Label>("Control/Health");
		curAmmo = GetNode<Label>("Control/CurAmmo");
		resAmmo = GetNode<Label>("Control/ResAmmo");

		healthFlickerTimer = GetNode<Timer>("Control/Health/HealthFlicker");
		healthFlickerTimer.Timeout += () => OnHealthFlickerTimeOut();

		player = (Player) GetParent().GetParent();
	}

	public void updateHealthDisplay()
	{
		health.Text = $"{player.currHealth}";
		if(((float) player.currHealth/player.maxHealth)<0.5f)
		{
			if(healthFlickerTimer.TimeLeft==0)
			{
				healthFlickerTimer.Start();
			}
		}
		else
		{
			healthFlickerTimer.Stop();
			health.AddThemeConstantOverride("outline_size",0);
		}
	}

	void OnHealthFlickerTimeOut()
	{
		int outlineSize = health.GetThemeConstant("outline_size")==25 ? 0 : 25; 
		health.AddThemeConstantOverride("outline_size", outlineSize);
	}

	void updateAmmoDisplays()
	{
		Weapon currWeapon = player.weaponHolder.currWeapon; 
		curAmmo.Text = $"{currWeapon.curAmmo[0]}";
		resAmmo.Text = $"{currWeapon.curAmmo[1]}";

		// resAmmo text turns red when below 25% capacity
		Color resAmmoColour = ((float) currWeapon.curAmmo[1]/currWeapon.maxAmmo[1])<=0.25f ? ammoRed : ammoBlue; 
		resAmmo.AddThemeColorOverride("font_color", resAmmoColour);

		// curAmmo text turns red when on last clip
		Color curAmmoColour = currWeapon.curAmmo[1]==0 ? ammoRed : ammoWhite;
		curAmmo.AddThemeColorOverride("font_color", curAmmoColour);
	}
	public override void _Process(double delta)
	{
		updateHealthDisplay();
		updateAmmoDisplays();
	}
}
