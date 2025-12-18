using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic; //Needed for Lists
using System.Xml; //Needed for XML functionality
using System.Xml.Serialization; //Needed for XML Functionality
using System.IO;
using System.Xml.Linq; //Needed for XDocument

public class PlayerXMLScript : MonoBehaviour
{
    public List<string> _elementName = new List<string>();
	float xmlFileLength;
	XmlDocument xmlDoc;

    // Start is called before the first frame update
    void Start()
    {
        xmlDoc = new XmlDocument ();
	    CreateXMLFile_data (); 
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

    public void CreateXMLFile_data()
	{
		string filepath = Application.persistentDataPath +"/SkinDataFile.xml";
		FileStream stream;// = new FileStream ();
		if (File.Exists (filepath))
		{
			stream = new FileStream (Application.persistentDataPath + "/SkinDataFile.xml", FileMode.Open);
		}
		else
		{
			stream = new FileStream (Application.persistentDataPath + "/SkinDataFile.xml", FileMode.Create);
		}

		xmlFileLength = stream.Length;
		stream.Close ();
		SaveDataToXML ();
	}
	XmlElement elmRootChat;

	public void SaveDataToXML()
	{
		string filepath = Application.persistentDataPath +"/SkinDataFile.xml";
		Debug.Log ("filepath...." + filepath);

		//XmlDocument xmlDoc = new XmlDocument ();
		if (xmlFileLength == 0.00) 
		{
			xmlDoc.LoadXml ("<Chat></Chat>");
			elmRootChat = xmlDoc.DocumentElement;
			//			for(int i=1; i<=10; i++)
			//			{
			EnterDataToXml (elmRootChat);
			//			}
		}
		else
		{
			xmlDoc.Load (filepath);
			elmRootChat = xmlDoc.DocumentElement;
			//			for (int i = 1; i <= 10; i++) 
			//			{
			EnterDataToXml (elmRootChat);
			//			}
		}
		xmlDoc.Save(filepath);																		// save file
	}

    public void EnterDataToXml(XmlElement rm,string msg="")
	{

		if(PlayerPrefs.GetString("Skin_NameList") == "")
		{
			XmlNodeList roomList = xmlDoc.GetElementsByTagName ("Chat"); 

			foreach (XmlNode messageInfo in roomList) 
			{
				XmlNodeList messagecontent = messageInfo.ChildNodes;
				// Debug.Log (messagecontent.Count);
				if (messagecontent.Count > 0) 
				{
					for (int i = 0; i < messagecontent.Count; i++) {
						if (messagecontent [i].Name != "Skin_1") {
							XmlElement messages = xmlDoc.CreateElement ("Skin_1"); 				// create the Message Node.
							messages.SetAttribute ("ChatType", "true"); 							// Attribute Set
							messages.InnerText = "message"; 										// Message Node InnerText Msg1.
							rm.AppendChild (messages);					
						}
					}
				}
				else
				{
					XmlElement messages = xmlDoc.CreateElement("Skin_1"); 				// create the Message Node.
					messages.SetAttribute("ChatType","true"); 							// Attribute Set
					messages.InnerText = "message"; 									// Message Node InnerText Msg1.
					rm.AppendChild (messages);	
				}
			}
		}
		else
		{	

			XmlNodeList roomList = xmlDoc.GetElementsByTagName ("Chat"); 

			foreach (XmlNode messageInfo in roomList) 
			{
				XmlNodeList messagecontent = messageInfo.ChildNodes;
				for (int i = 0; i < messagecontent.Count; i++) 
				{					
					_elementName.Add (messagecontent[i].Name);
				}
				if (_elementName.Contains (PlayerPrefs.GetString ("Skin_NameList"))) 
				{
					Debug.Log ("Already Presents");
				} 
				else 
				{
					XmlElement messages = xmlDoc.CreateElement(PlayerPrefs.GetString("Skin_NameList")); 				// create the Message Node.

					Debug.Log("PlayerPrefs.GetString.... "+ PlayerPrefs.GetString("Skin_NameList"));
					messages.SetAttribute("ChatType","true"); 														// Attribute Set
					messages.InnerText = "message"; 																// Message Node InnerText Msg1.
					rm.AppendChild (messages);	
				}
			}

		}
	}

}
