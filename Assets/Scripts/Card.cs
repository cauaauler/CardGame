using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

[CreateAssetMenu]
public class Card : ScriptableObject
{
    public string textCardOnly;
    public CardSprite sprite;
    public string cardName;
    public int cardID;
    public string dialogue;
    public string leftQuote;
    public string rightQuote;

    public Card leftCard;
    public Card rightCard;
     public Item item;
    public string itemGapName;
    public string labyrinthName;

    public Card parentCard = null;

    
}

