using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roundTimer : MonoBehaviour {

    public float minTime, maxTime;

    private float time, timeLeft, deltaProgress, endPosition;
    private GameObject score;

    [HideInInspector]
    public int numberOfTimer;
    [HideInInspector]
    public bool stop = false;

    // Use this for initialization
    void Start () {
        score = GameObject.Find("ScoreText");
        time = Random.Range(minTime, maxTime);
        timeLeft = time;
        deltaProgress = transform.localScale.y / time;
        endPosition = transform.position.y - (transform.localScale.y / 2);//Вычисляем, в какой точке должен оказаться якорь полосы
    }

    // Update is called once per frame
    void Update() {
        if (!stop)
        {
            timeLeft -= Time.deltaTime;
            transform.localScale = new Vector3(0.4f, deltaProgress * timeLeft, 1f);//уменьшаем полосу таймера    
            transform.position = new Vector3(transform.position.x, (deltaProgress / 2 * timeLeft) + endPosition, transform.position.z);//перемещаем полосу

			if (transform.localScale.y <= 0) { //когда заканчивается таймер, разрушаем этот объект и отправляем его номер главному скрипту
				score.GetComponent<game>().changeRound = numberOfTimer; 
				Destroy (gameObject);
			}
        }
    }
}
