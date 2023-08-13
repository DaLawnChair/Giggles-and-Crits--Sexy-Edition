using Godot;
using System;
using System.Collections.Generic;

public partial class WeaponHolder : Node3D
{
	public List<Weapon> weaponList = new List<Weapon>();
	public int currWeaponIndex=0;
	int prevWeaponIndex=1;
	public Weapon currWeapon;



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
		currWeapon = weaponList[0];
		currWeapon.enabled = true;
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
		else if(Input.IsActionJustPressed("weaponSwitch"))
		{
			enableGun(prevWeaponIndex);
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
			currWeapon = weaponList[currWeaponIndex];
		}
		weaponList[currWeaponIndex].weaponSelect();
	}
	public Boolean addAmmo(float value)
	{
		Boolean addedAmmo=false;
		foreach(Weapon weapon in weaponList)
		{
			if(weapon.curAmmo[1]!=weapon.maxAmmo[1])
			{
				addedAmmo = true;
			}
			weapon.curAmmo[1] = Math.Min(weapon.maxAmmo[1], weapon.curAmmo[1] + (int) (value*weapon.maxAmmo[1]));
			GD.Print(weapon.curAmmo[1]);
		}
		GD.Print(addedAmmo);
		return addedAmmo;
	}

}
