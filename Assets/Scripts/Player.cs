using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public LayerMask jumpLayer;

    public GameObject pauseMenu;
    public GameObject deathMenu;

    public SpriteRenderer playerSprite;

    public Image Image;

    public int maxHealth;
    public int direction;
    public int currentHealth;

    public float movementSpeed;
    public float jumpForce;

    private float movementX;
    private float jumpCD;

    private bool isGrounded;
    private bool isCrouch;
    private bool isAlive = true;

    private void Start()
    {
        currentHealth = maxHealth;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void CheckPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void CheckDirection()
    {
        if (movementX != 0)
        {
            if (movementX > 0)
            {
                playerSprite.flipX = false;

                direction = 1;
            }
            else
            {
                playerSprite.flipX = true;

                direction = -1;
            }
        }
    }

    private void FixedUpdate()
    {
        if (currentHealth > 0)
        {
            Movement();
        }
    }

    private void Update()
    {
        if (!isAlive)
        {
            // terminar xd
        }

        Crouch();
        Jump();
        CheckPause();
        CheckDirection();

        isGrounded = Physics2D.Raycast(transform.position + new Vector3(0, 0.1f, 0), new Vector2(0, -1), 0.2f, jumpLayer).collider != null;
    }
    
    /// <summary>
    /// Gestiona el movimiento del Player
    /// </summary>
    private void Movement()
    {
        movementX = Input.GetAxisRaw("Horizontal") * movementSpeed * Time.deltaTime;
        transform.Translate(movementX, 0, 0);
    }
    /// <summary>
    /// Gestiona el salto
    /// </summary>
    private void Jump()
    {
        if (jumpCD <= 0 && isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                    jumpCD = 0.5f;
            }
        } else
            jumpCD -= Time.deltaTime;
    }
    /// <summary>
    /// Esta función sirve para agacharse
    /// </summary>
    private void Crouch()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            GetComponent<CapsuleCollider2D>().offset = new Vector2(0, 0.625f);
            GetComponent<CapsuleCollider2D>().size = new Vector2(1, 1.25f);
        }
        else
        {
            GetComponent<CapsuleCollider2D>().offset = new Vector2(0, 1.25f);
            GetComponent<CapsuleCollider2D>().size = new Vector2(1, 2.50f);
        }
    }
    /// <summary>
    /// Función que calcula según el daño
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        if (currentHealth > 0)
            currentHealth -= damage;
        else
            Death();
    }
    /// <summary>
    /// Gestiona la muerte y fin de partida
    /// </summary>
    private void Death()
    {
        deathMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
