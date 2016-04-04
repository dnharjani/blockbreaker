using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {

	static AudioController instance = null;

	// Use this for initialization
	void Awake () {
		if (instance != null) {
			Destroy (gameObject);
		} 
		else {
			instance = this;
			GameObject.DontDestroyOnLoad (gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
