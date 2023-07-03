using Godot;
using System;

public partial class Weapon : Node3D
{
    String name;
    String type;

    int[] curAmmo = new int[2];
    int[] maxAmmo = new int[2];
    int reloadGain;
    float reloadTime;
    float fireRate;
    float switchTime;
    protected AnimationPlayer animations;
    protected CharacterBody3D player;

    public Vector3 playerVelocity;
    public Boolean playerGrounded;

    public void _Ready(String _name, String _type, int[] _curAmmo, int[] _maxAmmo, int _reloadGain, float _reloadTime, float _fireRate, float _switchTime)
    {
        name = _name;
        type = _type;
        curAmmo = _curAmmo;
        maxAmmo = _maxAmmo;
        reloadGain = _reloadGain;
        reloadTime = _reloadTime;
        fireRate = _fireRate;
        switchTime = _switchTime;
        animations =  GetNode<AnimationPlayer>("AnimationPlayer");        
    }
    //alt-fire


    // void fire();
    // void reload();
    // void 

    public override void _Process(double delta)
	{
		gunAnimationPlayer();
	}
	public void gunAnimationPlayer()
	{

		if(animations.CurrentAnimation == (name+"Fire"))
		{
		}
		else if(Input.IsActionJustPressed("fire"))
		{
			animations.Stop();
			animations.Play(name+"Fire");
		}
		else if ((playerVelocity != Vector3.Zero) && playerGrounded) 
		{
			animations.Play(name+"Move");
		}
		else
		{
			animations.Play(name+"Idle");
		}
		GD.Print(animations.CurrentAnimation);
	}



}