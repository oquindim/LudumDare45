using UnityEngine;
using System.Collections;

public class Food : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D other)
	{
        Destroy(gameObject);
		Debug.Log(other.gameObject.GetComponent<PlatformerCharacter2D>().getGravity());
	}
}
