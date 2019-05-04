//using Gamer.Base;

//namespace Gamer.Asset.Ultima.Loader
//{
//    public static class LoadObject
//    {
//        static IAssetPack Asset;

//        public static void Awake()
//        {
//        }

//        public static void Start()
//        {
//            Asset = assetManager.GetAssetPack(null).Result;

//            //MakeObject("sta010").transform.Translate(Vector3.left * 1 + Vector3.up);
//            //MakeObject("sta069").transform.Translate(Vector3.left * 1 + Vector3.down);

//            MakeObject("lnd001").transform.Translate(Vector3.right * 1);
//            MakeObject("lnd002").transform.Translate(Vector3.right * 1 + Vector3.up);
//            MakeObject("lnd516").transform.Translate(Vector3.right * 1 + Vector3.down);

//            //MakeObject("gmp065").transform.Translate(Vector3.back * 5);

//            //MakeTexture("sta010");
//            //MakeTexture("sta069");

//            //MakeTexture("lnd001");
//            //MakeTexture("lnd002");
//            //MakeTexture("lnd516");
//            //MakeTexture("lnd1137");

//            //MakeTexture("gmp065");

//            //MakeTexture("tex789");
//        }

//        static void MakeObject(string path) => Asset.CreateObject(path);

//        static void MakeTexture(string path)
//        {
//            var textureManager = new TextureManager(Asset);
//            var materialManager = new MaterialManager(textureManager);
//            var obj = GameObject.Find("Cube"); // CreatePrimitive(PrimitiveType.Cube);
//            var meshRenderer = obj.GetComponent<MeshRenderer>();
//            var materialProps = new MaterialProps
//            {
//                Textures = new MaterialTextures { MainFilePath = path },
//            };
//            meshRenderer.material = materialManager.BuildMaterialFromProperties(materialProps);
//        }

//        static void MakeCursor(string path)
//        {
//            var cursor = Asset.LoadTexture(path);
//            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
//        }

//        public static void OnDestroy()
//        {
//            Asset?.Dispose();
//            Asset = null;
//        }

//        public static void Update()
//        {
//        }
//    }
//}
