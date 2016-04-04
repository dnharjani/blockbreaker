using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Camera camera;
	public static GameManager instance = null;

	void Awake () {
		if (instance != null) {
			Destroy (gameObject);
		} 
		else {
			instance = this;
			GameObject.DontDestroyOnLoad (gameObject);
		}
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}

	public static void slowDownTime() {
		Time.timeScale = 0.5F;
		Time.fixedDeltaTime = 0.02F * Time.timeScale;
	}

	public static void speedUpTime() {
		Time.timeScale = 1F;
		Time.fixedDeltaTime = 0.02F * Time.timeScale;
	}
}
