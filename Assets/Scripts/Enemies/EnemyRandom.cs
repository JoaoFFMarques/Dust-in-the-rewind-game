using System.Collections;
using UnityEngine;

public class EnemyRandom : MonoBehaviour, IEnemy
{
    private Animator m_EnemyAni;
    private SpriteRenderer m_EnemySR;
    private BoxCollider m_EnemyBC;
    private bool m_IsLookingLeft;

    private Vector3 m_Movement;
    private Vector3 m_Destination;
    private bool m_IsMoving;

    public BoxCollider m_MoveArea;
    public int m_MaxDistance;
    public float m_BoundsSizeX;
    public float m_BoundsSizeY;
    public float m_MoveSpeedy;
    public float m_TimetoWalk;

    void Start()
    {
        m_MoveArea.size = new Vector2(m_BoundsSizeX, m_BoundsSizeY);
        m_EnemyAni = GetComponent<Animator>();
        m_EnemySR = GetComponent<SpriteRenderer>();
        m_EnemyBC = GetComponent<BoxCollider>();
        StartCoroutine("Walk");
    }
    void Update()
    {
        if(m_Movement.x > 0 && m_IsLookingLeft)
            Rotate();
        else if(m_Movement.x < 0 && !m_IsLookingLeft)
            Rotate();

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
    public void Move()
    {
        if(!m_MoveArea.bounds.Contains(m_Destination))
            SortDirection();
        else
            transform.position = Vector3.MoveTowards(transform.position, m_Destination, 1 * m_MoveSpeedy * Time.fixedDeltaTime);

        if(transform.position == m_Destination)
            m_IsMoving = false;
    }
    private void SortDirection()
    {
        int rand = Random.Range(0, 100);

        if(rand < 33)
            m_Movement.x = -m_MaxDistance;
        else if(rand < 66)
            m_Movement.x = m_MaxDistance;
        else
            m_Movement.x = 0;

        rand = Random.Range(0, 100);

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
