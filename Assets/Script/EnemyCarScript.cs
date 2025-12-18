using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCarScript : MonoBehaviour
{
    public bool _brokenroad, _patternMove, _finishedlevel;
    // Start is called before the first frame update
    void Start()
    {
        _brokenroad = false;
        _patternMove = false;
        _finishedlevel = false;
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    // void OnCollisionEnter( Collision coll )
    void OnTriggerEnter(Collider coll)
    {
        ObstacleGen _obstacleScript = FindObjectOfType<ObstacleGen>();
        GameController _gcScript = FindObjectOfType<GameController>();
        PlayerCarMovement _playerCarScript = FindObjectOfType<PlayerCarMovement>();
        if( coll.gameObject.tag == "EntranceCollider" )
        {
            Debug.Log("Enter...");
            _brokenroad = true;
            _patternMove = true;
            _obstacleScript.XClamp_max = 0f;        //1.0
            _obstacleScript.XClamp_min = 0f;        //1.0
            // _playerCarScript.max = 1.0f;
        }
        else if( coll.gameObject.tag == "ExitCollider" )
        {
            Debug.Log("Exit...");
            Invoke("BooleanCall", 0.5f);
        }
        else if( coll.gameObject.tag == "EntranceCollider_Straight" )
        {
            _patternMove = true;
        }
    }

    void OnCollisionEnter( Collision coll )
    {
        GameController _gcScript = FindObjectOfType<GameController>();
        ObstacleGen _obstacleScript = FindObjectOfType<ObstacleGen>();
        if( coll.gameObject.tag == "FinishLevel" )
        {
            _finishedlevel = true;
            _gcScript.PlayerCar.transform.GetChild(1).GetComponent<Rigidbody>().isKinematic = false;            
            StartCoroutine(_obstacleScript.StopObstacleGen());
            _gcScript.GameControllerObj.GetComponent<ObstacleGen>().enabled = false;
            Debug.Log("Reached finish line...");
        }
    }

    void BooleanCall()
    {
        GameController _gcScipt = FindObjectOfType<GameController>();
        ObstacleGen _obstacleScript = FindObjectOfType<ObstacleGen>();
        _obstacleScript.patternCount = ( _obstacleScript.patternCount + 1 ) % _gcScipt.levelManager.LevelController[_gcScipt.CurrentLevel].PatternList.Count;
        _obstacleScript.XClamp_max = 0;         //1.0f
        _obstacleScript.XClamp_min = 0;         //1.5f
        _brokenroad = false;
    }
}
