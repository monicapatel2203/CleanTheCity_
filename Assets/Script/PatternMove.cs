using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternMove : MonoBehaviour
{
    public List<GameObject> PatternMoveList;
    public GameObject BlackHole;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EnemyCarScript enemyCarScript = FindObjectOfType<EnemyCarScript>();
        if( enemyCarScript._patternMove == true )
        {

        }
        else
        {
            if( PlayerPrefs.GetInt("CurrentLevel") > 6 )
            {
                if(PatternMoveList.Count > 0)
                {
                    // Vector3 targetPosition = new Vector3(PatternMoveList[0].transform.localPosition.x + 4, PatternMoveList[0].transform.localPosition.y, PatternMoveList[0].transform.localPosition.z);
                    for(int i = 0; i < PatternMoveList.Count; i++)
                    {
                        if( PatternMoveList[i].transform.childCount > 0 )
                        {
                            float _range = Random.Range(-3.0f, 3.0f);
                            Vector3 targetPosition = new Vector3(3.0f ,0, 0);
                            // if(Vector3.Distance( PatternMoveList[i].transform.GetChild(0).localPosition, BlackHole.transform.localPosition ) < 20.0f)
                            if( BlackHole.transform.position.z > PatternMoveList[i].transform.GetChild(0).localPosition.z - 40.0f )
                            {
                                if( PatternMoveList[i].tag == "PatternAnim_Left" )
                                {
                                    // PatternMoveList[i].transform.Translate(3.0f);
                                    // PatternMoveList[i].transform.localPosition = new Vector3(Mathf.Lerp( PatternMoveList[i].transform.localPosition.x, PatternMoveList[i].transform.localPosition.x + 2.0f, 2.0f ), PatternMoveList[i].transform.localPosition.y, PatternMoveList[i].transform.localPosition.z);
                                    if(PatternMoveList[i].transform.localPosition.x > -3.0f)
                                    {
                                        // PatternMoveList[i].transform.localPosition = Vector3.Lerp(PatternMoveList[i].transform.localPosition, targetPosition, 100.0f * Time.smoothDeltaTime);
                                        // PatternMoveList[i].transform.localPosition = new Vector3(3.0f, 0, 0);
                                        PatternMoveList[i].GetComponent<Animation>().Play("PatternAnim_Left");          //PatternAnim_Left
                                    }
                                }
                                else if( PatternMoveList[i].tag == "PatternAnim_Right" )
                                {
                                    // PatternMoveList[i].transform.Translate(3.0f);
                                    // PatternMoveList[i].transform.localPosition = new Vector3(Mathf.Lerp( PatternMoveList[i].transform.localPosition.x, PatternMoveList[i].transform.localPosition.x + 2.0f, 2.0f ), PatternMoveList[i].transform.localPosition.y, PatternMoveList[i].transform.localPosition.z);
                                    if(PatternMoveList[i].transform.localPosition.x < 3.0f)
                                    {
                                        // PatternMoveList[i].transform.localPosition = Vector3.Lerp(PatternMoveList[i].transform.localPosition, targetPosition, 100.0f * Time.smoothDeltaTime);
                                        // PatternMoveList[i].transform.localPosition = new Vector3(3.0f, 0, 0);
                                        PatternMoveList[i].GetComponent<Animation>().Play("PatternAnim_Right");          //PatternAnim_Left
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
