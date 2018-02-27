using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeHolder : MonoBehaviour {
    public float speed;
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (BirdController.instance != null) {
            if (BirdController.instance.flag == 1) {
                Destroy(GetComponent<PipeHolder>());
            }
        }
        _PipeMovement();
    }
    void _PipeMovement() {
        Vector3 temp = transform.position; // Take current place
        temp.x -= speed * Time.deltaTime; // giảm x theo hàm delta time.
        transform.position = temp;
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Destroy")
        {
            Destroy (gameObject);
        }
    }
}
