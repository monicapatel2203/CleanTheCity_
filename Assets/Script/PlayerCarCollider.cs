using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarCollider : MonoBehaviour
{
    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    void OnTriggerEnter(Collider coll)
    {
        GameController _gcScript = FindObjectOfType<GameController>();
        PlayerCarMovement _movementScript = FindObjectOfType<PlayerCarMovement>();
        EnemyCarScript enemyCarScript = FindObjectOfType<EnemyCarScript>();
        BrokenPartGenScript _brokenGenScript = FindObjectOfType<BrokenPartGenScript>();
        if( coll.gameObject.tag == "EntranceCollider" )
        {
            // Debug.Log("Enter...");
        }
        else if( coll.gameObject.tag == "ExitCollider" )
        {
            if( _brokenGenScript.count == 0 )
            {
                // _gcScript.PlayerCar_Right.GetComponent<FrontbackMovement>().enabled = true;
                // _gcScript.PlayerCar_Right.GetComponent<FrontbackMovement>().min = -10.0f;
                // _gcScript.PlayerCar_Right.GetComponent<FrontbackMovement>().max = 2.0f;
                Invoke("RecallValues_Right1", 0.6f);
            }
            else if(_brokenGenScript.count == 1)
            {
                // _gcScript.PlayerCar_Left.GetComponent<FrontbackMovement>().enabled = true;
                Invoke("RecallValues_Left", 1.0f);
            }
            else if( _brokenGenScript.count == 2 )
            {
                Invoke("RecallValues_Right2", 0.6f);
            }
        }
    }

    void RecallValues_Right1()
    {
        GameController _gcScript = FindObjectOfType<GameController>();
        _gcScript.PlayerCar_Right.GetComponent<FrontbackMovement>().min = -10.0f;
        _gcScript.PlayerCar_Right.GetComponent<FrontbackMovement>().max = 2.0f;
        _gcScript.PlayerCar_Right.SetActive(true);
        // _gcScript.PlayerCar_Right.GetComponent<FrontbackMovement>().speed = 3.0f;
    }
    void RecallValues_Right2()
    {
        GameController _gcScript = FindObjectOfType<GameController>();
        _gcScript.PlayerCar_Right.GetComponent<FrontbackMovement>().min = -10.0f;
        _gcScript.PlayerCar_Right.GetComponent<FrontbackMovement>().max = 2.0f;
        _gcScript.PlayerCar_Right.SetActive(true);
        // _gcScript.PlayerCar_Right.GetComponent<FrontbackMovement>().speed = 3.0f;
    }

    void RecallValues_Left()
    {
        GameController _gcScript = FindObjectOfType<GameController>();
        _gcScript.PlayerCar_Left.GetComponent<FrontbackMovement>().min = -10.0f;
        _gcScript.PlayerCar_Left.GetComponent<FrontbackMovement>().max = 1.0f;
        if( PlayerPrefs.GetInt("CurrentLevel") > 4 )
        {
            _gcScript.PlayerCar_Left.SetActive(true);
            _gcScript._playerLeft_enable = false;
        }
        else
        {
            _gcScript.PlayerCar_Left.SetActive(false);
        }
    }
}
