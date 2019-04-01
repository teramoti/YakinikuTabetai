using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Momoya;
//writer name is SatoMomoya
//モンスターにとって必要な情報を入れたクラス
namespace Momoya
{
 abstract public class Monster : MonoBehaviour
    {
        //定数の宣言
        public const int MinimumRarity = 1;         //最低限のレアリティ
        public const int MaximumRarity = 6;         //最大限のレアリティ
        public const int FallPoint    = -5;         //落下ポイント
        [SerializeField]
        protected  float speed ;                       //スピード
        [SerializeField]
        protected  float jumpPower;                  //ジャンプパワー

        //変数の宣言
        protected int rarity;     //モンスターのレアリティ
        [SerializeField]
        protected int startRarity;//モンスターの初期レアリティ
        protected Vector3 startPos; //モンスターの初期位置
        protected Vector3 pos;    //モンスターのポジション
        protected Vector3 vec;    //モンスターのベクトル
        protected Vector3 monsterSpeed; //モンスターのスピード
        protected Flag flag;      //フラグ

       
        //列挙型の定義
        protected enum StateFlag //状態のフラグ
        {
            Move = (1 << 0),  //移動フラグ
            Jump = (1 << 1),  //ジャンプフラグ
            Deth = (1 << 2),  //死亡フラグ
        }

        protected enum MonsterState
        {
            Normal, //普通の状態
            Jump  , //ジャンプ状態

            NumState,
        }

        // Use this for initialization
        void Start()
        {
            rarity = startRarity; //初期レアリティをセット
            this.startPos = this.transform.position;
            this.vec = this.GetComponent<Rigidbody>().velocity;

            this.flag = GetComponent<Flag>();
            //初期化処理(子クラス用)
            Initialize();

        }

        // Update is called once per frame
        void Update()
        {


            PositionCtrl(); //ポジションの処理

            Move();         //移動の処理
        }
     
        //レアリティをゲットとセットするためのプロパティ
        public int Rarity
        {
            get { return this.rarity; }
            set
            {
                //レアリティの最小値のチェック
                if (value < MinimumRarity)
                {
                    Debug.Log("想定されていたレアリティより低い数値を渡されました");
                    value = MinimumRarity;//レアリティを設定されている数値に変える
                }
                rarity = value;
            }

        }

        //一番最初のレアリティをゲットとセットするプロパティ
        public int StartRarity
        {
            get { return startRarity; }
            set { startRarity = value; }
        }

        //ポジションをコントロールする関数
        protected void PositionCtrl()
        {
            Vector3 direction = new Vector3(vec.x, vec.y, vec.z) * speed;

            //移動させる
            GetComponent<Rigidbody>().velocity = new Vector3(direction.x, GetComponent<Rigidbody>().velocity.y, direction.z);

            if(transform.position.y < FallPoint)
            {
                this.transform.position = startPos;
            }
           
        }

        //初期化する関数(子クラス用)
        public virtual void Initialize()
        {

        }
        
        //移動する関数
        public abstract void Move();

        //何かと当たった時の関数
        protected void OnCollisionEnter(Collision collision)
        {
            //当たった何かのタグを調べる
            switch (collision.transform.tag)
            {
                case "Ground": flag.On((uint)StateFlag.Jump);   break; //groundと触れていればジャンプフラグをtrueにする
            }

        }

        //何かと離れたときの関数
        protected void OnCollisionExit(Collision collision)
        {
            //離れた何かのタグを調べる
            switch (collision.transform.tag)
            {
                case "Ground": flag.Off((uint)StateFlag.Jump); break; //groundを離れたらジャンプフラグoffにする
            }
        }

    }

}