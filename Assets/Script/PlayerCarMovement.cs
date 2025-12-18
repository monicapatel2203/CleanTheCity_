using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarMovement : MonoBehaviour
{
    public float min=2f;
    public float max=3f;

    // Start is called before the first frame update
    void Start()
    {
        // min = transform.position.x;
        // max = transform.position.x+3;
    }

    // Update is called once per frame
    void Update()
    {
        // transform.localPosition = new Vector3(Mathf.PingPong(Time.time * 5,max-min)+min, transform.localPosition.y, transform.localPosition.z);
    }
}
