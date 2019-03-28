namespace CBX.Unity.Editors.Editor
{
    using System;
    using CBX.Unity.Editors.Editor;
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(AutoTileMap))]
    public class AutoTileMapEditor : Editor
    {
        int o = 0;
        // エディタにシーンビューのイベントを処理
        private void OnSceneGUI()
        {
            // AutoTileMapを取得
            AutoTileMap map = (AutoTileMap)target;
            // シーンビューの更新
            SceneView.RepaintAll();

            // 現在のイベントへの参照を取得します
            Event current = Event.current;



            for (int i = 0; i < map.m_rows; i++)
            {
                for (int j = 0; j < map.m_columns; j++)
                {
                    int r = UnityEngine.Random.Range(0, 100);
                    Draw(j, i, r);
                }
            }




            // シーンビューでUIチップを描画して、タイルの描画方法と消去方法をユーザーに通知します
            Handles.BeginGUI();
            GUI.Label(new Rect(10, Screen.height - 90, 100, 100), "LMB: Draw");
            GUI.Label(new Rect(10, Screen.height - 105, 100, 100), "RMB: Erase");
            Handles.EndGUI();
        }

        // 現在のツールを表示ツールに設定します
        private void OnEnable()
        {
            Tools.current = Tool.View;
            Tools.viewTool = ViewTool.FPS;
        }

        // ブロックの描画
        private void Draw(int x, int y, int random)
        {
            // AutoTileMapを取得
            AutoTileMap map = (AutoTileMap)target;

            // マウスの位置がどのタイルにあるか取得
            Vector2 tilePos = new Vector2(x, y);


            // その位置のブロックの中身を入れる
            GameObject cube = GameObject.Find(string.Format("Tile_{0}_{1}", tilePos.x, tilePos.y));

            if (map.m_stagePercent.Length >= y)
            {
                if (map.m_stagePercent[y] > random)
                {
                    // そのブロックが存在してたら返す
                    if (cube != null && cube.transform.parent != map.transform)
                    {
                        return;
                    }

                    //　nullだったら
                    if (cube == null)
                    {
                        // ブロックを作成する
                        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    }

                    // ブロックの位置の初期化
                    Vector3 tilePositionInLocalSpace = new Vector3((tilePos.x * map.m_tileWidth) + (map.m_tileWidth / 2), (tilePos.y * map.m_tileHeight) + (map.m_tileHeight / 2));
                    cube.transform.position = map.transform.position + tilePositionInLocalSpace;

                    // サイズの初期化
                    cube.transform.localScale = new Vector3(map.m_tileWidth, map.m_tileHeight, 1);

                    // 親子関係を結ぶ
                    cube.transform.parent = map.transform;

                    // マテリアルの初期化
                    cube.GetComponent<Renderer>().material = map.m_material;

                    // ブロックの名前の初期化
                    cube.name = string.Format("Tile_{0}_{1}", tilePos.x, tilePos.y);
                }
                else
                {
                    Erase(x, y);
                }
            }

        }

        // ブロックを消す処理
        private void Erase(int x, int y)
        {
            // AutoTileMapを取得
            AutoTileMap map = (AutoTileMap)target;

            // マウスの位置がどのタイルにあるか取得
            Vector2 tilePos = new Vector2(x, y);

            // 当たってるタイルを取得
            GameObject cube = GameObject.Find(string.Format("Tile_{0}_{1}", tilePos.x, tilePos.y));

            // ブロックの中がなかったら
            if (cube != null && cube.transform.parent == map.transform)
            {
                // そのブロックを消す
                UnityEngine.Object.DestroyImmediate(cube);
            }
        }


    }
}