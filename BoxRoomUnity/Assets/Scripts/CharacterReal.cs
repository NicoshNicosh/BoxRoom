using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static DirectionUtils;

public class CharacterReal : MonoBehaviour
{

    private Rigidbody Rigidbody;

    public float WalkSpeed;
    public float TurnSpeed;

    public Animator CharacterAnimator;
    public DirectionFlags InputDirection;
    public DirectionFlags CurrentDirection = DirectionFlags.Up;

    public List<ActionPoint> actionPoints;
    public ActionPoint CurrentAp;


    
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        InputDirection = GetInputDirection();
        if (InputDirection != DirectionFlags.None) CurrentDirection = InputDirection;
        HandleMovement(InputDirection);
        HandleRotation(CurrentDirection);
        HandleActionPoints();
    }

    private void HandleActionPoints()
    {
        var pt = actionPoints
            .Where(it => it.IsCharValid(this))
            .OrderBy(it => Vector2.Distance(it.transform.position, transform.position))
            .FirstOrDefault();

        if (pt != CurrentAp)
        {
            if (CurrentAp) CurrentAp.CharExit(this);
            CurrentAp = pt;
            if(CurrentAp) CurrentAp.CharEnter(this);
        }

        if (CurrentAp && Input.GetKeyDown(KeyCode.Space))
        {
            CurrentAp.CharInteract();
        }
    }

    private void HandleRotation(DirectionFlags inputDirection)
    {

        transform.rotation = Quaternion.Slerp(
            transform.rotation, 
            CurrentDirection.ToRotation(), 
            TurnSpeed * Time.deltaTime);
    }

    private void HandleMovement(DirectionFlags inputDirection)
    {
        if (inputDirection == DirectionFlags.None) return;
        
        Vector3 dir =  inputDirection.ToVector();

        Rigidbody.MovePosition(Rigidbody.position + dir.normalized * (WalkSpeed * Time.deltaTime));
        CharacterAnimator.SetBool("IsWalking", true);
    
    }

    private void OnTriggerEnter(Collider other)
    {
        var ap = other.GetComponent<ActionPoint>();
        actionPoints.Add(ap);
    }

    private void OnTriggerExit(Collider other)
    {
        var ap = other.GetComponent<ActionPoint>();
        actionPoints.Remove(ap);
    }
}
