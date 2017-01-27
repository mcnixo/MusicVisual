using UnityEngine;
using System.Collections;

public class songControl : MonoBehaviour {
    
    public float theTime = 0;
    AudioSource audio;
	// Use this for initialization
	void Start () {
        audio = gameObject.GetComponent<AudioSource>();
        audio.time = 30;
       
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
