using Game.Core.Components;
using Game.Estate.Tes.Records;

namespace Game.Estate.Tes.Components
{
    public class BOOKComponent : BASEComponent
    {
        static PlayerComponent _player = null;
        static TesUIManager _uiManager = null;

        public static PlayerComponent Player
        {
            get
            {
                if (_player == null)
                    _player = FindObjectOfType<PlayerComponent>();
                return _player;
            }
        }

        public static TesUIManager UIManager
        {
            get
            {
                if (_uiManager == null)
                    _uiManager = FindObjectOfType<TesUIManager>();
                return _uiManager;
            }
        }

        void Start()
        {
            usable = true;
            pickable = false;
            var book = (BOOKRecord)record;
            objData.interactionPrefix = "Read ";
            objData.name = book.FULL.Value ?? book.EDID.Value;
            //objData.icon = TESUnity.instance.Engine.textureManager.LoadTexture(BOOK.ITEX.value, "icons");
            objData.weight = book.DATA.Weight.ToString();
            objData.value = book.DATA.Value.ToString();
        }

        public override void Interact()
        {
            var book = (BOOKRecord)record;
            if (book.DESC.Value == null)
            {
                if (book.DATA.Flags == 1) OnTakeScroll(book);
                else OnTakeBook(book);
            }
            if (book.DATA.Flags == 1)
            {
                UIManager.Scroll.Show(book);
                UIManager.Scroll.OnClosed += OnCloseScroll;
                UIManager.Scroll.OnTake += OnTakeScroll;
            }
            else
            {
                UIManager.Book.Show(book);
                UIManager.Book.OnClosed += OnCloseBook;
                UIManager.Book.OnTake += OnTakeBook;
            }
            Player.Pause(true);
        }

        void OnTakeScroll(BOOKRecord obj)
        {
            var inventory = FindObjectOfType<PlayerInventory>();
            inventory.Add(this);
        }

        void OnCloseScroll(BOOKRecord obj)
        {
            UIManager.Scroll.OnClosed -= OnCloseScroll;
            UIManager.Scroll.OnTake -= OnTakeScroll;
            Player.Pause(false);
        }

        void OnTakeBook(BOOKRecord obj)
        {
            var inventory = FindObjectOfType<PlayerInventory>();
            inventory.Add(this);
        }

        void OnCloseBook(BOOKRecord obj)
        {
            UIManager.Book.OnClosed -= OnCloseBook;
            UIManager.Book.OnTake -= OnTakeBook;
            Player.Pause(false);
        }
    }
}