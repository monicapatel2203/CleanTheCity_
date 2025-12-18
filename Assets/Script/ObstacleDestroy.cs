using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDestroy : MonoBehaviour
{
    public bool _move;
    public GameObject _obstacleParticle;
    // Start is called before the first frame update
    void Start()
    {
        _move = false;
       Invoke("DestroyObstacle", 6.0f);
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

    void DestroyObstacle()
    {
        Destroy(this.gameObject);
    }

    void OnCollisionEnter( Collision coll )
    {
        GameController _gcontroler = FindObjectOfType<GameController>();
        ObstacleGen _obstacleGen = FindObjectOfType<ObstacleGen>();
        if( coll.gameObject.tag == "CurvyRoad" )
        {
            _move = true;
            // if( _obstacleGen.patternCount == 8 )
            if( _obstacleGen.patternObj.Count > 0 )
            {
                if( _obstacleGen.patternObj[0] != null )
                {
                    if( _obstacleGen.patternObj[0].gameObject.tag == "BurstObstacle" )
                    {
                        this.gameObject.SetActive(false);
                        GameObject go = Instantiate( _obstacleGen.ObstcaleBurst, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity );
                        for( int i = 0; i < go.transform.childCount; i++ )
                        {
                            go.transform.GetChild(i).GetComponent<MeshRenderer>().material = _obstacleGen.material;
                        }
                    }
                }
            }
        }
    }

}
