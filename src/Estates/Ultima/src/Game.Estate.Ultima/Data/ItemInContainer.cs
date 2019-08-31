﻿namespace Game.Estate.Ultima.Data
{
    public class ItemInContainer
    {
        public readonly Serial Serial;
        public readonly int ItemId;
        public readonly int Amount;
        public readonly int X;
        public readonly int Y;
        public readonly int GridLocation;
        public readonly Serial ContainerSerial;
        public readonly int Hue;

        public ItemInContainer(Serial serial, int itemId, int amount, int x, int y, int gridLocation, int containerSerial, int hue)
        {
            Serial = serial;
            ItemId = itemId;
            Amount = amount;
            X = x;
            Y = y;
            GridLocation = gridLocation;
            ContainerSerial = containerSerial;
            Hue = hue;
        }
    }
}
