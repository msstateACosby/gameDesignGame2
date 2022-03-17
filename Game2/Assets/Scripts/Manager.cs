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

    [SerializeField]
    GameObject playerPanel;

    [SerializeField]
    GameObject computerPanel;
    GameObject playerInstantiantedPanel, computerInstantiatedPanel;
    
    Character playerCharacter;
    Character computerCharacter;

    GameObject playerGameObj;
    GameObject computerGameObj;

    List<GameObject> cards;

    ProcessCard defaultProcessCard;

    [SerializeField]
    Text playerActions, computerActions;


    bool isComptuterTurn;
    float timer;

    BasicAI computerAI;

   

    
    // Start is called before the first frame update
    void Start()
    {
        foreach (Character ch in characters)
        {
            ch.initialize();
        }
        //placeholder selector
        playerCharacter = characters[0];
        computerCharacter = characters[1];

        defaultProcessCard = new ProcessCard();
        computerAI = new BasicAI();

        createCharacters();

        cards = new List<GameObject>();

        createCards(playerCharacter);

        createCharPanels();
        
       

    }

    // Update is called once per frame
    void Update()
    {
        if (isComptuterTurn)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                Debug.Log("wha");
                computerPlayCard();
            }
        }
        
       
    }

    void createCharacters ()
    {  
        playerGameObj = Instantiate( playerCharacter.CharSprite, new Vector3(-4, 1, 0), Quaternion.identity);
        computerGameObj = Instantiate( computerCharacter.CharSprite, new Vector3(4, 1, 0), Quaternion.identity);

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
        string textString = "";
        if (chosenChar.AvailableCards[x].Damage != 0)
        {
            textString += "Attack: " + chosenChar.AvailableCards[x].Damage.ToString() + "\n";
        }
        if (chosenChar.AvailableCards[x].Heal != 0)
        {
            textString += "Heal: " + chosenChar.AvailableCards[x].Heal.ToString() + "\n";
        }
        if (chosenChar.AvailableCards[x].Shield != 0)
        {
            textString += "Shield: " + chosenChar.AvailableCards[x].Shield.ToString() + "\n";
        }
        if (chosenChar.AvailableCards[x].Powerup != 0)
        {
            textString += "Powerup: " + chosenChar.AvailableCards[x].Powerup.ToString() + "\n";
        }

        spawnedObj.transform.GetChild(2).GetComponent<Text>().text = (textString);
    }
    void computerPlayCard()
    {
        int chosenCard = computerAI.chooseCard(computerCharacter, playerCharacter);
        
        

        
        
        

        Card cardPlayed = playCard(computerCharacter, playerCharacter, chosenCard);
        displayActionText(computerActions, "Enemy Played Card: " + cardPlayed.Title);
        isComptuterTurn  = false ;
            
    }
    void displayActionText(Text textObj, string message)
    {
        Text spawnedObj = Instantiate(textObj);
        spawnedObj.transform.SetParent(mainCanvas.transform, false);
        spawnedObj.text = message;
        Destroy(spawnedObj.gameObject, 2);
    }
    void clickCard(int x)
    {
        if (isComptuterTurn)
        {
            //maybe display a message that it is not your turn.
            return;
        }
        foreach(GameObject card in cards)
        {
            Destroy(card);

        }
        Card cardplayed = playCard(playerCharacter, computerCharacter, x);
        
        cards = new List<GameObject>();
        createCards(playerCharacter);

        displayActionText(playerActions, "You Played Card: " + cardplayed.Title);
        isComptuterTurn = true;
        timer = Random.Range(2.0f, 4.0f);
    }
    Card playCard(Character player, Character playee, int x)
    {
        Card cardPlayed = player.AvailableCards[x];
        defaultProcessCard.process(player.AvailableCards[x], player, playee);
        updatePanelGraphics();




        player.removeAvailableCard(x);
        return cardPlayed;
    }
    void updatePanelGraphics()
    {
        
        
    
        
        playerInstantiantedPanel.transform.GetChild(0).GetComponent<Slider>().value = playerCharacter.Health;
        
        computerInstantiatedPanel.transform.GetChild(0).GetComponent<Slider>().value = (float)computerCharacter.Health;
        
    }
    void createCharPanels()
    {
        GameObject spawnedPanel = Instantiate(playerPanel);
        spawnedPanel.transform.SetParent(mainCanvas.transform, false);

        spawnedPanel.transform.GetChild(1).GetComponent<Text>().text = playerCharacter.Name;
        Slider charSlider = spawnedPanel.transform.GetChild(0).GetComponent<Slider>();
        charSlider.maxValue = playerCharacter.MaxHealth;
        
        playerInstantiantedPanel = spawnedPanel;
        

        spawnedPanel = Instantiate(computerPanel);
        spawnedPanel.transform.SetParent(mainCanvas.transform, false);

        spawnedPanel.transform.GetChild(1).GetComponent<Text>().text = computerCharacter.Name;
        charSlider = spawnedPanel.transform.GetChild(0).GetComponent<Slider>();
        charSlider.maxValue = computerCharacter.MaxHealth;

        computerInstantiatedPanel = spawnedPanel;
        
        
        updatePanelGraphics();

    }
}
