﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;

//プレイヤーのデータは
//名前
//レアリティ
//HP
//攻撃力
//武器
//装備(頭)
//装備(胴体)
//の順番で保存されている

namespace Momoya
{

    public class SavePlayer : MonoBehaviour
    {
        //列挙型の宣言
        enum PlayerData
        {
            Name,  //名前
            Rarity,//レアリティ
            HP,    //体力
            Attack,//攻撃力
            Speed, //スピード
            Weapon,//武器
            Head,  //頭
            Body  //体
            
        }
        
        //変数の宣言
        public Monster monster;

        private string fileName; //ファイルの名前
        private string filePath; //ファイルパス

        private List<string> _csvData;
        // Start is called before the first frame update
        void Start()
        {
            fileName = "PlayerData.csv";
            filePath = Application.dataPath + @"\Scenes\Data\" + fileName;
            _csvData = new List<string>();


            SetData();
        }

        // Update is called once per frame
        void Update()
        {

            //テスト
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Save();
            }

        }


        //セーブ時の関数
        private void Save()
        {
            StreamWriter sw = new StreamWriter(filePath, false, Encoding.GetEncoding("Unicode"));
            // データ出力


            //名前を書き込む
             string[] name = { "Name", "" + monster.Name.ToString() };
             string namewriter = string.Join(",", name);
             sw.WriteLine(namewriter);
               //レアリティを書き込む
                string[] rarity = { "Rarity", "" + monster.Rarity.ToString() };
                string raritywrite = string.Join(",", rarity);
                sw.WriteLine(raritywrite);
                //HPを書き込む
                string[] hp = { "HP", "" + monster.HP.ToString() };
                string hpwrite = string.Join(",", hp);
                sw.WriteLine(hpwrite);
                //攻撃力を書き込む
                string[] attack = { "Attack", "" + monster.Attack.ToString() };
                string attackwrite = string.Join(",", attack);
                sw.WriteLine(attackwrite);
                //スピードを書き込む
                string[] speed = { "Speed", "" + monster.Speed.ToString() };
                string sppedwriter = string.Join(",", speed);
                sw.WriteLine(sppedwriter);
              //武器を書き込む
              string[] weapon = { "Weapon", "" + "None" };
                string weaponwrite = string.Join(",", weapon);
                sw.WriteLine(weaponwrite);
               //装備(頭)を書き込む
                string[] head = { "Head", "" + "None" };
                string headwrite = string.Join(",", head);
                sw.WriteLine(headwrite);
               // 装備(頭)を書き込む
                string[] body = { "Body", "" + "None" };
                string bodywrite = string.Join(",", body);
                sw.WriteLine(bodywrite);

            // StreamWriterを閉じる
            sw.Close();

        }
        
        //csvファイルから手に入れたデータをモンスターに入れる
        public void SetData()
        {
            ReadFile();

            monster.Name = _csvData[(int)PlayerData.Name];//名前
            int tmp;
            
            Int32.TryParse(_csvData[(int)PlayerData.Rarity], out tmp);//レアリティ
            monster.Rarity = tmp; //int型に変換したものを入れる

            Int32.TryParse(_csvData[(int)PlayerData.HP], out tmp); //HP
            monster.HP = tmp;//int型に変換したものを入れる

            Int32.TryParse(_csvData[(int)PlayerData.Attack], out tmp); //攻撃力
            monster.Attack = tmp;//int型に変換したものを入れる

            Int32.TryParse(_csvData[(int)PlayerData.Speed], out tmp);
            monster.Speed = tmp;

            //確認用
            Debug.Log("Name" + monster.Name);
            Debug.Log("Rarity" + monster.Rarity);
            Debug.Log("HP" + monster.HP);
            Debug.Log("Attack" + monster.Attack);
            Debug.Log("Speed" + monster.Speed);
            //武器はでき次第追加で書く
        }

        //ファイル読み込み
        public void ReadFile()
        {
            _csvData.Clear();

            //　一括で取得
            string[] texts = File.ReadAllText(filePath).Split(new char[] { ',', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var text in texts)
            {
                //データ内のいらない文字列を飛ばす
                if (text != "Name" && text != "Rarity" &&
                    text != "HP" && text != "Attack" && text != "Speed" &&
                    text != "Weapon" && text != "Head" && text != "Body")
                {
                    _csvData.Add(text); //カンマ区切りでデータを取得
                }

            }



        }
    }
}