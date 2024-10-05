using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float moveSpeed = 5f; // Grundgeschwindigkeit des Spielers
    public float sprintSpeed = 10f; // Geschwindigkeit beim Sprinten
    public float acceleration = 10f; // Beschleunigung
    public float deceleration = 10f; // Verzögerung beim Abbremsen
    public float jumpHeight = 5f; // Höhe des Sprungs
    public float gravity = -40f; // Schwerkraft

    public float mouseSensitivity = 100f; // Empfindlichkeit der Maus
    public Transform cameraTransform; // Transform der Kamera

    private Rigidbody _rigidbody;   // Referenz auf den Rigidbody
    private CharacterController controller; // Referenz auf den CharacterController
    private Vector3 velocity;   // Bewegung des Spielers als Vektor
    private bool isGrounded;    // Signalisiert ob der Spieler am Boden ist
    private float xRotation = 0f; // Für die Begrenzung der X-Achsen-Rotation (Hoch/Runter schauen)

    void Start()
    {
        // === Private Komponenten initialisieren, da sie nicht im Editor sichtbar sind ===
        controller = GetComponent<CharacterController>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // === Mausinput ===
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // === X-Rotation (nach oben und unten schauen), begrenzt auf -90 bis 90 Grad ===
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // === Kamera hoch/runter rotieren ===
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // === Spieler-Körper links/rechts drehen ===
        transform.Rotate(Vector3.up * mouseX);

        // === Bewegung ===
        isGrounded = controller.isGrounded;

        // === WASD - Input ===
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = transform.right * horizontal + transform.forward * vertical; // Bewegt sich relativ zur Blickrichtung

        // === Beschleunigung ===
        if (direction.magnitude >= 0.1f)
        {
            velocity = Vector3.Lerp(velocity, direction * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed), Time.deltaTime * acceleration);
        }
        else
        {
            // === Abbremsen ===
            velocity = Vector3.Lerp(velocity, Vector3.zero, Time.deltaTime * deceleration);
        }

        // === Springen ===
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // === Schwerkraft & (Luft-)widerstand ===
        velocity.y += gravity * Time.deltaTime;
        _rigidbody.drag = isGrounded ? 0 : 5;

        // === Bewegung anwenden ===
        controller.Move(velocity * Time.deltaTime);
    }
}
