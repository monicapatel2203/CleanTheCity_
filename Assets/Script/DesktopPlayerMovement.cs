using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktopPlayerMovement : MonoBehaviour
{
    public float movementSpeed, minSwipeLength;
	Vector2 firstPressPos, secondPressPos, currentSwipe;
    public bool MoveStart, move;	
	public Vector3 SetPos;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        move = false;
        MoveStart = false;
		movementSpeed = 100;                 //500
		minSwipeLength = 2f;              //0.25f
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.GetChild(0).localPosition = new Vector3(0, 0, 0);
        if (Input.GetMouseButtonDown(0))
        {
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }

        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if( Physics.Raycast( ray,out hit) )// && move == true )
            {
                if( hit.collider.tag == "CurvyRoad" )
                {
                    this.transform.position = new Vector3( hit.point.x, 1, this.transform.position.z );
                }
            }
            
            
            // secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            // currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);


            // // Debug.Log ("Position__ "+this.transform.position);
            // if (currentSwipe.magnitude < minSwipeLength) 
            // {
                
            //     return;
            // }

            currentSwipe.Normalize();
            // Debug.Log("Pos..." + this.transform.localPosition.z + "   " + Camera.main.transform.right);
            // Debug.Log("X...." + currentSwipe.x + "   Y...." + currentSwipe.y);
            // if(currentSwipe.y < 0)
            // {
            //     this.transform.position -= Camera.main.transform.forward * Time.deltaTime * (movementSpeed /2f);
            //     this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y,this.transform.position.z);
            //     Debug.Log("Swipe down...");
            // }
            // if(currentSwipe.y > 0)
            // {
            //     this.transform.position += Camera.main.transform.forward * Time.deltaTime * (movementSpeed /2f);
            //     this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y,this.transform.position.z);
            //     Debug.LogError("Swipe Up..." + this.transform.position);
            // }
            // if (currentSwipe.x < 0 )                //&& currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            // {
            //     if( this.transform.localPosition.x > -30f )                                //2.6
            //     {
            //         this.transform.position -= Camera.main.transform.right * Time.deltaTime * (movementSpeed);             //  /2f
            //         this.transform.position = new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z);
            //         // this.transform.localPosition = new Vector3(this.transform.localPosition.x, 0, 0);
            //     }
            // }
            // if (currentSwipe.x > 0)                //&& currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) 
            // {
            //     if( this.transform.localPosition.x < 30f )
            //     {
            //         this.transform.position += Camera.main.transform.right * Time.deltaTime * (movementSpeed);              //  /2f
            //         this.transform.position = new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z);
            //         // this.transform.localPosition = new Vector3(this.transform.localPosition.x, 0, 0);
            //     }
            // }

				firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
    }

    
}
