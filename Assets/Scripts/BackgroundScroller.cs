using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] float scrollingSpeed = 5f;
    Material myMaterial;
    Vector2 offset;
    // Update is called once per frame
    private void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
        offset = new Vector2(scrollingSpeed, 0f);
    }


    void Update()
    {
        myMaterial.mainTextureOffset += offset * Time.deltaTime;
    }
}
