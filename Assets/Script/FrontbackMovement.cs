using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontbackMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float min=2f;
    public float max=3f;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        // min = transform.position.x;
        // max = transform.position.x+3;
    }

    // Update is called once per frame
    void Update()
    {
        // transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, Mathf.PingPong(Time.time * speed,max-min)+min);
    }
}
