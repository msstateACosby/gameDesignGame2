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
        
        createCards(characters[0]);
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void createCards (Character chosenCharacter)
    {
        int cardAmount = chosenCharacter.AvailableCards.Count;
        
        for (int x = 0; x < cardAmount; x++)
        {
            
            createCard(chosenCharacter, x, cardAmount);
        }

    }
    void createCard(Character chosenChar, int x, int totalCardAmount)
    {
        //this is pretty much all example code
        GameObject spawnedObj = Instantiate(cardObj);
        spawnedObj.transform.SetParent(mainCanvas.transform);
        RectTransform rectTrans = spawnedObj.GetComponent<RectTransform>();

        rectTrans.anchoredPosition = new Vector2((x - totalCardAmount/2 + ( (totalCardAmount % 2 ==0 ) ? .5f: 0) ) *rectTrans.sizeDelta.x, 0);
        
        spawnedObj.transform.GetChild(0).GetComponent<Text>().text = (chosenChar.AvailableCards[x].Title);
        spawnedObj.transform.GetChild(1).GetComponent<Text>().text = (chosenChar.AvailableCards[x].Description);
        spawnedObj.transform.GetChild(2).GetComponent<Text>().text = ("Attack " + chosenChar.AvailableCards[x].Damage.ToString());
    }
}
