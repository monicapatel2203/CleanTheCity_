using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyobstaclePos : MonoBehaviour
{
    public List<GameObject> ObstacleStart_Pos;
    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    void Update()
    {
        LevelUpScript _lvlScript = FindObjectOfType<LevelUpScript>();
        if( _lvlScript._lvlComplete == true )
        {
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp( 8.0f, -6.6f, 2.0f ), transform.localPosition.z);       
        }
    }
}
