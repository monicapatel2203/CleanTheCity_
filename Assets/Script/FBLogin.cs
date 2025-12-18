using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using System;

public class FBLogin : MonoBehaviour
{
    // public GameObject controllerObject;
    void Awake()
    {
       if (!FB.IsInitialized)
       {
           // Initialize the Facebook SDK
           FB.Init(InitCallback, OnHideUnity);
       }
       else
       {
           // Already initialized, signal an app activation App Event
           FB.ActivateApp();
       }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitCallback()
    {
       if (FB.IsInitialized)
       {
           // Signal an app activation App Event
           FB.ActivateApp();
           // Continue with Facebook SDK
           // ...
       }
       else
       {
           Debug.Log("Failed to Initialize the Facebook SDK");
       }
    }

    public void OnFBLogin()
    {
       var perms = new List<string>() { "public_profile", "email" };
       FB.LogInWithReadPermissions(perms, AuthCallback);
    }
    

    public void OnFBLogout()
    {
        FB.LogOut();
    }


    private void AuthCallback(ILoginResult result)
    {
       Debug.Log("Is user loged in with FB or not.....!"+ FB.IsLoggedIn);
       if (FB.IsLoggedIn)
       {
           // AccessToken class will have session details
           var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
           // Print current access token's User ID
           Debug.Log(aToken.UserId);
           // Print current access token's granted permissions
           foreach (string perm in aToken.Permissions)
           {
               Debug.Log(perm);
           }
       }
       else
       {
           Debug.Log("User cancelled login");
       }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }

    public void OnFBShare()
    {
        #if UNITY_ANDROID
        FB.ShareLink(new Uri("https://play.google.com/store/apps/details?id=com.se7en.wyh&hl=en"),callback: ShareCallback);
		#elif UNITY_IPHONE
		FB.ShareLink(new Uri("https://apps.apple.com/us/app/wash-your-hands-reminder-byguy/id1508263676?ls=1"),callback: ShareCallback);
		#endif
    }



    private void ShareCallback (IShareResult result) 
    {
        if (result.Cancelled || !String.IsNullOrEmpty(result.Error)) 
        {
            Debug.Log("ShareLink Error: "+result.Error);
        } 
        else if (!String.IsNullOrEmpty(result.PostId))
        {
            // Print post identifier of the shared content
            Debug.Log(result.PostId);
        } 
        else 
        {
            // Share succeeded without postID
            Debug.Log("ShareLink success!");
            // controllerObject.GetComponent<Controller>().FBSharePanelClose();
        }
    }
}
