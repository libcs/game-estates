﻿using Game.Estate.Ultima.Resources;
using System.Collections.Generic;

namespace Game.Estate.Ultima.Data
{
    /// <summary>
    /// Contains a list of all chair objects, which are hardcoded in the legacy client.
    /// </summary>
    public static class Chairs
    {
        static Dictionary<int, ChairData> _chairs;

        static Chairs()
        {
            _chairs = new Dictionary<int, ChairData>();
            // 0x0459 - 0x045C - marble benches
            AddChairData(0x0459, Direction.South, ChairType.ReversibleFacing);
            AddChairData(0x045A, Direction.East, ChairType.ReversibleFacing);
            AddChairData(0x045B, Direction.South, ChairType.ReversibleFacing);
            AddChairData(0x045C, Direction.East, ChairType.ReversibleFacing);
            // 0x0A2A - 0x0A2B - two stools
            AddChairData(0x0A2A, Direction.South, ChairType.AnyFacing);
            AddChairData(0x0A2B, Direction.South, ChairType.AnyFacing);
            //0x0B2C - 0x0B33 - chairs
            AddChairData(0x0B2C, Direction.East, ChairType.ReversibleFacing);
            AddChairData(0x0B2D, Direction.South, ChairType.ReversibleFacing);
            AddChairData(0x0B2E, Direction.South, ChairType.SingleFacing);
            AddChairData(0x0B2F, Direction.East, ChairType.SingleFacing);
            AddChairData(0x0B30, Direction.West, ChairType.SingleFacing);
            AddChairData(0x0B31, Direction.North, ChairType.SingleFacing);
            AddChairData(0x0B32, Direction.South, ChairType.SingleFacing);
            AddChairData(0x0B33, Direction.East, ChairType.SingleFacing);
            //0x0B4E - 0x0B6A - chairs, benches, one stool
            AddChairData(0x0B4E, Direction.East, ChairType.SingleFacing);
            AddChairData(0x0B4F, Direction.South, ChairType.SingleFacing);
            AddChairData(0x0B50, Direction.North, ChairType.SingleFacing);
            AddChairData(0x0B51, Direction.West, ChairType.SingleFacing);
            AddChairData(0x0B52, Direction.East, ChairType.SingleFacing);
            AddChairData(0x0B53, Direction.South, ChairType.SingleFacing);
            AddChairData(0x0B54, Direction.North, ChairType.SingleFacing);
            AddChairData(0x0B55, Direction.West, ChairType.SingleFacing);
            AddChairData(0x0B56, Direction.East, ChairType.SingleFacing);
            AddChairData(0x0B57, Direction.South, ChairType.SingleFacing);
            AddChairData(0x0B58, Direction.West, ChairType.SingleFacing);
            AddChairData(0x0B59, Direction.North, ChairType.SingleFacing);
            AddChairData(0x0B5A, Direction.East, ChairType.SingleFacing);
            AddChairData(0x0B5B, Direction.South, ChairType.SingleFacing);
            AddChairData(0x0B5C, Direction.North, ChairType.SingleFacing);
            AddChairData(0x0B5D, Direction.West, ChairType.SingleFacing);
            AddChairData(0x0B5E, Direction.South, ChairType.AnyFacing);
            AddChairData(0x0B5F, Direction.East, ChairType.ReversibleFacing);
            AddChairData(0x0B60, Direction.East, ChairType.ReversibleFacing);
            AddChairData(0x0B61, Direction.East, ChairType.ReversibleFacing);
            AddChairData(0x0B62, Direction.East, ChairType.ReversibleFacing);
            AddChairData(0x0B63, Direction.East, ChairType.ReversibleFacing);
            AddChairData(0x0B64, Direction.East, ChairType.ReversibleFacing);
            AddChairData(0x0B65, Direction.South, ChairType.ReversibleFacing);
            AddChairData(0x0B66, Direction.South, ChairType.ReversibleFacing);
            AddChairData(0x0B67, Direction.South, ChairType.ReversibleFacing);
            AddChairData(0x0B68, Direction.South, ChairType.ReversibleFacing);
            AddChairData(0x0B69, Direction.South, ChairType.ReversibleFacing);
            AddChairData(0x0B6A, Direction.South, ChairType.ReversibleFacing);
            // 0x0B91 - 0x0B4 - benches with high backs
            AddChairData(0x0B91, Direction.South, ChairType.SingleFacing);
            AddChairData(0x0B92, Direction.South, ChairType.SingleFacing);
            AddChairData(0x0B93, Direction.East, ChairType.SingleFacing);
            AddChairData(0x0B94, Direction.East, ChairType.SingleFacing);
            // 0x1049 - 0x104A - benches
            AddChairData(0x1049, Direction.East, ChairType.ReversibleFacing);
            AddChairData(0x104A, Direction.South, ChairType.ReversibleFacing);
            // 0x11FC - bamboo stool
            AddChairData(0x11FC, Direction.South, ChairType.AnyFacing);
            // 0x1207 - 0x120C - stone benches
            AddChairData(0x1207, Direction.South, ChairType.ReversibleFacing);
            AddChairData(0x1208, Direction.South, ChairType.ReversibleFacing);
            AddChairData(0x1209, Direction.South, ChairType.ReversibleFacing);
            AddChairData(0x120A, Direction.South, ChairType.ReversibleFacing);
            AddChairData(0x120B, Direction.South, ChairType.ReversibleFacing);
            AddChairData(0x120C, Direction.South, ChairType.ReversibleFacing);
            //0x1218 - 0x121B - stone chairs
            AddChairData(0x1218, Direction.South, ChairType.SingleFacing);
            AddChairData(0x1219, Direction.East, ChairType.SingleFacing);
            AddChairData(0x121A, Direction.North, ChairType.SingleFacing);
            AddChairData(0x121B, Direction.West, ChairType.SingleFacing);
            // 0x1DC7 - 0x1DD2 - long sandstone / marbe benches
            AddChairData(0x1DC7, Direction.East, ChairType.ReversibleFacing);
            AddChairData(0x1DC8, Direction.East, ChairType.ReversibleFacing);
            AddChairData(0x1DC9, Direction.East, ChairType.ReversibleFacing);
            AddChairData(0x1DCA, Direction.South, ChairType.ReversibleFacing);
            AddChairData(0x1DCB, Direction.South, ChairType.ReversibleFacing);
            AddChairData(0x1DCC, Direction.South, ChairType.ReversibleFacing);
            AddChairData(0x1DCD, Direction.East, ChairType.ReversibleFacing);
            AddChairData(0x1DCE, Direction.East, ChairType.ReversibleFacing);
            AddChairData(0x1DCF, Direction.East, ChairType.ReversibleFacing);
            AddChairData(0x1DD0, Direction.South, ChairType.ReversibleFacing);
            AddChairData(0x1DD1, Direction.South, ChairType.ReversibleFacing);
            AddChairData(0x1DD2, Direction.South, ChairType.ReversibleFacing);
            // 0x2DE3 - 0x2DE6 - elven chairs 1
            AddChairData(0x2DE3, Direction.East, ChairType.SingleFacing);
            AddChairData(0x2DE4, Direction.South, ChairType.SingleFacing);
            AddChairData(0x2DE5, Direction.West, ChairType.SingleFacing);
            AddChairData(0x2DE6, Direction.North, ChairType.SingleFacing);
            // 0x2DEB - 0x2DEE - elven chairs 2
            AddChairData(0x2DEB, Direction.North, ChairType.SingleFacing);
            AddChairData(0x2DEC, Direction.South, ChairType.SingleFacing);
            AddChairData(0x2DED, Direction.East, ChairType.SingleFacing);
            AddChairData(0x2DEE, Direction.West, ChairType.SingleFacing);
            // 0x3DFF - 0x3E00 - dark stone benches
            AddChairData(0x3DFF, Direction.South, ChairType.ReversibleFacing);
            AddChairData(0x3E00, Direction.East, ChairType.ReversibleFacing);
        }

        public static void AddChairData(int itemId, Direction direction, ChairType chairType)
        {
            if (_chairs.ContainsKey(itemId))
                _chairs.Remove(itemId);
            _chairs.Add(itemId, new ChairData(itemId, direction, chairType));
        }

        public static bool CheckItemAsChair(int itemId, out ChairData value)
        {
            if (_chairs.TryGetValue(itemId, out value))
                return true;
            value = ChairData.Null;
            return false;
        }

        public class ChairData
        {
            public readonly int ItemID;
            public readonly Direction Facing;
            public readonly ChairType ChairType;
            public readonly int SittingPixelOffset;

            /*private Texture2DInfo _texture;
            public Texture2DInfo Texture
            {
                get
                {
                    if (_texture == null)
                    {
                        var provider = ServiceRegistry.GetService<IResourceProvider>();
                        var baseTexture = provider.GetItemTexture(ItemID);
                        _texture = provider.GetItemTexture(ItemID);
                    }
                    return _texture;
                }
            }*/

            public static ChairData Null = new ChairData(0, Direction.ValueMask, ChairType.AnyFacing);

            /// <summary>
            /// Creates a new chair data object.
            /// </summary>
            /// <param name="itemId">ItemID of the chair.</param>
            /// <param name="facing">The valid facing of the chair. Must be North, West, South, or East.</param>
            /// <param name="chairType">Whether the chair is a single facing (chair) reversible facing (bench) or any facing (stool) object.</param>
            public ChairData(int itemId, Direction facing, ChairType chairType)
            {
                ItemID = itemId;
                Facing = facing;
                ChairType = chairType;
                SittingPixelOffset = TileData.ItemData[itemId].Unknown4;
                if (SittingPixelOffset > 32)
                    SittingPixelOffset -= 32;
            }

            public Direction GetSittingFacing(Direction inFacing)
            {
                if (ChairType == ChairType.SingleFacing)
                    return Facing;
                inFacing = DirectionHelper.GetCardinal(inFacing);
                if (inFacing == Facing)
                    return Facing;
                if (ChairType == ChairType.ReversibleFacing)
                {
                    if (DirectionHelper.Reverse(inFacing) == Facing)
                        return inFacing;
                }
                else if (ChairType == ChairType.AnyFacing)
                    return inFacing; // which has been made cardinal already, so this works.
                return Facing;
            }
        }

        public enum ChairType
        {
            /// <summary>
            /// The chair has only one valid facing. The mobile defaults to being drawn in the single default facing.
            /// </summary>
            SingleFacing = 0,
            /// <summary>
            /// The chair has two valid facings which are mirrored. 
            /// </summary>
            ReversibleFacing = 1,
            /// <summary>
            /// Mobiles can face any direction so long as it is NWS or E. The mobile defaults to being drawn in the default facing until it attempts to switch to another valid facing.
            /// </summary>
            AnyFacing = 2
        }
    }
}
