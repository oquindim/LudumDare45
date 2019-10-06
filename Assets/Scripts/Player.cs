using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	[System.Serializable]
	public class PlayerStats
	{
		public int hp = 100;
		public int jabuticaba = 0;


	}

	public PlayerStats playerStats = new PlayerStats();
	public float fallBoundary = -20f;
	public GameObject jabuContainer;
	private int jabuTotal;
	public GameObject portal;

	void Start () {
		jabuTotal = jabuContainer.transform.childCount;
	}

	void Update()
	{
		if(transform.position.y <= fallBoundary)
			DamagePlayer(10000);
	}

	public void DamagePlayer(int dmg)
	{
		playerStats.hp -= dmg;
		if(playerStats.hp <= 0)
		{
			GameMaster.KillPlayer(this);
		}
	}

	private void OnTriggerEnter2D (Collider2D other)
    {
		if (other.gameObject.tag == "jabuticaba") {
			Destroy(other.gameObject);
			playerStats.jabuticaba += 1;
			Debug.Log("entrou");
			JabuticabaCheck ();
		}


    }

	private void JabuticabaCheck () {
		if (playerStats.jabuticaba == jabuTotal) {
			Debug.Log("completou");
			portal.GetComponent<Animator>().SetBool("open",true);
			portal.GetComponent<BoxCollider2D>().enabled=true;
		}
	}

}
