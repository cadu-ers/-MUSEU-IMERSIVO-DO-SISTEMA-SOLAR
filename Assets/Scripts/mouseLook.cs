using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensibilidade = 2.0f;
    private float rotacaoX = 0;
    private float rotacaoY = 0;

    // Flag pública para controlar se a câmera deve seguir o mouse
    public bool podeRotacionar = true;

    void Start()
    {
        // Trava o mouse no centro da tela
        TravarMouse();

        // Inicializa rotação com a rotação atual da câmera
        Vector3 anguloAtual = transform.eulerAngles;
        rotacaoY = anguloAtual.y;
        rotacaoX = anguloAtual.x;
    }

    void Update()
    {
        // Só rotaciona se estiver habilitado
        if (podeRotacionar)
        {
            float mouseX = Input.GetAxis("Mouse X") * sensibilidade;
            float mouseY = Input.GetAxis("Mouse Y") * sensibilidade;

            rotacaoY += mouseX;
            rotacaoX -= mouseY;
            rotacaoX = Mathf.Clamp(rotacaoX, -90f, 90f);

            transform.eulerAngles = new Vector3(rotacaoX, rotacaoY, 0);
        }

        // ESC libera o mouse a qualquer momento (escape de emergência)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LiberarMouse();
        }
    }

    /// <summary>
    /// Trava o mouse no centro e ativa rotação por mouse.
    /// </summary>
    public void TravarMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        podeRotacionar = true;
    }

    /// <summary>
    /// Libera o mouse para usar em UI e pausa rotação por mouse.
    /// </summary>
    public void LiberarMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        podeRotacionar = false;
    }
}