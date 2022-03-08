using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Unity Editor wants this to edit instances of the class within the editor.
[System.Serializable]
public class Card
{
    // so these are essentially class variables but c# lets you do some interesting "property" syntax. 
    //Basically allows for these to be generally accessible, but only settable privately.
    //Generally safer, especially working in groups where everyone might not know what is and isnt safe to change
    //doesnt really apply for this, but you can imagine some code might result in someone accidently overriting the reference to the rendering 
    //Fluff info about cards
    [SerializeField]
    private string title, description;
    public string Title { get => title; private set => title = value; }
    public string Description { get => description; private set => description = value; }
    //values for card effects
    //probably add complexity later
    //right now it works
    [SerializeField]
    private int damage, heal, shield, powerup;
    public int Damage{ get => damage; private set => damage = value; }
    public int Heal{ get => heal; private set => heal = value; }
    public int Shield{ get => shield; private set => shield = value; }
    //i have ideas about powerup, but honestly unsure how exactly it should work. Maybe a multiplier?
    public int Powerup{ get => powerup; private set => powerup = value; }

    //Card Processor
    ProcessCard processCard = new ProcessCard();
    //initializer 
    public Card(string title, string description, int damage, int heal, int shield, int powerup)
    {
        Title = title;
        Description = description;
        Damage = damage;
        Heal = heal;
        Shield = shield;
        Powerup = powerup;
    }
    
}