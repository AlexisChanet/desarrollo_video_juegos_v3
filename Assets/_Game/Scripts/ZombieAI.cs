using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 2.5f;
    public float detectionRange = 10f;

    [Header("Animación Procedural")]
    public float wobbleSpeed = 12f;
    public float wobbleAngle = 10f; // Grados que se inclina hacia los lados

    private Transform player;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        GameObject target = GameObject.FindGameObjectWithTag("Player");
        if (target != null)
        {
            player = target.transform;
        }
    }

    void FixedUpdate()
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position);
        float distance = direction.magnitude;

        if (distance <= detectionRange)
        {
            direction.Normalize();

            // Movimiento hacia el jugador
            rb.linearVelocity = direction * speed;

            // Ángulo base hacia el jugador
            float baseAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Oscilación senoidal para simular el paso al caminar
            float wobble = Mathf.Sin(Time.time * wobbleSpeed) * wobbleAngle;
            rb.rotation = baseAngle + wobble;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
}