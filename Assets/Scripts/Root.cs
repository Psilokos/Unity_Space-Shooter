using UnityEngine;
using System.Collections;

public class Root : MonoBehaviour
{
	void Start()
	{
		Component[] root_childrens = this.GetComponentsInChildren(typeof(Transform), true);

		foreach (Component children in root_childrens)
			if (children.name == "Pause menu canvas")
				children.gameObject.SetActive(true);
	}
}
