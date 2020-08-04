using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Stack<Vector3> m_RecordedPositions;

    private Rigidbody m_PlayerRB;
    private Animator m_PlayerAnim;
    private SpriteRenderer m_PlayerSR;
    private Vector3 m_Movement;
    private Vector3 m_Pos;

    private bool m_IsMoving;
    private bool m_IsLookingLeft;

    private bool m_IsDead;

    private bool m_IsTouchUp;
    private bool m_IsTouchDown;
    private bool m_IsTouchRight;
    private bool m_IsTouchLeft;

    private bool m_IsLookingUp, m_IsLookingDown, m_isLookingSide;

    private CounterUI m_Counter;

    public LayerMask m_ObjectLayer;
    public Transform m_Up, m_Down, m_Right, m_Left;
    public int m_MaxDistance;
    public bool m_IsRewind;
    public float m_MoveSpeedy;
    public bool m_End;

    void Start()
    {
       
        m_PlayerAnim = GetComponent<Animator>();
        m_PlayerRB = GetComponent<Rigidbody>();
        m_PlayerSR = GetComponent<SpriteRenderer>();
        m_RecordedPositions = new Stack<Vector3>();
        m_Counter = GameObject.FindGameObjectWithTag("Counter").GetComponent<CounterUI>();

        Record();
    }

    void Update()
    {
        if(!m_IsDead)
        {
            CheckTouch();

            if(!m_IsMoving)
                Control();
        }

        PlayerAnimations();       
    }

    private void FixedUpdate()
    {
        if(!m_IsDead)
        {
            if(m_IsMoving)
                Move();

            if(m_IsRewind)
                Rewind();
        }        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Wall") && !m_IsRewind)
        {
            m_IsMoving = false;
            Record();
            m_Movement.x = 0;
            m_Movement.y = 0;
        }

        if(collision.gameObject.CompareTag("Harm"))
        {
            m_IsDead = true;
            m_IsLookingUp = false;
            m_IsLookingDown = false;
            m_isLookingSide = false;
            m_IsMoving = false;
        }
    }

    private void CheckTouch()
    {
        m_IsTouchUp = Physics.CheckSphere(m_Up.position, 0.1f, m_ObjectLayer, QueryTriggerInteraction.Ignore);
        m_IsTouchDown = Physics.CheckSphere(m_Down.position, 0.1f, m_ObjectLayer, QueryTriggerInteraction.Ignore);
        m_IsTouchLeft = Physics.CheckSphere(m_Left.position, 0.1f, m_ObjectLayer, QueryTriggerInteraction.Ignore);
        m_IsTouchRight = Physics.CheckSphere(m_Right.position, 0.1f, m_ObjectLayer, QueryTriggerInteraction.Ignore);
    }

    private void ChangeDirection(Vector2 pos)
    {
        if(pos.y > 0)
        {
            m_IsLookingUp = true;
            m_IsLookingDown = false;
            m_isLookingSide = false;
        }
        else if(pos.y < 0)
        {
            m_IsLookingUp = false;
            m_IsLookingDown = true;
            m_isLookingSide = false;
        }
        else if(pos.x != 0)
        {
            m_IsLookingUp = false;
            m_IsLookingDown = false;
            m_isLookingSide = true;
        }
    }

    private void ChangeDirectionRewind(Vector2 pos)
    {
        if(pos.y > 0)
            m_PlayerAnim.Play("Player_Walk_Up");
        else if(pos.y < 0)
            m_PlayerAnim.Play("Player_Walk_Down");
        else 
            m_PlayerAnim.Play("Player_Walk_Side");

        if(pos.x > 0 && m_IsLookingLeft)
            Rotate();
        else if(pos.x < 0 && !m_IsLookingLeft)
            Rotate();
    }

    private void Control()
    {
        if(Input.GetButton("right") && !m_IsTouchRight)
        {
            m_Movement.x = m_MaxDistance;
            m_IsMoving = true;
        }
        else if(Input.GetButton("left") && !m_IsTouchLeft)
        {
            m_Movement.x = -m_MaxDistance;
            m_IsMoving = true;
        }
        else
            m_Movement.x = 0;

        if(Input.GetButton("up") && !m_IsTouchUp)
        {
            m_Movement.y = m_MaxDistance;
            m_IsMoving = true;
        }
        else if(Input.GetButton("down") && !m_IsTouchDown)
        {
            m_Movement.y = -m_MaxDistance;
            m_IsMoving = true;
        }
        else
            m_Movement.y = 0;

        ChangeDirection(m_Movement);

        m_Pos = transform.position + m_Movement;
    }

    private void Move()
    {        
        if(m_Movement.x > 0 && m_IsLookingLeft)
            Rotate();
        else if(m_Movement.x < 0 && !m_IsLookingLeft)
            Rotate();

        m_PlayerRB.MovePosition(m_PlayerRB.position + m_Movement * m_MoveSpeedy * Time.fixedDeltaTime);

        if(transform.position == m_Pos)
        {
            Record();
            m_IsMoving = false;
            m_Counter.Decrease(m_MaxDistance);
        }
    }   

    private void Rotate()
    {    
        m_IsLookingLeft = !m_IsLookingLeft;
        m_PlayerSR.flipX = !m_PlayerSR.flipX;
    }

    public void Record()
    {
        m_RecordedPositions.Push(transform.position);
    }

    public void Rewind()
    {
        ChangeDirectionRewind(transform.position - m_RecordedPositions.Peek());

        transform.position = Vector3.MoveTowards(transform.position, m_RecordedPositions.Peek(), 3 * Time.deltaTime);

        if(transform.position == m_RecordedPositions.Peek())
            m_RecordedPositions.Pop();

        if(m_RecordedPositions.Count == 0)
        {
            m_IsRewind = false;
            m_IsMoving = false;
            m_Movement.x = 0;
            m_Movement.y = 0;
        }
    }

    private void PlayerAnimations()
    {
        m_PlayerAnim.SetBool("IsWalking", m_IsMoving);
        m_PlayerAnim.SetBool("IsSide", m_isLookingSide);
        m_PlayerAnim.SetBool("IsUp", m_IsLookingUp);
        m_PlayerAnim.SetBool("IsDown", m_IsLookingDown);
        m_PlayerAnim.SetBool("IsDead", m_IsDead);
    }

    public void OnDead()
    {
        gameObject.SetActive(false);
        m_IsDead = false;
        m_End = true;
        
    }
}
