using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
	private Image healthImage;
	public PlayerController player;

	void Start() {
		healthImage = GetComponent<Image> ();
	}

	void Update() {
		if(((float)player.health / 100.0f) < healthImage.fillAmount) {
			healthImage.fillAmount -= 0.01f;
		}
	}
}
