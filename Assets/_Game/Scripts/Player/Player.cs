using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Player : Character
{
    public static Player ins;
    [SerializeField] private DynamicJoystick joystick;
    public float moveSpeed;
    private IState<Player> currentState;

    public Vector3 movementDirection;
    bool isMoving;
    public bool canAttack;

    private void MakeInstance()
    {
        if (ins == null)
        {
            ins = this;
        }
    }

    private void Awake()
    {
        MakeInstance();
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
        Move();

    }

    public void ChangeState(IState<Player> newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    public override void OnInit()
    {
        base.OnInit();
        ChangeWeapon(WeaponType.Candy_Yellow);
    }

    public override void Move()
    {
        movementDirection = new Vector3(joystick.Horizontal, 0, joystick.Vertical).normalized;
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            isMoving = true;
            ChangeState(new PMoveState());
        }
        else
        {
            isMoving = false;
            SetTargetCharacter(sightCharacter.GetNearestTarget(this.transform));

            if (Target != null)
            {
                ChangeState(new PAttackState());
            }
            else
            {
                ChangeState(new PIdleState());
            }  
        }
    }

    public override void Attack()
    {
        base.Attack();
    }
}
