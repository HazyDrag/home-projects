using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{


    public Text timerLabel;
	public TextAsset settings;

    private float deltaProgress, timeLeft;
	private GameObject score;
	private float time;
    
    // Use this for initialization
    void Start()
    {
		time = ReadSettings ();
        deltaProgress = transform.localScale.x / time;
        timeLeft = time;
        score = GameObject.Find("ScoreText");
    }

	float ReadSettings(){ //Парсим считанный из файла текст, чтобы получить необходимое количество секунд для таймера
		string[] parseSettings = settings.text.Split (';');
		parseSettings [0].Trim ();
		string[] timerSettings = parseSettings [0].Split ('=');

		return Convert.ToSingle(timerSettings[1]);
	}

    void Update()
    {
        timeLeft -= Time.deltaTime;

        int minutes = Convert.ToInt32(timeLeft) / 60; //осталось минут
        int seconds = Convert.ToInt32(timeLeft) % 60;//осталось секунд
                                                     //обновляем надпись
        timerLabel.text = string.Format("{0:0}:{1:00}", minutes, seconds);

        transform.localScale = new Vector3(deltaProgress * timeLeft, 1, 1);//уменьшаем полосу таймера    

		if (transform.localScale.x <= 0) {
			score.GetComponent<game> ().end = true;
			Destroy (gameObject);
		}
    }
}
