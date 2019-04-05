using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//writer name is SatoMomoya
//ゴールした時のスクリプト
namespace Momoya
{
    public class Goal : MonoBehaviour
    {
        //定数の宣言

        //変数の宣言
        [SerializeField]
        private Player player; //プレイヤー

        [SerializeField]
        private int rarityCondition;        //ゴール時のレアリティ条件
        Flag goalFlag;   //ゴールに関するフラグ

        enum GoalState
        {
            PlayerTouch = (1 << 0),//プレイヤーがゴールに触れた
            CanGoal     = (1 << 1),//ゴール可能 
            CannotGoal  = (1 << 2) //ゴール不可能
        }


        // Start is called before the first frame update
        void Start()
        {
            goalFlag = GetComponent<Flag>();
        }

        // Update is called once per frame
        void Update()
        {
            //もしプレイヤーがゴールに触れた場合
            if (goalFlag.Is((uint)GoalState.PlayerTouch))
            {
                //ゴール条件を満たしていた場合
                if (player.Rarity > rarityCondition - 1)
                {
                    //ゴール
                    goalFlag.On((uint)GoalState.CanGoal);
                    Debug.Log("ゴールできた");
                }
                else
                {
                    //ゴールできない
                    goalFlag.On((uint)GoalState.CannotGoal);
                    Debug.Log("ゴールできない");
                }
            }
        }

        //当たり判定(トリガー)
        private void OnTriggerEnter(Collider other)
        {
            switch (other.tag)
            {
                case "Player": goalFlag.On((uint)GoalState.PlayerTouch); break;

            }

        }

        //離れた判定(トリガー)
        private void OnTriggerExit(Collider other)
        {
            switch (other.tag)
            {
                case "Player": goalFlag.Off((uint)GoalState.PlayerTouch); break;

            }

        }

    }

}