using Game.Core;
using Game.Estate.Ultima.Resources;

namespace Game.Estate.Ultima.Data
{
    public class ContextMenuItem
    {
        public ContextMenuItem(int responseCode, int stringId, int flags, int hue)
        {
            // get the resource provider
            var provider = ServiceSet.Get<IResourceProvider>();
            Caption = provider.GetString(stringId);
            ResponseCode = responseCode;
        }

        public int ResponseCode { get; }

        public string Caption { get; }

        public override string ToString() => $"{Caption} [{ResponseCode}]";
    }
}
