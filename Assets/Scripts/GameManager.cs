using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public PlayerController rectangle;
	public Text covidDefeatedText;
	public Image winStatusImage;
	public Sprite youGot1StarsSprite;
	public Sprite youGot2StarsSprite;
	public Sprite youGot3StarsSprite;
	public Sprite youLoseSprite;

	public Image homeButton;
	public Image retryButton;

	public Spawner spawner;
	public int covidDefeated;
	public bool isGameEnd;

	// Update is called once per frame
	void Start () {
		isGameEnd = false;
		winStatusImage.color = Color.clear;
		homeButton.color = Color.clear;
		retryButton.color = Color.clear;
	}

	void Update () {
		if (!isGameEnd) {
			covidDefeatedText.text = "Covid-19 Defeated : " + covidDefeated;

			if (rectangle.getHealth () <= 0) {
				if (covidDefeated < 25) {
					covidDefeatedText.text = "Game Over!";
					winStatusImage.sprite = youLoseSprite;
					isGameEnd = true;
				} else if (covidDefeated >= 25 && covidDefeated < 40) {
					covidDefeatedText.text = "You have defeated " + covidDefeated + " covid-19!";
					winStatusImage.sprite = youGot1StarsSprite;
					isGameEnd = true;
				} else if (covidDefeated >= 40) {
					covidDefeatedText.text = "You have defeated " + covidDefeated + " covid-19!";
					winStatusImage.sprite = youGot2StarsSprite;
					isGameEnd = true;
				}
			}

			if (covidDefeated >= 50) {
				covidDefeatedText.text = "You have defeated all the covid-19!";
				winStatusImage.sprite = youGot3StarsSprite;
				isGameEnd = true;
			}
		} else {
			winStatusImage.color = Color.white;
			homeButton.color = Color.white;
			retryButton.color = Color.white;
		}
	}
}
