using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    int x = 0;

    //GameObjects
    public GameObject cardGameObject;
    public SpriteRenderer cardSpriteRenderer;
    public cardController mainCardController;
    public resourceManager resourceManager;
    public Vector2 defaultPositionCard;
    public Vector3 cardRotation;

    //Variaveis diversas
    public float fMovingSpeed;
    public float fSideMargin;
    public float fSideTrigger;
    public float fTransparency;
    public float fRotation;
    public float divideValue;
    public float backgroundDivideValue;
    public Color textColor;
    public Color quoteBackgroundColor;

    //UI
    public TMP_Text actionQuote;
    public TMP_Text characterDialogue;
    public TMP_Text characterName;
    public TMP_Text textCardOnly;
    public SpriteRenderer quoteBackground;

    //Variáveis dos cards
    private string textOnly;
    private string charName;
    private string dialogue;
    private string leftQuote;
    private string rightQuote;
    public Card currentCard;
    public Card testCard;
    //public UnityEngine.UI.Button[] imageGap;

    // public GameObject[] labyrinth;
    public GameObject game;
    //Card backup;
    public TMP_Text textGlossaryObject;
    //public string textGlossary;

    private void Start()
    {
        LoadCard(testCard);
        //A posição que a carta vai voltar ao ser solta
        defaultPositionCard = cardGameObject.transform.position;
    }

    //Muda o texto dependendo da posição da carta
    public void UpdateActionQuote()
    {
        if (cardGameObject.transform.position.x > 0)
        {
            actionQuote.text = rightQuote;
        }
        else
        {
            actionQuote.text = leftQuote;
        }
    }

    //Acontece a todo frame
    public void Update()
    {

        //Muda a cor do texto dependendo da posição da carta
        textColor.a = Mathf.Min(Mathf.Abs(cardGameObject.transform.position.x / divideValue), 1);

        //Se textCardOnly for vazio, vai aparecer o background da resposta
        if (currentCard.textCardOnly == "")
        {
            quoteBackgroundColor.a = Mathf.Min(Mathf.Abs(cardGameObject.transform.position.x / backgroundDivideValue), fTransparency);
        }
        else
        {
            //Se não for vazio, não aparece background
            quoteBackgroundColor.a = 0;
        }

        //Precisa ficar aqui para o gradiente funcionar
        actionQuote.color = textColor;
        quoteBackground.color = quoteBackgroundColor;

        UpdateActionQuote();

        //Se clicado vai entrar
        if (Input.GetMouseButton(0) && mainCardController.isMouseOver)
        {
            //A carta segue o mouse, mas só no eixo x
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            cardGameObject.transform.position = new Vector2(pos.x, defaultPositionCard.y);
        }
        //Se parar de ser clicado
        else
        {
            //Volta para o centro
            cardGameObject.transform.position = Vector2.MoveTowards(cardGameObject.transform.position, defaultPositionCard, fMovingSpeed);
            //Não lembro o porque do eulerAngles, mas tem algo a ver com voltar para o meio
            cardGameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        }

        //Se o mouse estiver na direita e for solto, newCardRight()
        if (cardGameObject.transform.position.x >= fSideTrigger)
        {
            if (Input.GetMouseButtonUp(0))
            {
                NewCardRight();
                //Não lembro porque do x
                x++;
            }
        }
        //Não deixa soltar a carta muito perto do centro, o Margin é mais perto do centro do que o Trigger(área que vai executar a ação)
        else if (cardGameObject.transform.position.x > -fSideMargin)
        {
            textColor.a = 0;
        }
        //Se não for nenhum dos dois, vai ter que ser a esquerda
        else
        {
            if (Input.GetMouseButtonUp(0))
            {
                NewCardLeft();
                x++;
            }
        }

        //Para fazer a carta ter um pequeno ângulo ao ser arrastada
        cardGameObject.transform.eulerAngles = new Vector3(0, 0, -cardGameObject.transform.position.x * 1.5f);
    }

    public void LoadCard(Card card)
    {
        cardSpriteRenderer.sprite = resourceManager.sprites[(int)card.sprite];
        charName = card.cardName;
        dialogue = card.dialogue;
        leftQuote = card.leftQuote;
        rightQuote = card.rightQuote;
        currentCard = card;

        if (card.textGlossary != ".")
        {
            textGlossaryObject.text += card.textGlossary + "\n\n";
            card.textGlossary = ".";
        }

        //Atribuindo as variáveis para o objeto
        textCardOnly.text = textOnly;
        characterName.text = charName;
        characterDialogue.text = dialogue;

        //Altera da resposta quando não tem imagem
        RectTransform rectTransform = characterDialogue.GetComponent<RectTransform>();
        if (currentCard.textCardOnly == ".")
        {
            rectTransform.sizeDelta = new Vector2(107, rectTransform.sizeDelta.y);
            rectTransform.anchoredPosition = new Vector2(-3.8f, rectTransform.anchoredPosition.y);
            characterDialogue.alignment = TextAlignmentOptions.Top;


        }
        //Se tiver alguma pessoa respondendo volta para o normal
        else
        {
            rectTransform.sizeDelta = new Vector2(87, rectTransform.sizeDelta.y);
            rectTransform.anchoredPosition = new Vector2(-1.2f, rectTransform.anchoredPosition.y);
            characterDialogue.alignment = TextAlignmentOptions.TopLeft;

        }


    }

    public void NewCardLeft()
    {
        LoadCard(currentCard.leftCard);
    }

    public void NewCardRight()
    {
        LoadCard(currentCard.rightCard);
    }
}
