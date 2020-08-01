using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindBlock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().m_IsRewind=true;
        }
    }

}
