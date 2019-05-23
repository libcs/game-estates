/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

using System;
using System.Collections.Generic;

namespace Niflib
{
    /*!
    * Stores the mapping between object names and factory function pointers to create them
    */
    public partial class ObjectRegistry
    {
        static Dictionary<string, Func<NiObject>> object_map;

        /*!
         * Registers a new type of NiObject for use by the Read functions.
         * \param[in] type_name The textual name of the object that will appear in the NIF file.  For example: "NiNode"
         * \param[in] create_func The Create function of the NiObject that will be called during file reading to initialize new objects of the types encountered in the file.
         */
        public static void RegisterObject(string type_name, Func<NiObject> create_func) => object_map[type_name] = create_func;

        /*! 
         * NIFLIB_HIDDEN function.  For internal use only.
         * Called during file reads to create objects of a named type.
         */
        public static NiObject CreateObject(string type_name) => object_map.TryGetValue(type_name, out var func) ? func() : null;
    }
}
