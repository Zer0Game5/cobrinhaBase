using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CobrinhaGame : MonoBehaviour
{
    private List<GameObject>corpoCobrinha = new List<GameObject>();
    private string direcaoString;
    private Vector3 direcao;

    public GameObject corpo;

    private float tamanho=1;
    public GameObject fruta;

    private Vector3 posFinal;

    public float distanciaPecas=1.2f;

    public Transform cima;
    public Transform baixo;
    public Transform direita;
    public Transform esquerda;

    void Start()
    {
        for(int n=0;n<3;n++){
            corpoCobrinha.Add(Instantiate(corpo,new Vector3(transform.position.x+tamanho,transform.position.y,0),Quaternion.identity));
            tamanho +=1;
        }
        
        float x =Random.RandomRange(esquerda.position.x,direita.position.x);
        float y =Random.RandomRange(baixo.position.y,cima.position.y);
        Instantiate(fruta,new Vector2(x,y),Quaternion.identity);
        StartCoroutine("Mover");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A)||Input.GetKeyDown(KeyCode.LeftArrow)){
            direcaoString ="esquerda";
        }else if(Input.GetKeyDown(KeyCode.D)||Input.GetKeyDown(KeyCode.RightArrow)){
            direcaoString="direita";
        }else if(Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.UpArrow)){
            direcaoString="cima";
        }else if(Input.GetKeyDown(KeyCode.S)||Input.GetKeyDown(KeyCode.DownArrow)){
            direcaoString="baixo";
        }
    }


    IEnumerator Mover(){
        yield return new WaitForSeconds(0.2f);
        switch(direcaoString){
            case "cima":
                direcao = Vector3.up;
                transform.rotation = Quaternion.Euler(0,0,-90f);
            break;
            case "baixo":
                direcao = Vector3.down;
                transform.rotation = Quaternion.Euler(0,0,90);
            break;
            case "direita":
                direcao = Vector3.right;
                transform.rotation = Quaternion.Euler(0,0,180);
            break;
            case "esquerda":
                direcao = Vector3.left;
                transform.rotation = Quaternion.Euler(0,0,0);
            break;
        }

        direcao*=distanciaPecas;
        posFinal =transform.position;
        transform.position+= direcao;

        foreach(GameObject t in corpoCobrinha){
            Vector3 temp = t.transform.position;
            t.transform.position = posFinal;
            posFinal=temp;
        }

        StartCoroutine("Mover");
    }


    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag=="fruta"){
            Destroy(other.gameObject);
            corpoCobrinha.Add(Instantiate(corpo,new Vector3(corpoCobrinha[corpoCobrinha.Count-1].transform.position.x,corpoCobrinha[corpoCobrinha.Count-1].transform.position.y,0),Quaternion.identity));
            float x =Random.RandomRange(esquerda.position.x,direita.position.x);
            float y =Random.RandomRange(baixo.position.y,cima.position.y);
            Instantiate(fruta,new Vector2(x,y),Quaternion.identity);
        }    
    }
}
