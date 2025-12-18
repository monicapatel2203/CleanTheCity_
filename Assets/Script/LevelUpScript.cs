using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Xml; //Needed for XML functionality
using System.Xml.Serialization; //Needed for XML Functionality
using System.IO;
using System.Xml.Linq; //Needed for XDocument
using UnityEngine.EventSystems;
using GameAnalyticsSDK;

public class LevelUpScript : MonoBehaviour
{
    public GameObject ControllerEnemyCar, ControllerPlayerCar, ControllerPlayer, GameController, Player, PlayerCar, LookAt, MoveTo, WinParticle_Left, WinParticle_Right, dummyPlayer, BlackHole_image, BlackHole_Skin;
    public Transform Destination_Pos;
    public Vector3 targetPos, PlayerPos;
    public List<Material> dummyPlayer_mat;
    public List<GameObject> BlackHole_sprites, LockImage;
    // public List<Image> LockImage;
    public List<int> BlackHole_price;
    public GameObject GetItBtn;
    public bool _lvlComplete, DummyGenerated, _keycollect, _keyUsed;
    GameObject dummyObj;
    public List<Sprite> StickerImages;
    public Image StickerObj;
    float _generateValue;
    public static int _keyUsed_Value;

    // Start is called before the first frame update
    void Start()
    {
        _lvlComplete = false;
        DummyGenerated = false;
        _keycollect = false;
        _keyUsed = false;
        _keyUsed_Value = 0;
        targetPos.x = Destination_Pos.transform.position.x;
        targetPos.y = Destination_Pos.transform.position.y;
        targetPos.z = Destination_Pos.transform.position.z;
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    void OnCollisionEnter( Collision coll )
    {
        GameController _gcScript = FindObjectOfType<GameController>();
        ObstacleGen _obstacleGen = FindObjectOfType<ObstacleGen>();
        if( coll.gameObject.tag == "FinishLevel" )
        {
            _gcScript._applicationPause = false;
            PlayerPrefs.SetInt("CurrentLevel", PlayerPrefs.GetInt("CurrentLevel") + 1);
            int score = int.Parse(_gcScript.ScoreTxt.text);
            _gcScript.ScoreTxt_LC.text = score.ToString();
            _gcScript.LevelTxt_LC.text = _gcScript.Current_LevelTxt.text;
            if( score > PlayerPrefs.GetInt("HighScore") )
            {
                _gcScript.HighScoreTxt.text = score.ToString();
                PlayerPrefs.SetInt("HighScore", score);
            }
            else
            {
                _gcScript.HighScoreTxt.text = PlayerPrefs.GetInt("HighScore").ToString();
            }
            Instantiate( WinParticle_Left, new Vector3( -11.0f, -30.0f, 1317.0f ), Quaternion.Euler(-120.0f, -90.0f, 0) );
            Instantiate( WinParticle_Right, new Vector3( 11.0f, -30.0f, 1317.0f ), Quaternion.Euler(-60.0f, -90.0f, 0) );
            if (PlayerPrefs.GetInt ("SoundOn") == 0)
            {
                _gcScript.WinAudio.Play();
            }
            else
            {
                _gcScript.WinAudio.Stop();
            }
            Invoke("DummyPlayerfun", 0.8f);
            GameObject[] Obstacle_generate = GameObject.FindGameObjectsWithTag("Obstacle");
            foreach (GameObject obstacle in Obstacle_generate)
            {
                GameObject.Destroy(obstacle);
            }
            _lvlComplete = true;
            StartCoroutine(_obstacleGen.StopObstacleGen());
            GameController.GetComponent<ObstacleGen>().enabled = false;
            Player.GetComponent<Controller>().enabled = false;            
            _gcScript.InGamePanel.SetActive(false);
            _gcScript.LevelUpPanel.SetActive(true);
            _gcScript.PlayerCar_Right.GetComponent<FrontbackMovement>().enabled = false;
            _gcScript.PlayerCar_Left.GetComponent<FrontbackMovement>().enabled = false;
            Camera.main.GetComponent<FluffyUnderware.Curvy.Examples.ChaseCam>().LookAt = LookAt.transform;
            Camera.main.GetComponent<FluffyUnderware.Curvy.Examples.ChaseCam>().MoveTo = MoveTo.transform;
            this.gameObject.GetComponent<LevelUpScript>().enabled = false;
            PlayerCar.GetComponent<PlayerCarMovement>().enabled = false;
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            PlayerXMLScript _playerXml = FindObjectOfType<PlayerXMLScript>();
            int _diamondCount = int.Parse(_gcScript.DiamondTxt.text);
            int flag = 0;
            for( int j = 0; j < BlackHole_sprites.Count; j++ )
            {
                flag = 0;
                Debug.Log("1..." + BlackHole_sprites[j].name);
                if( _playerXml._elementName.Count > 1 )
                {
                    for( int k = 0; k < _playerXml._elementName.Count; k++ )
                    {
                        if( BlackHole_sprites[j].name == _playerXml._elementName[k] )
                        {
                            Debug.Log("2..." + BlackHole_sprites[j].name);
                            flag = 1;
                            break;
                        }
                    }
                    if(flag == 0)
                    {
                        Debug.Log("3..." + BlackHole_sprites[j].name);
                        BlackHole_image.SetActive(true);
                        GetItBtn.SetActive(true);
                        BlackHole_Skin.GetComponent<Image>().sprite = BlackHole_sprites[j].GetComponent<Image>().sprite;
                        break;
                    }
                    else
                    {
                        Debug.Log("4..." + BlackHole_sprites[j].name);
                        BlackHole_image.SetActive(false);
                        GetItBtn.SetActive(false);
                    }
                }
                else
                {
                    BlackHole_image.SetActive(true);
                    GetItBtn.SetActive(true);
                    BlackHole_Skin.GetComponent<Image>().sprite = BlackHole_sprites[j + 1].GetComponent<Image>().sprite;
                    break;
                }
            }

            /* GameAnalytics Data that have cleared the level successfully... */
            string strLevel = "LEVEL_" + _gcScript.Current_LevelTxt.text;
            int _currentLvl_diamond = PlayerPrefs.GetInt("DiamondCount") - _gcScript._totalCollect_diamond;
            _gcScript.DiamondTxt_LC.text = _currentLvl_diamond.ToString();
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, strLevel, score.ToString());
            GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "Diamonds", _currentLvl_diamond, "LEVEL_CLEARED", strLevel);
            GameAnalytics.NewDesignEvent(strLevel + ":Completed", 1);
            if(_keycollect)
            {
                GameAnalytics.NewDesignEvent("Key:Collected", 1);
            }
            else
            {
                GameAnalytics.NewDesignEvent("Key:Missed", 1);
            }
            if(_keyUsed)
            {
                GameAnalytics.NewDesignEvent("Key:Used", _keyUsed_Value);			// 1 in place no. of times key function called....
            }

            if( _currentLvl_diamond > 0 )
            {
                _gcScript.DoubleDiamondBtn.interactable = true;
                _gcScript.DoubleDiamondBtn.GetComponent<Animation>().Play();
            }
            else
            {
                _gcScript.DoubleDiamondBtn.interactable = false;
                _gcScript.DoubleDiamondBtn.GetComponent<Animation>().Stop();
            }
        }
    }

    void DummyPlayerfun()
    {
        int _matRange = Random.Range(0, dummyPlayer_mat.Count);
        // int _count = Random.Range(0, 2);
        // if(_count == 0)
        // {
        //     _generateValue = 10.0f;
        // }
        // else
        // {
        //     _generateValue = -10.0f;
        // }
        // dummyObj =  Instantiate( dummyPlayer, new Vector3(2.75f, -28.0f, 1326.0f), Quaternion.Euler(0, -90.0f, 0) );
        dummyObj =  Instantiate( dummyPlayer, new Vector3(this.transform.position.x - 3.0f, -28.0f, this.transform.position.z), Quaternion.Euler(0, -180.0f, 0) );
        dummyObj.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = dummyPlayer_mat[_matRange];
        dummyObj.transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
        DummyGenerated = true;
    }

    void OnTriggerEnter(Collider coll)
    {
        GameController _gcScript = FindObjectOfType<GameController>();
        PlayerCarMovement _movementScript = FindObjectOfType<PlayerCarMovement>();
        if( coll.gameObject.tag == "EntranceCollider" )
        {
            this.transform.parent.GetComponent<PlayerCarMovement>().enabled = false;
            // PlayerPos.x = this.transform.parent.localPosition.x;
            // PlayerPos.y = this.transform.parent.localPosition.y;
            // PlayerPos.z = this.transform.parent.localPosition.z;
        }
        else if( coll.gameObject.tag == "ExitCollider" )
        {
            // this.transform.parent.localPosition = new Vector3(PlayerPos.x, PlayerPos.y, PlayerPos.z);
            this.transform.parent.GetComponent<PlayerCarMovement>().enabled = true;
            // Invoke("EnableScript", 0.1f);
        }
    }

    void EnableScript()
    {
        this.transform.parent.GetComponent<PlayerCarMovement>().enabled = true;
    }

}
