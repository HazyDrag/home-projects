using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class game : MonoBehaviour {


	private GameObject [] rounds = new GameObject[3];
    private GameObject [] roundTimers = new GameObject[3];
    private GameObject [] squares = new GameObject[3];
	private List<Color> colorsSquares = new List<Color> ();
	private int count;

	public Vector3 [] positionsSquares, positionsRounds, positionsRoundTimers;
	public GameObject square, round, roundTimer, dialogEnd;
	public Text score, bestScore;
	public TextAsset settings;
	public GameObject recordText1, recordText2;

	[HideInInspector]
	public int numberOfSquare, changeRound = -1;
	[HideInInspector]
	public bool plus, end = false;
	[HideInInspector]
	public List<Color> colorsRounds = new List<Color>();

	// Use this for initialization
	void Start () {
		ReadColors ();
		count = 0;
		for (int i = 0; i < positionsSquares.Length; i++) { //Создаем квадраты и круги (и их таймеры) и назначаем им случайные цвета, следим, чтобы эти цвета не повторялись
			squares [i] = Instantiate (square, positionsSquares [i], Quaternion.identity) as GameObject;
			rounds [i] = Instantiate (round, positionsRounds [i], Quaternion.identity) as GameObject;
            roundTimers [i] = Instantiate(roundTimer, positionsRoundTimers[i], Quaternion.identity) as GameObject;

            int randomInd = Random.Range (0, colorsSquares.Count - 1);
			squares [i].GetComponent <SpriteRenderer> ().color = colorsSquares [randomInd];
			colorsSquares.RemoveAt (randomInd);
			squares [i].GetComponent <MoveSquare> ().numberOfSquare = i;

			randomInd = Random.Range (0, colorsRounds.Count - 1);
			rounds [i].GetComponent <SpriteRenderer> ().color = colorsRounds [randomInd];
			colorsRounds.RemoveAt (randomInd);

            roundTimers [i].GetComponent<roundTimer>().numberOfTimer = i;
        }
	}

	void ReadColors(){
		colorsSquares.Clear ();
		colorsRounds.Clear ();

		string[] settingsParse = settings.text.Split ('=');
		string[] hexColors = settingsParse [2].Split (',');

		for (int i = 0; i < hexColors.Length; i++) {
			Color32 newColor = HexToColor(hexColors [i].Trim ());
			colorsSquares.Add (newColor);
		}

		colorsRounds.AddRange (colorsSquares);
	}

	Color HexToColor(string hex){
		byte r = byte.Parse (hex.Substring (0, 2), System.Globalization.NumberStyles.HexNumber);
		byte g = byte.Parse (hex.Substring (2, 2), System.Globalization.NumberStyles.HexNumber);
		byte b = byte.Parse (hex.Substring (4, 2), System.Globalization.NumberStyles.HexNumber);

		return new Color32 (r, g, b, 255);
	}
	
	// Update is called once per frame
	void Update () {
        if (numberOfSquare != -1)
            ReplaceSquare();

        if (changeRound != -1)
            ChangeColorOfRound();

        if (end)
            End();
    }

    void ReplaceSquare()
    {
        //Удаляем квадрат, возвращаем его цвет в список доступных, и создаем новый с рандомным цветом
		Color oldColor = squares[numberOfSquare].GetComponent<SpriteRenderer>().color;
        Destroy(squares[numberOfSquare]);

        squares[numberOfSquare] = Instantiate(square, positionsSquares[numberOfSquare], Quaternion.identity) as GameObject;

        int randomInd = Random.Range(0, colorsSquares.Count - 1);
		Color newColor = colorsSquares[randomInd];
		squares [numberOfSquare].GetComponent<SpriteRenderer> ().color = newColor;
		colorsSquares.Remove(newColor);
		colorsSquares.Add (oldColor);

        squares[numberOfSquare].GetComponent<MoveSquare>().numberOfSquare = numberOfSquare;

        numberOfSquare = -1; //обнуляем для ожидания следующего

        if (plus)
        {
			GameObject.Find ("AudioSuccess").GetComponent<AudioSource> ().Play ();

            count++;
            score.text = "Score: " + count.ToString();

			if (PlayerPrefs.GetInt ("BestScore") < count) {
				PlayerPrefs.SetInt ("BestScore", count);
				bestScore.text = "Best score: " + count.ToString();
			}

            plus = false;
        }
    }

    void ChangeColorOfRound() {
        //Меняем цвет круга, у которого закончился таймер
        Color oldColor = rounds[changeRound].GetComponent<SpriteRenderer>().color;
        int randomInd = Random.Range(0, colorsRounds.Count - 1);
		Color newColor = colorsRounds[randomInd];
		rounds [changeRound].GetComponent<SpriteRenderer> ().color = newColor;
		colorsRounds.Remove(newColor);
        colorsRounds.Add(oldColor);

        roundTimers[changeRound] = null;//создаем таймер для этого круга заново
        roundTimers[changeRound] = Instantiate(roundTimer, positionsRoundTimers[changeRound], Quaternion.identity) as GameObject;
        roundTimers[changeRound].GetComponent<roundTimer>().numberOfTimer = changeRound;

        changeRound = -1;
    }

    void End() {
		GameObject.Find ("AudioOver").GetComponent<AudioSource> ().Play ();

        end = false;

        foreach(GameObject go in roundTimers)
            go.GetComponent<roundTimer>().stop = true;

        foreach(GameObject go in squares)
            go.GetComponent<MoveSquare>().stop = true;

		dialogEnd.SetActive (true);

		if (PlayerPrefs.GetInt ("BestScore") == count) {			
			recordText1.SetActive (true);
			recordText2.SetActive (false);
		}
    }
}
