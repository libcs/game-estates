//namespace Asset.Tes.Loader
//{
//    public static class LoadObject
//    {
//        static IAssetPack Asset;

//        public static void Awake()
//        {
//            Utils.InUnity = true;
//        }

//        //var assetUri = "file:///C:/Program%20Files%20(x86)/Steam/steamapps/common/Morrowind/Data%20Files/Morrowind.*";
//        //var dataUri = "file:///C:/Program%20Files%20(x86)/Steam/steamapps/common/Morrowind/Data%20Files/Morrowind.esm#Morrowind";
//        //var file2Uri = "file://192.168.1.3/User/_ASSETS/Fallout4/Textures1";
//        //var file4Uri = "http://192.168.1.3/assets/Morrowind/Morrowind.bsa";
//        //var file4Uri = "http://192.168.1.3/assets/Morrowind/Morrowind.bsa";
//        public static void Start()
//        {
//            //var assetUri = "game://Morrowind/Morrowind.bsa";
//            //var dataUri = "game://Morrowind/Morrowind.esm";
//            var assetUri = "game://SkyrimVR/Skyrim*";
//            //var dataUri = "game://SkyrimVR/Skyrim.esm";
//            //var assetUri = "game://Fallout4VR/Fallout4*";
//            //var dataUri = "game://Fallout4VR/Fallout4.esm";

//            var assetManager = AssetManager.GetAssetManager(EngineId.Tes);
//            Asset = assetManager.GetAssetPack(assetUri).Result;
//            //Data = assetManager.GetDataPack(dataUri).Result;

//            // Morrowind
//            //MakeObject("meshes/i/in_dae_room_l_floor_01.nif");
//            //MakeObject("meshes/w/w_arrow01.nif");
//            //MakeObject("meshes/x/ex_common_balcony_01.nif");

//            // Skyrim
//            var nifFileLoadingTask = Asset.LoadObjectInfoAsync("meshes/actors/alduin/alduin.nif").Result;
//            MakeObject("meshes/markerx.nif");
//            //MakeObject("meshes/w/w_arrow01.nif");
//            //MakeObject("meshes/x/ex_common_balcony_01.nif");

//        }

//        static void MakeObject(string path) => Asset.CreateObject(path);

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
