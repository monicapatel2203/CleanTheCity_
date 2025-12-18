using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdScript : MonoBehaviour
{
    public bool _claimAds_Reward;
    public GameObject ClaimReward, _targetPosition, claimReward_pos;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        _claimAds_Reward = false;
        PlayerPrefs.SetInt("KeyReward", 0);
        PlayerPrefs.SetInt("AdReward", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if( _claimAds_Reward == true )
        {
            DisplayImageLerp(ClaimReward);
        }   
    }

    public void GetKeyFun()
    {
        MainMenu _mainmenuScript = FindObjectOfType<MainMenu>();
        GameController _gcontroler = FindObjectOfType<GameController>();
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            _gcontroler.InternetPanel_Ingame.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetInt("KeyReward", 1);
            _mainmenuScript.PreloadOrShowRewardedInterstitial();
        }
    }

    public void AdsBtn()
    {
        MainMenu _mainmenuScript = FindObjectOfType<MainMenu>();
        GameController _gcontroler = FindObjectOfType<GameController>();
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            _gcontroler.InternetPanel_Ingame.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetInt("AdReward", 1);
            _gcontroler.DoubleDiamondBtn.GetComponent<Animation>().Stop();
            _mainmenuScript.PreloadOrShowRewardedInterstitial();
        }
    }

    public void InternetPanel_Close()
    {
        GameController _gcScript = FindObjectOfType<GameController>();
        _gcScript.InternetPanel_Ingame.SetActive(false);
    }

    public void CheckRewardFun()
    {
        if( PlayerPrefs.GetInt("AdReward") == 1 )
        {
            _claimAds_Reward = true;
            timer = 0;
            PlayerPrefs.SetInt("AdReward", 0);
        }
    }

    public void DisplayImageLerp(GameObject _referencedObj)
	{
        GameController _gcontroler = FindObjectOfType<GameController>();
        _referencedObj.SetActive(true);
        timer += Time.deltaTime;
		_referencedObj.transform.position = Vector2.Lerp (_referencedObj.transform.position, _targetPosition.transform.position, timer / 10.0f);
		if(timer > 1)
        {
            _referencedObj.SetActive(false);
            _referencedObj.transform.position = claimReward_pos.transform.position;
            int _diamondCount = PlayerPrefs.GetInt("DiamondCount") - _gcontroler._totalCollect_diamond;
            _diamondCount = _diamondCount * 2;
            _gcontroler.DiamondTxt_LC.text = _diamondCount.ToString();
            int _total_diamond = _gcontroler._totalCollect_diamond + _diamondCount;
            _gcontroler.DiamondTxt.text = _total_diamond.ToString();
            _gcontroler.DiamondTxt_Home.text = _total_diamond.ToString();
            PlayerPrefs.SetInt("DiamondCount", _total_diamond);
            _claimAds_Reward = false;
		}
	}

    public void Checkfun()
    {
        GameController _gcontroler = FindObjectOfType<GameController>();
        if( PlayerPrefs.GetInt("KeyReward") == 1 )
        {
            Debug.Log("     " + PlayerPrefs.GetInt("Key"));
            PlayerPrefs.SetInt("Key", PlayerPrefs.GetInt("Key") + 1);
            for( int i = 0; i < PlayerPrefs.GetInt("Key"); i++ )
            {
                if( i <= PlayerPrefs.GetInt("Key") )
                {
                    _gcontroler.Key[PlayerPrefs.GetInt("Key") - 1].GetComponent<Image>().enabled = true;
                    _gcontroler.Key_GO[PlayerPrefs.GetInt("Key") - 1].GetComponent<Image>().enabled = true;
                    _gcontroler.Key_GO[PlayerPrefs.GetInt("Key") - 1].transform.parent.GetComponent<Animation>().Play("ButtonAnim");
                }
                else
                {
                    _gcontroler.Key[PlayerPrefs.GetInt("Key") - 1].GetComponent<Image>().enabled = false;
                    _gcontroler.Key_GO[PlayerPrefs.GetInt("Key") - 1].GetComponent<Image>().enabled = false;
                }
            }
            if( PlayerPrefs.GetInt("Key") > 0 )
            {
                _gcontroler.KeyBtn.interactable = true;
            }
            else
            {
                _gcontroler.KeyBtn.interactable = false;
            }
            if( PlayerPrefs.GetInt("Key") <= 0 )
            {
                _gcontroler.GetKeyBtn.interactable = true;
                _gcontroler.GetKeyBtn.GetComponent<Animation>().Play("ButtonAnim");
            }
            else
            {
                _gcontroler.GetKeyBtn.interactable = false;
                _gcontroler.GetKeyBtn.GetComponent<Animation>().Stop();
            }
            PlayerPrefs.SetInt("KeyReward", 0);
        }
    }

}
