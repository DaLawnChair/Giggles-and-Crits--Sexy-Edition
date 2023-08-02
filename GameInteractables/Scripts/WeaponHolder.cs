using Godot;
using System;
using System.Collections.Generic;

public partial class WeaponHolder : Node3D
{
	List<Weapon> weaponList = new List<Weapon>();
	int currWeaponIndex=0;
	int prevWeaponIndex=1;



	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		foreach (Node3D node in GetChildren())
		{
			if(node.HasMethod("fireGun"))
			{
				weaponList.Add( (Weapon) node);
			}
		}
		GD.Print(weaponList.ToString());
		weaponList[0].enabled = true;
	}

    public override void _Input(InputEvent @event)
    {
		if(Input.IsActionJustPressed("weapon1"))
		{
			enableGun(0);
		}
		else if(Input.IsActionJustPressed("weapon2"))
		{
			enableGun(1);
		}
		else if(Input.IsActionJustPressed("weapon3"))
		{
			enableGun(2);
		}
		else if(Input.IsActionJustPressed("weapon4"))
		{
			enableGun(3);
		}
    }

	void enableGun(int selectedGun=0)
	{
		if(selectedGun<0 || selectedGun>=weaponList.Count)
		{
			return;
		}
		if(selectedGun!=currWeaponIndex)
		{
			weaponList[currWeaponIndex].weaponDeselect();
			prevWeaponIndex = currWeaponIndex;
			currWeaponIndex = selectedGun;
		}
		weaponList[currWeaponIndex].weaponSelect();
	}

}
