using UnityEngine;
using System.Collections;

public class Shot : MonoBehaviour
{
	public	float		speed;
	public	GameObject	asteroid_explosion;
	public	GameObject	player_explosion;
	public	GameObject	enemy_explosion;

	void	Start()
	{
		if (this.tag == "PlayerShot")
			this.transform.rotation = Quaternion.identity;
		this.rigidbody.velocity = this.transform.forward * this.speed;
	}

	void OnTriggerEnter(Collider object_collider)
	{
		if (this.tag == "PlayerShot" && object_collider.tag == "Asteroid")
			this.Explode(ref object_collider, this.asteroid_explosion);
		else if (this.tag == "PlayerShot" && object_collider.tag == "Enemy")
			this.Explode(ref object_collider, this.enemy_explosion);
		else if (this.tag == "EnemyShot" && object_collider.tag == "Player")
			this.Explode(ref object_collider, this.player_explosion);
	}

	private void Explode(ref Collider touched_collider, GameObject explosion)
	{
		if (this.tag == "PlayerShot")
			this.GetComponentInParent<GameController>().UpdateScore(touched_collider.gameObject);
		Destroy(this.gameObject);
		Instantiate(explosion, this.transform.position, this.transform.rotation);
		Destroy(touched_collider.gameObject);
	}
}
