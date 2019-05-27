public class Loader : UnityEngine.MonoBehaviour
{
    void Awake() => RedirectLoader.Awake();
    void Start() => RedirectLoader.Start();
    void OnDestroy() => RedirectLoader.OnDestroy();
    void Update() => RedirectLoader.Update();
}
