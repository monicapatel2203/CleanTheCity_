using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRotation : MonoBehaviour
{
    public GameObject Controller;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // transform.localEulerAngles = Controller.transform.localEulerAngles;
        transform.rotation = Quaternion.Euler(Controller.transform.localRotation.eulerAngles.x, Controller.transform.localRotation.eulerAngles.y, Controller.transform.localRotation.eulerAngles.z);
    }
}
