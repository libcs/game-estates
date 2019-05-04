using Gamer.Base.Core;
using Gamer.Estate.Ultima.Resources;

namespace Gamer.Estate.Ultima.Data
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
