using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stickers : MonoBehaviour
{

    public List<Button> StickerList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
        for(int i = 0; i < StickerList.Count ; i++)
        {            
            if( i < PlayerPrefs.GetInt("Stickers") )
            {
                Debug.Log(PlayerPrefs.GetInt("Stickers") + "  i..." + i);
                StickerList[i].interactable = true;
            }
            else
            {
                StickerList[i].interactable = false;
            }
        }
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
}
