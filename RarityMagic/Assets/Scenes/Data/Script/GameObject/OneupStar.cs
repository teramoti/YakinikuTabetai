using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//writer name is SatoMomoya
//モンスターが触るととレアリティが一つ上がるオブジェクト

namespace Momoya
{

    public class OneupStar : MonoBehaviour
    {
        //定数の宣言

        [SerializeField]
        private int rarityUpNum;//レアリティの上がる度合い(念のため1以上上げることも設定できる)

        //変数の宣言
        Monster touchMonster;//触れたモンスター
        Flag    disappearFlag; //消えるフラグ

        //列挙型の宣言
        enum DisapperFlag
        {
            Disppear = (1 << 0),//消える
        }


        // Start is called before the first frame update
        void Start()
        {
            disappearFlag = GetComponent<Flag>();
        }

        // Update is called once per frame
        void Update()
        {
            //消えるフラグがtrueなら消える
            if(disappearFlag.Is((uint)DisapperFlag.Disppear))
            {
                Destroy(this.gameObject);
            }
        }

        //モンスターと接触した場合の処理
      

        private void OnTriggerEnter(Collider other)
        {
            switch (other.gameObject.tag)
            {
                //触れたモンスターのレアリティを上げ消えるフラグをtrueにする
                case "Player": other.gameObject.GetComponent<Monster>().Rarity = other.gameObject.GetComponent<Monster>().Rarity + rarityUpNum;
                    disappearFlag.On((uint)DisapperFlag.Disppear);    break;
                case "Monster": other.gameObject.GetComponent<Monster>().Rarity = other.gameObject.GetComponent<Monster>().Rarity + rarityUpNum;
                    disappearFlag.On((uint)DisapperFlag.Disppear); break;
            }
        }

    }
}