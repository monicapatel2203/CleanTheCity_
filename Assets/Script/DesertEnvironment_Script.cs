using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesertEnvironment_Script : MonoBehaviour
{
    public List<GameObject> CactusObj, BigStoneObj, BigTree, StonePillarObj;
    // Start is called before the first frame update
    void Start()
    {
        for(int  i = 0; i < CactusObj.Count / 2; i++)
        {
            int _cactusCount = Random.Range(0, CactusObj.Count);
            CactusObj[_cactusCount].SetActive(true);
        }

        for(int j = 0; j < BigStoneObj.Count / 2; j++)
        {
            int _stoneCount = Random.Range(0, BigStoneObj.Count);
            BigStoneObj[_stoneCount].SetActive(true);
        }

        for(int j = 0; j < BigTree.Count / 2; j++)
        {
            int _treeCount = Random.Range(0, BigTree.Count);
            BigTree[_treeCount].SetActive(true);
        }

        for(int j = 0; j < StonePillarObj.Count / 2; j++)
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
