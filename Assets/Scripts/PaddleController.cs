﻿using UnityEngine;
using System.Collections;

public class PaddleController : MonoBehaviour {



	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
	}

	void OnCollisionEnter2D(Collision2D collision) {
		StartCoroutine (DestroyParent ());
	}

	IEnumerator DestroyParent() {
		yield return new WaitForSeconds (0.1f);
		Destroy (gameObject.transform.parent.gameObject);
	}

}
