using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleUiMove : MonoBehaviour
{
    float rightMax = 0.0f;   //左へ移動可能 (z)最大値
    float leftMax = -2.0f;    //右へ移動可能 (z)最大値
    float currentPosition;    //現在の位置(z)保存
    float direction = 2.0f;   //移動速度+方向

    void Start()
    {
        currentPosition = transform.position.z;
    }

    void Update()
    {
        currentPosition += Time.deltaTime * direction;


        if (currentPosition >= rightMax)
        {
            direction *= -1;
            currentPosition = rightMax;
        }


        //現在の位置(x) 右へ移動可能 (x)最大値より大きい、もしくは同じの場合
        //移動速度+方向-1を掛けて反転、現在の位置を右へ移動可能 (x)最大値に設定


        else if (currentPosition <= leftMax)
        {
            direction *= -1;
            currentPosition = leftMax;
        }


        //現在の位置(x) 左へ移動可能 (x)最大値より大きい、もしくは同じの場合
        //移動速度+方向-1を掛けて反転、現在の位置を左へ移動可能 (x)最大値に設定

        transform.position = new Vector3(transform.position.x, transform.position.y, currentPosition);
        //"Stone"の位置を計算された現在の位置に処理
    }
}
