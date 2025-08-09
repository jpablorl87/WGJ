using UnityEngine;

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

   
    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    void Update()
    {
        
        decayTimer += Time.deltaTime;
        if (decayTimer >= decayInterval)
        {
            float oldAnxiety = anxiety;
            anxiety -= decayAmount;
            anxiety = Mathf.Max(anxiety, 0f);

            float change = oldAnxiety - anxiety; 
            Debug.Log($"[Baja Ansiedad] Antes: {oldAnxiety}, Cambio: -{change}, Ahora: {anxiety}");

            decayTimer = 0f;
        }

        // muerte por ansiedad máxima
        if (anxiety >= maxAnxiety)
        {
            Die("Anxiety reached maximum");
        }

        // muerte por el borde izquierdo de pantalla 
        float zDist = Mathf.Abs(mainCamera.transform.position.z);
        float leftX = mainCamera.ViewportToWorldPoint(new Vector3(0f, 0.5f, zDist)).x;

        if (transform.position.x < leftX + marginLeftDeath)
        {
            Die("Crossed the left border");
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

            float change = anxiety - oldAnxiety; // cuanto subió
            Debug.Log($"[Sube Ansiedad] Antes: {oldAnxiety}, Cambio: +{change}, Ahora: {anxiety}");

            if (anxiety >= maxAnxiety)
            {
                Die("Anxiety reached maximum");
            }
        }
    }

    void Die(string reason)
    {
        isDead = true;
        Debug.Log($"[Death] {reason}");
        Destroy(gameObject);
    }
}
