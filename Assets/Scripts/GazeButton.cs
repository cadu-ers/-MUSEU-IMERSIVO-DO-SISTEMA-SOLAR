using UnityEngine;
using UnityEngine.Events;

public class GazeButton : MonoBehaviour
{
    public float tempoNecessario = 2f; // Os 2 segundos que você pediu
    private float contador = 0f;
    private bool estaOlhando = false;
    public UnityEvent aoCompletar; // O que vai acontecer quando der os 2s

    void Update()
    {
        if (estaOlhando)
        {
            contador += Time.deltaTime;
            if (contador >= tempoNecessario)
            {
                aoCompletar.Invoke();
                estaOlhando = false; // Evita que ative várias vezes seguidas
                contador = 0;
            }
        }
    }

    // Essas funções serão chamadas pelo GazeManager
    public void SetOlhando(bool estado)
    {
        estaOlhando = estado;
        if (!estado) contador = 0; // Reseta se desviar o olhar
    }
}