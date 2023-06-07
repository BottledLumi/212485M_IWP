using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeIcon : MonoBehaviour
{
    private Sprite icon;
    [SerializeField] Image iconImage;
    [SerializeField] CanvasRenderer iconCanvasRenderer;
    public Sprite Icon
    {
        get { return icon; }
        set { 
            icon = value;
            
            if (!icon)
                iconCanvasRenderer.SetAlpha(0f);
            else
            {
                iconImage.sprite = Icon;
                iconCanvasRenderer.SetAlpha(1f);
            }
        }
    }
}
