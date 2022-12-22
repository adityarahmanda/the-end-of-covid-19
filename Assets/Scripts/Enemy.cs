using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public Transform target;
	public Animator anim;
	public float speed;
	public int damage;

	public GameManager gameManager;

	void Start(){
		target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
		anim = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Animator> ();
		gameManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
	}

	// Update is called once per frame
	void Update () {
		if (target != null) {
			transform.Translate (Vector2.left * speed * Time.deltaTime);

			if (target.position.x > transform.position.x) {
				transform.eulerAngles = new Vector3 (0, 180, 0);
			} else {
				transform.eulerAngles = new Vector3 (0, 0, 0);
			}
		}

		if (gameManager.isGameEnd) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.tag == "Player") {
			PlayerController player = collision.GetComponent<PlayerController> ();
			player.Attacked (damage);
			if (player.transform.position.x < transform.position.x) {
				player.IsKnockbackFromRight = true;
			} else {
				player.IsKnockbackFromRight = false;
			}
		}
	}

	public void TakeDamage(){
		gameManager.covidDefeated++;
		anim.SetTrigger ("CameraShake");
		Destroy (gameObject);
	}
}
