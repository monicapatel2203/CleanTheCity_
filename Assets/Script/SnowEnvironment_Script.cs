using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowEnvironment_Script : MonoBehaviour
{
    public List<GameObject> CactusObj, SnowManObj, SimpleTreeObj, CurveTreeObj, StonePillarObj;
    // Start is called before the first frame update
    void Start()
    {
        for(int  i = 0; i < CactusObj.Count / 2; i++)
        {
            int _cactusCount = Random.Range(0, CactusObj.Count);
            CactusObj[_cactusCount].SetActive(true);
        }

        for(int  i = 0; i < SnowManObj.Count / 2; i++)
        {
            int _SnowmanCount = Random.Range(0, SnowManObj.Count);
            SnowManObj[_SnowmanCount].SetActive(true);
        }

        for(int  i = 0; i < SimpleTreeObj.Count; i++)
        {
            int _simpleTreeCount = Random.Range(0, SimpleTreeObj.Count);
            SimpleTreeObj[_simpleTreeCount].SetActive(true);
        }

        for(int  i = 0; i < CurveTreeObj.Count / 2; i++)
        {
            int _curvetreeCount = Random.Range(0, CurveTreeObj.Count);
            CurveTreeObj[_curvetreeCount].SetActive(true);
        }

        for(int  i = 0; i < StonePillarObj.Count / 2; i++)
        {
            int _pillarCount = Random.Range(0, StonePillarObj.Count);
            StonePillarObj[_pillarCount].SetActive(true);
        }
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
}
