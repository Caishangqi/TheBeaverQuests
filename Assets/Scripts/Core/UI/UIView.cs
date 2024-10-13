using System;
using UnityEngine;

namespace Core.UI
{
    public class UIView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;  //绘制图标

        public void Pause()
        {
            Time.timeScale = 0;
        }
    }
}