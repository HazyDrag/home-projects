using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class playbutton : MonoBehaviour
{

    public Sprite layer_pushed, layer_notpushed;
    public float speed, tilt;
    public Vector3 target;
    private bool switcher = false;

    // Меняем слой на кнопке при нажатии на нее
    void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().sprite = layer_pushed;
    }

    void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().sprite = layer_notpushed;
    }

    void OnMouseUpAsButton()
    {
		GameObject.Find ("Click Audio").GetComponent<AudioSource> ().Play ();

		switch (gameObject.name) {
		case "PlayButton":
			switcher = true;
			break;
		case "RetryButton":
			SceneManager.LoadScene("play");
			break;
		case "HomeButton":
			SceneManager.LoadScene("main");
			break;
		}
    }

    void Update()
    {
        if (switcher)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed * (transform.position.x + 0.1f));
            transform.Rotate(-1f * tilt * Vector3.forward * Time.deltaTime * speed * (transform.position.x + 0.1f));
            if(transform.position == target)
				SceneManager.LoadScene("play");
        }        
    }

}
