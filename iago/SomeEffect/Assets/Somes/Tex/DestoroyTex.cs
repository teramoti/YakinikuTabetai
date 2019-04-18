using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class DestoroyTex : MonoBehaviour
{
    [SerializeField]
    private VisualEffect m_VFX = null;
    [SerializeField]
    private SpriteRenderer m_Sprite = null;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            m_VFX.SendEvent("OnStart");
            Color color = m_Sprite.color;
            color.a = 1f;
            m_Sprite.color = color;
        }

        if (Input.GetMouseButtonDown(1))
        {
            Color color = m_Sprite.color;
            color.a = 1f;
            m_Sprite.color = color;
        }
    }
}
