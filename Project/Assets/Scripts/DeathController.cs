using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DeathController : MonoBehaviour {

    public Button restartButton;
    public Button menuButton;
    public TMP_Text deathScoreText;
    public GameObject deathCurtain;

    void Start () {
		
	}
	
    public void SetStats(int score, float timeLeft, int healthLeft)
    {
        deathCurtain.SetActive(true);
        deathScoreText.gameObject.SetActive(true);
        deathScoreText.text = "Score: " + score;
        restartButton.gameObject.SetActive(true);
        RectTransform rt = restartButton.GetComponent<RectTransform>();
        rt.localPosition = new Vector3(0, -50, 0);
        menuButton.gameObject.SetActive(true);
    }
}
