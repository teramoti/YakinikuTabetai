using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaHandle : MonoBehaviour
{

    float counter;
    [SerializeField]
    private Rigidbody rigidbody;


    // Start is called before the first frame update
    void Start()
    {
        // 移動しない設定
    rigidbody.constraints = RigidbodyConstraints.FreezePosition;
    // 位置 X だけ固定
    rigidbody.constraints = RigidbodyConstraints.FreezePositionX;

    }

    private void Update()
    {
        // 回転、位置ともに固定
        rigidbody.constraints = RigidbodyConstraints.FreezeAll;

        // ワールドのy軸に沿って1秒間に90度回転
        transform.Rotate(new Vector3(90, 0, 0) * Time.deltaTime, Space.World);
    }
}
