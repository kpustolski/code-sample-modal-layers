namespace CodeSampleModalLayer
{
    public interface IModalLayer
    {
        public void ShowLayer();
        public void HideLayer();
        public string GetId();
        public void AssignId(int layerIndex);
    }
}