using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject CollectibleParticle, KeyParticle, HoleParticle, _playreDuplicate, EnemyCar;
    public Text KeyText;
    public bool _playerdie;
    // Start is called before the first frame update
    void Start()
    {
        _playerdie = false;
        KeyText.text = PlayerPrefs.GetInt("Key").ToString();
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

    void OnCollisionEnter( Collision coll )
    {
        GameController _gcontroler = FindObjectOfType<GameController>();
        ObstacleGen _obstacleGen = FindObjectOfType<ObstacleGen>();
        PropsGen _propsGen = FindObjectOfType<PropsGen>();
        MenuController _menuScript = FindObjectOfType<MenuController>();
        if( coll.gameObject.tag == "Obstacle" )
        {
            // Destroy(this.gameObject);
            if (PlayerPrefs.GetInt ("SoundOn") == 0)
            {
                _gcontroler.ObstacleCube.Play();
            }
            else
            {
                _gcontroler.ObstacleCube.Stop();
            }
            coll.gameObject.GetComponent<Rigidbody>().mass = 0f;
            coll.gameObject.GetComponent<Rigidbody>().drag = -20.0f;
            GameObject _burstParticle = Instantiate(coll.gameObject.GetComponent<ObstacleDestroy>()._obstacleParticle, new Vector3( coll.gameObject.transform.position.x, coll.gameObject.transform.position.y - 0.5f, coll.gameObject.transform.position.z ), Quaternion.identity );
            _burstParticle.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
            coll.gameObject.GetComponent<BoxCollider>().enabled = false;
            coll.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);            
            GameController.score = GameController.score + 1;
            _gcontroler.ScoreTxt.text = GameController.score.ToString();
        }
        else if( coll.gameObject.tag == "Props" )
        {
            if (PlayerPrefs.GetInt ("SoundOn") == 0)
            {
                _gcontroler.Stonebreak.Play();
            }
            else
            {
                _gcontroler.Stonebreak.Stop();
            }
            Instantiate( _propsGen.PropsParticle, new Vector3( coll.transform.position.x, coll.transform.position.y, coll.transform.position.z ), Quaternion.identity );
            Destroy(coll.gameObject);
            Destroy(coll.transform.parent.gameObject);
        }
        else if( coll.gameObject.tag == "Collectible" )
        {
            Instantiate( CollectibleParticle, new Vector3( coll.transform.position.x, coll.transform.position.y + 1.0f, coll.transform.position.z ), Quaternion.identity );
            Destroy(coll.gameObject);
            // GameController._diamondCount = GameController._diamondCount + 1;
            PlayerPrefs.SetInt("DiamondCount", PlayerPrefs.GetInt("DiamondCount") + 1);
            _gcontroler.DiamondTxt.text = PlayerPrefs.GetInt("DiamondCount").ToString();
        }
        else if( coll.gameObject.tag == "Key" )
        {
            Instantiate( KeyParticle, new Vector3( coll.transform.position.x, coll.transform.position.y + 1.0f, coll.transform.position.z ), Quaternion.identity );
            Destroy(coll.gameObject);
            LevelUpScript _lvlScript = FindObjectOfType<LevelUpScript>();
            _lvlScript._keycollect = true;
            PlayerPrefs.SetInt("Key", PlayerPrefs.GetInt("Key") + 1);
            for( int i = 0; i < PlayerPrefs.GetInt("Key"); i++ )
            {
                if( i <= PlayerPrefs.GetInt("Key") )
                {
                    _gcontroler.Key[PlayerPrefs.GetInt("Key") - 1].GetComponent<Image>().enabled = true;
                    _gcontroler.Key_GO[PlayerPrefs.GetInt("Key") - 1].GetComponent<Image>().enabled = true;
                }
                else
                {
                    _gcontroler.Key[PlayerPrefs.GetInt("Key") - 1].GetComponent<Image>().enabled = false;
                    _gcontroler.Key_GO[PlayerPrefs.GetInt("Key") - 1].GetComponent<Image>().enabled = false;
                }
            }
        }
        else if( coll.gameObject.tag == "brokenroad_coll" )
        {
            StartCoroutine(_obstacleGen.StopObstacleGen());
            PlayerPrefs.SetInt("brokenRoad_coll", 1);
            Debug.Log("Player Collided with broken road...");
            EnemyCar.transform.localPosition = new Vector3(EnemyCar.transform.localPosition.x, -15.0f, EnemyCar.transform.localPosition.z);
            _playerdie = true;
            // _gcontroler.KeyBtn.GetComponent<Image>().enabled = false;
            _gcontroler.Player.GetComponent<Controller>().enabled = false;
            _gcontroler._applicationPause = false;
            _gcontroler._playerRight_enable = true;
            // StopCoroutine(_gcontroler.CountDown_KeyBtn());
            this.gameObject.SetActive(false);
            GameObject _particle = Instantiate(HoleParticle);
            _particle.transform.parent = _gcontroler.Player.transform;
            _particle.transform.localPosition = new Vector3(this.transform.localPosition.x + 0.3f, this.transform.localPosition.y, this.transform.localPosition.z - 0.2f);
            _particle.transform.localRotation = Quaternion.Euler(-90.0f, 0, 0);
            _particle.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            _playreDuplicate = Instantiate(this.gameObject, new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, this.transform.localPosition.z),  Quaternion.identity);
            _playreDuplicate.transform.parent = _gcontroler.Player.transform;
            _playreDuplicate.SetActive(true);
            _playreDuplicate.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, this.transform.localPosition.z);
            _playreDuplicate.transform.localRotation = Quaternion.Euler(90.0f, 0, 0);
            _playreDuplicate.GetComponent<Rigidbody>().drag = 20.0f;
            _playreDuplicate.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            _playreDuplicate.gameObject.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
            _playreDuplicate.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            GameObject[] Obstacle_generate = GameObject.FindGameObjectsWithTag("Obstacle");
            foreach (GameObject obstacle in Obstacle_generate)
            {
                GameObject.Destroy(obstacle);
            }
            GameObject[] Props_generated = GameObject.FindGameObjectsWithTag("Props");
            foreach (GameObject props in Props_generated)
            {
                GameObject.Destroy(props);
            }
            _gcontroler.GameControllerObj.GetComponent<ObstacleGen>().enabled = false;
            _gcontroler.GameControllerObj.GetComponent<GameController>().enabled = false;
            _gcontroler.Controller.GetComponent<FluffyUnderware.Curvy.Controllers.SplineController>().enabled = false;
            _gcontroler.PlayerController.GetComponent<FluffyUnderware.Curvy.Controllers.SplineController>().enabled = false;
            _gcontroler.PlayerCarController.GetComponent<FluffyUnderware.Curvy.Controllers.SplineController>().enabled = false;
            // Camera.main.GetComponent<FluffyUnderware.Curvy.Examples.ChaseCam>().LookAt = _gcontroler.LookAt.transform;
            Camera.main.GetComponent<FluffyUnderware.Curvy.Examples.ChaseCam>().MoveTo = _gcontroler.MoveTo.transform;
            _gcontroler.PlayerCar.GetComponent<PlayerCarMovement>().enabled = false;
            _gcontroler.BkAudio.Stop();
            if( PlayerPrefs.GetInt("Key") > 0 )
            {
                _gcontroler.KeyBtn.interactable = true;
                _gcontroler.KeyBtn.GetComponent<Animation>().enabled = true;
            }
            else
            {
                _gcontroler.KeyBtn.interactable = false;
                _gcontroler.KeyBtn.GetComponent<Animation>().enabled = false;
            }
            if( PlayerPrefs.GetInt("Key") <= 0 )
            {
                _gcontroler.GetKeyBtn.GetComponent<Animation>().enabled = true;
                _gcontroler.GetKeyBtn.interactable = true;
            }
            else
            {
                _gcontroler.GetKeyBtn.interactable = false;
                _gcontroler.GetKeyBtn.GetComponent<Animation>().enabled = false;
            }
            _gcontroler.Player.GetComponent<Controller>().enabled = false;
            Invoke("EnablePanel", 2.0f);
        }
    }

    void EnablePanel()
    {
        GameController _gcontroler = FindObjectOfType<GameController>();
        _gcontroler.InGamePanel.SetActive(false);
        _gcontroler.GameOverPanel.SetActive(true);
        int score = int.Parse(_gcontroler.ScoreTxt.text);
        _gcontroler.ScoreTxt_GO.text = score.ToString();
        if( score > PlayerPrefs.GetInt("HighScore") )
        {
            _gcontroler.HighScoreTxt_GO.text = score.ToString();
            PlayerPrefs.SetInt("HighScore", score);
        }
        else
        {
            _gcontroler.HighScoreTxt_GO.text = PlayerPrefs.GetInt("HighScore").ToString();
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        GameController _gcScript = FindObjectOfType<GameController>();
        PlayerCarMovement _movementScript = FindObjectOfType<PlayerCarMovement>();
        EnemyCarScript enemyCarScript = FindObjectOfType<EnemyCarScript>();
        BrokenPartGenScript _brokenGenScript = FindObjectOfType<BrokenPartGenScript>();
        if( coll.gameObject.tag == "EntranceCollider" )
        {
            // Debug.Log("Enter...");
            // this.transform.parent.GetComponent<Animation>().Play("ScaleDown");
            if( _brokenGenScript.count == 0 )
            {
                // _gcScript.PlayerCar_Right.GetComponent<FrontbackMovement>().enabled = false;
                _gcScript.PlayerCar_Right.GetComponent<FrontbackMovement>().min = -15.0f;
                _gcScript.PlayerCar_Right.GetComponent<FrontbackMovement>().max = -4.0f;
                _gcScript.PlayerCar_Right.SetActive(false);
                // _gcScript.PlayerCar_Right.GetComponent<FrontbackMovement>().speed = 1.0f;
            }
            else if(_brokenGenScript.count == 1)
            {
                _gcScript.PlayerCar_Left.GetComponent<FrontbackMovement>().min = -15.0f;
                _gcScript.PlayerCar_Left.GetComponent<FrontbackMovement>().max = -5.0f;
                _gcScript.PlayerCar_Left.SetActive(false);
                _gcScript._playerLeft_enable = true;
                // _gcScript.PlayerCar_Left.GetComponent<FrontbackMovement>().enabled = false;
            }
            else if( _brokenGenScript.count == 2 )
            {
                _gcScript.PlayerCar_Right.GetComponent<FrontbackMovement>().min = -15.0f;
                _gcScript.PlayerCar_Right.GetComponent<FrontbackMovement>().max = -10.0f;
                _gcScript.PlayerCar_Right.SetActive(false);
            }            
        }
        else if( coll.gameObject.tag == "ExitCollider" )
        {
            enemyCarScript._patternMove = false;
            // Debug.Log("Exit...");
            // this.transform.parent.GetComponent<Animation>().Play("ScaleUp");
        }
        else if( coll.gameObject.tag == "Entrance_hole" )
        {            
            PlayerPrefs.SetInt("brokenRoad_coll", 1);
            Debug.Log(PlayerPrefs.GetInt("brokenRoad_coll"));
        }
        else if( coll.gameObject.tag == "Exit_hole" )
        {
            PlayerPrefs.SetInt("brokenRoad_coll", 0);
        }
        else if( coll.gameObject.tag == "ExitCollider_Straight" )
        {
            enemyCarScript._patternMove = false;
        }
    }
}
