using UnityEngine;
using System.Collections;

public class TimeoutDestroy : MonoBehaviour
{
	public	float	lifetime;

	void Start()
	{
		Destroy(this.gameObject, this.lifetime);
	}
}
