/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

using System.Collections.Generic;
using System.IO;

namespace Niflib
{
    public abstract class RefObject
    {
        /*!
         * A constant value which uniquly identifies objects of this type.
         */
        public static readonly Type_ TYPE = new Type_("RefObject", null);

        /*!
         * Used to determine the type of a particular instance of this object.
         * \return The type constant for the actual type of the object.
         */
        public virtual new Type_ GetType() => TYPE;

        /*!
         * Summarizes the information contained in this object in English.
         * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed out.
         * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
         */
        public abstract string AsString(bool verbose = false);

        /*!
         * Used to determine whether this object is exactly the same type as the given type constant.
         * \return True if this object is exactly the same type as that represented by the given type constant.  False otherwise.
         */
        public bool IsSameType(Type_ compare_to) => GetType().IsSameType(compare_to);

        /*!
         * Used to determine whether this object is exactly the same type as another object.
         * \return True if this object is exactly the same type as the given object.  False otherwise.
         */
        public bool IsSameType(RefObject obj) => GetType().IsSameType(obj.GetType());

        /*!
         * Used to determine whether this object is a derived type of the given type constant.  For example, all NIF objects are derived types of NiObject, and a NiNode is also a derived type of NiObjectNET and NiAVObject.
         * \return True if this object is derived from the type represented by the given type constant.  False otherwise.
         */
        public bool IsDerivedType(Type_ compare_to) => GetType().IsDerivedType(compare_to);

        /*!
         * Used to determine whether this object is a derived type of another object.  For example, all NIF objects are derived types of NiObject, and a NiNode is also a derived type of NiObjectNET and NiAVObject.
         * \return True if this object is derived from the type of of the given object.  False otherwise.
         */
        public bool IsDerivedType(RefObject obj) => GetType().IsDerivedType(obj.GetType());

        /*!
         * Formats a human readable string that includes the type of the object, and its name, if it has one.
         * \return A string in the form:  address(type), or adress(type) {name}
         */
        /*! Used to format a human readable string that includes the type of the object */
        public virtual string GetIDString() => $"({GetType().GetTypeName()})";

        /*!
         * Returns the number of references that currently exist for this object.
         * \return The number of references to this object that are in use.
         */
        //uint GetNumRefs();

        /*! NIFLIB_HIDDEN function.  For internal use only. */
        protected abstract void Read(StreamReader s, List<uint> link_stack, NifInfo info);
        /*! NIFLIB_HIDDEN function.  For internal use only. */
        protected abstract void Write(StreamWriter s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info);
        /*! NIFLIB_HIDDEN function.  For internal use only. */
        protected abstract void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info);
        /*! NIFLIB_HIDDEN function.  For internal use only. */
        protected abstract List<NiObject> GetRefs();
    }
}