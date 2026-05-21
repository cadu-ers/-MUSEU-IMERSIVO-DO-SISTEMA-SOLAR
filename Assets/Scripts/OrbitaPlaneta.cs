using UnityEngine;

public class OrbitaPlaneta : MonoBehaviour
{
    [Header("Configuração")]
    public Transform sol;
    public float velocidadeOrbita = 8f;
    public float velocidadeRotacao = 10f;

    // Controle estático: pausa TODAS as órbitas de uma vez
    public static bool OrbitasPausadas = false;

    void Update()
    {
        // Se estiver pausado, não faz NADA — nem translação, nem rotação do pivô
        if (OrbitasPausadas) return;

        // Translação ao redor do sol (modo "explícito" com RotateAround)
        if (sol != null)
            transform.RotateAround(sol.position, Vector3.up, velocidadeOrbita * Time.deltaTime);

        // Rotação no eixo Y (modo "pivô parentado" — quando este GameObject é o pivô da órbita)
        transform.Rotate(Vector3.up, velocidadeRotacao * Time.deltaTime);
    }
}