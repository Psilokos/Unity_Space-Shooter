using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour
{
	public	float			rotation_speed;
	public	float			tumble_speed;
	public	GameObject		asteroid_1;
	public	GameObject		asteroid_2;
	public	GameObject		asteroid_3;
	private	GameObject		asteroid_model;
	private	GameObject[]	asteroid_nbr;

	void Start()
	{
		int		which_asteroid = Random.Range(0, 100);

		this.rigidbody.angularVelocity = Random.insideUnitSphere * this.rotation_speed;
		this.rigidbody.velocity = new Vector3(0.0f, 0.0f, -this.tumble_speed);
		this.asteroid_nbr = new GameObject[3];
		this.asteroid_nbr[0] = this.asteroid_1;
		this.asteroid_nbr[1] = this.asteroid_2;
		this.asteroid_nbr[2] = this.asteroid_3;
		this.asteroid_model = Instantiate(this.asteroid_nbr[which_asteroid % 3], this.transform.position, this.transform.rotation) as GameObject;
		this.asteroid_model.transform.parent = this.transform;
	}
}
