// ########################################################
// #
// #
// #            Script written by Felix
// #
// #            Referenced and sourced:
// #            JollyFranchers - Youtube
// #
// #
// #
// #
// #
// ########################################################

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustSortingLayer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = (int)(transform.position.y * -100);
    }

}
