using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class PlayerContorl : Base
{
    [Header("坦克等级")]
    public int level;

    [Header("坦克血量")]
    public int health;
    public float speed;
    private PlayerInput playerInput;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Fire fire;
    private VariableJoystick variableJoystick;
    private Button fireBt;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        fire = GetComponent<Fire>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Debug.Log("Application.platform ===========>" + Application.platform);
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.WindowsEditor)
        {
            variableJoystick = GameObject.FindWithTag("Joystick")?.GetComponent<VariableJoystick>();
            fireBt = GameObject.FindWithTag("PlayerFireButton")?.GetComponent<Button>();
            fireBt?.onClick.AddListener(playerFire);
        }
    }

    private void Start()
    {
        if (!isLocalPlayer) return;
        EventHelper.CallPlayerCreated(gameObject);
        spriteRenderer.color = Color.green;
    }

    private void OnEnable()
    {
        playerInput.onActionTriggered += OnInputTriggered;
    }

    private void OnDisable()
    {
        playerInput.onActionTriggered -= OnInputTriggered;
    }

    private void FixedUpdate()
    {

        Move();
    }
    private Vector2 currentDir = new Vector2(1, 0);
    private void Move()
    {
        if (!isLocalPlayer) return;
        if (playerInput.actions["Move"].IsPressed())
        {
            Vector2 vector2 = playerInput.actions["Move"].ReadValue<Vector2>();
            moveAction(vector2);
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.WindowsEditor)
        {
            if (variableJoystick != null)
            {
                Vector2 move = new Vector2(variableJoystick.Horizontal, variableJoystick.Vertical);
                //左右 x  上下 y
                if (Mathf.Abs(move.x) < 0.05 && Mathf.Abs(move.y) < 0.05)
                {
                    animator.SetFloat("Speed", 0);
                    return;
                }
                moveAction(move);
            }
            // Debug.Log("========>" + move);
        }

    }

    private void moveAction(Vector2 vector2)
    {
        // animator.SetInteger("Level", level);

        animator.SetFloat("Speed", 1);
        if (Mathf.Abs(vector2.x) > Mathf.Abs(vector2.y))
        {

            if (vector2.x > 0)
            {
                transform.position = new Vector3(transform.position.x + Time.deltaTime * speed, transform.position.y, transform.position.z);
                transform.rotation = Quaternion.Euler(0, 0, -90);
            }
            else
            {
                transform.position = new Vector3(transform.position.x - Time.deltaTime * speed, transform.position.y, transform.position.z);
                transform.rotation = Quaternion.Euler(0, 0, 90);
            }
        }
        else
        {
            if (vector2.y > 0)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * speed, transform.position.z);
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime * speed, transform.position.z);
                transform.rotation = Quaternion.Euler(0, 0, 180);
            }
        }



    }


    private void OnInputTriggered(InputAction.CallbackContext context)
    {
        if (!isLocalPlayer) return;
        if (playerInput.actions["Fire"].IsPressed())
            if (context.action.name == "Fire")
            {
                if (context.phase == InputActionPhase.Performed)
                {
                    playerFire();
                    // fire.CreateBullect();
                }
            }
    }

    private void playerFire()
    {
        if (!isLocalPlayer) return;
        OpenFire();
    }

    public void OnActtacked(int damage)
    {
        health = health - damage;
        Debug.Log("health ========>" + health);
    }

    [Command]
    private void OpenFire()
    {
        fire.CreateBullect(BullctType.Player);
    }
}
