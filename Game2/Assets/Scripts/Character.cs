using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Character
{
    [SerializeField]
    private string name;
    [SerializeField]
    private GameObject sprite;
    
    
    [SerializeField]
    private int maxHealth, shield, powerup;

    private int health;
    
    public string Name { get => name ; private set => name = value; }
    public GameObject CharSprite { get => sprite; private set => sprite = value; }
    public int Health { get => health; private set => health = value; }
    public int MaxHealth { get => maxHealth; private set=> health = value; }
    public int Shield {get => shield; private set => shield = value; }
    public int Powerup {get => powerup; private set => powerup = value;}
    //characters keep track of the overall list of cards, and the available cards.
    //The overall list is all the available cards
    //The available cards lists are the cards left in the hand to play.
    [SerializeField]
    Card[] Cards;

    //no need to serialize available cards because the currently available cards are not editied in the editor.
    List<Card> availableCards;
    public List<Card> AvailableCards { get => availableCards; private set => availableCards = value; }
    
    
    public Character(string name, int maxhealth, GameObject sprite, Card[] cards)
    {
        Name = name;
        maxHealth = maxhealth;
        health = maxHealth;
        CharSprite = sprite;
        Cards = cards;
        AvailableCards = new List<Card>(cards);
        

    }
    public void removeAvailableCard(int x)
    {
        availableCards.RemoveAt(x);
    }
    public void initialize()
    {
        AvailableCards = new List<Card>(Cards);
        health = maxHealth;
        
    }
    public void increaseHealth(int amount)
    {
        health += amount;
        if (health > maxHealth) health = maxHealth;

    }
    public void setShield(int amount)
    {
        shield = amount;
        
    }
    public void setPowerup(int amount)
    {
        powerup = amount;
        
    }
    


}