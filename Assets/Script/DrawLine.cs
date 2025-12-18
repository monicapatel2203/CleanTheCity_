using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private float counter, dist;
    public Transform origin,destination;
    public float lineDrawSpeed = 6f;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, origin.position);
        lineRenderer.SetWidth(0.7f, 0.7f);

        dist = Vector3.Distance(origin.position, destination.position);

        // float x = Mathf.Lerp(0, dist, counter);

        // Vector3 pointA = origin.position;
        // Vector3 pointB = destination.position;

        // Vector3 pointAlongline = x * Vector3.Normalize(pointB - pointA) + pointA;

        // lineRenderer.SetPosition(1, destination.position);
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, origin.position);
        lineRenderer.SetPosition(1, destination.position);
        // if(counter < dist)
        // {
        //     counter += 0.1f / lineDrawSpeed;
        //     // float x = Mathf.Lerp(0, dist, counter);

        //     // Vector3 pointA = origin.localPosition;
        //     // Vector3 pointB = destination.localPosition;

        //     // Vector3 pointAlongline = x * Vector3.Normalize(pointB - pointA) + pointA;

        //     lineRenderer.SetPosition(1, destination.localPosition);
        // }
    }
}
