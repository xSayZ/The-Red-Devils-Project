// ########################################################
// #
// #
// #            Script written by Felix
// #
// #            Referenced and sourced:
// #            Chris' Tutorials - Youtube
// #
// #
// #
// #
// #
// ########################################################

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour, IDataPersistence
{

    [Header("Variables")]
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float collisionOffset = 0.05f; 

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Contact Filter")]
    [SerializeField] private ContactFilter2D movementFilter;

    Vector2 movementInput;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    public event Action onBattleStart;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();;
    }

    public void LoadData(GameData data)
    {
        this.transform.position = data.playerPosition;
    }

    public void SaveData(GameData data)
    {
        data.playerPosition = this.transform.position;
    }

    void Update()
    {
        animator.SetFloat("Horizontal", movementInput.x);
        animator.SetFloat("Vertical", movementInput.y);
        animator.SetFloat("Speed", movementInput.sqrMagnitude);

        AdjustSortingLayer();
    }

    public void HandleFixedUpdate()
    {

        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            return;
        }

        HandleHorizontalMovement();

        // If movement input is not 0, try to move
        if (movementInput != Vector2.zero)
        {
            bool success = TryMove(movementInput);

            if (!success)
            {
                success = TryMove(new Vector2(movementInput.x, 0));

                if (!success)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }
            }


        }
    }

    private bool TryMove(Vector2 direction)
    {
        // Check for potential collisions
        int count = rb.Cast(
            movementInput, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
            movementFilter, // The setting that determine where a collision can occur on such as layers to collide with
            castCollisions, // List of collisions to store the found collisions into after the Cast is finished
            moveSpeed * Time.fixedDeltaTime + collisionOffset); // The amount to cast equal to the movement plus an offset

        if (count == 0){
            rb.MovePosition(rb.position + movementInput * moveSpeed * Time.fixedDeltaTime);
            return true;
        } else {
            return false;
        }
            
        
    }

    void HandleHorizontalMovement()
    {
        movementInput = InputManager.GetInstance().GetMoveDirection();

    }

    private void AdjustSortingLayer()
    {
        spriteRenderer.sortingOrder = (int)(transform.position.y * -100);
    }

}
