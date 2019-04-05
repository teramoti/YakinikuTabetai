using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackZone : MonoBehaviour
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

    public void OnTriggerEnter(Collider other)
    {
        switch (other.transform.tag)
        {
            //攻撃範囲に入ったら
            case "Player":
                enemyController.AttackFlag = true;
                playerPos = other.transform.position;
                break;
        }
    }

    public Vector3 PlayerPos
    {
        get { return playerPos; }
        set { playerPos = value; }
    }
}
