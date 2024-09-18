using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace GameTool
{
    public class CutoffMaskUI : Image
    {
        private static readonly int StencilComp = Shader.PropertyToID("_StencilComp");

        public override Material materialForRendering
        {
            get
            {
                Material mat = new Material(base.materialForRendering);
                mat.SetInt(StencilComp, (int)CompareFunction.NotEqual);
                return mat;
            }
        }
    }
}
