using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    [SerializeField] Transform camaraJugador;
    [SerializeField] float sensibilidadRaton;
    [SerializeField] float velocidadJugador;

    [SerializeField] bool verCursor; // Para ocultar el cursor

    float pitchCamara = 0.0f; // guarda la rotación vertical de la cámara para luego poder limitar el rango de movimiento de la cámara
    CharacterController characterController; // Para usar el CharacterController

    private void Start()
    {
        characterController = GetComponent<CharacterController>(); 
        if (verCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false; // Poner el cursor invisible 
        }
    }

    private void Update()
    {
        UpdateMirarRaton(); // Llamar a al método para mover la cámara
        UpdateMovimiento(); // Llamar a al método para mover el jugador
    }

    void UpdateMirarRaton ()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")); // Devuelven el movimiento del raton en los ejes X e Y

        pitchCamara -= mouseDelta.y * sensibilidadRaton; // Calcular el ángulo de rotación vertical del mouse
        pitchCamara = Mathf.Clamp(pitchCamara, -90.0f, 90.0f); // Limitar ese valor para evitar que de vueltas la cámara

        camaraJugador.localEulerAngles = Vector3.right * pitchCamara; // La rotación vertical se aplica a la cámara

        transform.Rotate(Vector3.up * mouseDelta.x * sensibilidadRaton); // La rotación horizontal se aplica a la cámara
    }

    void UpdateMovimiento()
    {
        Vector2 inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); // Capturar las teclas de movimiento (WASD)
        inputDir.Normalize(); // Asegura que la magnitud de la dirección no supere a 1

        Vector3 velocidad = (transform.forward * inputDir.y + transform.right * inputDir.x) * velocidadJugador; // La dirección se descompone en componentes hacia adelante y a los lados y a esto se le multiplica la velocidad del jugador

        characterController.Move(velocidad * Time.deltaTime); // Para mantener el movimiento independientemente de los FPS

    }
}
