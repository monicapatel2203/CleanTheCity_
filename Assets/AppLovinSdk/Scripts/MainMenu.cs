//
//  MainMenu.cs
//  Unity Plugin Demo
//
//  Created by Thomas So on 3/12/18.
//  Copyright © 2018 AppLovin. All rights reserved.
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	// Put AppLovin SDK Key here or in your AndroidManifest.xml / Info.plist
	private const string SDK_KEY = "LpT4Hy_S3KbPvmAIQEAt3G80PtiKPTQq3SyaF55TMOilEheiOtsSQs8hMpMNoBdLUK1XeczynaBkiKz8y6k8T5";

	// Rewarded Video Button Texts
	private const string REWARDED_VIDEO_BUTTON_TITLE_PRELOAD = "Preload Rewarded Video";
	private const string REWARDED_VIDEO_BUTTON_TITLE_LOADING = "Show Rewarded Video";
	private const string REWARDED_VIDEO_BUTTON_TITLE_SHOW = "Show Rewarded Video";

	// UI Components
	private Text RewardedVideoButtonTitle;
	private Text StatusText;
	public bool _claimAds_Reward;
	public GameObject ClaimReward, _targetPosition, claimReward_pos;
    float timer;

	// Controlled State
	private bool IsPreloadingRewardedVideo = false;

	void Start ()
	{
		_claimAds_Reward = false;
		// // Setup UI
		// RewardedVideoButtonTitle = GameObject.Find ("RewardedVideoButtonTitle").GetComponent<Text> ();
		// RewardedVideoButtonTitle.text = REWARDED_VIDEO_BUTTON_TITLE_PRELOAD;
		// StatusText = GameObject.Find ("StatusText").GetComponent<Text> ();
		// StatusText.text = "";

		// Check if user replaced the SDK key
		if ("YOUR_SDK_KEY_HERE".Equals (SDK_KEY)) {
			// StatusText.text = "ERROR: PLEASE UPDATE YOUR SDK KEY IN Assets/MainMenu.cs";
		} else {
			// Set SDK key and initialize SDK
			AppLovin.SetSdkKey (SDK_KEY);
			AppLovin.InitializeSdk ();
			AppLovin.SetUnityAdListener ("Main Camera");
			AppLovin.SetRewardedVideoUsername ("demo_user");

			AppLovin.PreloadInterstitial ();

			IsPreloadingRewardedVideo = true;
			AppLovin.LoadRewardedInterstitial ();
		}
	}

	// Update is called once per frame
    void Update()
    {
        if( _claimAds_Reward == true )
        {
            DisplayImageLerp(ClaimReward);
        }   
    }

	public void PreloadInterstitialads ()
	{
		Log ("Preload  Interstitial ad");

		// Optional: You can call `AppLovin.PreloadInterstitial()` and listen to the "LOADED" event to preload the ad from the network before showing it
		AppLovin.PreloadInterstitial ();
	}

	public void ShowInterstitial ()
	{
		Log ("Showing interstitial ad");

		// Optional: You can call `AppLovin.PreloadInterstitial()` and listen to the "LOADED" event to preload the ad from the network before showing it
		AppLovin.ShowInterstitial ();
	}

	public void PreloadRewardedInterstitial ()
	{
		Log ("Preloading rewarded ad...");

		IsPreloadingRewardedVideo = true;
		RewardedVideoButtonTitle.text = REWARDED_VIDEO_BUTTON_TITLE_LOADING;

		AppLovin.LoadRewardedInterstitial ();
	}

	public void PreloadOrShowRewardedInterstitial ()
	{
		if (AppLovin.IsIncentInterstitialReady ()) {

			Log ("Showing rewarded ad...");

			IsPreloadingRewardedVideo = false;
			// RewardedVideoButtonTitle.text = REWARDED_VIDEO_BUTTON_TITLE_PRELOAD;

			AppLovin.ShowRewardedInterstitial ();
		} else {

			Log ("Preloading rewarded ad...");

			IsPreloadingRewardedVideo = true;
			// RewardedVideoButtonTitle.text = REWARDED_VIDEO_BUTTON_TITLE_LOADING;

			AppLovin.LoadRewardedInterstitial ();
		}
	}

	public void ShowBanner ()
	{
		Log ("Showing banner ad");
		AppLovin.ShowAd (AppLovin.AD_POSITION_CENTER, AppLovin.AD_POSITION_BOTTOM);
	}

	private void onAppLovinEventReceived (string ev)
	{
		// Log AppLovin event
		Log (ev);

		//
		// Special Handling for Rewarded events
		//

		if (ev.Contains ("REWARD")) {
			if (ev.Equals ("REWARDAPPROVEDINFO")) {
				// Process an event like REWARDAPPROVEDINFO:100:Credits
				char[] delimiter = { '|' };
				string[] split = ev.Split (delimiter);

				// Pull out the amount of virtual currency.
				double amount = double.Parse (split [1]);

				// Pull out the name of the virtual currency
				string currencyName = split [2];

				// Do something with this info - for example, grant coins to the user
				// myFunctionToUpdateBalance(currencyName, amount);

				Log ("Rewarded " + amount + " " + currencyName);
			}
		}
		if(ev.Contains("HIDDENREWARDED")) 
		{
			// A rewarded video has been closed.  Preload the next rewarded video.
			// AppLovin.LoadRewardedInterstitial();
			Log("Reward Ad gain...");
			GameController _gcontroler = FindObjectOfType<GameController>();

			/* To get Extra key */
			if( PlayerPrefs.GetInt("KeyReward") == 1 )
			{
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
					_gcontroler.KeyBtn.GetComponent<Animation>().enabled = true;
				}
				else
				{
					_gcontroler.KeyBtn.interactable = false;
					_gcontroler.KeyBtn.GetComponent<Animation>().enabled = false;
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

			/* Get Extra diamonds */
			if( PlayerPrefs.GetInt("AdReward") == 1 )
			{
				_gcontroler.DoubleDiamondBtn.interactable = false;
				_claimAds_Reward = true;
				timer = 0;
				PlayerPrefs.SetInt("AdReward", 0);
			}
    	}
		// Check if this is a Rewarded Video preloading event
		else if (IsPreloadingRewardedVideo && (ev.Equals ("LOADED") || ev.Equals ("LOADFAILED"))) {

			IsPreloadingRewardedVideo = false;

			if (ev.Equals ("LOADED")) {
				// RewardedVideoButtonTitle.text = REWARDED_VIDEO_BUTTON_TITLE_SHOW;
			} else {
				// RewardedVideoButtonTitle.text = REWARDED_VIDEO_BUTTON_TITLE_PRELOAD;
			}
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

	private void Log (string message)
	{
		// StatusText.text = message;
		Debug.Log (message);
	}
}
