using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    public Sprite SoundOn, SoundOff; // Sound On Off Button...
    public Button MusicBtn_home, MusicBtn_LC, MusicBtn_GO;
    public AudioSource BtnSource;
    // Start is called before the first frame update
    void Start()
    {
        GameController _gcScript = FindObjectOfType<GameController>();
        if (PlayerPrefs.GetInt ("SoundOn") == 0)
        {
            _gcScript.BkAudio.enabled = true;
            MusicBtn_home.transform.GetComponent<Image> ().sprite = SoundOn;
            MusicBtn_LC.transform.GetComponent<Image> ().sprite = SoundOn;
            MusicBtn_GO.transform.GetComponent<Image> ().sprite = SoundOn;
        }
        else
        {
            _gcScript.BkAudio.enabled = false;
            MusicBtn_home.transform.GetComponent<Image> ().sprite = SoundOff;
            MusicBtn_LC.transform.GetComponent<Image> ().sprite = SoundOff;
            MusicBtn_GO.transform.GetComponent<Image> ().sprite = SoundOff;
        }
    }

    // public void OnSoundOnOff()
    // {
    //     StartCoroutine(OnSoundOnOffCoroutine());
    // }

    public void OnSoundOnOffCoroutine()
    {
        // yield return new WaitForSeconds(0.8f);
        GameController _gcScript = FindObjectOfType<GameController>();
        if (PlayerPrefs.GetInt ("SoundOn") == 0)
		{
			PlayerPrefs.SetInt ("SoundOn", 1);
			MusicBtn_home.transform.GetComponent<Image> ().sprite = SoundOff;
            MusicBtn_LC.transform.GetComponent<Image> ().sprite = SoundOff;
            MusicBtn_GO.transform.GetComponent<Image> ().sprite = SoundOff;
            _gcScript.BkAudio.enabled = false;
		}
		else
		{
			MusicBtn_home.transform.GetComponent<Image> ().sprite = SoundOn;
            MusicBtn_LC.transform.GetComponent<Image> ().sprite = SoundOn;
            MusicBtn_GO.transform.GetComponent<Image> ().sprite = SoundOn;
			PlayerPrefs.SetInt ("SoundOn", 0);
            _gcScript.BkAudio.enabled = true;
		}
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    public void PlaySound()
	{
		if (PlayerPrefs.GetInt ("SoundOn") == 0)
		{
			BtnSource.Play();
		}
		else
		{
			BtnSource.Stop();
		}
	}
}
