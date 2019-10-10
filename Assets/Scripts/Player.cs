using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {

	[System.Serializable]
	public class PlayerStats
	{
		public float hp = 2;
		public int jabuticaba = 0;
		public float gravity;
	}

	public PlayerStats playerStats = new PlayerStats();
	public float fallBoundary = -20f;
	public Text weightText;
	public Text gravityText;
	public GameObject jabuContainer;
	private int jabuTotal;
	public GameObject portal;
	public PlatformerCharacter2D dplayer;

	void Start () {
		Debug.Log(playerStats.jabuticaba);
		jabuTotal = jabuContainer.transform.childCount;
		dplayer = GetComponent<PlatformerCharacter2D>();
		// Pass values between screens
		if (PlayerPrefs.HasKey("weight")) {
			playerStats.hp = PlayerPrefs.GetFloat("weight");
			dplayer.setWeight(playerStats.hp);

		} else{
			playerStats.hp = dplayer.getWeight();
			PlayerPrefs.SetFloat("weight", playerStats.hp);
		}
		updateWeightText();
		playerStats.gravity = dplayer.getGravity();
		gravityText.text = "Gravity: " + playerStats.gravity.ToString ();

	}
	void updateWeightText() {
		weightText.text = "Weight: " + playerStats.hp.ToString ();
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
	void UpdateWeight(float val) {
		dplayer.setWeight(playerStats.hp + val);
		PlayerPrefs.SetFloat("weight",playerStats.hp + val);
		updateWeightText();
	}
	private void OnTriggerEnter2D (Collider2D other)
    {
		if (other.gameObject.tag == "jabuticaba") {
			Destroy(other.gameObject);
			playerStats.jabuticaba += 1;
			playerStats.hp += 0.2f;
			UpdateWeight(1);
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
