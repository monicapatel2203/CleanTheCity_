using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarScipt : MonoBehaviour
{
    public GameObject ExplosionParticle;
    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    void OnCollisionEnter( Collision coll )
    {
        GameController _gcontroler = FindObjectOfType<GameController>();
        ObstacleGen _obstacleGen = FindObjectOfType<ObstacleGen>();
        PropsGen _propsGen = FindObjectOfType<PropsGen>();
        if( coll.gameObject.tag == "Obstacle" )
        {
            StartCoroutine(_obstacleGen.StopObstacleGen());
            _gcontroler._playerRight_enable = true;
            Instantiate(ExplosionParticle, new Vector3( this.transform.position.x, this.transform.position.y + 1.5f, this.transform.position.z + 3.0f), Quaternion.identity);
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
            _gcontroler.EnemyCar_Parent.transform.localPosition = new Vector3(_gcontroler.EnemyCar_Parent.transform.localPosition.x, -14.5f, _gcontroler.EnemyCar_Parent.transform.localPosition.z);
            _gcontroler.GameControllerObj.GetComponent<ObstacleGen>().enabled = false;
            _gcontroler.GameControllerObj.GetComponent<GameController>().enabled = false;
            _gcontroler._applicationPause = false;
            _gcontroler.Controller.GetComponent<FluffyUnderware.Curvy.Controllers.SplineController>().enabled = false;
            _gcontroler.PlayerController.GetComponent<FluffyUnderware.Curvy.Controllers.SplineController>().enabled = false;
            _gcontroler.PlayerCarController.GetComponent<FluffyUnderware.Curvy.Controllers.SplineController>().enabled = false;
            Camera.main.GetComponent<FluffyUnderware.Curvy.Examples.ChaseCam>().LookAt = _gcontroler.LookAt.transform;
            Camera.main.GetComponent<FluffyUnderware.Curvy.Examples.ChaseCam>().MoveTo = _gcontroler.MoveTo.transform;
            _gcontroler.PlayerCar.GetComponent<PlayerCarMovement>().enabled = false;
            if( PlayerPrefs.GetInt ("SoundOn") == 0 )
            {
                _gcontroler.CarCrashAudio.Play();
            }
            else
            {
                _gcontroler.CarCrashAudio.Stop();
            }
            _gcontroler.BkAudio.Stop();
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
            _gcontroler.InGamePanel.SetActive(false);
            _gcontroler.GameOverPanel.SetActive(true);
            _gcontroler.Player.GetComponent<Controller>().enabled = false;
            _gcontroler.PlayerCar_Right.GetComponent<FrontbackMovement>().enabled = false;
            _gcontroler.PlayerCar_Left.GetComponent<FrontbackMovement>().enabled = false;
            Destroy(coll.gameObject);
        }
        else if( coll.gameObject.tag == "Props" )
        {
            StartCoroutine(_obstacleGen.StopObstacleGen());
            _gcontroler._playerRight_enable = true;
            Instantiate(ExplosionParticle, new Vector3( this.transform.position.x, this.transform.position.y + 1.5f, this.transform.position.z + 3.0f), Quaternion.identity);
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
            _gcontroler._applicationPause = false;
            _gcontroler.Controller.GetComponent<FluffyUnderware.Curvy.Controllers.SplineController>().enabled = false;
            _gcontroler.PlayerController.GetComponent<FluffyUnderware.Curvy.Controllers.SplineController>().enabled = false;
            _gcontroler.PlayerCarController.GetComponent<FluffyUnderware.Curvy.Controllers.SplineController>().enabled = false;
            Camera.main.GetComponent<FluffyUnderware.Curvy.Examples.ChaseCam>().LookAt = _gcontroler.LookAt.transform;
            Camera.main.GetComponent<FluffyUnderware.Curvy.Examples.ChaseCam>().MoveTo = _gcontroler.MoveTo.transform;
            _gcontroler.PlayerCar.GetComponent<PlayerCarMovement>().enabled = false;
            if( PlayerPrefs.GetInt ("SoundOn") == 0 )
            {
                _gcontroler.CarCrashAudio.Play();
            }
            else
            {
                _gcontroler.CarCrashAudio.Stop();
            }
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
            _gcontroler.InGamePanel.SetActive(false);
            _gcontroler.GameOverPanel.SetActive(true);
            _gcontroler.Player.GetComponent<Controller>().enabled = false;
            _gcontroler.PlayerCar_Right.GetComponent<FrontbackMovement>().enabled = false;
            _gcontroler.PlayerCar_Left.GetComponent<FrontbackMovement>().enabled = false;
            Destroy(coll.gameObject);
        }
        else if( coll.gameObject.tag == "Key" )
        {
            Destroy(coll.gameObject);
        }
        else if( coll.gameObject.tag == "Collectible" )
        {
            Destroy(coll.gameObject);
        }
    }
}
