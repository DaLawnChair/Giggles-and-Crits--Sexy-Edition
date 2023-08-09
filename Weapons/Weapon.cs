using Godot;
using System;

public partial class Weapon : Node3D
{
    String name;
    String type;

    public int[] curAmmo;
    public int[] maxAmmo;

    protected int damage;
    int fireLoss;
    int reloadGain;
    float reloadTime;
    float fireRate;
    float switchTime;
    protected AnimationPlayer animations;
    protected CharacterBody3D player;

    public Vector3 playerVelocity;
    public Boolean playerGrounded;

    public Boolean enabled=false;
    public void _Ready(String _name, String _type, int[] _curAmmo, int[] _maxAmmo, int _damage, int _fireLoss, int _reloadGain, float _reloadTime, float _fireRate, float _switchTime)
    {
        name = _name;
        type = _type;
        curAmmo = _curAmmo;
        maxAmmo = _maxAmmo;
        damage = _damage;
        fireLoss = _fireLoss;
        reloadGain = _reloadGain;
        reloadTime = _reloadTime;
        fireRate = _fireRate;
        switchTime = _switchTime;
        animations =  GetNode<AnimationPlayer>("AnimationPlayer");    
        animations.AnimationFinished += (name) => onAnimationPlayerAnimationFinished(name);
        //animations.AnimationStarted += (name) => onAnimationPlayerAnimationStarted(name);
    }

    public override void _Process(double delta)
	{
        if(enabled)
        {
            gunAnimationPlayer();
            //GD.Print($"{name}, [{curAmmo[0]},{curAmmo[1]}]");
        }
	}
	public void gunAnimationPlayer()
	{

        if(animations.CurrentAnimation == (name+"Fire"))
		{
		}
		else if(animations.CurrentAnimation == (name+"Raise"))
        {
        }
		else if(Input.IsActionPressed("fire"))
		{
			fireGun();
		}
		else
		{
            startReload();
            if(animations.CurrentAnimation != name+"Reload")
            {
                if ((playerVelocity != Vector3.Zero) && playerGrounded) 
                {
                    animations.Play(name+"Move");
                }
                else
                {
			        animations.Play(name+"Idle");
                }
            }
		}
	}

    public virtual void fireGun()
    {
        if(curAmmo[0]<=0)
        {
            return;
        }

        //Jank that teleports the shotgun to the "RESET" animation, skipping blendtime and thus only finishing when in position
        animations.Stop();
        animations.Play(name+"FireReset");
        animations.Stop();
        animations.Play(name+"FireReset");
    }

    public void startReload()
    {
        if((curAmmo[0]>=maxAmmo[0]) || (curAmmo[1]==0))
        {
            return;
        }
        animations.Play(name+"Reload");
    }
    public void reloadGun()
    {
        int curGain = Math.Min(reloadGain, curAmmo[1]);
        curAmmo[1] -= curGain;
        curAmmo[0] += curGain;
        if((curAmmo[0]<maxAmmo[0]) && (curAmmo[1]>0))
        {
            animations.Play(name+"Reload");
        }
    }
    private void onAnimationPlayerAnimationFinished(String animationName)
    {
        if(animationName==name+"Reload")
        {
            reloadGun();
        }
        if(animationName==name+"FireReset")
        {
            curAmmo[0] -= fireLoss;
            if(type=="hitscan")
            {
                animations.Play(name+"Fire");
                fireBullet();
            }  
            else if(type=="projectile")
            {
                animations.Play(name+"Fire");
                fireProjectile();
            }
        }
        if(animationName==name+"Raise")
        {
            enabled = true;
        } 
    }

    public virtual void fireBullet(){}
    public virtual void fireProjectile(){}

    public void weaponSelect()
    {
        animations.Stop();
        Visible = true;
        animations.Play(name+"Raise");
    }
    public void weaponDeselect()
    {
        animations.Stop();
        enabled = false;
        Visible = false;
    }
}