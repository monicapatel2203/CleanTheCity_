using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObstacleGen : MonoBehaviour
{
    public List<GameObject> ObstaclePrefab, Obstacle_StartPos, patternObj, Patterns, PatternList;
    public List<GameObject> ObstaclePos_secondPos, patternObj_secondPos, Pattern_secondPos;
    public GameObject Obstacle_Obj, ObstacleObj_secondPos, ObstcaleBurst, PatternMoveScript;
    public int count, patternCount, patternCount_secondPos, Count_PatternGeneration, PatternChild_Count;
    public bool PatternGenerate_bool, patternObj_bool, PatternGeneratebool_secondPos, patternObjbool_secondPos;
    public Material material;
    public PhysicMaterial physicMaterial;
    public List<float> Z_Pos, X_Pos;
    public Vector3 PatternPos;
    public AudioSource audioSource;
    public List<AnimationClip> clip;
    public float XClamp_max, XClamp_min, _patternDelay;
    int c = 0, patternMove = 0;

    // Start is called before the first frame update
    void Start()
    {
        _patternDelay = 0.3f;
        PatternGenerate_bool = false;
        patternObj_bool = true;
        PatternGeneratebool_secondPos = false;
        patternObjbool_secondPos = true;
        count = 4;
        Count_PatternGeneration = 0;
        XClamp_min = 0.5f;          //1.5
        XClamp_max = 0.5f;          //1.0
        Invoke("ObstacleInstantiate", 0.5f);                                 //1.0f, 0.1f
    }

    // Update is called once per frame
    void Update()
    {
        GameController _gcScipt = FindObjectOfType<GameController>();
        EnemyCarScript enemyCarScript = FindObjectOfType<EnemyCarScript>();
        if( enemyCarScript.GetComponent<EnemyCarScript>()._brokenroad == true )
        {
            patternCount = 0;
        }
        /*****  generation from first truck  *****/
        if( Count_PatternGeneration >= 12 )          //12
        {
            patternCount = ( patternCount + 1 ) % _gcScipt.levelManager.LevelController[_gcScipt.CurrentLevel].PatternList.Count;
            // patternCount = ( patternCount + 1 ) % PatternList.Count;
            Count_PatternGeneration = 0;
        }


        if( PatternGenerate_bool == true )
        {
            Count_PatternGeneration = Count_PatternGeneration + 1;
            patternMove = patternMove + 1;            
            _gcScipt.levelManager.LevelController[_gcScipt.CurrentLevel].PatternList[patternCount].tag = "Untagged";
            patternObj = new List<GameObject>( new GameObject[] { new GameObject() });
            for( int j = 0; j < _gcScipt.levelManager.LevelController[_gcScipt.CurrentLevel].PatternMoveCount.Count; j++ )
            {
                if( (patternMove % _gcScipt.levelManager.LevelController[_gcScipt.CurrentLevel].PatternMoveCount[j] ) == 0 )
                {
                    // Debug.Log("patternMove...." + patternMove);
                    _gcScipt.levelManager.LevelController[_gcScipt.CurrentLevel].PatternList[patternCount].tag = "PatternMove";
                    break;
                }
            }
            for( int i = 0; i < _gcScipt.levelManager.LevelController[_gcScipt.CurrentLevel].PatternList.Count; i++ )
            // for( int i = 0; i < PatternList.Count; i++ )
            {
                if( patternCount == i )
                {
                    if( _gcScipt.levelManager.LevelController[_gcScipt.CurrentLevel].PatternList[patternCount].tag == "HugePattern" )
                    // if( PatternList[patternCount].tag == "HugePattern" )
                    {
                        XClamp_min = 0;
                        XClamp_max = 0;
                    }
                    else
                    {
                        XClamp_min = 0.5f;         //1.0f
                        XClamp_max = 0.5f;         //1.0f
                    }
                    patternObj[0] = _gcScipt.levelManager.LevelController[_gcScipt.CurrentLevel].PatternList[patternCount];
                    // patternObj[0] = PatternList[patternCount];
                }
            }
            Invoke("ObstacleInstantiate", 0.01f);               //0.01
            PatternGenerate_bool = false;
        }
    }

    // void FixedUpdate()
    // {
    //     // if(samples[audioSource.timeSamples] > 0.08f)            //0.08
    //     // {
    //     //     Debug.Log(samples[audioSource.timeSamples]);
    //     //     // StartCoroutine(PatterEnum());
    //     //     patternObj = new List<GameObject>( new GameObject[] { new GameObject() });
    //     //     for( int i = 0; i < Pattern.Count; i++ )
    //     //     {
    //     //         if( patternCount == i )
    //     //         {
    //     //             patternObj[0] = Pattern[patternCount];
    //     //         }
    //     //     }
    //     //     Invoke("ObstacleInstantiate", 0.01f);               //0.01
    //     // }
    // }


    void ObstacleInstantiate()
    {
        StartCoroutine(ObstacleCoroutine());
    }
    IEnumerator ObstacleCoroutine()                                     //For enemyCar_1
    {
        GameController _gcScipt = FindObjectOfType<GameController>();
        int _generateCount = Random.Range(0, ObstaclePrefab.Count);
        if( patternObj_bool == true )
        {
            patternObj = new List<GameObject>( new GameObject[] { new GameObject() });
            // patternCount = Random.Range(0, Pattern.Count);
            patternCount = 0;
            // patternCount = Random.Range(0, PatternList.Count);
            // patternCount = Random.Range(0, _gcScipt.levelManager.LevelController[_gcScipt.CurrentLevel].PatternList.Count);
            if( _gcScipt.levelManager.LevelController[_gcScipt.CurrentLevel].PatternList[patternCount].tag == "HugePattern" )
            // if( PatternList[patternCount].tag == "HugePattern" )
            {
                XClamp_min = 0;
                XClamp_max = 0;
            }
            else
            {
                XClamp_min = 0.5f;             //1.5f
                XClamp_max = 0.5f;             //1.0f
            }
            for( int i = 0; i < _gcScipt.levelManager.LevelController[_gcScipt.CurrentLevel].PatternList.Count; i++ )
            // for( int i = 0; i < PatternList.Count; i++ )
            {
                if( patternCount == i )
                {
                    patternObj[0] = _gcScipt.levelManager.LevelController[_gcScipt.CurrentLevel].PatternList[patternCount];
                    // patternObj[0] = PatternList[patternCount];
                    PatternPos = patternObj[0].transform.localPosition;
                }
            }
            if( patternObj.Count > 0 )
            {
                if(patternObj != null)
                {
                    for( int  k = 0; k < patternObj[0].transform.childCount; k++ )
                    {
                        Z_Pos.Add(patternObj[0].transform.GetChild(k).position.z);
                        X_Pos.Add(patternObj[0].transform.GetChild(k).position.x );
                    }
                }
            }
            patternObj_bool = false;
        }
        for( int i = 0; i < 1; i++ )
        {
            GameObject EmptyObj;
            EmptyObj = new GameObject("PatternGenerate");
            EmptyObj.AddComponent<Animation>();
            // if( PatternList[patternCount].tag == "PatternMove")
            if( _gcScipt.levelManager.LevelController[_gcScipt.CurrentLevel].PatternList[patternCount].tag == "PatternMove" )
            {
                int ClipCount = Random.Range(0, clip.Count);
                if( ClipCount == 0 )        //LeftAnim play
                {
                    EmptyObj.tag = "PatternAnim_Left";
                    // Debug.LogError("1..."+ EmptyObj.tag + "  " + EmptyObj.name);
                    EmptyObj.GetComponent<Animation>().AddClip(clip[ClipCount], "PatternAnim_Left");
                }
                else
                {
                    EmptyObj.tag = "PatternAnim_Right";
                    // Debug.LogError("2..."+ EmptyObj.tag + "  " + EmptyObj.name);
                    EmptyObj.GetComponent<Animation>().AddClip(clip[ClipCount], "PatternAnim_Right");
                }
                PatternMoveScript.GetComponent<PatternMove>().PatternMoveList.Add(EmptyObj);
            }            
            // int ObstaclePos = Random.Range(0, Obstacle_StartPos.Count);
            // int count = Random.Range(0, Obstacle_StartPos[ObstaclePos].transform.childCount);
            int ObstaclePos = Random.Range(0, _gcScipt.levelManager.LevelController[_gcScipt.CurrentLevel].ObstacleStart_Pos.Count );
            int count = Random.Range(0, _gcScipt.levelManager.LevelController[_gcScipt.CurrentLevel].ObstacleStart_Pos[ObstaclePos].transform.childCount);
            int _prefabCount = Random.Range(0, ObstaclePrefab.Count);
            if( patternObj.Count > 0 )
            {
                if(patternObj[0] != null)
                {
                    for( int  k = 0; k < patternObj[0].transform.childCount; k++ )
                    {
                        Z_Pos.Add(patternObj[0].transform.GetChild(k).position.z);
                        X_Pos.Add(patternObj[0].transform.GetChild(k).position.x );
                        // Debug.Log(X_Pos[k] + "     " + Z_Pos[k]);
                    }
                    if( PatternChild_Count == 0 )
                    {
                        patternObj[0].transform.localPosition = new Vector3( Random.Range( PatternPos.x - XClamp_min, PatternPos.x + XClamp_max ), patternObj[0].transform.localPosition.y, patternObj[0].transform.localPosition.z );
                        PatternChild_Count = patternObj[0].transform.childCount;
                    }
                        for( int j = 0; j < patternObj[0].transform.childCount; j++ )
                        {
                            if( c < _gcScipt.CollectibleList.Count )
                            {
                                if( _gcScipt.CollectibleList.Count > 0 )
                                {
                                    int m = 0;
                                    do
                                    {
                                        if( _gcScipt.CollectibleList[c] != null )
                                        {
                                            try
                                            {
                                                if(m > 15)
                                                {
                                                    throw new System.Exception();
                                                }
                                                if( _gcScipt._EnmyObj.transform.position.z - 30.0f >= _gcScipt.CollectibleList[c + 2].transform.localPosition.z )
                                                {
                                                    c = c + 3;
                                                    for( int  k = 0; k < patternObj[0].transform.childCount; k++ )
                                                    {
                                                        Z_Pos.Add(patternObj[0].transform.GetChild(k).position.z);
                                                    }
                                                    // Debug.Log("Increment c..." + c);
                                                }
                                                else if( _gcScipt._EnmyObj.transform.position.z + 20.0f >= _gcScipt.CollectibleList[c].transform.localPosition.z && _gcScipt._EnmyObj.transform.position.z - 30.0f <= _gcScipt.CollectibleList[c + 2].transform.localPosition.z )
                                                {
                                                    if( Z_Pos.Count > 0 )
                                                    {
                                                        Z_Pos.Clear();
                                                    }
                                                    // Debug.LogError("stop gen..." + c);
                                                } 
                                                else
                                                {
                                                    Obstacle_Obj = Instantiate( ObstaclePrefab[_prefabCount], new Vector3( X_Pos[j] , _gcScipt.levelManager.LevelController[_gcScipt.CurrentLevel].ObstacleStart_Pos[0].transform.GetChild(count).position.y, Z_Pos[j] ), Quaternion.identity );
                                                    PatternChild_Count = PatternChild_Count - 1;
                                                    Obstacle_Obj.transform.parent = EmptyObj.transform;
                                                    Obstacle_Obj.transform.GetChild(0).localScale = new Vector3(1.15f, 1.15f, 1.15f);
                                                    material = Obstacle_Obj.transform.GetChild(0).GetComponent<MeshRenderer>().material;                                                
                                                    break;
                                                }
                                            }
                                            catch(System.Exception e)
                                            {
                                                print(e);
                                                break;
                                            }
                                        }
                                        yield return new WaitForSeconds(0.01f);         //0.1
                                        m = m + 1;
                                    }
                                    while(c < _gcScipt.CollectibleList.Count);
                                }
                            }
                            else
                            {
                                Obstacle_Obj = Instantiate( ObstaclePrefab[_prefabCount], new Vector3( X_Pos[j] , _gcScipt.levelManager.LevelController[_gcScipt.CurrentLevel].ObstacleStart_Pos[0].transform.GetChild(count).position.y, Z_Pos[j] ), Quaternion.identity );
                                PatternChild_Count = PatternChild_Count - 1;
                                Obstacle_Obj.transform.parent = EmptyObj.transform;
                                Obstacle_Obj.transform.GetChild(0).localScale = new Vector3(1.15f, 1.15f, 1.15f);
                                material = Obstacle_Obj.transform.GetChild(0).GetComponent<MeshRenderer>().material;
                                yield return new WaitForSeconds(0.01f);         //0.1
                            }
                        }
                }
            }
            if( patternObj.Count > 0 )
            {
                if( patternObj[0] != null )
                {
                    patternObj.Remove(patternObj[0]);
                }
            }
            X_Pos.Clear();
            Z_Pos.Clear();
            yield return new WaitForSeconds(0.25f);      //0.3
        }
        StartCoroutine(PatterEnum());
    }

    IEnumerator PatterEnum()
    {
        yield return new WaitForSeconds(0.001f);                //0.001
        PatternGenerate_bool = true;
    }

    public IEnumerator StopObstacleGen()
    {
        CancelInvoke("ObstacleInstantiate");
        yield return new WaitForSeconds(0.001f);
    }

    public IEnumerator StopGeneration()
    {
        PatternGenerate_bool = false;
        CancelInvoke("ObstacleInstantiate");
        yield return new WaitForSeconds(1.0f);
        PatternGenerate_bool = true;
        // StartCoroutine(ObstacleCoroutine());
    }
}
