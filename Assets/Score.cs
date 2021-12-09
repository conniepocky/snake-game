using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    // Start is called before the first frame update
    
    static Score instance;

    private Text text;
    private int score;

    void Start()
    {
	text = GetComponent<Text>();	

    }

    public void AddScore() {
	score += 1;
	text.text = "Score " + score.ToString();
    }

    public void GameOver() {
	text.text = "Game Over\n" + "Score " + score.ToString();
    }
}
