using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml; //Needed for XML functionality
using System.Xml.Serialization; //Needed for XML Functionality
using System.IO;
using System.Xml.Linq; //Needed for XDocument
using UnityEngine.EventSystems;
using GameAnalyticsSDK;

public class SkinSelectScript : MonoBehaviour
{
    string Skin_Name, SelectedDiamond;
    public GameObject UnlockPopup, collectiblePopup;
    public List<GameObject> SkinImgList, SkinList, LockImg, SkinCoinBtn;
    public Sprite Selected_blue, Selected_yellow;
    Image lock_image;
    public Text DiamondText_Skin;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(PlayerPrefs.GetInt("SkinSelected"));
        UnlockPopup.SetActive(false);
        // DiamondText_Skin.text = PlayerPrefs.GetInt("DiamondCount").ToString();
        string _filePath = Application.persistentDataPath +"/SkinDataFile.xml";
		XmlDocument xmlDoc = new XmlDocument ();
		if (File.Exists (_filePath)) 
		{
			xmlDoc.Load (_filePath);
			XmlNodeList roomList = xmlDoc.GetElementsByTagName ("Chat"); 

			foreach (XmlNode messageInfo in roomList) 
			{
				XmlNodeList messagecontent = messageInfo.ChildNodes;
				foreach (XmlNode chatInfo in messagecontent) 
				{					
					string temp = chatInfo.InnerText;
					string element_name = chatInfo.Name;

					for(int k = 0; k < SkinList.Count; k++)
					{
						if(element_name == SkinList[k].name)
						{
                            LockImg[k].SetActive(false);
							LockImg[k].GetComponent<Image>().enabled = false;
                            SkinCoinBtn[k].SetActive(false);
						}
					}
				}
			}
		}

        for(int i = 0; i < SkinImgList.Count; i++)
        {
            if(i == PlayerPrefs.GetInt("SkinSelected"))
            {
                SkinImgList[i].GetComponent<Image>().enabled = true;
                SkinImgList[i].GetComponent<Image>().sprite = Selected_blue;
                SkinList[i].GetComponent<Button>().onClick.Invoke();
            }
            else
            {
                SkinImgList[i].GetComponent<Image>().enabled = false;
            }
        }
    }

    void OnEnable()
    {
        DiamondText_Skin.text = PlayerPrefs.GetInt("DiamondCount").ToString();
        for(int i = 0; i < SkinImgList.Count; i++)
        {
            if(i == PlayerPrefs.GetInt("SkinSelected"))
            {
                SkinImgList[i].GetComponent<Image>().enabled = true;
                SkinImgList[i].GetComponent<Image>().sprite = Selected_blue;
                SkinList[i].GetComponent<Button>().onClick.Invoke();
            }
            else
            {
                SkinImgList[i].GetComponent<Image>().enabled = false;
            }
        }
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

    public void _IsPlayerPurchase(string _SelectedDiamond)          //Price of the blackhole skin function
    {
        SelectedDiamond = _SelectedDiamond;
    }
    public void SkinLockImage(Image _lockImage)                     //Lock on blackhole image
    {
        lock_image = _lockImage;
    }

    public void SkinSelect()
    {
        if(SelectedDiamond == "0")
        {
            PlayerPrefs.SetInt("SkinSelected", 0);
            SkinImgList[0].GetComponent<Image>().enabled = true;
            SkinImgList[0].GetComponent<Image>().sprite = Selected_blue;
        }
        else
        {
            if(lock_image.IsActive())
            {
                UnlockPopup.SetActive(true);
                for(int i = 0; i < SkinList.Count; i++)
                {
                    if(Skin_Name == SkinList[i].name)
                    {
                        SkinImgList[i].GetComponent<Image>().enabled = true;
                        SkinImgList[i].GetComponent<Image>().sprite = Selected_yellow;
                    }
                    else
                    {
                        SkinImgList[i].GetComponent<Image>().enabled = false;
                    }
                }
            }
            else
            {
                for(int i = 0; i < SkinList.Count; i++)
                {
                    Debug.Log(Skin_Name);
                    if(Skin_Name == SkinList[i].name)
                    {
                        PlayerPrefs.SetInt("SkinSelected", i);
                        SkinImgList[i].GetComponent<Image>().enabled = true;
                        SkinImgList[i].GetComponent<Image>().sprite = Selected_blue;
                        GameAnalytics.NewDesignEvent($"BlackHole:Unlocked:{Skin_Name}", 1);                         // When user change Blackhall Skin
                    }
                    else
                    {
                        SkinImgList[i].GetComponent<Image>().enabled = false;
                    }
                }
            }
        }
    }

    public void YesBtn()
    {
        GameController _gcController = FindObjectOfType<GameController>();
        int _collectedDiamond = PlayerPrefs.GetInt("DiamondCount");
        int _diamonds = int.Parse(SelectedDiamond);
        if( _collectedDiamond >= _diamonds )
        {
            UnlockPopup.SetActive(false);
            _collectedDiamond -= _diamonds;
            DiamondText_Skin.text = _collectedDiamond.ToString();
            _gcController.DiamondTxt.text = DiamondText_Skin.text;
            _gcController.DiamondTxt_Home.text = DiamondText_Skin.text;
            PlayerPrefs.SetInt("DiamondCount", PlayerPrefs.GetInt("DiamondCount") - _diamonds);

            for(int i = 0; i < SkinList.Count; i++)
            {
                if(Skin_Name == SkinList[i].name)
                {
                    Debug.Log(Skin_Name);
                    PlayerPrefs.SetInt("SkinSelected", i);
                    SkinImgList[i].GetComponent<Image>().enabled = true;
                    SkinImgList[i].GetComponent<Image>().sprite = Selected_blue;
                    PlayerPrefs.SetString("Skin_NameList",Skin_Name);
                    LockImg[i].SetActive(false);
                    SkinCoinBtn[i].SetActive(false);

                    /* GameAnalytics Data when use unlock new blackhole skin.... */
                    GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Diamonds", _diamonds, "Blackhall", Skin_Name); // When user SPENDS 450 Gems for black hall skins
                    GameAnalytics.NewDesignEvent($"BlackHole:Unlocked:{Skin_Name}", 1); // When New  Blackhall Skin Unlocked

                    PlayerXMLScript _XmlData = FindObjectOfType<PlayerXMLScript> ();		//To store player data in xml file when player is unlocked or purchased
				    _XmlData.CreateXMLFile_data ();
                }
                else
                {
                    SkinImgList[i].GetComponent<Image>().enabled = false;
                }
            }
        }
        else
        {
            MenuController _menucontroller = FindObjectOfType<MenuController>();
            _menucontroller.StorePanel.SetActive(true);
            _menucontroller.SkinSelectionPanel.SetActive(false);
            UnlockPopup.SetActive(false);
            // collectiblePopup.SetActive(true);
        }
    }

    public void NoBtn()
    {
        UnlockPopup.SetActive(false);
    }

    public void OkBtn()
    {
        collectiblePopup.SetActive(false);
    }

    public void SkinName(GameObject _skinName)
    {
        Skin_Name = _skinName.name;
        for(int i = 0; i < SkinList.Count; i++)
        {
            if(Skin_Name == SkinList[i].name)
            {
                SkinImgList[i].GetComponent<Image>().enabled = true;
                if(PlayerPrefs.GetInt("SkinSelected") == i)
                {
                    SkinImgList[i].GetComponent<Image>().sprite = Selected_blue;
                }
                else
                {
                    SkinImgList[i].GetComponent<Image>().sprite = Selected_yellow;
                }
            }
            else
            {
                SkinImgList[i].GetComponent<Image>().enabled = false;
            }
        }
    }

}
