using UnityEngine;
using Redirect = Gamer.Estate.Tes.Loader.LoadObject;

public class Loader : MonoBehaviour
{
    void Awake() { Redirect.Awake(); }

    void Start() { Redirect.Start(); }

    void OnDestroy() { Redirect.OnDestroy(); }

    void Update() { Redirect.Update(); }
}
