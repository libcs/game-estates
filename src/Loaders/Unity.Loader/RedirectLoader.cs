//using Redirect = Gamer.Estate.Tes.Loader.LoadAsset;
//using Redirect = Gamer.Estate.Tes.Loader.LoadData;
using Redirect = Gamer.Estate.Tes.Loader.LoadEngine;

public class RedirectLoader : UnityEngine.MonoBehaviour
{
    public static void Awake() => Redirect.Awake();
    public static void Start() => Redirect.Start();
    public static void OnDestroy() => Redirect.OnDestroy();
    public static void Update() => Redirect.Update();
}