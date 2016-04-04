using UnityEngine;
using System.Collections;

public class JuiceBarController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (LinesHandler.currentLineJuiceInUse > 0) {
			transform.localScale = new Vector3(1 - LinesHandler.currentLineJuiceInUse / LinesHandler.maxLineJuice, transform.localScale.y, transform.localScale.z);
		} else {
			transform.localScale = new Vector3(LinesHandler.currentLineJuice / LinesHandler.maxLineJuice, transform.localScale.y, transform.localScale.z);
		}


	}
}
