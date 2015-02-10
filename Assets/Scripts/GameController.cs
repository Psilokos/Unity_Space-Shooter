using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour
{
	public			Text			score_text;
	public			GameObject		restart_text;
	public			GameObject		player;
	public			GameObject		asteroid;
	public			GameObject		enemy;
	public			float			time_before_first_wave;
	public			float			time_between_spawns;
	public			float			time_between_waves;
	public			float			time_until_restart;
	private			GameObject[]	wave_object;
	private	static	GameObject		last_killed;
	private			float			time_at_death;
	private			bool			player_state;
	private			int				score;

	void Start()
	{
		GameController.last_killed	= null;
		this.player_state			= true;
		this.score					= 0;
		this.wave_object			= new GameObject[2];
		this.wave_object[0]			= this.asteroid;
		this.wave_object[1]			= this.enemy;
		this.restart_text.SetActive(!this.player_state);
		StartCoroutine(this.SpawnWaves());
	}

	void Update()
	{
		if (Input.GetAxis("Quit") != 0)
			Application.Quit();
		if (!this.player && this.player_state)
		{
			this.time_at_death	= Time.time;
			this.player_state	= false;
		}
		else if (!this.player_state)
			this.WaitUntilRestart();
	}

	private IEnumerator SpawnWaves()
	{
		int	wave_cnt = 1;

		yield return new WaitForSeconds(this.time_before_first_wave);
		while (this.player_state)
		{
			for (int i = 0; i < wave_cnt; ++i)
			{
				int	which_object = Random.Range(0, 3);

				Instantiate
				(
					this.wave_object[(which_object < 2) ? (0) : (1)],
					new Vector3(Random.Range(-4.5f, 4.5f), 0.0f, 13.0f),
					Quaternion.identity
				);
				yield return new WaitForSeconds
				(
					(i < wave_cnt - 1) ?
					(this.time_between_spawns) :
					(0.0f)
				);
			}
			++wave_cnt;
			yield return new WaitForSeconds(this.time_between_waves);
		}
	}

	private void WaitUntilRestart()
	{
		if (Time.time - this.time_at_death >= this.time_until_restart)
		{
			this.restart_text.SetActive(!this.player_state);
			if (Input.GetKey(KeyCode.R))
				Application.LoadLevel(Application.loadedLevel);
		}
	}

	public void UpdateScore(GameObject object_touched)
	{
		if (object_touched != GameController.last_killed)
		{
			if (object_touched.tag == "Asteroid")
				this.score += 5;
			else if (object_touched.tag == "Enemy")
				this.score += 10;
			GameController.last_killed = object_touched;
		}
		this.score_text.text = "Score: " + this.score.ToString();
	}
}
