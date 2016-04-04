﻿using UnityEngine;
using System.Collections;

public class LinesHandler : MonoBehaviour
{
	public Color c1 = Color.white;
	public Color c2 = Color.white;
	public GameObject PaddleCollider;
	public static float maxLineJuice = 5f;
	public static float currentLineJuice = 5f;
	public static float currentLineJuiceInUse = 0f;

	private LineRenderer currentLine; 
	private LineRenderer lastLine;
	private Vector3 mousePos;    
	private Vector3 startPos;    
	private Vector3 endPos;    

	void Start()
	{
	}

	void Update ()
	{
		if (currentLineJuice < maxLineJuice) {
			currentLineJuice += 0.1f;
			currentLineJuice = Mathf.Min (currentLineJuice, maxLineJuice);
		}

		if(Input.GetMouseButtonDown(0))
		{
			if (lastLine != null) {
				
			}

			if (currentLine == null) {
				currentLine = createLine();
			}

			mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mousePos.z = 0.5f;
			currentLine.SetPosition(0,mousePos);
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

				if (Vector3.Distance (startPos, maxVector) > 1) {
					currentLine.SetPosition(1,maxVector);
					endPos = maxVector;
					addColliderToLine(currentLine);
					lastLine = currentLine;

					currentLineJuice -= Vector3.Distance (startPos, maxVector);

					currentLine = null;

				} else {
					Destroy (currentLine.gameObject);
					currentLine = null;
				}
			}

			currentLineJuiceInUse = 0f;
			GameManager.speedUpTime ();
		}
		else if(Input.GetMouseButton(0))
		{
			if(currentLine)
			{
				currentLine.SetVertexCount(2);
				mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				mousePos.z = 0;

				Vector3 copy = new Vector3 (startPos.x, startPos.y, startPos.z);
				Vector3 maxVector = Vector3.MoveTowards (copy, mousePos, currentLineJuice);

				currentLineJuiceInUse = Vector3.Distance (startPos, maxVector);

				currentLine.SetPosition(1,maxVector);
			}


		}
	}

	private LineRenderer createLine()
	{
		GameObject Line = new GameObject ("Line");
		LineRenderer newLine = Line.AddComponent<LineRenderer>();

		//newLine.material = new Material(Shader.Find("Particles/Additive"));
		newLine.SetVertexCount(1);
		newLine.SetWidth(0.2f,0.2f);
		newLine.SetColors(Color.white, Color.white);
		newLine.useWorldSpace = true;

		return newLine;

	}

	private void addColliderToLine(LineRenderer thisLine)
	{
		GameObject collider = (GameObject) Instantiate (PaddleCollider);

		BoxCollider2D col = collider.AddComponent<BoxCollider2D> ();
		col.transform.parent = thisLine.transform; 
		float lineLength = Vector3.Distance (startPos, endPos); 
		col.size = new Vector3 (lineLength, 0.2f, 1f); 
		Vector3 midPoint = (startPos + endPos)/2;
		col.transform.position = midPoint; 
		float angle = (Mathf.Abs (startPos.y - endPos.y) / Mathf.Abs (startPos.x - endPos.x));
		if((startPos.y<endPos.y && startPos.x>endPos.x) || (endPos.y<startPos.y && endPos.x>startPos.x))
		{
			angle*=-1;
		}
		angle = Mathf.Rad2Deg * Mathf.Atan (angle);
		col.transform.Rotate (0, 0, angle);
	}
}