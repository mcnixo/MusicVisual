using UnityEngine;
using System.Collections;

public class Surprise : MonoBehaviour {
    public float startposx, startposy, startposz;
    public float endposx, endposy, endposz;
    public bool reached = false, restart = false;
    public float moveSpeed = 0.1f;
    public float count = 0;
    public float startCount = 0;
    public Circle control;
    public Rigidbody rig;
    public bool checkBeat = false;
    public float boundary = -3;
    
	// Use this for initialization
	void Start () {
        startposx = gameObject.transform.position.x;
        startposy = gameObject.transform.position.y;
        startposz = gameObject.transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
        startCount+= Time.deltaTime;
        if(gameObject.transform.position.y < boundary)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, boundary, gameObject.transform.position.z);
        }
        if (control.beatHit && gameObject.transform.position.y == boundary)
        {
            checkBeat = true;
            rig.velocity += 9 * Vector3.up;
        }
        if (!control.beatHit)
        {
            checkBeat = false;
        }
        if (!reached && startCount > 30)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + moveSpeed, gameObject.transform.position.y, gameObject.transform.position.z);
            if(gameObject.transform.position.x > 20)
            {
                reached = true;
            }
        }
        if(reached && restart == false)
        {
            count += Time.deltaTime;
            if(count > 25)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x - moveSpeed, gameObject.transform.position.y, gameObject.transform.position.z);
                if(gameObject.transform.position.x < -20)
                {
                    
                    restart = true;
                    count = 0;
                }

            }
        }
        if(restart)
        {
            count += Time.deltaTime;
            if(count > 15)
            {
                restart = false;
                reached = false;
                count = 0;
            }
        }

    }
}
