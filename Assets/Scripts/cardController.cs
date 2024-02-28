using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class cardController : MonoBehaviour
{
    public BoxCollider2D thisCard;
    public bool isMouseOver;
    

    private void Start()
    {
        thisCard = GetComponent<BoxCollider2D>();

    }
    private void OnMouseDown()
    {
        isMouseOver = true;
    }
    private void OnMouseExit()
    {
        isMouseOver = false;
    }
    
    
}

public enum CardSprite //enum é basicamente um array, como esse está público posso usar nos outros scripts
{
    Zoe,
    Pai,
    Holograma,
    Isaac,
    //ADA
    Ada,
    Babbage,
    ProfessorUK,
    Professor2UK,
    //MARIE
    Marie,
    Pierre,
    Pedestre,
    ProfessorFR,
    CientistaFR,
    //UTEIS
    Portal,
    Fundo,
    DonoDaBanca,
    TelaPreta,

       

}

