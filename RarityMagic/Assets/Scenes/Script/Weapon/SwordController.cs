using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    private bool attackFlag;
    // Start is called before the first frame update
    void Start()
    {
        attackFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            attackFlag = true;
            
        }

        if(attackFlag)
        {
            if(transform.rotation.z >= -45)
            {
                transform.Rotate(new Vector3(0, 0, transform.rotation.z - 1));
            }
            else
            {
                attackFlag = false;
            }
        }
    }
}
