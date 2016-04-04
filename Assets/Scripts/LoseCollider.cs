using UnityEngine;
using System.Collections;

public class LoseCollider : MonoBehaviour {

	public LevelManager levelManager;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.CompareTag ("Player")) {
			//levelManager.LoadLevel ("Start Menu");	
		}
	}
}
