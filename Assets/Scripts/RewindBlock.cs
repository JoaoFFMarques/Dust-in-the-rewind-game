using UnityEngine;

public class RewindBlock : MonoBehaviour
{
    public GameObject m_Fog;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().m_IsRewind = true;
            m_Fog.SetActive(false);
        }
    }

}
