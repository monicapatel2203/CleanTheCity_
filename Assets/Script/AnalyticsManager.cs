using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;
public class AnalyticsManager : MonoBehaviour
{
    private bool _isIntialize = false;
    private AnalyticsManager Instance;

    void Awake()
    {
        if(!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {        
        if(!_isIntialize)
        {
            _isIntialize = true;
            GameAnalytics.Initialize();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
