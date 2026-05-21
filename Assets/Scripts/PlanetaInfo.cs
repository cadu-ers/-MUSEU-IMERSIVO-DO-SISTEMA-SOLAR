using UnityEngine;

public class PlanetaInfo : MonoBehaviour
{
    [Header("Informações")]
    public string nomePlaneta;

    [TextArea(3, 10)]
    public string descricaoPlaneta;

    public string[] curiosidades;

    [Header("Áudio")]
    [Tooltip("Áudio narrado para este planeta. Quando terminar, a câmera volta automaticamente.")]
    public AudioClip narracao;

    public void Selecionar()
    {
        Debug.Log("Planeta selecionado: " + nomePlaneta);
    }
}