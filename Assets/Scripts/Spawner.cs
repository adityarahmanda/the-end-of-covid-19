using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject covid;
	private GameObject player;
	public Transform[] spawnPos;

	private int randomIndex;
	public int totalCovid;

	private float timeBtwSpawn;
	public float startTimeBtwSpawn;

	void Start(){
		player = GameObject.FindGameObjectWithTag("Player");
	}

	// Update is called once per frame
	void Update () {
		if (player != null) {
			if (totalCovid > 0) {
				if (timeBtwSpawn <= 0) {
					randomIndex = Random.Range (0, spawnPos.Length);
					Instantiate (covid, spawnPos [randomIndex].position, spawnPos [randomIndex].rotation);
					totalCovid--;
					timeBtwSpawn = startTimeBtwSpawn - (Random.Range (0.0f, startTimeBtwSpawn));
				} else {
					timeBtwSpawn -= Time.deltaTime;
				}
			}
		}
	}
}
