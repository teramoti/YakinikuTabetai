using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//アイテムのクラス

namespace Momoya
{

    public  class Item : Object
    {
        //列挙型の定義
        enum ItemType
        {
            Weapon, //武器
            Head,   //頭
            Body,　 //体
        }
        //定数の定義

        //変数の宣言
        [SerializeField]
        protected int startRarity;//装備の初期レアリティ
        [SerializeField]
        protected int setHp = 10; //ステータス用のhp
        [SerializeField]
        protected int setAttack = 10;//ステータス用の攻撃
        [SerializeField]
        protected int setSpeed = 10; //ステータス用のスピード
        //ステータス

        ObjectStatus.Status status;

        // Start is called before the first frame update
        void Start()
        {
            //レアリティセット
            rarity = startRarity;
            //ステータスをセット
            status.hp = setHp;
            status.attack = setAttack;
            status.speed = setSpeed;
        }

        // Update is called once per frame
        void Update()
        {

        }

        //アイテム特有の能力
        public virtual void Action()
        {
            //何もない
        }


    


        //HPのプロパティ
        public int HP
        {
            get { return status.hp; }
            set { status.hp = value; }
        }

        //Attackのプロパティ
        public int Attack
        {
            get { return status.attack; }
            set { status.attack = value; }
        }

        //Speedのプロパティ
        public int Speed
        {
            get { return status.speed; }
            set { status.speed = value; }
        }

    }

}