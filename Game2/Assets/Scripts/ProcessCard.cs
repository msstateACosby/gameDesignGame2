using System;
using UnityEngine;
public class ProcessCard
{
    public virtual void process(Card card, Character actor, Character actee)
    {

        actee.increaseHealth(Math.Min(actee.Shield-(card.Damage+actee.Powerup), 0));
        actor.increaseHealth(card.Heal);
        actor.setShield(card.Shield);
        actor.setPowerup(card.Powerup);
    }
}
