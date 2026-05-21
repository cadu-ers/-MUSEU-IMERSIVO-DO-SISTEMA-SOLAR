using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIPlanetaController : MonoBehaviour
{
    [Header("Painel")]
    [SerializeField] private GameObject painelInfo;

    [Header("Textos (TMP)")]
    [SerializeField] private TMP_Text textoNome;
    [SerializeField] private TMP_Text textoDescricao;
    [SerializeField] private TMP_Text textoCuriosidades;

    [Header("Botão Voltar")]
    [SerializeField] private Button botaoVoltar;

    [Header("Referências")]
    [SerializeField] private CameraZoom cameraZoom;
    [SerializeField] private GazeManager gazeManager;

    private void Start()
    {
        if (botaoVoltar != null)
            botaoVoltar.onClick.AddListener(Voltar);

        EsconderInfo();
    }

    public void MostrarInfo(string nome, string descricao, string curiosidades)
    {
        if (painelInfo != null) painelInfo.SetActive(true);

        if (textoNome != null) textoNome.text = nome;
        if (textoDescricao != null) textoDescricao.text = descricao;
        if (textoCuriosidades != null) textoCuriosidades.text = curiosidades;
    }

    public void EsconderInfo()
    {
        if (painelInfo != null) painelInfo.SetActive(false);
    }

    public void Voltar()
    {
        if (cameraZoom != null) cameraZoom.VoltarZoom();
        EsconderInfo();
        if (gazeManager != null) gazeManager.DesbloquearGaze();
    }
}