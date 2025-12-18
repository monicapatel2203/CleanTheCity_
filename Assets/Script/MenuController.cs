using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GameAnalyticsSDK;

public class MenuController : MonoBehaviour
{
    public GameObject gameCanvas, Menucanvas, MenuPanel, HomePanel, SkinSelectionPanel, controller, GameController, Player, PlayerCar, EnemyCar_subobj, PlayerController, PlayerCarController, QuitPanel, StorePanel, StickersPanel;
    public GameObject PlayerCar_Child;
    public List<Material> CarMat;
    static int _randomMat, _randomMat_additionalCar;
    public ScrollRect StoreScroll;
    public bool _truckObj;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        if (!PlayerPrefs.HasKey ("HomePanel"))
        {
            PlayerPrefs.SetInt ("HomePanel",1);
            PlayerPrefs.Save();
        }
        if( PlayerPrefs.GetInt("PlayGame") == 1 )
        {
            PlayerPrefs.SetInt("PlayGame", 0);
            _truckObj = false;
            PlayerCar_Child.GetComponent<MeshRenderer>().material = CarMat[PlayerPrefs.GetInt("CarMat")];
            GameController.GetComponent<GameController>().PlayerCar_Left.GetComponent<MeshRenderer>().material = CarMat[PlayerPrefs.GetInt("CarMat_additional")];
            GameController.GetComponent<GameController>().PlayerCar_Right.GetComponent<MeshRenderer>().material = CarMat[PlayerPrefs.GetInt("CarMat_additional")];
            gameCanvas.SetActive(true);
            Menucanvas.SetActive(false);
            Player.GetComponent<Animation>().enabled = false;
            GameController.SetActive(false);
            // Camera.main.GetComponent<FluffyUnderware.Curvy.Examples.ChaseCam>().LookAt = GameController.GetComponent<GameController>().LookAt_HomePanel.transform;
            Camera.main.GetComponent<FluffyUnderware.Curvy.Examples.ChaseCam>().MoveTo = GameController.GetComponent<GameController>().MoveTo_HomePanel.transform;
            // Camera.main.GetComponent<FluffyUnderware.Curvy.Examples.ChaseCam>().RollTo = GameController.GetComponent<GameController>().LookAt_HomePanel.transform;
            PlayerCar_Child.GetComponent<SimpleCarController>().enabled = false;
            GameController.GetComponent<GameController>().PlayerCar_Left.GetComponent<FrontbackMovement>().enabled = false;
            GameController.GetComponent<GameController>().PlayerCar_Right.GetComponent<FrontbackMovement>().enabled = false;
            GameController.GetComponent<GameController>().CountDown_Txt.enabled = true;
            // GameController.GetComponent<GameController>().EnemyCar_Parent.transform.localPosition = new Vector3(GameController.GetComponent<GameController>().EnemyCar_Parent.transform.localPosition.x, -13.0f, GameController.GetComponent<GameController>().EnemyCar_Parent.transform.localPosition.z);
            GameController.GetComponent<GameController>().CountDown_Txt.text = "3";
            GameController.GetComponent<GameController>().PauseBtn.interactable = false;
            GameController.GetComponent<GameController>().CountDown_Txt.GetComponent<Animation>().Play("CountDownAnim");
            yield return new WaitForSeconds(1.5f);
            GameController.GetComponent<GameController>().CountDown_Txt.GetComponent<Animation>().Stop();
            GameController.GetComponent<GameController>().CountDown_Txt.GetComponent<Animation>().Play("CountDownAnim");
            GameController.GetComponent<GameController>().CountDown_Txt.text = "2";
            yield return new WaitForSeconds(1.5f);
            GameController.GetComponent<GameController>().CountDown_Txt.GetComponent<Animation>().Stop();
            GameController.GetComponent<GameController>().CountDown_Txt.GetComponent<Animation>().Play("CountDownAnim");
            GameController.GetComponent<GameController>().CountDown_Txt.text = "1";
            yield return new WaitForSeconds(1.5f);
            _truckObj = true;

            /* GameAnalytics to store the data of currentlevel name */
            string strLevel = "LEVEL_" + GameController.GetComponent<GameController>().Current_LevelTxt.text;
	        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, strLevel);

            GameController.GetComponent<GameController>().CountDown_Txt.GetComponent<Animation>().Stop();
            GameController.GetComponent<GameController>().CountDown_Txt.enabled = false;
            GameController.GetComponent<GameController>().enabled = true;
            GameController.GetComponent<GameController>().PauseBtn.interactable = true;
            Player.GetComponent<Controller>().enabled = true;
            controller.GetComponent<FluffyUnderware.Curvy.Controllers.SplineController>().enabled = true;
            PlayerController.GetComponent<FluffyUnderware.Curvy.Controllers.SplineController>().enabled = true;
            PlayerCarController.GetComponent<FluffyUnderware.Curvy.Controllers.SplineController>().enabled = true;
            GameController.SetActive(true);
            PlayerCar_Child.GetComponent<SimpleCarController>().enabled = true;
            GameController.GetComponent<GameController>().PlayerCar_Left.GetComponent<FrontbackMovement>().enabled = true;
            GameController.GetComponent<GameController>().PlayerCar_Right.GetComponent<FrontbackMovement>().enabled = true;
            GameController.GetComponent<ObstacleGen>().enabled = true;
            PlayerCar.GetComponent<PlayerCarMovement>().enabled = true;
            // Camera.main.GetComponent<FluffyUnderware.Curvy.Examples.ChaseCam>().LookAt = GameController.GetComponent<GameController>().LookAt_gc.transform;
            Camera.main.GetComponent<FluffyUnderware.Curvy.Examples.ChaseCam>().ChaseTime = 1.0f;
            Camera.main.GetComponent<FluffyUnderware.Curvy.Examples.ChaseCam>().MoveTo = GameController.GetComponent<GameController>().MoveAt_gc.transform;
            GameController.GetComponent<GameController>().EnemyCar_Parent.transform.localPosition = new Vector3(GameController.GetComponent<GameController>().EnemyCar_Parent.transform.localPosition.x, -9.5f, GameController.GetComponent<GameController>().EnemyCar_Parent.transform.localPosition.z);
            yield return new WaitForSeconds(1.5f);
            Camera.main.GetComponent<FluffyUnderware.Curvy.Examples.ChaseCam>().ChaseTime = 0.7f;
            yield return new WaitForSeconds(1.5f);
            GameController.GetComponent<GameController>().EnemyCar_Parent.transform.localPosition = new Vector3(GameController.GetComponent<GameController>().EnemyCar_Parent.transform.localPosition.x, -8.0f, GameController.GetComponent<GameController>().EnemyCar_Parent.transform.localPosition.z);
            yield return new WaitForSeconds(1.0f);
            GameController.GetComponent<GameController>().EnemyCar_Parent.transform.localPosition = new Vector3(GameController.GetComponent<GameController>().EnemyCar_Parent.transform.localPosition.x, -7.0f, GameController.GetComponent<GameController>().EnemyCar_Parent.transform.localPosition.z);
            // Camera.main.GetComponent<FluffyUnderware.Curvy.Examples.ChaseCam>().RollTo = GameController.GetComponent<GameController>().LookAt_gc.transform;
            // yield return new WaitForSeconds(0.5f);
            // GameController.GetComponent<GameController>().EnemyCar_Parent.transform.localPosition = new Vector3(GameController.GetComponent<GameController>().EnemyCar_Parent.transform.localPosition.x, -12f, GameController.GetComponent<GameController>().EnemyCar_Parent.transform.localPosition.z);
            // yield return new WaitForSeconds(2.0f);
        }
        else
        {
            _truckObj = false;
            _randomMat = Random.Range(0, CarMat.Count);
            PlayerPrefs.SetInt("CarMat", _randomMat);
            // int flag = 1;
            // if(flag == 1)
            // {
            //     _randomMat_additionalCar = Random.Range(0, CarMat.Count);
            // }
            // if(_randomMat == _randomMat_additionalCar)
            // {
            //     flag = 0;
            //     PlayerPrefs.SetInt("CarMat_additional", _randomMat_additionalCar);
            // }
            // else
            // {
            //     flag = 1;
            // }
            if(_randomMat > 0)
            {
                _randomMat_additionalCar = Random.Range(0, _randomMat);
                PlayerPrefs.SetInt("CarMat_additional", _randomMat_additionalCar);
            }
            else
            {
                _randomMat_additionalCar = 1;
                PlayerPrefs.SetInt("CarMat_additional", _randomMat_additionalCar);
            }
            Player.GetComponent<Animation>().enabled = true;
            PlayerCar_Child.GetComponent<MeshRenderer>().material = CarMat[PlayerPrefs.GetInt("CarMat")];
            GameController.GetComponent<GameController>().PlayerCar_Left.GetComponent<MeshRenderer>().material = CarMat[PlayerPrefs.GetInt("CarMat_additional")];
            GameController.GetComponent<GameController>().PlayerCar_Right.GetComponent<MeshRenderer>().material = CarMat[PlayerPrefs.GetInt("CarMat_additional")];
            // GameController.GetComponent<GameController>().EnemyCar_Parent.transform.localPosition = new Vector3(GameController.GetComponent<GameController>().EnemyCar_Parent.transform.localPosition.x, -13.0f, GameController.GetComponent<GameController>().EnemyCar_Parent.transform.localPosition.z);
            // Camera.main.GetComponent<FluffyUnderware.Curvy.Examples.ChaseCam>().LookAt = GameController.GetComponent<GameController>().LookAt_HomePanel.transform;
            Camera.main.GetComponent<FluffyUnderware.Curvy.Examples.ChaseCam>().MoveTo = GameController.GetComponent<GameController>().MoveTo_HomePanel.transform;
            // Camera.main.GetComponent<FluffyUnderware.Curvy.Examples.ChaseCam>().RollTo = GameController.GetComponent<GameController>().LookAt_HomePanel.transform;
            gameCanvas.SetActive(false);
            Menucanvas.SetActive(true);
            GameController.GetComponent<GameController>().PlayerCar_Left.GetComponent<FrontbackMovement>().enabled = false;
            GameController.GetComponent<GameController>().PlayerCar_Right.GetComponent<FrontbackMovement>().enabled = false;
            GameController.GetComponent<GameController>()._applicationPause = false;
            if( PlayerPrefs.GetInt("HomePanel") == 1)
            {
                HomePanel.SetActive(true);
                MenuPanel.SetActive(false);
                yield return new WaitForSeconds(1.5f);
                HomePanel.SetActive(false);
                MenuPanel.SetActive(true);
                PlayerPrefs.SetInt("HomePanel",0);
            }
            else if( PlayerPrefs.GetInt("HomePanel") == 2 )
            {
                HomePanel.SetActive(false);
                MenuPanel.SetActive(false);
                SkinSelectionPanel.SetActive(true);
                PlayerPrefs.SetInt("HomePanel", 0);
            }
            else
            {
                HomePanel.SetActive(false);
                MenuPanel.SetActive(true);
                PlayerPrefs.SetInt("HomePanel",1);
            }
            // GameController.SetActive(false);
            Player.GetComponent<Controller>().enabled = true;
            GameController.GetComponent<ObstacleGen>().enabled = false;
            PlayerCar.GetComponent<PlayerCarMovement>().enabled = false;            
            controller.GetComponent<FluffyUnderware.Curvy.Controllers.SplineController>().enabled = false;
        }
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

    public void PlayBtn()
    {
        SceneManager.LoadScene(0);
        PlayerPrefs.SetInt("PlayGame",1);
    }

    public void QuitBtn()
    {
        QuitPanel.SetActive(true);
    }

    public void YesBtn()
    {
        Application.Quit();
    }

    public void NoBtn()
    {
        QuitPanel.SetActive(false);
    }

    public void SkinSelectBtn()
    {
        StoreScroll.verticalNormalizedPosition = 1;
        SkinSelectionPanel.SetActive(true);
        MenuPanel.SetActive(false);
    }
    public void StoreBtn()
    {
        StorePanel.SetActive(true);
        MenuPanel.SetActive(false);
    }
    public void StickerBtn()
    {
        StickersPanel.SetActive(true);
        MenuPanel.SetActive(false);
    }
    public void HomeBtn_Stickers()
    {
        StickersPanel.SetActive(false);
        MenuPanel.SetActive(true);
    }

    public GameObject _particle;
    public void HomeBtn_Menu()
    {
        MenuPanel.SetActive(true);
        SkinSelectionPanel.SetActive(false);
        // if( PlayerPrefs.GetInt("NoAd") == 1 )
        // {
        //     MenuPanel.SetActive(true);
        //     SkinSelectionPanel.SetActive(false);
        // }
        // else
        // {
        //     if( Application.internetReachability == NetworkReachability.NotReachable )
        //     {
        //         MenuPanel.SetActive(true);
        //         SkinSelectionPanel.SetActive(false);
        //     }
        //     else
        //     {
        //         MainMenu _mainmenuScript = FindObjectOfType<MainMenu>();
        //         _mainmenuScript.ShowInterstitial();
        //         StartCoroutine(HomeBtnSkin_Coroutine());
        //     }   
        // }
        for(int i = 0; i < GameController.GetComponent<GameController>().PlayerMat_List.Count; i++)
        {
            if(i == PlayerPrefs.GetInt("SkinSelected"))
            {
                Debug.Log(PlayerPrefs.GetInt("SkinSelected"));
               GameController.GetComponent<GameController>().PlayerParent.GetComponent<MeshRenderer>().material = GameController.GetComponent<GameController>().PlayerMat_List[i];
               if(GameController.GetComponent<GameController>().ParticleList[i] != null)
               {
                   if(_particle != null)
                   {
                       Destroy(_particle);
                       Debug.Log(_particle.name);
                       _particle = Instantiate(GameController.GetComponent<GameController>().ParticleList[i], new Vector3 (0,0,0), Quaternion.identity);
                        _particle.transform.parent = GameController.GetComponent<GameController>().PlayerParent.transform;
                        _particle.transform.localPosition = new Vector3(0, 0, 0);
                        _particle.transform.localRotation = Quaternion.Euler(0, 0, 0);
                        _particle.transform.localScale = new Vector3(1,1,1);
                   }
                   else
                   {
                       Destroy(_particle);
                       _particle = Instantiate(GameController.GetComponent<GameController>().ParticleList[i], new Vector3 (0,0,0), Quaternion.identity);
                        _particle.transform.parent = GameController.GetComponent<GameController>().PlayerParent.transform;
                        _particle.transform.localPosition = new Vector3(0, 0, 0);
                        _particle.transform.localRotation = Quaternion.Euler(0, 0, 0);
                        _particle.transform.localScale = new Vector3(1,1,1);
                   }
               }
            }
        }
    }
    IEnumerator HomeBtnSkin_Coroutine()
    {
        yield return new WaitForSeconds(1.8f);
        MenuPanel.SetActive(true);
        SkinSelectionPanel.SetActive(false);
    }

    public void HomeBtn_Store()
    {
        if( PlayerPrefs.GetInt("NoAd") == 1 )
        {
            StorePanel.SetActive(false);
            MenuPanel.SetActive(true);
        }
        else
        {
            if( Application.internetReachability == NetworkReachability.NotReachable )
            {
                StorePanel.SetActive(false);
                MenuPanel.SetActive(true);
            }
            else
            {
                MainMenu _mainmenuScript = FindObjectOfType<MainMenu>();
                _mainmenuScript.ShowInterstitial();
                StartCoroutine(HomeBtn_Coroutine());
            }
        }
    }
    IEnumerator HomeBtn_Coroutine()
    {
        yield return new WaitForSeconds(1.8f);
        StorePanel.SetActive(false);
        MenuPanel.SetActive(true);
    }

    public void OkBtn()
    {
        GameController _gcScript = FindObjectOfType<GameController>();
        _gcScript.InternetPanel.SetActive(false);
    }

    public void OkBtn_RemoveAds()
    {
        Purchaser _purchaserScript = FindObjectOfType<Purchaser>();
        _purchaserScript.RemoveAdPanel.SetActive(false);
    }
}
