using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    float screenWidth = 0;
    float screenMovementFactor = 0;
    float x = 0, boundaryvalue_Max, boundaryvalue_Min;

    // Start is called before the first frame update
    void Start()
    {
        screenWidth = Screen.width;
        screenMovementFactor = screenWidth / 6.0f;              //2.
        boundaryvalue_Max = 6.0f;       //8.0
        boundaryvalue_Min = -11.0f;     //-9.0f
    }

    // Update is called once per frame
    void Update()       //FixedUpdate
    {
        if (Input.touchCount > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Moved)
            {
                x = this.transform.position.x + (Input.touches[0].deltaPosition.x * 10f / screenMovementFactor);        //35

                Vector3 newScale = new Vector3();
                // newScale.x = Mathf.Clamp(transform.localPosition.x, -8.0f, 8.0f);
                newScale.x = Mathf.Clamp(transform.localPosition.x, boundaryvalue_Min, boundaryvalue_Max);
                transform.localPosition = newScale;
                this.transform.position = new Vector3(x, this.transform.position.y, this.transform.position.z);
                if (this.transform.localPosition.x <= boundaryvalue_Min)            //-6.0f
                {
                    // this.transform.localPosition = Vector3.one * -8.0f;
                    // this.transform.localPosition = Vector3.left * 6.0f;
                    this.transform.localPosition = Vector3.right * boundaryvalue_Min;
                }
                else if (this.transform.localPosition.x >= boundaryvalue_Max)       //8.0f
                {
                    this.transform.localPosition = Vector3.right * boundaryvalue_Max;
                }                    

                // if( _holeScript._holebool == true )
                // {
                //     boundaryvalue_Max = -0.5f;
                //     boundaryvalue_Min = -1.0f;
                // }
                // else
                // {
                //     boundaryvalue_Max = 6.0f;       //8.0
                //     boundaryvalue_Min = -8.0f;
                // }
                // if( this.transform.localPosition.x > boundaryvalue_Max )            //6.0f
                // {
                //     this.transform.localPosition = new Vector3(boundaryvalue_Max, this.transform.localPosition.y, this.transform.localPosition.z);
                // }
                // else if( this.transform.localPosition.x < boundaryvalue_Min )       //-6.0f
                // {
                //     this.transform.localPosition = new Vector3(boundaryvalue_Min, this.transform.localPosition.y, this.transform.localPosition.z);
                // }
                // else
                // {
                //     // x = this.transform.position.x + (Input.touches[0].deltaPosition.x * 10f / screenMovementFactor);        //35
                //     this.transform.position = new Vector3(x, this.transform.position.y, this.transform.position.z);
                // }

                // this.transform.position = new Vector3(x, this.transform.position.y, this.transform.position.z);
            }
        }
    }
}
