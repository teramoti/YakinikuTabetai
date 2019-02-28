using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーのスクリプト

namespace Momoya
{

    public class Player : Monster
    {

        //定数の定義
        public  const float cooltimeCount = 0.0f; //技を出したときにクールタイムを
        //変数の定義
        private float cooltime;                   //クールタイムをカウントする
       
        private Flag attackStateFlag;             //攻撃時のフラグ

        private Collision hitObject;              //ヒットしたオブジェクト

        //列挙型の定義
       public enum AttackState                         //攻撃用のステート
        {
            CanAttack    = (1 << 0),   //攻撃可能
            CanNotAttack = (1 << 1),   //攻撃不可能
        }

        //初期化関数
        public override void Initialize()
        {
            this.attackStateFlag = GetComponent<Flag>();
            
            attackStateFlag.Off((uint)AttackState.CanAttack);       
            attackStateFlag.Off((uint)AttackState.CanNotAttack);    //アタックフラグをfalseに
        }

        //Move関数
        public override void Move()
        {
            //十字キーの入力をセット
            vec.x = Input.GetAxis("Horizontal");
            vec.y = Input.GetAxis("Vertical");

            //ベクトルxが0.0じゃない場合動いている
            if (Mathf.Abs(vec.x) != 0.0f)
            {
                flag.On((uint)StateFlag.Move);
            }

            //ベクトルxが0.0なら止まっている
            if (Mathf.Abs(vec.x) == 0.0f)
            {
                flag.Off((uint)StateFlag.Move);
            }

            StealRarity();

            GiveRarity();




            Jump(); //ジャンプ

            Debug.Log(this.rarity);

        }
        
        //レアリティを奪う関数
        private void StealRarity()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (GetComponentInChildren<AttackZone>().HitFlag)
                {
                    //当たっているオブジェクトのレアリティを下げ自分のレアリティを上げる
                    hitObject.gameObject.GetComponent<Monster>().Rarity = hitObject.gameObject.GetComponent<Monster>().Rarity - 1;
                    this.rarity += 1;
                  

                }
            }
        }

        //レアリティをあげる関数
        private void GiveRarity()
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                if (GetComponentInChildren<AttackZone>().HitFlag)
                {
                    //当たっているオブジェクトのレアリティを上げ自分のレアリティを下げる
                    hitObject.gameObject.GetComponent<Monster>().Rarity = hitObject.gameObject.GetComponent<Monster>().Rarity + 1;
                    this.rarity -= 1;
                }
            }
        }

        //ジャンプするための関数
        private void Jump()
        {
            //スペースキーを押された時、地面についていればジャンプする
            if (Input.GetKeyDown(KeyCode.Space) && flag.Is((uint)StateFlag.Jump))
            {
                GetComponent<Rigidbody>().AddForce(Vector3.up * jumpPower);
            }
        }

        //何が当たったのか知らせるプロパティ
        public Collision HitObject
        {
            get { return hitObject; }
            set { hitObject = value; }

        }


    }

}