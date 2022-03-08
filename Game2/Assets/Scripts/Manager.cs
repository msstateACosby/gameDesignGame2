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
    
    Character playerCharacter;
    Character computerCharacter;

    List<GameObject> cards;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Character ch in characters)
        {
            ch.initialize();
        }
        //placeholder selector
        playerCharacter = characters[0];
        cards = new List<GameObject>();

        createCards(playerCharacter);
        

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
        cards.Add(spawnedObj);
        spawnedObj.transform.SetParent(mainCanvas.transform);
        RectTransform rectTrans = spawnedObj.GetComponent<RectTransform>();

        Button button = spawnedObj.GetComponent<Button>();
        button.onClick.AddListener(delegate{clickCard(x);});

        rectTrans.anchoredPosition = new Vector2((x - totalCardAmount/2 + ( (totalCardAmount % 2 ==0 ) ? .5f: 0) ) *rectTrans.sizeDelta.x, 0);
        
        spawnedObj.transform.GetChild(0).GetComponent<Text>().text = (chosenChar.AvailableCards[x].Title);
        spawnedObj.transform.GetChild(1).GetComponent<Text>().text = (chosenChar.AvailableCards[x].Description);
        spawnedObj.transform.GetChild(2).GetComponent<Text>().text = ("Attack " + chosenChar.AvailableCards[x].Damage.ToString());
    }
    void clickCard(int x)
    {
        Debug.Log(x);
        foreach(GameObject card in cards)
        {
            Destroy(card);

        }
        playerCharacter.removeAvailableCard(x);
        cards = new List<GameObject>();
        createCards(playerCharacter);
    }
}
