using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SoundController : MonoBehaviour
{
	public			AudioSource		ambient_music;
	public			AudioSource		explosion_asteroid;
	public			AudioSource		explosion_enemy;
	public			AudioSource		explosion_player;
	public			AudioSource		shot_enemy;
	public			AudioSource		shot_player;
	private	static	SoundController	instance;

	void Awake()
	{
		if (SoundController.instance)
			DestroyImmediate(this.gameObject);
		else
		{
			SoundController.instance = this;
			DontDestroyOnLoad(SoundController.instance.gameObject);
		}
	}

	public void MusicVolume(Slider music_slider)
	{
		this.ambient_music.volume = music_slider.value;
	}

	public void EffectsVolume(Slider effects_slider)
	{
		this.explosion_asteroid.volume	= effects_slider.value;
		this.explosion_enemy.volume		= effects_slider.value;
		this.explosion_player.volume	= effects_slider.value;
		this.shot_enemy.volume			= effects_slider.value;
		this.shot_player.volume			= effects_slider.value;
	}

	public void Mute(Toggle mute_toggle)
	{
		this.ambient_music.mute			= mute_toggle.isOn;
		this.explosion_asteroid.mute	= mute_toggle.isOn;
		this.explosion_enemy.mute		= mute_toggle.isOn;
		this.explosion_player.mute		= mute_toggle.isOn;
		this.shot_enemy.mute			= mute_toggle.isOn;
		this.shot_player.volume			= ((mute_toggle.isOn) ? (0.0f) : (1.0f));
	}
}
