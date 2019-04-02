using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArraySamples : MonoBehaviour {

    string[] listaDeStringsComDezEspacos = new string[10];
    string[] listaDeStringsComItems = new string[]{"Mario","Luigi","Toad","Princess"};
    Sprite[] listaDeSprites;
    int[] listaDeInteiros;
    string tipoPrimitivoComValor = "tipoPrimitivoComValor";
    Vector2 posicaoInicial = new Vector2(10, 10);
    Vector2[] listaPontos = new Vector2[] { new Vector2(10,10), new Vector2(20, 10), new Vector2(30, 10) };


    // Use this for initialization
    void Start () {
        print(">>>" + listaDeStringsComDezEspacos.Length);
        print(">>>" + listaDeStringsComItems.Length);
        print(">>>" + listaDeStringsComItems[0]);
        print(listaDeInteiros.Length);

        for (int i = 0; i < listaDeStringsComItems.Length; i++)
            print(listaDeStringsComItems[i]);

    }
}
