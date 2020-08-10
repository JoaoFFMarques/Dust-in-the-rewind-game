using UnityEngine;

public class RewindBlock : MonoBehaviour
{
    private GameManager m_GameManager;

    private void Start()
    {
        m_GameManager = FindObjectOfType(typeof(GameManager)) as GameManager;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().StartRewind();
            m_GameManager.ShowEnemies();

        }
    }
}
