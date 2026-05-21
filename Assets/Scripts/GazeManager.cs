using UnityEngine;
using System.Collections;

public class GazeManager : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private CameraZoom cameraZoom;
    [SerializeField] private UIPlanetaController uiController;
    [SerializeField] private MouseLook mouseLook;
    [SerializeField] private AudioSource audioSource;

    [Header("Configurações")]
    [SerializeField] private float tempoDeFoco = 2f;
    [SerializeField] private float distanciaRaycast = 1000f;
    [Tooltip("Pequeno delay extra após o áudio terminar antes de voltar (em segundos).")]
    [SerializeField] private float delayPosAudio = 1f;
    [Tooltip("Tempo para voltar se NÃO houver áudio configurado (em segundos).")]
    [SerializeField] private float tempoSemAudio = 8f;

    private PlanetaInfo currentPlaneta;
    private float lookTimer = 0f;
    private bool planetaTriggered = false;
    private bool bloqueado = false;
    private Coroutine retornoAutomatico;

    public bool estaOlhandoPlaneta { get; private set; } = false;
    public float progressoGaze { get; private set; } = 0f;

    private void Start()
    {
        if (playerCamera == null)
            playerCamera = Camera.main;
    }

    private void Update()
    {
        if (bloqueado)
        {
            estaOlhandoPlaneta = false;
            progressoGaze = 0f;
            return;
        }

        Ray ray = playerCamera.ScreenPointToRay(
            new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f)
        );

        if (Physics.Raycast(ray, out RaycastHit hit, distanciaRaycast))
        {
            PlanetaInfo planetaInfo = hit.collider.GetComponent<PlanetaInfo>();
            if (planetaInfo != null)
            {
                if (currentPlaneta != planetaInfo)
                {
                    currentPlaneta = planetaInfo;
                    lookTimer = 0f;
                    planetaTriggered = false;
                }

                lookTimer += Time.unscaledDeltaTime;
                estaOlhandoPlaneta = true;
                progressoGaze = Mathf.Clamp01(lookTimer / tempoDeFoco);

                if (lookTimer >= tempoDeFoco && !planetaTriggered)
                {
                    AtivarPlaneta(planetaInfo);
                    planetaTriggered = true;
                }
                return;
            }
        }

        ResetLook();
    }

    private void AtivarPlaneta(PlanetaInfo planeta)
    {
        OrbitaPlaneta.OrbitasPausadas = true;

        cameraZoom.targetPlanet = planeta.transform;
        cameraZoom.FazerZoom();

        string curiosidadesTexto = "";
        if (planeta.curiosidades != null && planeta.curiosidades.Length > 0)
        {
            for (int i = 0; i < planeta.curiosidades.Length; i++)
                curiosidadesTexto += "• " + planeta.curiosidades[i] + "\n";
        }

        uiController.MostrarInfo(
            planeta.nomePlaneta,
            planeta.descricaoPlaneta,
            curiosidadesTexto
        );

        if (mouseLook != null) mouseLook.LiberarMouse();

        bloqueado = true;

        // INICIA NARRAÇÃO + RETORNO AUTOMÁTICO
        if (retornoAutomatico != null) StopCoroutine(retornoAutomatico);
        retornoAutomatico = StartCoroutine(TocarNarracaoEVoltar(planeta));
    }

    private IEnumerator TocarNarracaoEVoltar(PlanetaInfo planeta)
    {
        float tempoDeEspera;

        if (planeta.narracao != null && audioSource != null)
        {
            // Toca o áudio
            audioSource.clip = planeta.narracao;
            audioSource.Play();
            tempoDeEspera = planeta.narracao.length + delayPosAudio;
        }
        else
        {
            // Sem áudio: usa tempo padrão
            tempoDeEspera = tempoSemAudio;
        }

        // Aguarda usando unscaledDeltaTime (caso TimeScale esteja em 0)
        float elapsed = 0f;
        while (elapsed < tempoDeEspera)
        {
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        // Só volta se ainda estiver bloqueado (usuário pode ter apertado Voltar)
        if (bloqueado)
        {
            VoltarAutomaticamente();
        }
    }

    private void VoltarAutomaticamente()
    {
        // Mesma lógica do botão Voltar, mas sem precisar de clique
        if (cameraZoom != null) cameraZoom.VoltarZoom();
        if (uiController != null) uiController.EsconderInfo();
        DesbloquearGaze();
    }

    public void DesbloquearGaze()
    {
        OrbitaPlaneta.OrbitasPausadas = false;

        if (mouseLook != null) mouseLook.TravarMouse();

        // Para o áudio se estiver tocando
        if (audioSource != null && audioSource.isPlaying)
            audioSource.Stop();

        // Cancela o retorno automático se foi pulado manualmente
        if (retornoAutomatico != null)
        {
            StopCoroutine(retornoAutomatico);
            retornoAutomatico = null;
        }

        bloqueado = false;
        ResetLook();
    }

    private void ResetLook()
    {
        lookTimer = 0f;
        planetaTriggered = false;
        currentPlaneta = null;
        estaOlhandoPlaneta = false;
        progressoGaze = 0f;
    }
}