namespace Game.Estate.Ultima.Resources
{
    public interface IResource<T>
    {
        T GetResource(int resourceIndex);
    }
}
