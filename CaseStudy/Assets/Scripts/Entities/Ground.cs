using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionExit(Collision other) 
    {
        IKillable ikillable=other.gameObject.GetComponent<IKillable>();
        if(ikillable!=null)
        {
            //add animation
            ikillable.Die();
        }
    }
}
