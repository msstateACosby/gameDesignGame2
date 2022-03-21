using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
    GameObject playerInstantiatedPanel, computerInstantiatedPanel;
    
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
    float endGameTimer;
    bool endedGame = false;

    bool waitForComputerToFinishPlaying = false;
    float waitingTimer;

    BasicAI computerAI;

    [SerializeField]
    int testCharSelection = 0;

   

    
    // Start is called before the first frame update
    void Start()
    {
        foreach (Character ch in characters)
        {
            ch.initialize();
        }
        //placeholder selector if testing the battle scene.
        if (ScenePassInfo.charSelected == -1) playerCharacter = characters[testCharSelection];
        //otherwise choose what the player chose in the previous scene.
        else playerCharacter = characters[ScenePassInfo.charSelected];
        
        //clever way to exclude the player's chosen character from the range.
        int comCharId = Random.Range(0,  4);
        if (comCharId >= ScenePassInfo.charSelected)
        {
            Debug.Log(comCharId);
            comCharId += 1;
        }
        
        computerCharacter = characters[comCharId];

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
                waitForComputerToFinishPlaying = true;
                waitingTimer = 1f;
            }
        }
        if (endedGame)
        {
            endGameTimer -= Time.deltaTime;
            if (endGameTimer < 0)
            {
                SceneManager.LoadScene("Menu");
            }
        }
        if (waitForComputerToFinishPlaying)
        {
            waitingTimer -= Time.deltaTime;
            if (waitingTimer < 0)
            {
                waitForComputerToFinishPlaying = false;
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
        Destroy(spawnedObj.gameObject, 1);
    }
    void clickCard(int x)
    {
        if (isComptuterTurn || waitForComputerToFinishPlaying)
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

        playerGameObj.GetComponent<Animator>().Play(cardplayed.AnimationName);
        isComptuterTurn = true;
        timer = Random.Range(2.0f, 4.0f);
    }
    Card playCard(Character player, Character playee, int x)
    {
        Card cardPlayed = player.AvailableCards[x];
        defaultProcessCard.process(player.AvailableCards[x], player, playee);
        updatePanelGraphics();




        player.removeAvailableCard(x);

        if (playerCharacter.Health == 0)
        {
            endedGame = true;
            endGameTimer = 1f;
            ScenePassInfo.won = 0;
        }
        else if (computerCharacter.Health == 0)
        {
            endedGame = true;
            endGameTimer = 1f;
            ScenePassInfo.won = 1;
        }
        return cardPlayed;
    }
    void updatePanelGraphics()
    {
        
        
    
        
        playerInstantiatedPanel.transform.GetChild(0).GetComponent<Slider>().value = playerCharacter.Health;
        playerInstantiatedPanel.transform.GetChild(2).GetComponent<Text>().text = playerCharacter.Health.ToString() + "/" + playerCharacter.MaxHealth.ToString();
        playerInstantiatedPanel.transform.GetChild(3).GetComponent<Text>().text = "Shield: " + playerCharacter.Shield.ToString();
        playerInstantiatedPanel.transform.GetChild(4).GetComponent<Text>().text = "Powerup: " + playerCharacter.Powerup.ToString();

        computerInstantiatedPanel.transform.GetChild(0).GetComponent<Slider>().value = (float)computerCharacter.Health;
        computerInstantiatedPanel.transform.GetChild(2).GetComponent<Text>().text = "Shield: " + computerCharacter.Shield.ToString();
        computerInstantiatedPanel.transform.GetChild(3).GetComponent<Text>().text = "Powerup: " + computerCharacter.Powerup.ToString();
        
    }
    void createCharPanels()
    {
        GameObject spawnedPanel = Instantiate(playerPanel);
        spawnedPanel.transform.SetParent(mainCanvas.transform, false);

        spawnedPanel.transform.GetChild(1).GetComponent<Text>().text = playerCharacter.Name;
        Slider charSlider = spawnedPanel.transform.GetChild(0).GetComponent<Slider>();
        charSlider.maxValue = playerCharacter.MaxHealth;
        
        playerInstantiatedPanel = spawnedPanel;
        

        spawnedPanel = Instantiate(computerPanel);
        spawnedPanel.transform.SetParent(mainCanvas.transform, false);

        spawnedPanel.transform.GetChild(1).GetComponent<Text>().text = computerCharacter.Name;
        charSlider = spawnedPanel.transform.GetChild(0).GetComponent<Slider>();
        charSlider.maxValue = computerCharacter.MaxHealth;

        computerInstantiatedPanel = spawnedPanel;
        
        
        updatePanelGraphics();

    }
}
