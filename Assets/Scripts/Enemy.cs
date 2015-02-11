using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	public	GameObject		shot_box;
	public	GameObject		shot;
	public	Transform		shot_spawn_left;
	public	Transform		shot_spawn_right;
	public	float			tumble_speed;
	public	float			fire_rate;
	private	float			next_shot;

	void Start()
	{
		this.next_shot = 0.0f;
		this.transform.Rotate(Vector3.up, 180.0f);
		this.rigidbody.velocity = new Vector3(0.0f, 0.0f, -this.tumble_speed);
	}

	void Update()
	{
		GameObject		shot_box;
		GameObject[]	shots;

		if (Time.time > this.next_shot)
		{
			this.next_shot				= Time.time + this.fire_rate;
			shot_box					= Instantiate(this.shot_box, this.transform.position, this.transform.rotation) as GameObject;
			shot_box.transform.parent	= this.transform;
			shots						= new GameObject[2];
			shots[0]					= Instantiate(this.shot, this.shot_spawn_left.position, this.shot_spawn_left.rotation) as GameObject;
			shots[1]					= Instantiate(this.shot, this.shot_spawn_right.position, this.shot_spawn_right.rotation) as GameObject;
			foreach (GameObject shot in shots)
				shot.transform.parent = shot_box.transform;
		}
	}
}
