using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapselMove : MonoBehaviour
{
    float _loopCount;
    bool _isAnimating;

    float rotationTime;

    // Start is called before the first frame update
    void Start()
    {
        rotationTime = 0;

    }

    // Update is called once per frame
    void Update()
    {
        rotationTime += Time.deltaTime;

        if(rotationTime>=0.5f)
        {
            _loopCount = 0;
             _isAnimating = true;
            rotationTime = 0.0f;
        }

        if (_isAnimating)
        {
        }

        {
            Vector3 eulerAngle = new Vector3(0f, 0f, -5f);
            transform.Rotate(eulerAngle);

            _loopCount++;

            if (_loopCount == 18)
            {
                _isAnimating = false;
            }
        }
    }
}
