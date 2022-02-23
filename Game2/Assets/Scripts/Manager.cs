using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Manager : MonoBehaviour
{
    
    public Character[] characters;
    [SerializeField]
    Canvas mainCanvas;

    //probably refactor this later
    [SerializeField]
    GameObject cardObj;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Character ch in characters)
        {
            ch.initialize();
        }
        
        createCard();
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void createCard()
    {
        //this is pretty much all example code
        GameObject spawnedObj = Instantiate(cardObj);
        spawnedObj.transform.SetParent(mainCanvas.transform);
        RectTransform rectTrans = spawnedObj.GetComponent<RectTransform>();
        rectTrans.anchoredPosition = new Vector2(.5f, 0);
        
        spawnedObj.transform.GetChild(0).GetComponent<Text>().text = (characters[0].AvailableCards[0].Title);
        spawnedObj.transform.GetChild(1).GetComponent<Text>().text = (characters[0].AvailableCards[0].Description);
        spawnedObj.transform.GetChild(2).GetComponent<Text>().text = ("Attack " + characters[0].AvailableCards[0].Damage.ToString());
    }
}
