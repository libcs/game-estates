/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

using System;
using System.Collections.Generic;

namespace Niflib
{
    public static partial class Nif
    {
        const string FIX_LINK_POP_ERROR = "Trying to pop a link from empty stack. This is probably a bug.";
        const string FIX_LINK_INDEX_ERROR = "Object index was not found in object map.  This NIF file may be invalid or imporperly supported.";
        const string FIX_LINK_CAST_ERROR = "Link could not be cast to required type during file read. This NIF file may be invalid or improperly supported.";

        public static T FixLink<T>(Dictionary<uint, NiObject> objects, Stack<uint> link_stack, Stack<NiObject> missing_link_stack, NifInfo info)
            where T : RefObject
        {
            if (link_stack.Count == 0)
                throw new Exception(FIX_LINK_POP_ERROR);
            var index = link_stack.Pop();
            var missing_obj = missing_link_stack.Count != 0 ? missing_link_stack.Pop() : null;
            //Check if link is NULL
            if (info.version > Nif.VER_3_3_0_13)
            {
                if (index == 0xFFFFFFFF) return missing_obj as T;
            }
            else
            {
                if (index == 0) return missing_obj as T;
            }

            if (!objects.TryGetValue(index, out var it))
            {
                if (info.version > Nif.VER_3_3_0_13)
                    throw new Exception(FIX_LINK_INDEX_ERROR);
                else return null;
            }

            var obj = it as T;
            if (obj == null)
                throw new Exception($@"
{FIX_LINK_CAST_ERROR}
Type of object with index {index} was:  {it.GetType().GetTypeName()}
Required type was: {typeof(T).Name}
");
            return obj;
        }

    }
}