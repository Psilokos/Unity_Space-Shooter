using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
	private	static	PauseMenu	instance;

	void Awake()
	{
		if(PauseMenu.instance)
			DestroyImmediate(this.gameObject);
		else
		{
			DontDestroyOnLoad(this.gameObject);
			PauseMenu.instance = this;
		}
	}

	public void Restart()
	{
		Application.LoadLevel(Application.loadedLevel);
	}

	public void Quit()
	{
		Application.Quit();
	}
}
