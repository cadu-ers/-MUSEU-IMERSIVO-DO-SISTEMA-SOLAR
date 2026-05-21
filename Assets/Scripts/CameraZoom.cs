using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour
{
    [Header("Configurações de Zoom")]
    [SerializeField] private float duracaoZoom = 0.5f;
    [Tooltip("Quantas vezes o raio do planeta a câmera fica de distância. Recomendado: 3-5")]
    [SerializeField] private float multiplicadorDistancia = 4f;
    [Tooltip("Altura em relação ao planeta, proporcional ao raio.")]
    [SerializeField] private float multiplicadorAltura = 0.4f;

    [Header("Iluminação")]
    [Tooltip("Sol para posicionar a câmera no lado iluminado.")]
    [SerializeField] private Transform sol;

    public Transform targetPlanet;

    private Vector3 posicaoOriginal;
    private Quaternion rotacaoOriginal;
    private bool posicaoOriginalSalva = false;
    private Coroutine zoomAtual;

    public void FazerZoom()
    {
        if (targetPlanet == null) return;

        if (!posicaoOriginalSalva)
        {
            posicaoOriginal = transform.position;
            rotacaoOriginal = transform.rotation;
            posicaoOriginalSalva = true;
        }

        if (zoomAtual != null) StopCoroutine(zoomAtual);
        zoomAtual = StartCoroutine(ZoomCoroutine());
    }

    public void VoltarZoom()
    {
        if (!posicaoOriginalSalva) return;

        if (zoomAtual != null) StopCoroutine(zoomAtual);
        zoomAtual = StartCoroutine(VoltarCoroutine());
    }

    /// <summary>
    /// Calcula o raio visual real do planeta, considerando colliders e escala.
    /// </summary>
    private float CalcularRaioPlaneta()
    {
        // Tenta pegar pelo Sphere Collider primeiro (mais preciso)
        SphereCollider sphere = targetPlanet.GetComponent<SphereCollider>();
        if (sphere != null)
        {
            // Pega a maior escala entre os 3 eixos
            Vector3 scale = targetPlanet.lossyScale;
            float maxScale = Mathf.Max(scale.x, Mathf.Max(scale.y, scale.z));
            return sphere.radius * maxScale;
        }

        // Fallback: usa o Renderer pra calcular o bounds
        Renderer rend = targetPlanet.GetComponentInChildren<Renderer>();
        if (rend != null)
        {
            return rend.bounds.extents.magnitude;
        }

        // Último fallback: valor fixo
        return 1f;
    }

    private IEnumerator ZoomCoroutine()
    {
        Vector3 planetPos = targetPlanet.position;

        // Calcula distância proporcional ao tamanho do planeta
        float raioPlaneta = CalcularRaioPlaneta();
        float distanciaTotal = raioPlaneta * multiplicadorDistancia;
        float alturaCamera = raioPlaneta * multiplicadorAltura;

        // Direção pro lado iluminado pelo Sol
        Vector3 horizDir;
        if (sol != null)
        {
            Vector3 dirPlanetaParaSol = (sol.position - planetPos);
            horizDir = new Vector3(dirPlanetaParaSol.x, 0f, dirPlanetaParaSol.z).normalized;
        }
        else
        {
            Vector3 dirToCam = transform.position - planetPos;
            horizDir = new Vector3(dirToCam.x, 0f, dirToCam.z).normalized;
        }

        if (horizDir.sqrMagnitude < 0.001f)
            horizDir = Vector3.back;

        // Distância horizontal considerando a altura
        float horizDist = Mathf.Sqrt(
            Mathf.Max(0f, distanciaTotal * distanciaTotal - alturaCamera * alturaCamera)
        );

        Vector3 targetPos = planetPos + horizDir * horizDist + Vector3.up * alturaCamera;
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;
        Quaternion targetRot = Quaternion.LookRotation(planetPos - targetPos);

        float elapsed = 0f;
        while (elapsed < duracaoZoom)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsed / duracaoZoom);
            transform.position = Vector3.Lerp(startPos, targetPos, t);
            transform.rotation = Quaternion.Slerp(startRot, targetRot, t);
            yield return null;
        }

        transform.position = targetPos;
        transform.rotation = targetRot;
        zoomAtual = null;
    }

    private IEnumerator VoltarCoroutine()
    {
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;

        float elapsed = 0f;
        while (elapsed < duracaoZoom)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsed / duracaoZoom);
            transform.position = Vector3.Lerp(startPos, posicaoOriginal, t);
            transform.rotation = Quaternion.Slerp(startRot, rotacaoOriginal, t);
            yield return null;
        }

        transform.position = posicaoOriginal;
        transform.rotation = rotacaoOriginal;
        zoomAtual = null;
    }
}