/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

namespace Niflib
{
    public class Type_
    {
        public readonly Type_ BaseType;
        public readonly int InternalTypeNumber;
        readonly string name;
        static int num_types = 0;

        public Type_(string name, Type_ baseType)
        {
            this.name = name;
            BaseType = baseType;
            InternalTypeNumber = num_types++;
        }

        public Type_(Type_ src, Type_ baseType)
        {
            name = src.name;
            BaseType = src.BaseType;
            InternalTypeNumber = num_types++;
        }

        public string GetTypeName() => name;

        public bool IsSameType(Type_ compare_to) => compare_to == this;

        public bool IsDerivedType(Type_ compare_to)
        {
            Type_ search = this;
            while (search != null)
            {
                if (search == compare_to)
                    return true;
                search = search.BaseType;
            }
            return false;
        }

        //   public bool operator<(Type compare_to ) => this < compare_to;
        public NiObject Create => ObjectRegistry.CreateObject(name);
    }
}