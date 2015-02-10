using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	public	float		tumble_speed;
	public	GameObject	shot;
	public	Transform	shot_spawn_left;
	public	Transform	shot_spawn_right;
	public	float		fire_rate;
	private	float		next_shot = 0.0f;

	void Start()
	{
		this.transform.Rotate(Vector3.up, 180.0f);
		this.rigidbody.velocity = new Vector3(0.0f, 0.0f, -this.tumble_speed);
	}

	void Update()
	{
		if (Time.time > this.next_shot)
		{
			this.next_shot = Time.time + this.fire_rate;
			Instantiate(this.shot, this.shot_spawn_left.position, this.shot_spawn_left.rotation);
			Instantiate(this.shot, this.shot_spawn_right.position, this.shot_spawn_right.rotation);
		}
	}
}
