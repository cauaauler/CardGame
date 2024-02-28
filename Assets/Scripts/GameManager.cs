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
   
    //Vari�veis dos cards
    private string textOnly;
    private string charName;
    private string dialogue;
    private string leftQuote;
    private string rightQuote;
    public Card currentCard;
    public Card testCard;
    public UnityEngine.UI.Button[] imageGap;
    public GameObject[] labyrinth;
    public GameObject game;
    Card backup;

    private void Start(){
        LoadCard(testCard);
        //A posição que a carta vai voltar ao ser solta
        defaultPositionCard = cardGameObject.transform.position;
    }

    //Muda o texto dependendo da posição da carta
    public void UpdateActionQuote()
    { 
        
        if(cardGameObject.transform.position.x > 0){
            actionQuote.text = rightQuote;
        }else{
            actionQuote.text = leftQuote;
        }
    }
    public void apagarFlecha()
    {
        labyrinth[2].SetActive(false);
    }
    //Acontece a todo frame
    bool oneTime = true;
    public void Update()
    {
        
        //Se existir algum labirinto na carta vai habilitar
       
        if(currentCard.labyrinthName != null){
            for(int i = 0; i<labyrinth.Length; i++){
                if(labyrinth[i].name == currentCard.labyrinthName){
                    if(oneTime){
                        labyrinth[i].SetActive(true);
                        oneTime = false;
                    }
                    
                    if (currentCard.labyrinthName == "Arrow")
                    {
                         Invoke("apagarFlecha", 3f);
                        
                         
                    }
                   
                    if(currentCard.labyrinthName != "Arrow"){
                        game.SetActive(false);
                    }
                    
                    //currentCard.labyrinthName = null;
                    
                }
                
            }
            
        }
        //Habilitar itens na mochila
        if(currentCard.item != null){
            for(int i =0; i<imageGap.Length; i++){
                if(imageGap[i].tag == currentCard.itemGapName){
                imageGap[i].image.sprite = currentCard.item.itemSprite;
                imageGap[i].interactable = true;
            }
            }
            
        }
        //Atribuindo as variáveis para o objeto
        textCardOnly.text = textOnly;
        characterName.text = charName;
        characterDialogue.text = dialogue;
        
        //Muda a cor do texto dependendo da posição da carta
        textColor.a = Mathf.Min(Mathf.Abs(cardGameObject.transform.position.x/divideValue), 1); 
        //Se textCardOnly for vazio, vai aparecer o background da resposta
        if(currentCard.textCardOnly == ""){
            quoteBackgroundColor.a = Mathf.Min(Mathf.Abs(cardGameObject.transform.position.x/backgroundDivideValue), fTransparency);
        //Se não for vazio, não aparece background
        }else{
            quoteBackgroundColor.a = 0;
        }
        //Precisa ficar aqui para o gradiente funcionar
        actionQuote.color = textColor;
        quoteBackground.color = quoteBackgroundColor;

        UpdateActionQuote();

        //Se clicado vai entrar
        if (Input.GetMouseButton(0) && mainCardController.isMouseOver ){
            //A carta segue o mouse
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
            cardGameObject.transform.position = pos;

        }
        //Se parar de ser clicado
        else{
            //Volta para o centro
            cardGameObject.transform.position = Vector2.MoveTowards(cardGameObject.transform.position, defaultPositionCard, fMovingSpeed); 
            //Não lembro o porque do eulerAngles, mas tem algo a ver com voltar para o meio
            cardGameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        }

        //Se o mouse estiver na direita e for solto, newCardRight()
        if (cardGameObject.transform.position.x >= fSideTrigger) 
        {
            if (Input.GetMouseButtonUp(0)){
                NewCardRight(); 
                //Não lembro porque do x
                x++;
            }
        }
        //Não deixa soltar a carta muito perto do centro, o Margin é mais perto do centro do que o Trigger(área que vai executar a ação)
        else if (cardGameObject.transform.position.x > -fSideMargin){
            textColor.a = 0;
        }
        //Se não for nenhum dos dois, vai ter que ser a esquerda
        else{
            if (Input.GetMouseButtonUp(0)){
                NewCardLeft();
                x++;
            }
        }

        //Para fazer a carta ter um pequeno ângulo ao ser arrastada
        cardGameObject.transform.eulerAngles = new Vector3(0, 0, -cardGameObject.transform.position.x*1.5f);
    }

    public void clicar(){
       
        Debug.Log("Carta " + currentCard.name);
        Debug.Log("Backup " + backup.name);
        Debug.Log("Parent " + currentCard.parentCard.name);
        LoadCard(currentCard.parentCard);
        
        

    }
    public void LoadCard(Card card)
    {
        cardSpriteRenderer.sprite = resourceManager.sprites[(int)card.sprite];
        charName = card.cardName;
        dialogue = card.dialogue;
        leftQuote = card.leftQuote;
        rightQuote = card.rightQuote;
        textOnly = card.textCardOnly;
        currentCard = card;
        //Não esta perfeito mas funciona
        if(backup != currentCard && currentCard.parentCard == null && currentCard.name != "01"){
            currentCard.parentCard = backup;
        }
       
        
        
        }
    public void NewCardLeft()
    {
            backup = currentCard;
            LoadCard(currentCard.leftCard);
    }
    public void NewCardRight()
    {
            backup = currentCard;
            LoadCard(currentCard.rightCard);
    }
}
