using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GameAnalyticsSDK;

public class GameController : MonoBehaviour
{
    GameObject[] _huddelsCount, _ObstacleCount;
    public GameObject GameOverPanel, Player, Controller, GameControllerObj, PlayerController, PlayerParent, PlayerCarController, PlayerCar, PlayerCar_Child, PlayerCar_Left, PlayerCar_Right, LookAt, MoveTo, LookAt_HomePanel, MoveTo_HomePanel, EnemyCar, ObstacleGenPos, EnvironmentParent, CollectibleGenPos;
    public GameObject  LevelUpPanel, PausePanel, InGamePanel, InternetPanel, InternetPanel_Ingame, CollectibleObj, KeyObj, KeyGenPos, LookAt_gc, MoveAt_gc, EnemyCar_Parent, LevelFillBar;
    public static int score, PropsCount, _diamondCount;
    public Text ScoreTxt, PropTxt, Current_LevelTxt, Next_LevelTxt, DiamondTxt, DiamondTxt_Home, DiamondTxt_LC, HighScoreTxt, ScoreTxt_LC, LevelTxt_LC, ScoreTxt_GO, HighScoreTxt_GO, CountDown_Txt;
    public int Count, count_patternObj, count_patternchildObj, CurrentLevel, _totalCollect_diamond;
    public List<GameObject> ObstaclePrefab, ObstacleList, CollectibleList, Key, Key_home, Key_GO, ParticleList;
    public List<Material> PlayerMat_List;
    public List<int> ObstacleValue, CollectibleValue;
    public AudioSource CarCrashAudio, BkAudio, Stonebreak, ObstacleCube, WinAudio;
    public List<AudioClip> GameAudio;
    public LevelManager levelManager;
    public Button KeyBtn, PauseBtn, GetKeyBtn, DoubleDiamondBtn;
    public GameObject _EnmyObj;
    public bool _applicationPause, _keybool, _playerRight_enable, _playerLeft_enable;
    public float _playerPos_z, _playerCarPos_z;

    // Start is called before the first frame update
    void Start()
    {
        _playerRight_enable = false;
        _playerLeft_enable = false;
        CountDown_Txt.enabled = false;
        _applicationPause = true;
        _keybool = false;
        // PlayerPrefs.DeleteAll();
        for(int i = 0; i < PlayerMat_List.Count; i++)
        {
            if(i == PlayerPrefs.GetInt("SkinSelected"))
            {
               PlayerParent.GetComponent<MeshRenderer>().material = PlayerMat_List[i];
               if(ParticleList[i] != null)
               {
                   GameObject _particle = Instantiate(ParticleList[i], new Vector3 (0,0,0), Quaternion.identity);
                    _particle.transform.parent = PlayerParent.transform;
                    _particle.transform.localPosition = new Vector3(0, 0, 0);
                    _particle.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    _particle.transform.localScale = new Vector3(1,1,1);
               }               
            }
        }
        ObstacleGen _obstacleScript = FindObjectOfType<ObstacleGen>();
        int _randomClip = Random.Range( 0, GameAudio.Count );
        _obstacleScript.audioSource.clip = GameAudio[_randomClip];
        _obstacleScript.audioSource.Play();
        score = 0;
        PropsCount = 0;
        Count = 0;
        count_patternObj = 4;
        count_patternchildObj = 0;
        LevelFillBar.transform.GetComponent<Image>().fillAmount = 1f;
        DiamondTxt.text = PlayerPrefs.GetInt("DiamondCount").ToString();
        DiamondTxt_Home.text = PlayerPrefs.GetInt("DiamondCount").ToString();
        PlayerPrefs.SetInt("brokenRoad_coll", 0);
        // PlayerPrefs.DeleteAll();
        if (!PlayerPrefs.HasKey ("StickersUnlock")) 
        {
            // PlayerPrefs.SetInt ("Stickers",0);
            PlayerPrefs.SetInt("StickersUnlock",0);            
            PlayerPrefs.Save();
        }
        if (!PlayerPrefs.HasKey ("CurrentLevel")) 
        {
            PlayerPrefs.SetInt ("CurrentLevel",0);
            PlayerPrefs.Save();
        }
        if( PlayerPrefs.GetInt("CurrentLevel") >= 59 )
        {
            CurrentLevel = Random.Range(54,59);
            int CurrentLevelNo = PlayerPrefs.GetInt("CurrentLevel");
            Current_LevelTxt.text = (CurrentLevelNo + 1).ToString();
            Next_LevelTxt.text = (CurrentLevelNo + 2).ToString();
        }
        else
        {
            CurrentLevel = PlayerPrefs.GetInt("CurrentLevel");
            Current_LevelTxt.text = (CurrentLevel + 1).ToString();
            Next_LevelTxt.text = (CurrentLevel + 2).ToString();
        }

        /* GameAnalytics Data for specical user that clear 100 levels */
        if(PlayerPrefs.GetInt("CurrentLevel") >= 100) 
        {
            string strError = "LEVEL_100_ACHIEVED";
            GameAnalytics.NewErrorEvent(GAErrorSeverity.Info, strError);
	    }

        if( PlayerPrefs.GetInt("CurrentLevel") > 4 )
        {
            PlayerCar_Left.SetActive(true);
            PlayerCar_Child.transform.localPosition = new Vector3(0.5f, PlayerCar_Child.transform.localPosition.y, PlayerCar_Child.transform.localPosition.z);
            PlayerCar_Right.transform.localPosition = new Vector3(4.26f, PlayerCar_Right.transform.localPosition.y, PlayerCar_Right.transform.localPosition.z);
        }
        else
        {
            PlayerCar_Left.SetActive(false);
            PlayerCar_Child.transform.localPosition = new Vector3(-2.5f, PlayerCar_Child.transform.localPosition.y, PlayerCar_Child.transform.localPosition.z);
            PlayerCar_Right.transform.localPosition = new Vector3(2.5f, PlayerCar_Right.transform.localPosition.y, PlayerCar_Right.transform.localPosition.z);
        }

        if( PlayerPrefs.GetInt("Key") <= 2)
        {
            int KeyValue = Random.Range(0, KeyGenPos.transform.childCount);
            Instantiate( KeyObj, new Vector3(KeyGenPos.transform.GetChild(KeyValue).position.x, KeyGenPos.transform.GetChild(KeyValue).position.y, KeyGenPos.transform.GetChild(KeyValue).position.z), Quaternion.identity );
        }

        for( int i = 0; i < Key.Count; i++ )
        {
            if( i <= PlayerPrefs.GetInt("Key") - 1  )
            {
                Key[i].GetComponent<Image>().enabled = true;
                Key_home[i].GetComponent<Image>().enabled = true;
                Key_GO[i].GetComponent<Image>().enabled = true;
            }
            else
            {
                Key[i].GetComponent<Image>().enabled = false;
                Key_home[i].GetComponent<Image>().enabled = false;
                Key_GO[i].GetComponent<Image>().enabled = false;
            }
        }

        _totalCollect_diamond = PlayerPrefs.GetInt("DiamondCount");

        GameObject _lvlenv =  Instantiate(levelManager.LevelController[CurrentLevel].Environment);
        _lvlenv.transform.parent = EnvironmentParent.transform;
        _lvlenv.SetActive(true);
        _lvlenv.transform.localPosition = new Vector3(0,0,0);
        _lvlenv.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        _EnmyObj = Instantiate(levelManager.LevelController[CurrentLevel].EnemyCar);
        _EnmyObj.transform.parent = EnemyCar_Parent.transform;
        _EnmyObj.SetActive(true);
        _EnmyObj.transform.localPosition = new Vector3(0,0,0);
        _EnmyObj.transform.localRotation = Quaternion.Euler(0,0,0);
        _EnmyObj.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        ObstacleGen _obstacleGen = FindObjectOfType<ObstacleGen>();
        if(LevelFillBar.transform.GetComponent<Image> ().fillAmount <= 1.0f)
        {
            LevelFillBar.transform.GetComponent<Image> ().fillAmount -= 1f/25 * Time.deltaTime; 
        }
        if( PlayerPrefs.GetInt("CurrentLevel") > 30 )
        {
            for( int i = 0; i < ObstacleGenPos.transform.childCount; i++ )
            {
                if( Vector3.Distance( _EnmyObj.transform.position, ObstacleGenPos.transform.GetChild(i).localPosition ) < 30.0f )
                // if( Vector3.Distance( EnemyCar.transform.position, levelManager.LevelController[CurrentLevel].Environment.GetComponent<EnvironmentScript>().ObstacleGenPos.transform.GetChild(i).localPosition ) < 30.0f )
                {
                    if( ObstacleValue.Count > 0 )
                    {
                        int flag = 0;
                        for( int j = 0; j < ObstacleValue.Count; j++ )
                        {
                            if( ObstacleValue[j] == i )
                            {
                                flag = 1;
                                break;
                            }
                        }
                        if( flag == 0 )
                        {
                            // int obstaclecount = Random.Range(0 , ObstaclePrefab.Count);
                            // ObstacleList.Add(Instantiate( ObstaclePrefab[obstaclecount], new Vector3( ObstacleGenPos.transform.GetChild(i).position.x , ObstacleGenPos.transform.GetChild(i).position.y, ObstacleGenPos.transform.GetChild(i).position.z ), Quaternion.identity ));
                            int obstaclecount = Random.Range(0 , levelManager.LevelController[CurrentLevel].ObstaclePrefab.Count);
                            ObstacleList.Add(Instantiate( levelManager.LevelController[CurrentLevel].ObstaclePrefab[obstaclecount], new Vector3( ObstacleGenPos.transform.GetChild(i).position.x , ObstacleGenPos.transform.GetChild(i).position.y, ObstacleGenPos.transform.GetChild(i).position.z ), Quaternion.identity ));
                            ObstacleValue.Add(i);
                            i = i+1;
                        }
                    }
                    else
                    {
                        // int propcount = Random.Range(0 , ObstaclePrefab.Count);
                        // ObstacleList.Add(Instantiate( ObstaclePrefab[propcount], new Vector3( ObstacleGenPos.transform.GetChild(i).position.x , ObstacleGenPos.transform.GetChild(i).position.y, ObstacleGenPos.transform.GetChild(i).position.z ), Quaternion.identity ));
                        int propcount = Random.Range(0 , levelManager.LevelController[CurrentLevel].ObstaclePrefab.Count);
                        ObstacleList.Add(Instantiate( levelManager.LevelController[CurrentLevel].ObstaclePrefab[propcount], new Vector3( ObstacleGenPos.transform.GetChild(i).position.x , ObstacleGenPos.transform.GetChild(i).position.y, ObstacleGenPos.transform.GetChild(i).position.z ), Quaternion.identity ));
                        if( ObstacleValue.Count == 0 )
                        {
                            ObstacleValue.Add(i);
                        }
                    }
                }
            }

            for( int obs = 0; obs < ObstacleList.Count; obs++ )        //To destroy Prop Objs when it is far from player
            {
                if( ObstacleList[obs] == null )
                {
                    ObstacleList.RemoveAt(obs);
                    ObstacleValue.RemoveAt(obs);
                }
            }
        }
        

        for( int i = 0; i < CollectibleGenPos.transform.childCount; i++ )
        {
            if( Vector3.Distance( _EnmyObj.transform.position, CollectibleGenPos.transform.GetChild(i).localPosition ) < 50.0f )            //30.0f
            // if( Vector3.Distance( EnemyCar.transform.position, levelManager.LevelController[CurrentLevel].Environment.GetComponent<EnvironmentScript>().ObstacleGenPos.transform.GetChild(i).localPosition ) < 30.0f )
            {
                if( CollectibleValue.Count > 0 )
                {
                    int flag = 0;
                    for( int j = 0; j < CollectibleValue.Count; j++ )
                    {
                        if( CollectibleValue[j] == i )
                        {
                            flag = 1;
                            break;
                        }
                    }
                    if( flag == 0 )
                    {
                        CollectibleList.Add(Instantiate( CollectibleObj, new Vector3( CollectibleGenPos.transform.GetChild(i).position.x , CollectibleGenPos.transform.GetChild(i).position.y, CollectibleGenPos.transform.GetChild(i).position.z ), Quaternion.Euler(180.0f, 0, 0) ));
                        CollectibleValue.Add(i);
                        i = i+1;
                    }
                }
                else
                {
                    CollectibleList.Add(Instantiate( CollectibleObj, new Vector3( CollectibleGenPos.transform.GetChild(i).position.x , CollectibleGenPos.transform.GetChild(i).position.y, CollectibleGenPos.transform.GetChild(i).position.z ), Quaternion.Euler(180.0f, 0, 0) ));
                    if( CollectibleValue.Count == 0 )
                    {
                        CollectibleValue.Add(i);
                    }
                }
            }
        }
    }

    void OnApplicationPause()
	{
		if(_applicationPause)
		{
			PausePanel.SetActive(true);
			Time.timeScale = 0;
		}
	}

    public void RestartGame()
    {
        Time.timeScale = 1;
        GameOverPanel.SetActive(false);
        PlayerPrefs.SetInt("PlayGame",1);
        PlayerPrefs.SetInt("HomePanel",0);
        SceneManager.LoadScene(0);
    }

    public void HomeBtn()
    {
        Time.timeScale = 1;
        GameOverPanel.SetActive(false);
        PlayerPrefs.SetInt("HomePanel",0);
        // PlayerPrefs.SetInt("PlayGame",1);
        SceneManager.LoadScene(0);
    }

    public void LevelUpGame()
    {
        LevelUpPanel.SetActive(false);
        MenuController _menuSCript = FindObjectOfType<MenuController>();
        int randomCount = Random.Range(0, _menuSCript.CarMat.Count);
        int randomCar_mat;
        PlayerPrefs.SetInt("CarMat", randomCount);
        if(randomCount > 0)
        {
            randomCar_mat = Random.Range(0, randomCount);
            PlayerPrefs.SetInt("CarMat_additional", randomCar_mat);
        }
        else
        {
            randomCar_mat = 1;
            PlayerPrefs.SetInt("CarMat_additional", randomCar_mat);
        }
        // PlayerPrefs.SetInt("CurrentLevel", PlayerPrefs.GetInt("CurrentLevel") + 1);
        PlayerPrefs.SetInt("PlayGame",1);
        PlayerPrefs.SetInt("HomePanel",0);
        SceneManager.LoadScene(0);
    }

    public void PauseGame()
    {
        PausePanel.SetActive(true);
        Player.GetComponent<Controller>().enabled = false;
        Controller.GetComponent<FluffyUnderware.Curvy.Controllers.SplineController>().enabled = false;
        PlayerController.GetComponent<FluffyUnderware.Curvy.Controllers.SplineController>().enabled = false;
        PlayerCarController.GetComponent<FluffyUnderware.Curvy.Controllers.SplineController>().enabled = false;
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        PausePanel.SetActive(false);
        Player.GetComponent<Controller>().enabled = true;
        Controller.GetComponent<FluffyUnderware.Curvy.Controllers.SplineController>().enabled = true;
        PlayerController.GetComponent<FluffyUnderware.Curvy.Controllers.SplineController>().enabled = true;
        PlayerCarController.GetComponent<FluffyUnderware.Curvy.Controllers.SplineController>().enabled = true;
    }

    public void GetItBtn()
    {
        MenuController _menuScript = FindObjectOfType<MenuController>();
        Time.timeScale = 1;
        GameOverPanel.SetActive(false);
        PlayerPrefs.SetInt("HomePanel",2);
        SceneManager.LoadScene(0);
        _menuScript.MenuPanel.SetActive(false);
        _menuScript.SkinSelectionPanel.SetActive(true);
    }

    public void KeyFunction()
    {
        _applicationPause = false;
        PauseBtn.interactable = false;
        ObstacleGen _obstacleScript = FindObjectOfType<ObstacleGen>();
        Player _playerScript = FindObjectOfType<Player>();
        BrokenPartGenScript _brokenPart = FindObjectOfType<BrokenPartGenScript>();
        LevelUpScript _lvlScript = FindObjectOfType<LevelUpScript>();
        _lvlScript._keyUsed = true;
        LevelUpScript._keyUsed_Value = LevelUpScript._keyUsed_Value + 1;
        Debug.Log("     " + PlayerPrefs.GetInt("brokenRoad_coll"));
        if( PlayerPrefs.GetInt("brokenRoad_coll") == 1 )
        {
            Debug.Log("Touched broken road... " + PlayerPrefs.GetInt("brokenRoad_coll"));
            PlayerParent.SetActive(true);
            _brokenPart.BrokenRoad[_brokenPart.count].SetActive(false);
            _brokenPart.StraightRoad[_brokenPart.count].SetActive(true);
            _brokenPart.BrokenRoad_GO[_brokenPart.count].SetActive(true);
            _brokenPart.StraightRoad_GO[_brokenPart.count].SetActive(false);
            Player.GetComponent<Controller>().enabled = true;
            PlayerPrefs.SetInt("brokenRoad_coll", 0);
        }
        GameOverPanel.SetActive(false);
        InGamePanel.SetActive(true);
        _playerScript._playerdie = false;
        PlayerCar_Left.GetComponent<FrontbackMovement>().enabled = false;
        PlayerCar_Right.GetComponent<FrontbackMovement>().enabled = false;
        Destroy(_playerScript._playreDuplicate);
        PlayerParent.SetActive(true);
        StartCoroutine(CountDown_KeyBtn());
        if (PlayerPrefs.GetInt ("SoundOn") == 0)
        {
            BkAudio.Play();
        }
        else
        {
            BkAudio.Stop();
        }
        PlayerPrefs.SetInt("Key", PlayerPrefs.GetInt("Key") - 1);
        for( int i = 0; i < Key.Count; i++ )
        {
            if( i <= PlayerPrefs.GetInt("Key") - 1 )
            {
                Key[i].GetComponent<Image>().enabled = true;
                Key_home[i].GetComponent<Image>().enabled = true;
                Key_GO[i].GetComponent<Image>().enabled = true;
            }
            else
            {
                Key[i].GetComponent<Image>().enabled = false;
                Key_home[i].GetComponent<Image>().enabled = false;
                Key_GO[i].GetComponent<Image>().enabled = false;
            }
        }
    }

    public IEnumerator CountDown_KeyBtn()
    {
        ObstacleGen _obstacleScript = FindObjectOfType<ObstacleGen>();
        Player _playerScript = FindObjectOfType<Player>();
        EnemyCarScript _enemyCarScript = FindObjectOfType<EnemyCarScript>();
        _playerScript.EnemyCar.transform.localPosition = new Vector3(_playerScript.EnemyCar.transform.localPosition.x, -15.0f, _playerScript.EnemyCar.transform.localPosition.z);
        CountDown_Txt.enabled = true;
        CountDown_Txt.text = "3";
        CountDown_Txt.GetComponent<Animation>().Play("CountDownAnim");
        yield return new WaitForSeconds(1.5f);
        CountDown_Txt.GetComponent<Animation>().Stop();
        CountDown_Txt.GetComponent<Animation>().Play("CountDownAnim");
        CountDown_Txt.text = "2";
        yield return new WaitForSeconds(1.5f);
        CountDown_Txt.GetComponent<Animation>().Stop();
        CountDown_Txt.GetComponent<Animation>().Play("CountDownAnim");
        CountDown_Txt.text = "1";
        yield return new WaitForSeconds(1.5f);
        Player.GetComponent<Controller>().enabled = true;
        CountDown_Txt.GetComponent<Animation>().Stop();
        CountDown_Txt.enabled = false;        
        GameControllerObj.GetComponent<ObstacleGen>().enabled = true;
        GameControllerObj.GetComponent<GameController>().enabled = true;
        PlayerCar_Left.GetComponent<FrontbackMovement>().enabled = true;
        PlayerCar_Right.GetComponent<FrontbackMovement>().enabled = true;
        Controller.GetComponent<FluffyUnderware.Curvy.Controllers.SplineController>().enabled = true;
        PlayerController.GetComponent<FluffyUnderware.Curvy.Controllers.SplineController>().enabled = true;
        PlayerCarController.GetComponent<FluffyUnderware.Curvy.Controllers.SplineController>().enabled = true;
        Camera.main.GetComponent<FluffyUnderware.Curvy.Examples.ChaseCam>().LookAt = LookAt_gc.transform;
        Camera.main.GetComponent<FluffyUnderware.Curvy.Examples.ChaseCam>().MoveTo = MoveAt_gc.transform;
        _playerScript.EnemyCar.transform.localPosition = new Vector3(_playerScript.EnemyCar.transform.localPosition.x, -13.0f, _playerScript.EnemyCar.transform.localPosition.z);
        PlayerCar.GetComponent<PlayerCarMovement>().enabled = true;
        _applicationPause = true;
        PauseBtn.interactable = true;
        _playerRight_enable = false;
        if( _enemyCarScript._finishedlevel == true )
        {
            _obstacleScript.PatternGenerate_bool = false;
        }
        else
        {
            _obstacleScript.PatternGenerate_bool = true;
        }
        yield return new WaitForSeconds(1.5f);
        if( PlayerPrefs.GetInt("CurrentLevel") > 4 )
        {
            if( _playerLeft_enable == true )
            {

            }
            else
            {
                PlayerCar_Left.SetActive(true);
            }
        }
        else
        {
            PlayerCar_Left.SetActive(false);
        }
        if( _playerRight_enable == true )
        {
            PlayerCar_Right.SetActive(false);
        }
        else
        {
            PlayerCar_Right.SetActive(true);
        }
        _playerScript.EnemyCar.transform.localPosition = new Vector3(_playerScript.EnemyCar.transform.localPosition.x, -9.5f, _playerScript.EnemyCar.transform.localPosition.z);
        yield return new WaitForSeconds(0.5f);
        _playerScript.EnemyCar.transform.localPosition = new Vector3(_playerScript.EnemyCar.transform.localPosition.x, -8.0f, _playerScript.EnemyCar.transform.localPosition.z);
    }

    public void OkBtn()
    {
        InternetPanel_Ingame.SetActive(false);
    }

}
