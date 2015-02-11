using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class PlayableAreaBoundary
{
    public float xMin;
    public float xMax;
    public float zMin;
    public float zMax;
}

public class PlayerController : MonoBehaviour
{
	public			GameController			game_controller;
    public			PlayableAreaBoundary	boundary;
	public			GameObject				explosion_asteroid;
	public			GameObject				explosion_enemy;
	public			GameObject				explosion_player;
	public			GameObject				shot;
	public			Transform				shot_spawn_left;
	public			Transform				shot_spawn_right;
	public			Text					life_text;
	public			float					speed;
	public			float					tilt;
	public			float					fire_rate;
	public			int						life { get; private set; }
	private			float					next_shot;
	private	static	GameObject				last_enemy_shot;

	void Start()
	{
		PlayerController.last_enemy_shot	= null;
		this.next_shot						= 0.0f;
		this.life							= 100;
	}

	void Update()
	{
		GameObject[] shots;

		if (Input.GetButton("Fire") && Time.time > this.next_shot)
		{
			this.next_shot	= Time.time + this.fire_rate;
			shots			= new GameObject[2];
			shots[0]			= Instantiate(this.shot, this.shot_spawn_left.position, this.shot_spawn_left.rotation) as GameObject;
			shots[1]			= Instantiate(this.shot, this.shot_spawn_right.position, this.shot_spawn_right.rotation) as GameObject;
			foreach (GameObject shot in shots)
				shot.transform.parent = this.game_controller.transform;
		}
	}

    void FixedUpdate()
    {
        float   h_move      = Input.GetAxis("Horizontal");
        float   v_move      = Input.GetAxis("Vertical");
        Vector3 movement    = new Vector3(h_move, 0.0f, v_move);

        this.rigidbody.velocity = movement * this.speed;
        this.rigidbody.position = new Vector3
        (
            Mathf.Clamp(this.rigidbody.position.x, this.boundary.xMin, this.boundary.xMax),
            0.0f,
            Mathf.Clamp(this.rigidbody.position.z, this.boundary.zMin, this.boundary.zMax)
        );
        this.rigidbody.rotation = Quaternion.Euler(this.rigidbody.velocity.z * this.tilt / 1.5f, 0.0f, this.rigidbody.velocity.x * -this.tilt);
    }

	void OnTriggerEnter(Collider object_collider)
	{
		if (object_collider.tag == "Asteroid")
			this.Explode(ref object_collider, this.explosion_asteroid);
		else if (object_collider.tag == "Enemy")
			this.Explode(ref object_collider, this.explosion_enemy);
	}

	private void Explode(ref Collider touched_object, GameObject explosion)
	{
		Instantiate(explosion, touched_object.transform.position, touched_object.transform.rotation);
		Destroy(touched_object.gameObject);
		Instantiate(this.explosion_player, this.transform.position, this.transform.rotation);
		Destroy(this.gameObject);
	}

	public void UpdateLife(GameObject enemy_shot, int life_pts)
	{
		if (enemy_shot != PlayerController.last_enemy_shot)
		{
			this.life							+= life_pts;
			this.life_text.text					= "Life: " + this.life.ToString() + "%";
			PlayerController.last_enemy_shot	= enemy_shot;
		}
	}
}
