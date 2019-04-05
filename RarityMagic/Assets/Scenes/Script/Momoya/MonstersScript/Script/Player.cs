using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーのスクリプト

namespace Momoya
{

    public class Player : Monster
    {
        //列挙型の定義
        enum HaveItem
        {
            Weapon, //武器
            Head,   //頭
            Body,    //体

            More,
        }

        //定数の定義
        

        public  const float cooltimeCount = 0.0f; //技を出したときにクールタイムを
        //変数の定義
        public Item[] haveItem = new Item[(int)HaveItem.More];
        [SerializeField]
        Vector3[] itemPos = new Vector3[(int)HaveItem.More];
        private float cooltime;                   //クールタイムをカウントする
       
        private Flag attackStateFlag;             //攻撃時のフラグ

        private Collision hitObject;              //ヒットしたオブジェクト

        [SerializeField] private Vector3 gravity;                  //重力
        [SerializeField] private Vector3 gravity2;                 //重力2

        private bool jumpFallFalg;

        private Vector3 lastPos;

        private Animator animator;

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

            animator = GetComponent<Animator>();

            jumpFallFalg = false;

            lastPos = transform.position;
        }

        //Move関数
        public override void Move()
        {
            //アイテムのポジション設定
            SetItemPos();
            //プレイヤーレアリティ
            PlayerRarity();
            //十字キーの入力をセット
            vec.x = Input.GetAxis("Horizontal");
            vec.y = Input.GetAxis("Vertical");

            //ベクトルxが0.0じゃない場合動いている
            if (Mathf.Abs(vec.x) != 0.0f)
            {
                flag.On((uint)StateFlag.Move);
                animator.SetBool("IsWalk", true);
            }

            //ベクトルxが0.0なら止まっている
            if (Mathf.Abs(vec.x) == 0.0f)
            {
                flag.Off((uint)StateFlag.Move);
                animator.SetBool("IsWalk", false);
            }

            StealRarity();

            GiveRarity();

            
            if(!flag.Is((uint)StateFlag.Jump))
            {
                animator.SetBool("IsJump", true);
                //重力を消す
                GetComponent<Rigidbody>().useGravity = false;

                if(!jumpFallFalg)
                {
                    GetComponent<Rigidbody>().AddForce(gravity, ForceMode.Acceleration);
                }
                else
                {
                    GetComponent<Rigidbody>().AddForce(gravity2, ForceMode.Acceleration);
                }

                float posY = transform.position.y - lastPos.y;
                if(posY > 0)
                {
                    jumpFallFalg = true;
                }
            }
            else
            {
                animator.SetBool("IsJump", false);
            }

            Debug.Log(vec);

            Jump(); //ジャンプ

            //最後の座標を入れる
            lastPos = transform.position;

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

        //アイテムのポジション設定
        public void SetItemPos()
        {
            for(int i= 0; i < (int)HaveItem.More; i++)
            {
                //アイテムのポジションを調整
                haveItem[i].transform.position = transform.position +  itemPos[i];
            }
        }

        //プレイヤーのレアリティを決める
        public void PlayerRarity()
        {
            int rarityCount = 0;
            for (int i = 0; i < (int)HaveItem.More; i++)
            {
                rarityCount += haveItem[i].Rarity;
            }

            rarity = rarityCount / (int)HaveItem.More ;
            Debug.Log("PlayerRarity" + rarity);
        }
        //アイテムの変更
        public void ChangeItem(Item item,int itemgroup)
        {
            haveItem[(int)itemgroup] = item;
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