using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Character
{
    [SerializeField]
    private string name;
    [SerializeField]
    private Sprite sprite;
    [SerializeField]
    private int health;
    public string Name { get; private set; }
    public Sprite CharSprite { get; private set; }
    public int Health { get; private set; }

    //characters keep track of the overall list of cards, and the available cards.
    //The overall list is all the available cards
    //The available cards lists are the cards left in the hand to play.
    [SerializeField]
    Card[] Cards;

    //no need to serialize available cards because the currently available cards are not editied in the editor.
    List<Card[]> availableCards;
    public List<Card> AvailableCards { get; private set; }
    
    
    public Character(string name, Sprite sprite, Card[] cards)
    {
        Name = name;
        CharSprite = sprite;
        Cards = cards;
        AvailableCards = new List<Card>(cards);
        

    }
    public void initialize()
    {
        AvailableCards = new List<Card>(Cards);
        
    }


}