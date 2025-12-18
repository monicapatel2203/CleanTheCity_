using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckObjScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("TruckObj", 4.0f, 4.0f);
    }

    void TruckObj()
    {
        MenuController _menuScript = FindObjectOfType<MenuController>();
        if( _menuScript._truckObj == true )
        {
            int i = Random.Range( 0, this.transform.childCount );
            // Debug.Log(i);
            this.transform.GetChild(i).GetComponent<MeshRenderer>().enabled = false;
        }
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
}
