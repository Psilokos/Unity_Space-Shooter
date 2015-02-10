using UnityEngine;
using System.Collections;

public class Boundary : MonoBehaviour
{
	void OnTriggerExit(Collider object_collider)
	{
		Destroy(object_collider.gameObject);
	}
}
