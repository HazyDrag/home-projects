using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveSquare : MonoBehaviour {

	private Vector3 standartPos;
	private Vector3 positionZ;
	private GameObject score;
	private DateTime click1 = new DateTime();

	[HideInInspector]
	public int numberOfSquare;
    [HideInInspector]
    public bool stop = false;

    // Use this for initialization
    void Start () {
		standartPos = transform.position;
		score = GameObject.Find ("ScoreText");
		click1 = DateTime.Today;
	}

	void OnMouseDrag () {
        if (!stop)
        {
            Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y); //Перемещение квадрата за курсором мыши при нажатии на него
            Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = objPosition;
            GetComponent<SpriteRenderer>().sortingOrder = 1;
        }
	}

	void OnMouseUp () {
		transform.position = standartPos; //возвращение на место
		GetComponent<SpriteRenderer> ().sortingOrder = 0;
	}

	void OnTriggerEnter2D(Collider2D other) { 
		if(other.gameObject.name == "Round(Clone)" 
			&& other.GetComponent<SpriteRenderer> ().color ==  GetComponent<SpriteRenderer> ().color) //если цвета совпали, то передаем главному скрипту информацию, чтобы он добавил очки и поменял цвет у этого квадрата
		{
			score.GetComponent <game> ().plus = true;
			score.GetComponent <game> ().numberOfSquare = numberOfSquare;
		}
	}

    void OnMouseDown()
    { //Обработчик для двойного клика
        if (!stop)
        {
            TimeSpan ts = DateTime.Now - click1;
            if (ts.TotalMilliseconds < 501)
            {
                bool switcher = false;
                foreach (Color c in score.GetComponent<game>().colorsRounds)//проверяем, есть ли цвет этого квадрата среди массива с неиспользованными цветами кругов
                    if (c == GetComponent<SpriteRenderer>().color)
                        switcher = true;

				if (switcher) {
					GameObject.Find ("AudioDestroy").GetComponent<AudioSource> ().Play ();
					score.GetComponent<game> ().numberOfSquare = numberOfSquare;
				}
            }
            else
                click1 = DateTime.Now;
        }
    }
}
