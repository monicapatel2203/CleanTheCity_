using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyObstacle", 8.0f);   
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    void DestroyObstacle()
    {
        Destroy(this.gameObject);
    }

    void OnCollisionEnter( Collision coll )
    {
        GameController _gcontroler = FindObjectOfType<GameController>();
        ObstacleGen _obstacleGen = FindObjectOfType<ObstacleGen>();
        if( coll.gameObject.tag == "Player" )
        {
            Destroy(this.gameObject);
            GameController.score = GameController.score + 1;
            _gcontroler.ScoreTxt.text = GameController.score.ToString();
        }
        else if( coll.gameObject.tag == "PlayerCar" )
        {
            StartCoroutine(_obstacleGen.StopObstacleGen());
            _gcontroler.GameControllerObj.GetComponent<ObstacleGen>().enabled = false;
            _gcontroler.GameControllerObj.GetComponent<GameController>().enabled = false;
            // _gcontroler.GameControllerObj.GetComponent<GameController>().MovingEnemyCar.GetComponent<EnemyCarMovement_2>().enabled = false;
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
            _gcontroler.GameOverPanel.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
