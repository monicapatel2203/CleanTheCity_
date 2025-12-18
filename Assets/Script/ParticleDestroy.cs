using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyObstacle", 3.0f);
    }

    void DestroyObstacle()
    {
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
}
