using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAnxiety : MonoBehaviour
{
    [Header("Anxiety")]
    private float anxiety = 0f;
    private float maxAnxiety = 100f;
    public float anxietyPerEnemy = 15f;
    private float decayTimer = 0f;
    public float decayInterval = 5f;
    public float decayAmount = 10f;

    [Header("Tags")]
    public string enemyTag = "Enemy";

    [Header("Camera and left border death")]
    public Camera mainCamera;
    public float marginLeftDeath = 0f;

    private bool isDead = false;

    // Referencias
    private AudioHandler audioHandler;
    private PlayerAnimationHandler animationHandler;
    private PlayerMovement movement;

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        audioHandler = GetComponent<AudioHandler>();
        animationHandler = GetComponent<PlayerAnimationHandler>();
        movement = GetComponent<PlayerMovement>();
    }
    void Update()
    {
        if (isDead) return;

        // Decaimiento de ansiedad con el tiempo
        decayTimer += Time.deltaTime;
        if (decayTimer >= decayInterval)
        {
            float oldAnxiety = anxiety;
            anxiety -= decayAmount;
            anxiety = Mathf.Max(anxiety, 0f);
            decayTimer = 0f;

            float change = oldAnxiety - anxiety;
            Debug.Log($"[Baja Ansiedad] Antes: {oldAnxiety}, Cambio: -{change}, Ahora: {anxiety}");
        }

        // Muerte por ansiedad
        if (anxiety >= maxAnxiety)
        {
            Die("Ansiedad máxima alcanzada");
        }

        // Muerte por salir de pantalla a la izquierda
        float zDist = Mathf.Abs(mainCamera.transform.position.z);
        float leftX = mainCamera.ViewportToWorldPoint(new Vector3(0f, 0.5f, zDist)).x;

        if (transform.position.x < leftX + marginLeftDeath)
        {
            Die("Cruzó el borde izquierdo");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead) return;

        if (other.CompareTag(enemyTag))
        {
            float oldAnxiety = anxiety;
            anxiety += anxietyPerEnemy;
            anxiety = Mathf.Min(anxiety, maxAnxiety);
            float change = anxiety - oldAnxiety;

            Debug.Log($"[Sube Ansiedad / HIT] Antes: {oldAnxiety}, Cambio: +{change}, Ahora: {anxiety}");

            // 🔊 Sonido de daño y 🎞️ animación de golpe
            animationHandler?.ForcePlayAnimation("HIT-ANSI", false);

            // Muerte por ansiedad (inmediata)
            if (anxiety >= maxAnxiety)
            {
                Die("Ansiedad máxima alcanzada");
            }
        }
    }

    /*private void Die(string reason)
    {
        if (isDead) return;
        isDead = true;

        Debug.Log($"[Muerte] {reason}");

        // Bloquea movimiento
        if (movement != null) movement.enabled = false;

        // Última animación de hit antes de destruir
        animationHandler?.ForcePlayAnimation("HIT-ANSI", false);

        // Destruye tras breve delay
        Destroy(gameObject, 1.5f);
    }*/
    private void Die(string reason)
    {
        if (isDead) return;
        isDead = true;
        Debug.Log($"[Muerte] {reason}");
        if (movement != null) movement.enabled = false;
        animationHandler?.ForcePlayAnimation("HIT-ANSI", false);
        // Cambia a pantalla Game Over inmediatamente
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameOver"); // Cambia "GameOver" por el nombre real de tu escena
    }
    public float GetAnxiety()
    {
        return anxiety;
    }
}
