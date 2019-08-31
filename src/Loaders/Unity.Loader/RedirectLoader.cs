// CRY
using Redirect = Game.Estate.Cry.Loader.LoadAsset;

// TES
//using Redirect = Game.Estate.Tes.Loader.LoadAsset;
//using Redirect = Game.Estate.Tes.Loader.LoadData;
//using Redirect = Game.Estate.Tes.Loader.LoadEngine;

// ULTIMA
//using Redirect = Game.Estate.Ultima.Loader.LoadAsset;
//using Redirect = Game.Estate.Ultima.Loader.LoadData;
//using Redirect = Game.Estate.Ultima.Loader.LoadEngine;

public class RedirectLoader : UnityEngine.MonoBehaviour
{
    public static void Awake() => Redirect.Awake();
    public static void Start() => Redirect.Start();
    public static void OnDestroy() => Redirect.OnDestroy();
    public static void Update() => Redirect.Update();
}