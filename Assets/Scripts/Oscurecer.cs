using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscurecer : MonoBehaviour
{
    // Start is called before the first frame update

    private SpriteRenderer render;

    public Material material;

    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        render.material = material;

    }
}
