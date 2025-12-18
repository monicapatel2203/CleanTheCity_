using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

public class OnLevelFailed : MonoBehaviour
{
    void OnEnable()
    {
        GameController _gcScript = FindObjectOfType<GameController>();
        LevelUpScript _lvlScript = FindObjectOfType<LevelUpScript>();
        string strLevel = "LEVEL_" +  _gcScript.Current_LevelTxt.text;
        int score = int.Parse(_gcScript.ScoreTxt.text);
        int _currentLvl_diamond = PlayerPrefs.GetInt("DiamondCount") - _gcScript._totalCollect_diamond;
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, strLevel, score);
        GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "Diamonds", _currentLvl_diamond, "LEVEL_FAILED", strLevel);
        if(_lvlScript._keycollect)
        {
            GameAnalytics.NewDesignEvent("Key:Collected", 1);
        }
        else
        {
            GameAnalytics.NewDesignEvent("Key:Missed", 1);
        }
        if(_lvlScript._keyUsed)
        {
            GameAnalytics.NewDesignEvent("Key:Used", LevelUpScript._keyUsed_Value);
        }
    }
    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
