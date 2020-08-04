using System;
using System.Collections;
using UnityEngine;

public class EnemySeeker : MonoBehaviour, IEnemy
{
    private Animator m_EnemyAni;
    private SpriteRenderer m_EnemySR;
    private bool m_IsLookingLeft;
    private GameObject m_PlayerPos;

    private Vector3 m_Movement;
    private Vector3 m_Destination;
    private bool m_IsMoving;

    private bool m_IsFollow;

    public float m_CloseDistance;

    public BoxCollider m_MoveArea;
    public int m_MaxDistance;
    public float m_BoundsSizeX;
    public float m_BoundsSizeY;
    public float m_MoveSpeedy;
    public float m_TimetoWalk;

    void Start()
    {
        m_MoveArea.size = new Vector2(m_BoundsSizeX, m_BoundsSizeY);
        m_PlayerPos = GameObject.FindGameObjectWithTag("Player");
        m_EnemyAni = GetComponent<Animator>();
        m_EnemySR = GetComponent<SpriteRenderer>();
        StartCoroutine("Walk");
    }
    void Update()
    {
        if(m_Movement.x > 0 && m_IsLookingLeft)
            Rotate();
        else if(m_Movement.x < 0 && !m_IsLookingLeft)
            Rotate();

        Seek();

        Move();

        m_EnemyAni.SetBool("IsWalk", m_IsMoving);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Wall"))
        {
            m_IsMoving = false;
            m_Movement.x = 0;
            m_Movement.y = 0;
        }
    }
    private void Rotate()
    {
        m_IsLookingLeft = !m_IsLookingLeft;
        m_EnemySR.flipX = !m_EnemySR.flipX;
    }
    public void Seek()
    {
        if((m_PlayerPos.transform.position - transform.position).sqrMagnitude <= Math.Pow(m_CloseDistance, 2))
        {
            m_IsFollow = true;
            m_IsMoving = true;
        }
        else
            m_IsFollow = false;

        if(!m_IsFollow && !m_MoveArea.bounds.Contains(transform.position))
            m_MoveArea.transform.position = transform.position;
    }
    public void Move()
    {
        if(!m_IsFollow)
        {
            if(!m_MoveArea.bounds.Contains(m_Destination))
                SortDirection();
            else
                transform.position = Vector3.MoveTowards(transform.position, m_Destination, m_MoveSpeedy * Time.fixedDeltaTime);

            if(transform.position == m_Destination)
                m_IsMoving = false;
        }
        else
            transform.position = Vector3.MoveTowards(transform.position, m_PlayerPos.transform.position, 2.5f * m_MoveSpeedy * Time.deltaTime);
    }
    private void SortDirection()
    {
        int rand = UnityEngine.Random.Range(0, 100);

        if(rand < 33)
            m_Movement.x = -m_MaxDistance;
        else if(rand < 66)
            m_Movement.x = m_MaxDistance;
        else
            m_Movement.x = 0;

        rand = UnityEngine.Random.Range(0, 100);

        if(rand < 33)
            m_Movement.y = -m_MaxDistance;
        else if(rand < 66)
            m_Movement.y = m_MaxDistance;
        else
            m_Movement.y = 0;

        if(m_Movement.x != 0 || m_Movement.y != 0)
            m_IsMoving = true;

        m_Destination = transform.position + m_Movement;

    }
    IEnumerator Walk()
    {
        if(!m_IsMoving)
            SortDirection();

        yield return new WaitForSeconds(m_TimetoWalk);
        StartCoroutine("Walk");
    }

}