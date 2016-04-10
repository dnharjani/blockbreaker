using UnityEngine;
using System.Collections;

public class PaddleController : MonoBehaviour {



	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
	}

	void OnCollisionEnter2D(Collision2D collision) {
		gameObject.transform.parent.gameObject.GetComponent<LinesHandler> ().handeLineColission (collision, gameObject.GetComponent<EdgeCollider2D> ().points);

		Destroy (gameObject);
	}

}
