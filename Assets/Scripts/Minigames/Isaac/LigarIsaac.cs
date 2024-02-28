using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LigarIsaac : MonoBehaviour
{
    public GameObject[] buttons;
    public GameObject[] comparar;
    public Canvas canvas;
    public GameObject mainGame;
    public Canvas UI;
    Boolean eIgual;
    public TMP_Text text;    
     public UnityEngine.UI.Image[] images;

    // Update is called once per frame
    void Update()
    {
        if(eIgual){
            canvas.enabled = false;
            mainGame.SetActive(true);
            //UI.enabled = true;
        }//else(!eIgual && )
        if(canvas.isActiveAndEnabled){
            //if(buttons.Length == comparar.Length){
                for(int i = 0; i < buttons.Length; i++){
                if(buttons[i] == comparar[i]){
                    eIgual = true;
                }else{
                    for (int j = 0; j < comparar.Length; j++)
                    {
                        if(comparar[5] != null){
                             text.text = "Ordem Incorreta, tente outra vez";
                            //NÃO SEI PORQUE O FOR ESTÁ ERRADO
                            //for (int k = 0; k < 6; k++)
                            //{
                               images[0].color = Color.white;
                               images[1].color = Color.white;
                               images[2].color = Color.white;
                               images[3].color = Color.white;
                               images[4].color = Color.white;
                               images[5].color = Color.white;
                            //}
                            comparar[j] = null;
                        }
                        
                    }
                    
                    
                    eIgual = false;
                    break;
                }
            }
            }
            
       // }
    }
    
}
