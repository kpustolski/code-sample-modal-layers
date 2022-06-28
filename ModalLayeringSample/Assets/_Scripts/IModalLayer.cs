using UnityEngine.Events;

namespace CodeSampleModalLayer
{
    /* 
        Note on Modal Layer system: 
        Under the assumption that each modal we make acts the same way,
        We could add the modal layer functions into ModalBase and remove the IModalLayer interface.
        This would require a refactor of the current modal layering system.
        However, If we want to create a one-off modal that behaves differently or modify how each modal behaves individually, 
        then using the IModalLayer interface may be a better direction to go.
    */
    public interface IModalLayer
    {
        public void ShowLayer();
        public void HideLayer(UnityAction cbOnHideLayer);
        public string GetId();
        public void AssignId(int layerIndex);
    }
}