using System;
using UnityEngine;
public class BasicAI
{
    public virtual int chooseCard(Character itself, Character other)
    {
        return UnityEngine.Random.Range(0, itself.AvailableCards.Count);
    }
}
