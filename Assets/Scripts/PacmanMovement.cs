using UnityEngine;

public class PacmanMovement : MonoBehaviour
{
    public float speed = 5f;

    private Vector2 input;
    private Vector2 movement;

    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Pac-Man empieza moviéndose hacia la derecha
        movement = Vector2.right;
    }

    void Update()
    {
        // Detectar flechas o WASD
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Movimiento estilo Pac-Man (solo un eje a la vez)
        if (input.x != 0)
            movement = new Vector2(input.x, 0);

        else if (input.y != 0)
            movement = new Vector2(0, input.y);

        UpdateRotation();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    void UpdateRotation()
    {
        // Derecha
        if (movement.x > 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);

        // Izquierda
        else if (movement.x < 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);

        // Arriba
        else if (movement.y > 0)
            transform.rotation = Quaternion.Euler(0, 0, 90);

        // Abajo
        else if (movement.y < 0)
            transform.rotation = Quaternion.Euler(0, 0, -90);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("coco"))
        {
            // SUMA EL PUNTO EN EL SCORE MANAGER
            ScoreManager.Instance.AddPoint();

            // DESAPARECE EL COCO
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("TunnelRight"))
        {
            // Aparece en el túnel izquierdo
            transform.position = new Vector3(-transform.position.x + 0.1f, transform.position.y, transform.position.z);
        }

        if (collision.CompareTag("TunnelLeft"))
        {
            // Aparece en el túnel derecho
            transform.position = new Vector3(-transform.position.x - 0.1f, transform.position.y, transform.position.z);
        }
    }
    public void Morir()
    {
        animator.SetTrigger("Muerte");
        speed = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Fantasma"))
        {
            LifeManager.Instance.QuitarVida();
        }
    }
    }

