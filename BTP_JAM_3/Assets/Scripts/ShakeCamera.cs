using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ShakeCamera : MonoBehaviour
{
    public RectTransform Camera;
    // Start is called before the first frame update
    void Start()
    {
        Camera.DOShakeAnchorPos(5f, new Vector2(3f,3f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
