using UnityEngine;
using System.Collections;

public class ST_PuzzleTile : MonoBehaviour
{
	// the target position for this tile.
	public Vector3 TargetPosition;

	// is this an active tile?  usually one per game is inactive.
	public bool Active = true;

	// is this tile in the correct location?
	public bool CorrectLocation = false;

	public bool CorrectLocationAnswer = false;

	bool move = false;

	public static bool flag = false;
	float timer;
	public int cantidadPregunta;

	
	// store this tiles array location.
	public Vector2 ArrayLocation = new Vector2();
	public Vector2 GridLocation = new Vector2();

	public references refe;


	GameObject CanvasP;
	GameObject CanvasA;


	void Awake()
	{
		// assign the new target position.
		TargetPosition = this.transform.localPosition;

		// start the movement coroutine to always move the objects to the new target position.
		StartCoroutine(UpdatePosition());
	}

	private void Start()
	{
		refe = GameObject.FindWithTag("MainCamera").GetComponent<references>();

		if (CanvasP == null)
			CanvasP = refe.CanvasP;
		if (CanvasA == null)
			CanvasA = refe.CanvasA;

		/*if (!flag)
		{
            if (CanvasP != null)
                CanvasP.SetActive(false);
			flag = true;
		}*/
	}

	public void LaunchPositionCoroutine(Vector3 newPosition)
	{
		// assign the new target position.
		TargetPosition = newPosition;

		// start the movement coroutine to always move the objects to the new target position.
		StartCoroutine(UpdatePosition());
	}


	public void ExecuteAdditionalMove()
	{
		// get the puzzle display and return the new target location from this tile. 
		LaunchPositionCoroutine(this.transform.parent.GetComponent<ST_PuzzleDisplay>().GetTargetLocation(this.GetComponent<ST_PuzzleTile>()));

	}

	void OnMouseDown()
	{
        if (ST_PuzzleDisplay.suffle)
        {
			LaunchPositionCoroutine(this.transform.parent.GetComponent<ST_PuzzleDisplay>().GetTargetLocation(this.GetComponent<ST_PuzzleTile>()));
			move = true;
			timer = 0;
		}
		// get the puzzle display and return the new target location from this tile. 
		
	}

	private void Update()
	{

		if (ArrayLocation == GridLocation) { CorrectLocation = true; } else { CorrectLocation = false; }

		if (move)
		{
			timer += Time.deltaTime;

		}

		if (timer > 2)
		{
			move = false;
			timer = 0;
		}

		if (CorrectLocation && move && timer > 1f)
		{

			if (!CorrectLocationAnswer)
			{
				cantidadPregunta = cantidadPregunta + 1;
				if (!UIManager.isplaying)
				{
					UIManager.firstCheckCorrect = true;
					CanvasA.SetActive(false);
					//CanvasP.SetActive(true);
					CorrectLocationAnswer = true;
					UIManager.isplaying = true;
					Debug.Log(cantidadPregunta + " Preguntas lanzadas");
					ST_PuzzleDisplay.inQuestion = true;
					ST_PuzzleDisplay.flagIsComplete = false;
				}
				else if (UIManager.isplaying && !CorrectLocationAnswer)
				{
					CorrectLocationAnswer = true;
					GameObject.FindGameObjectWithTag("Finish").GetComponent<UIManager>().Continuar();
					Debug.Log(cantidadPregunta + " Preguntas lanzadas");
					ST_PuzzleDisplay.inQuestion = true;
					ST_PuzzleDisplay.flagIsComplete = false;
				}
			}
		}

	}
	public IEnumerator UpdatePosition()
	{

		// whilst we are not at our target position.
		while (TargetPosition != this.transform.localPosition)
		{
			// lerp towards our target.
			this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, TargetPosition, 10f * Time.deltaTime);
			yield return null;
		}

		// after each move check if we are now in the correct location.


		// if we are not an active tile then hide our renderer and collider.
		if (Active == false)
		{
			this.GetComponent<Renderer>().enabled = false;
			this.GetComponent<Collider>().enabled = false;
		}

		yield return null;
	}


}