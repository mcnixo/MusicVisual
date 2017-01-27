using UnityEngine;
using System.Collections;

public class timeDial : MonoBehaviour {
    public float time = 0;
    public float movement = 0;
    public float startPos = 0;
    public AudioSource audio;
    public GameObject other;
    public bool stop = false, letgo = false;
    private float dist;
    private bool dragging = false, mousedrag = false;
    private Vector3 offset;
    private Transform toDrag;
    public float temphold = 0;
    Vector3 screenPoint;

    SpriteRenderer render;

    public GameObject ylwPic;
    public SpriteRenderer backdrop;
    // Use this for initialization
    void Start() {
        audio = other.GetComponent<AudioSource>();
        render = gameObject.GetComponent<SpriteRenderer>();
        backdrop = ylwPic.GetComponent<SpriteRenderer>();
        audio.time = 0;
        

    }

    // Update is called once per frame
    void Update() {
        
        render.material.color = new Color(0, 0 + (audio.time/audio.clip.length),1 -(audio.time / audio.clip.length));
        time = audio.time;
        PlayerPrefs.SetFloat("Time", time);
        temphold = audio.time;
        if (letgo)
        {
            audio.time = (gameObject.transform.position.x + 7.5f) / 15 * audio.clip.length;
            letgo = false;
        }
        if (mousedrag == false)
        {
            
            startPos = (audio.time / audio.clip.length) * 15;
            gameObject.transform.position = new Vector3(-7.5f + startPos, gameObject.transform.position.y);
        }
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 5.37f);
        

        Vector3 v3;

        if (Input.touchCount != 1)
        {
            dragging = false;
            return;
        }

        Touch touch = Input.touches[0];
        Vector3 pos = touch.position;

        if (touch.phase == TouchPhase.Began)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(pos);
            if (Physics.Raycast(ray, out hit) && (hit.collider.tag == "Draggable"))
            {
                Debug.Log("Here");
                toDrag = hit.transform;
                dist = hit.transform.position.z - Camera.main.transform.position.z;
                v3 = new Vector3(pos.x, pos.y, dist);
                v3 = Camera.main.ScreenToWorldPoint(v3);
                offset = toDrag.position - v3;
                dragging = true;
            }
        }
        if (dragging && touch.phase == TouchPhase.Moved)
        {
            v3 = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
            v3 = Camera.main.ScreenToWorldPoint(v3);
            toDrag.position = v3 + offset;
        }
        if (dragging && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
        {
            dragging = false;
        }
    }

    void OnMouseDown()
    {
        mousedrag = true;
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

    }

    void OnMouseUp()
    {
        mousedrag = false;
        
    }
    void OnMouseDrag()
    {
        if (stop == false) {
            mousedrag = true;
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
            letgo = true;
        }
        if (gameObject.transform.position.x < -7.45f || gameObject.transform.position.x > 7.45f)
            stop = true;
        if (stop)
        {
            gameObject.transform.position = new Vector3(-7.5f, 5.37f, 0);
            
            stop = false;
        }
            

    }

}


