using UnityEngine;

public class RewindBlock : MonoBehaviour
{  
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().m_IsRewind = true;            
        }
    }

}
