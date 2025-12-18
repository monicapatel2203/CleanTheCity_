using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelContent
{
    public string _levelname;
    public List<GameObject> PatternList, ObstaclePrefab;
    public GameObject Environment, EnemyCar;
    public List<GameObject> ObstacleStart_Pos;
    public List<int> PatternMoveCount;
}

public class LevelManager : MonoBehaviour
{
    public List<LevelContent> LevelController;
    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
