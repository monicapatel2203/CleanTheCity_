using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenPartGenScript : MonoBehaviour
{
    public List<GameObject> BrokenRoad, StraightRoad, BrokenRoad_GO, StraightRoad_GO;
    public int count;
    // Start is called before the first frame update
    void Start()
    {
        if( PlayerPrefs.GetInt("CurrentLevel") > 4 )
        {
            count = Random.Range(0, BrokenRoad.Count);
            // count = 2;
            for(int i = 0; i < BrokenRoad.Count; i++)
            {
                if( i == count )
                {
                    BrokenRoad[i].SetActive(true);
                }
                else
                {
                    BrokenRoad[i].SetActive(false);
                }
            }
            for(int i = 0; i < StraightRoad.Count; i++)
            {
                if( i == count )
                {
                    StraightRoad[i].SetActive(false);
                }
                else
                {
                    StraightRoad[i].SetActive(true);
                }
            }
        }
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
}
