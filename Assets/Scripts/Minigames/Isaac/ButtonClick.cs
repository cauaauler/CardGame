using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ButtonClick : MonoBehaviour
{
    public LigarIsaac ordemIsaac;
    public UnityEngine.UI.Image image;

   
    //Adiciona os botoes clicados em um array, que depois sera comparado com a resposta
    public void Adicionar() {
       ordemIsaac.text.text = "Ligue o robô ativando os botões na ordem correta";
        for(int i = 0;i < ordemIsaac.buttons.Length; i++){
            if(ordemIsaac.comparar[i] == null){
                
                ordemIsaac.comparar[i] = gameObject;
                ordemIsaac.images[i] = image;
                image.color = Color.green;
                break;
            }
   } 
        
       
}
}
