using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bestScore : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent <Text> ().text = "Best score: " + PlayerPrefs.GetInt ("BestScore").ToString();	
	}
}
