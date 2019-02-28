using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//writer name is Sato Momoya
//攻撃範囲スクリプト
namespace Momoya
{
    public class AttackZone : MonoBehaviour
    {
        //変数の宣言
        Flag hitFlag;       //当たっているかの判定
        //列挙型の宣言    
        enum HitState
        {
            Hit = (1 << 0), //当たった

        }


        // Start is callsed before the first frame update
        void Start()
        {
            hitFlag = GetComponent<Flag>();
         
        }



        // Update is called once per frame
        void Update()
        {

        }
       

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag != "Monster") return;

            switch(collision.gameObject.tag)
           {
                case "Monster": hitFlag.On((uint)HitState.Hit);
                    GetComponentInParent<Player>().HitObject = collision;   //当たっているオブジェクトを知らせる

                    break;   //モンスターと当たっていたら当たったことを知らせる

           }

        }

        //オブジェクトが離れた場合
        private void OnCollisionExit(Collision collision)
        {
            hitFlag.Off((uint)HitState.Hit);//あたったフラグをオフにする
            GetComponentInParent<Player>().HitObject = null;   //当たっているオブジェクト空にする
        }


        //当たったことを知らせるプロパティ
        public  bool HitFlag
        {
            get { return hitFlag.Is((uint)HitState.Hit); }
        }

    }
}