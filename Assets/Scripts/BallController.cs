using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {

	public GameObject GameManager;

	void Awake () {
	}

	void Start () {
		gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (Random.Range(0f,10f), Random.Range(5f, 10f));
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.CompareTag ("Paddle")) {
			this.gameObject.GetComponent<AudioSource> ().Play ();
			iTween.ShakePosition(GameManager, new Vector3(0.3f, 0.3f, 0f), 0.3f);
		}
	}
}
