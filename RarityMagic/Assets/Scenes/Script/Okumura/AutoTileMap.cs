//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/
//! @file   AutoTileMap.cs
//!
//! @brief  ゲーム関連のC++ファイル
//!
//! @date   2019/3/10
//!
//! @author オクムラ イヤゴ
//__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/__/

// 名前空間の使用 ==========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CBX.Unity.Editors.Editor;
using UnityEditor;

//----------------------------------------------------------------------
//!
//! @brief 自動タイルマップ
//!
//----------------------------------------------------------------------
public class AutoTileMap : MonoBehaviour
{

    public int m_rows = 10;   // タイルの行数
    public int m_columns = 20;　// タイルの列数
    public float m_tileWidth = 1;　// タイル幅
    public float m_tileHeight = 1;　// タイル高さ
    public Color m_outLineColor = Color.blue; // 外の線の色
    public Color m_mapLineColor = Color.white; // マップの線の色
    public Material m_material; // マテリアル
    public string m_tagName; // タグ
    [SerializeField, Range(0, 100)]
    public int[] m_stagePercent; // ブロックの出る確率
    int m_item = 100;

    private void Start()
    {

        for (int i = 0; i < m_rows; i++)
        {
            for (int j = 0; j < m_columns; j++)
            {
                int r = UnityEngine.Random.Range(0, 100);
                Draw(j, i, r);
            }
        }

        for (int i = 0; i < m_item; i++)
        {
            int row = UnityEngine.Random.Range(0, m_rows);
            int column = UnityEngine.Random.Range(0, m_columns);

            Item(column, row);
        }
    }
    // グリッドの描画処理
    private void OnDrawGizmosSelected()
    {
        // オブジェクトの初期位置の取得
        Vector3 position = transform.position;
        // 外の線の色を決める
        Gizmos.color = m_outLineColor;
        // 左下から右下の線を引く
        Gizmos.DrawLine(position,
            position + new Vector3(m_columns * m_tileWidth, 0, 0));
        // 左下から左上の線を引く
        Gizmos.DrawLine(position,
            position + new Vector3(0, m_rows * m_tileHeight, 0));
        // 右下から右上の線を引く
        Gizmos.DrawLine(position + new Vector3(m_columns * m_tileWidth, 0, 0),
            position + new Vector3(m_columns * m_tileWidth, m_rows * m_tileHeight, 0));
        // 左上から右上の線を引く
        Gizmos.DrawLine(position + new Vector3(0, m_rows * m_tileHeight, 0)
            , position + new Vector3(m_columns * m_tileWidth, m_rows * m_tileHeight, 0));
        // マップの色を決める
        Gizmos.color = m_mapLineColor;
        // 列数分回す
        for (float i = 1; i < m_columns; i++)
        {
            // 横の線を引く
            Gizmos.DrawLine(position + new Vector3(i * m_tileWidth, 0, 0), position + new Vector3(i * m_tileWidth, m_rows * m_tileHeight, 0));
        }
        // 行数分回す
        for (float i = 1; i < m_rows; i++)
        {
            // 縦の線を引く
            Gizmos.DrawLine(position + new Vector3(0, i * m_tileHeight, 0), position + new Vector3(m_columns * m_tileWidth, i * m_tileHeight, 0));
        }
    }

    // アイテムの描画処理
    private void Item(int row, int column)
    {



        UnityEngine.GameObject item = UnityEngine.GameObject.Find(string.Format("Item_{0}_{1}", 1, column));


        //　nullだったら
        if (item == null)
        {
            // ブロックを作成する
            item = UnityEngine.GameObject.CreatePrimitive(PrimitiveType.Sphere);
        }

        // ブロックの位置の初期化
        Vector3 tilePositionInLocalSpace = new Vector3((row * m_tileWidth) + (m_tileWidth / 2), (column * m_tileHeight) + (m_tileHeight / 2));
        item.transform.position = transform.position + tilePositionInLocalSpace;

        // サイズの初期化
        item.transform.localScale = new Vector3(m_tileWidth, m_tileHeight, 1);

        // 親子関係を結ぶ
        item.transform.parent = transform;

        // マテリアルの初期化
        item.GetComponent<Renderer>().material = m_material;


        // ブロックの名前の初期化
        item.name = string.Format("Item_{0}_{1}", row, column);





    }

    // ブロックの描画
    private void Draw(int x, int y, int random)
    {


        // マウスの位置がどのタイルにあるか取得
        Vector2 tilePos = new Vector2(x, y);


        // その位置のブロックの中身を入れる
        UnityEngine.GameObject cube = UnityEngine.GameObject.Find(string.Format("AutoTile_{0}_{1}", tilePos.x, tilePos.y));

        if (m_stagePercent.Length >= y)
        {
            if (m_stagePercent[y] > random)
            {
                // そのブロックが存在してたら返す
                if (cube != null && cube.transform.parent != transform)
                {
                    return;
                }

                //　nullだったら
                if (cube == null)
                {
                    // ブロックを作成する
                    cube = UnityEngine.GameObject.CreatePrimitive(PrimitiveType.Cube);
                }

                // ブロックの位置の初期化
                Vector3 tilePositionInLocalSpace = new Vector3((tilePos.x * m_tileWidth) + (m_tileWidth / 2), (tilePos.y * m_tileHeight) + (m_tileHeight / 2));
                cube.transform.position = transform.position + tilePositionInLocalSpace;

                // サイズの初期化
                cube.transform.localScale = new Vector3(m_tileWidth, m_tileHeight, 1);

                // 親子関係を結ぶ
                cube.transform.parent = transform;

                // マテリアルの初期化
                cube.GetComponent<Renderer>().material = m_material;

                // ブロックの名前の初期化
                cube.name = string.Format("AutoTile_{0}_{1}", tilePos.x, tilePos.y);

                // タグの追加
                cube.tag = m_tagName;
            }
            else
            {
                // ブロックを消す
                Erase(x, y);
            }
        }

    }

    // ブロックを消す処理
    private void Erase(int x, int y)
    {


        // マウスの位置がどのタイルにあるか取得
        Vector2 tilePos = new Vector2(x, y);

        // 当たってるタイルを取得
        UnityEngine.GameObject cube = UnityEngine.GameObject.Find(string.Format("AutoTile_{0}_{1}", tilePos.x, tilePos.y));

        // ブロックの中がなかったら
        if (cube != null && cube.transform.parent == transform)
        {
            // そのブロックを消す
            UnityEngine.Object.DestroyImmediate(cube);
        }
    }
}
