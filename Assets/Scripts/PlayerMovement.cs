using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject player;
    [Tooltip("Determina con que tanta fuerza el personaje vuela")]
    [Header("Movement")] [SerializeField] private float flyForce = 5f;
    [Space(10)]
    [Header("Animation Settings")]
    [Tooltip("Esta configuracion sirve para cambiar la velocidad de la animacion de caida")]
    [SerializeField] float fallingSpeed = 1f;
    [Tooltip("Este valor determina que tanta rotacion tendra el personaje cuando vuele")]
    [SerializeField, Range(0, 90)] private float maxAngleFlying = 37f;
    [Tooltip("Este valor determina que tanta rotacion tendra el personaje cuando caiga")]
    [SerializeField, Range(0, -90)] private float maxAngleFalling = -50f;
    [Tooltip("Audio que se reproducira al presionar la tecla para volar")]
    [SerializeField] AudioClip clip;
    Animator animator;
    float timer = 0;
    
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Application.targetFrameRate = 60;
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.isEditor)
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                rb.linearVelocityY = 0f;
                rb.AddForce(Vector2.up * flyForce, ForceMode2D.Impulse);
                AudioSource.PlayClipAtPoint(clip, transform.position);
                animator.SetBool("isFlying", true);
            }
            else
            {
                animator.SetBool("isFlying", false);
            }
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            if (Touchscreen.current.press.wasPressedThisFrame)
            {
                rb.linearVelocityY = 0f;
                rb.AddForce(Vector2.up * flyForce, ForceMode2D.Impulse);
                AudioSource.PlayClipAtPoint(clip, transform.position);
                animator.SetBool("isFlying", true);
            }
            else
            {
                animator.SetBool("isFlying", false);
            }
        }
        PlayerRotation();
    }
    
    void PlayerRotation()
    {
        if (rb.linearVelocityY > 0f)
        {
            player.transform.rotation = Quaternion.Euler(0f, 0f, maxAngleFlying);
            timer = 0;
        }
        
        if (rb.linearVelocityY < 0f)
        {
            timer += Time.deltaTime;
            player.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Lerp(maxAngleFlying, maxAngleFalling, timer * fallingSpeed));
        }
    }
}
