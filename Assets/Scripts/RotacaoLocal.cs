using UnityEngine;

public class RotacaoLocal : MonoBehaviour
{
    public float velocidade = 15f;

    void Update()
    {
        // Usa unscaledDeltaTime para girar mesmo com TimeScale = 0
        // E ignora a flag OrbitasPausadas — rotação local NUNCA para
        transform.Rotate(Vector3.up, velocidade * Time.unscaledDeltaTime, Space.Self);
    }
}