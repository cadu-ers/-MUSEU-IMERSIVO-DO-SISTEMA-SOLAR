using UnityEngine;
using UnityEngine.UI;

public class GazeCursor : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private Image cursorImage;
    [SerializeField] private Image gazeProgress; // NOVO: barra circular
    [SerializeField] private GazeManager gazeManager;
    [SerializeField] private MouseLook mouseLook;

    [Header("Cores do Crosshair")]
    [SerializeField] private Color corNormal = Color.white;
    [SerializeField] private Color corEmCima = Color.yellow;

    [Header("Animação do Progresso")]
    [Tooltip("Velocidade da animação suave do preenchimento.")]
    [SerializeField] private float velocidadeFill = 10f;

    private RectTransform cursorRect;

    void Start()
    {
        if (cursorImage != null)
        {
            cursorRect = cursorImage.GetComponent<RectTransform>();
            cursorRect.anchoredPosition = Vector2.zero;
        }

        if (gazeManager == null)
            gazeManager = FindFirstObjectByType<GazeManager>();

        if (mouseLook == null)
            mouseLook = FindFirstObjectByType<MouseLook>();

        // Começa com a barra vazia
        if (gazeProgress != null)
            gazeProgress.fillAmount = 0f;
    }

    void Update()
    {
        if (cursorImage == null) return;

        // Esconde tudo quando o mouse está liberado (painel aberto)
        bool deveEsconder = mouseLook != null && !mouseLook.podeRotacionar;

        cursorImage.enabled = !deveEsconder;
        if (gazeProgress != null) gazeProgress.enabled = !deveEsconder;

        if (deveEsconder) return;

        if (gazeManager == null) return;

        // Atualiza cor do crosshair
        cursorImage.color = gazeManager.estaOlhandoPlaneta ? corEmCima : corNormal;

        // Atualiza barra de progresso com animação suave
        if (gazeProgress != null)
        {
            float alvo = gazeManager.progressoGaze;
            gazeProgress.fillAmount = Mathf.MoveTowards(
                gazeProgress.fillAmount,
                alvo,
                velocidadeFill * Time.unscaledDeltaTime
            );
        }
    }
}