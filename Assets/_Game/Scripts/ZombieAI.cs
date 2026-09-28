using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 2.5f;
    public float detectionRange = 10f;

    [Header("Animación Procedural")]
    public float wobbleSpeed = 12f;
    public float wobbleAngle = 10f;

    private Transform player;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        BuscarJugador();
    }

    void Update()
    {
        if (player == null || !player.gameObject.activeInHierarchy)
        {
            BuscarJugador();
        }
    }

    void FixedUpdate()
    {
        // Si el jugador no existe o está desactivado (muerto), detenemos al zombi por completo
        if (player == null || !player.gameObject.activeInHierarchy)
        {
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
                rb.Sleep(); // Pone el cuerpo rígido a dormir para que Unity no intente aplicarle físicas ni temblores
            }
            return;
        }

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
            rb.angularVelocity = 0f;
        }
    }

    void BuscarJugador()
    {
        GameObject target = GameObject.FindGameObjectWithTag("Player");
        if (target != null && target.activeInHierarchy)
        {
            player = target.transform;
        }
        else
        {
            player = null;
        }
    }
}