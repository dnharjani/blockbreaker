using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LinesHandler : MonoBehaviour
{
	public Color c1 = Color.white;
	public Color c2 = Color.white;
	public GameObject PaddleCollider;
	public static float maxLineJuice = 20f;
	public static float currentLineJuice = 20f;
	public static float currentLineJuiceInUse = 0f;

	private LineRenderer currentLine; 
	private Vector3 mousePos;    
	private Vector3 startPos;    
	private Vector3 endPos;    
	private List<Vector2> currentLineVectors; 

	void Start()
	{
	}

	void Update ()
	{
		if (!currentLine && currentLineJuice < maxLineJuice) {
			currentLineJuice += 0.3f;
			currentLineJuice = Mathf.Min (currentLineJuice, maxLineJuice);
		}

		if(Input.GetMouseButtonDown(0))
		{
			if (currentLine == null) {
				currentLine = createLine();
			}

			mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mousePos.z = 0.5f;
			currentLineVectors = new List<Vector2> ();
			currentLineVectors.Add (new Vector2(mousePos.x, mousePos.y));

			currentLine.SetPosition(0,mousePos);
			currentLine.SetWidth (0.1f, 0.1f);
			startPos = mousePos;

			GameManager.slowDownTime ();
		}
		else if(Input.GetMouseButtonUp(0))
		{
			if(currentLine)
			{
				mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				mousePos.z = 0;

				Vector3 copy = new Vector3 (startPos.x, startPos.y, startPos.z);
				Vector3 maxVector = Vector3.MoveTowards (copy, mousePos, currentLineJuice);

				currentLineVectors.Add (new Vector2(maxVector.x, maxVector.y));

				currentLine.SetVertexCount(currentLineVectors.Count);
				currentLine.SetPosition(currentLineVectors.Count - 1,maxVector);
				addColliderToLine(currentLine, currentLineVectors);
				currentLineJuice -= Vector3.Distance (startPos, maxVector);

				currentLine = null;
			}

			currentLineJuiceInUse = 0f;
			GameManager.speedUpTime ();
		}
		else if(Input.GetMouseButton(0))
		{
			if(currentLine)
			{
				mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				mousePos.z = 0;

				Vector3 copy = new Vector3 (startPos.x, startPos.y, startPos.z);
				Vector3 maxVector = Vector3.MoveTowards (copy, mousePos, currentLineJuice);

				if (Vector3.Distance (copy, maxVector) > 0.1f) {
					currentLineJuice -= Vector3.Distance (startPos, maxVector);
					currentLineVectors.Add (new Vector2(maxVector.x, maxVector.y));

					if (currentLineJuice < maxLineJuice) {
						currentLine.SetVertexCount(currentLineVectors.Count);
						currentLine.SetPosition(currentLineVectors.Count - 1, maxVector);

						startPos = maxVector;
					}

				}
			}


		}
	}

	private LineRenderer createLine()
	{
		GameObject Line = new GameObject ("Line");
		LineRenderer newLine = Line.AddComponent<LineRenderer>();

		newLine.material = new Material(Shader.Find("Particles/Additive"));
		newLine.SetVertexCount(1);
		newLine.SetColors(Color.white, Color.white);
		newLine.useWorldSpace = true;

		return newLine;

	}

	private void addColliderToLine(LineRenderer thisLine, List<Vector2> lineVectors)
	{
		GameObject collider = (GameObject) Instantiate (PaddleCollider);

		EdgeCollider2D col = collider.AddComponent<EdgeCollider2D> ();
		col.transform.parent = thisLine.transform; 
		col.points = lineVectors.ToArray();

		//col.size = new Vector3 (lineLength, 0.2f, 1f); 
		//Vector3 midPoint = (startPos + endPos)/2;
		//col.transform.position = midPoint; 
		//float angle = (Mathf.Abs (startPos.y - endPos.y) / Mathf.Abs (startPos.x - endPos.x));
		//if((startPos.y<endPos.y && startPos.x>endPos.x) || (endPos.y<startPos.y && endPos.x>startPos.x))
		//{
		//	angle*=-1;
		//}
		//angle = Mathf.Rad2Deg * Mathf.Atan (angle);
		//col.transform.Rotate (0, 0, angle);
	}
}