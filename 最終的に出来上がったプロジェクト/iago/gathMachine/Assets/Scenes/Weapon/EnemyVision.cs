using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    private EnemyController enemyController;
    private Vector3 playerPos;
    // Start is called before the first frame update
    void Start()
    {
        enemyController = FindObjectOfType<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay(Collider other)
    {

        switch (other.transform.tag)
        {
            //視界に入っている間
            case "Player":
                enemyController.VisionFlag = true;
                playerPos = other.transform.position;
                break;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        switch (other.transform.tag)
        {
            //視界から出たら
            case "Player":
                enemyController.VisionFlag = false;

                break;
        }
    }

    public Vector3 PlayerPos
    {
        get { return playerPos; }
        set { playerPos = value; }
    }
}
