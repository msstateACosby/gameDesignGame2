using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject wonObj;
    // Start is called before the first frame update
    void Start()
    {
        GameObject mainCanvas = GameObject.Find("MainCanvas");
        if (ScenePassInfo.won == -1)
        {
            GameObject menu = Instantiate(mainMenu);
            menu.transform.SetParent(mainCanvas.transform, false);
        }
        else
        {
            GameObject won = Instantiate(wonObj);
            won.transform.SetParent(mainCanvas.transform, false);
            if (ScenePassInfo.won == 0)
            {
                won.transform.GetChild(0).GetComponent<Text>().text = "You lost this round...";

            }
            else
            {
                won.transform.GetChild(0).GetComponent<Text>().text = "Congradulations, you have won!";
            }
            ScenePassInfo.won = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
