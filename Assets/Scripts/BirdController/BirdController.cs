using System.Collections;
using UnityEngine;

public class BirdController : MonoBehaviour {
    public static BirdController instance;
    public float flag = 0; // gắn cờ
    public float bounceForce = 3;
    public int _score = 0;

    private Rigidbody2D myBody;
    private Animator anim;
    private GameObject spawner;

    [SerializeField]
    private AudioSource aus;
    [SerializeField]
    private AudioClip flyClip, pingClip, deadClip;

    private bool isAlive;
    private bool didFlap; // cho phép click khi còn sống
	// Use this for initialization
	void Awake() {
        isAlive = true;
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spawner = GameObject.Find("SpawnerPipe");
        _MakeInstance();
	}
	
	// Update is called once per frame
	void Update () {
        _BirdMoveMent();
       
	}
    void _BirdMoveMent() {
        if (Input.GetMouseButtonDown(0)) {
            if (isAlive)
            {
                    myBody.velocity = new Vector2(myBody.velocity.x, bounceForce);
                    aus.PlayOneShot(flyClip);
            }
        }
        if (myBody.velocity.y > 0) {
            float angle = 0;
            angle = Mathf.Lerp(0, 90, myBody.velocity.y/8 );
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else if (myBody.velocity.y < 0)
        {
            float angle = 0;
            angle = Mathf.Lerp(0, -90, -myBody.velocity.y/8 );
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    void _MakeInstance()
    {
        if (instance == null)
            instance = this;
    }

    // xử lý va chạm của chim với pipe và ground
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pipe" || collision.gameObject.tag == "Ground") {
            if (isAlive)
            {
                isAlive = false;
                flag = 1;
                Destroy(spawner);
                aus.PlayOneShot(deadClip);
                anim.Play("Died");
            }
            if(GamePlayController.instance != null)
            {
                GamePlayController.instance._setPanel(_score);
            }
        }
    }
    // xử lý khi qua pipe
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PipeHolder") {
            _score++;
            if(GamePlayController.instance != null)
             GamePlayController.instance._setScore(_score);
            aus.PlayOneShot(pingClip);
        }
    }
}
