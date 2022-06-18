using UnityEngine.Events;

namespace CodeSampleModalLayer
{
    public interface IModalLayer
    {
        public void ShowLayer();
        public void HideLayer(UnityAction cbOnHideLayer);
        public string GetId();
        public void AssignId(int layerIndex);
    }
}