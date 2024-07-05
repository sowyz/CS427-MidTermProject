using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollowTank : MonoBehaviour
{
    public Transform toFollow;
    RectTransform rectTransform;

    public void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void Update()
    {
        if(toFollow != null)
        {
            rectTransform.anchoredPosition = toFollow.localPosition;
        }
    }

}
