using Godot;
using System;


public partial class MobEntity : CharacterBody3D
{

	protected const float JumpForce = 50f;
	protected const float Sensitivity = 0.01f;

	protected const float MoveSpeedGround = 10.0f;
	protected const float MoveSpeedAir = 9.0f;
	
	protected const float Gravity = -4.9f;
	protected const float MaxFallSpeed = -9.8f;

    public int currHealth;
	public int maxHealth=100;

	protected float y_velocity=0;
	protected Boolean grounded;

    public void takeDamage(int damage)
	{
		currHealth -= damage;
        GD.Print(currHealth);
	}
}