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
    public bool canAttack;

    public static PIdleState IdleStateP = new PIdleState();
    public static PAttackState AttackStateP = new PAttackState();
    public static PMoveState MoveStateP = new PMoveState();
    public static PDeadState DeadStateP = new PDeadState();
    public static PWinState WinStateP = new PWinState();

    bool isWin;

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

    private void Start()
    {
        OnInit();
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
        HideTargetIndicator();
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
        this.gameObject.SetActive(true);
        ChangeState(Player.IdleStateP);
        ActiveTargetIndicator();
        isWin = false;
        NameCharacter = "Helo";
        CurWeaponType = (WeaponType)DataManager.ins.dt.idWeapon;
        ChangeWeapon(CurWeaponType);
        ChangeHat((HatType)DataManager.ins.dt.idHat);
        ChangePant((PantType)DataManager.ins.dt.idPant);
        ChangeShield((ShieldType)DataManager.ins.dt.idShield);
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        targetIndicator.OnDespawn();
    }

    public override void OnDeath()
    {
        base.OnDeath();
        if (currentState is PDeadState) return;
        ChangeState(Player.DeadStateP);
    }
    
    public void OnWin()
    {
        if (currentState is PWinState) return;
        ChangeState(Player.WinStateP);
    }    

    public override void Move()
    {
        if (IsDead)
        {
            OnDeath();
            return;
        } 

        if(isWin)
        {
            OnWin();
            return;
        }    

        movementDirection = new Vector3(joystick.Horizontal, 0, joystick.Vertical).normalized;
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            isMoving = true;
            ChangeState(Player.MoveStateP);
        }
        else
        {
            isMoving = false;
            SetTargetCharacter(sightCharacter.GetNearestTarget(CharacterTF()));
            if(currentState is PMoveState)
            {
                ChangeState(Player.IdleStateP);

            }
            if(Target != null && canAttack)
            {
                ChangeState(Player.AttackStateP);

            } 
        }
    }

    public override void Attack()
    {
        base.Attack();
    }

    public bool IsWin
    {
        get { return isWin; }
        set { isWin = value; }
    }
}
