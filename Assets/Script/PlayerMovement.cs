using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed, minSwipeLength;
	Vector2 firstPressPos, secondPressPos, currentSwipe;
    public bool MoveStart;
	public Vector3 SetPos;
    Vector3 touchPosWorld;
	RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        MoveStart = false;
		movementSpeed = 90;				//70
		minSwipeLength = 2f;			//0.25
    }

    // Update is called once per frame
    void Update()
    {
        DetectSwipe();
    }

    public void DetectSwipe ()
	{
		this.transform.GetChild(0).localPosition = new Vector3(0, 0.3f, 0);
		// if (Input.touches.Length > 0)
		// {
			// Touch t = Input.GetTouch(0);


			// if (t.phase == TouchPhase.Began) 
			// {
			// 	firstPressPos = new Vector2(t.position.x, t.position.y);
			// }

			// if (t.phase == TouchPhase.Moved)
			if(Input.GetMouseButton(0))
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
				if( Physics.Raycast( ray,out hit) )
				{
					if( hit.collider.tag == "CurvyRoad" )
					{
						this.transform.position = Vector3.Lerp( this.transform.position, new Vector3( hit.point.x, 1, this.transform.position.z ), 1.0f );
					}
				}
				// secondPressPos = new Vector2(t.position.x, t.position.y);
				// currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

				// if (currentSwipe.magnitude < minSwipeLength) 
				// {
					
				// 	return;
				// }

				// currentSwipe.Normalize();
				// if (currentSwipe.x < 0 )                //&& currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
				// {
				// 	if( this.transform.localPosition.x > -30f )								//2.6
				// 	{
				// 		this.transform.position -= Camera.main.transform.right * Time.deltaTime * (movementSpeed / 2f);				// / 2f
                //     	this.transform.position = new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z);
				// 	}                    
                //     // Debug.Log("Swipe left..." + this.transform.position + "  currentswipe.x..." + currentSwipe.x + "   currentSwipe.y..." + currentSwipe.y);
				// }
				// if (currentSwipe.x > 0 )                //&& currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) 
				// {
				// 	if( this.transform.localPosition.x < 30f )
				// 	{
				// 		this.transform.position += Camera.main.transform.right * Time.deltaTime * (movementSpeed  / 2f);
                //     	this.transform.position = new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z);
				// 	}                    
                //     // Debug.Log("Swipe right..." + this.transform.position + "  currentswipe.x..." + currentSwipe.x + "  currentSwipe.y..." + currentSwipe.y);
				// }

				// firstPressPos = new Vector2(t.position.x, t.position.y);
			}
		// }
		// else 
		// {

		// }
	}

	void OnCollisionEnter( Collision coll )
	{
		
	}
}
