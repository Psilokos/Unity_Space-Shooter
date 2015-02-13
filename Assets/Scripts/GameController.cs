using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using System;
using System.Collections;

public class GameController : MonoBehaviour
{
	public			PlayerController	player_controller;
	public			Text				score_text;
	public			Text				high_score_text;
	public			GameObject			life_slider;
	public			GameObject			restart_text;
	public			GameObject			game_over_text;
	public			GameObject			player;
	public			GameObject			asteroid;
	public			GameObject			enemy;
	public			float				time_before_first_wave;
	public			float				time_between_spawns;
	public			float				time_between_waves;
	public			float				time_until_restart;
	public			bool				is_paused { get; private set; }
	private			GameObject			pause_menu_canvas;
	private			GameObject[]		wave_object;
	private	static	GameObject			last_killed;
	private			float				time_at_death;
	private			bool				player_state;
	private			int					wave_cnt;
	private			int					score;
	private			int					high_score;

	void Start()
	{
		GameController.last_killed	= null;
		this.pause_menu_canvas		= GameObject.Find("Pause menu canvas");
		this.is_paused				= false;
		this.player_state			= true;
		this.score					= 0;
		this.wave_cnt				= 1;
		this.wave_object			= new GameObject[2];
		this.wave_object[0]			= this.asteroid;
		this.wave_object[1]			= this.enemy;
		this.restart_text.SetActive(!this.player_state);
		this.game_over_text.SetActive(!this.player_state);
		this.pause_menu_canvas.SetActive(this.is_paused);
		try
		{
			this.high_score	= Convert.ToInt32(System.IO.File.ReadAllText("high_score"));
		}
		catch
		{
			this.high_score	= 0;
		}
		StartCoroutine(this.SpawnWaves());
	}

	void Update()
	{
		if (Input.GetKeyUp(KeyCode.Escape))
			this.is_paused = !this.is_paused;

		if (!this.player && this.player_state)
		{
			this.time_at_death	= Time.time;
			this.player_state	= !this.player_state;
			this.game_over_text.SetActive(!this.player_state);
			System.IO.File.WriteAllText("high_score", this.high_score.ToString());
		}
		else if (!this.player_state)
			this.WaitUntilRestart();

		if (this.score >= this.high_score)
		{
			this.high_score				= this.score;
			this.high_score_text.text	= "High score: " + this.high_score.ToString();
		}

		this.PauseMenu();
	}

	private IEnumerator SpawnWaves()
	{
		GameObject	enemy;

		yield return new WaitForSeconds(this.time_before_first_wave);
		this.score_text.text		= "Score: " + this.score.ToString() + "\nWave: " + this.wave_cnt.ToString();
		this.high_score_text.text	= "High score: " + this.high_score.ToString();
		while (this.player_state)
		{
			for (int i = 0; i < this.wave_cnt; ++i)
			{
				int	which_object = Random.Range(0, 3);

				enemy = Instantiate
				(
					this.wave_object[(which_object < 2) ? (0) : (1)],
					new Vector3(Random.Range(-4.5f, 4.5f), 0.0f, 13.0f),
					Quaternion.identity
				) as GameObject;
				enemy.transform.parent = this.transform;
				yield return new WaitForSeconds
				(
					(i < this.wave_cnt - 1) ?
					(this.time_between_spawns) :
					(0.0f)
				);
			}
			yield return new WaitForSeconds(this.time_between_waves);
			++this.wave_cnt;
			this.score_text.text = "Score: " + this.score.ToString() + "\nWave: " + this.wave_cnt.ToString();
		}
	}

	private void WaitUntilRestart()
	{
		if (Time.time - this.time_at_death >= this.time_until_restart)
		{
			this.life_slider.SetActive(this.player_state);
			this.restart_text.SetActive(!this.player_state);
			if (Input.GetKey(KeyCode.R))
			{
				this.pause_menu_canvas.SetActive(true);
				Application.LoadLevel(Application.loadedLevel);
			}
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
		this.score_text.text = "Score: " + this.score.ToString() + "\nWave: " + this.wave_cnt.ToString();
	}

	private void PauseMenu()
	{
		this.pause_menu_canvas.SetActive(this.is_paused);
		Time.timeScale			= ((this.is_paused) ? (0.0f) : (1.0f));
	}
}
