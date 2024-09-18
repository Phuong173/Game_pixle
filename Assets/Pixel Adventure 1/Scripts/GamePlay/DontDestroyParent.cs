using System;
using GameTool;
using UnityEngine;

namespace Pixel_Adventure_1.Scripts
{
    public class DontDestroyParent : SingletonMonoBehavior<DontDestroyParent>
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }
    }
}
