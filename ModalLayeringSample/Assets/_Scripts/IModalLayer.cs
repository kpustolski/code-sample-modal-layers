using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeSampleModalLayer
{
    public interface IModalLayer
    {
        public void ShowLayer();
        public void HideLayer();
        public string GetId();
    }
}