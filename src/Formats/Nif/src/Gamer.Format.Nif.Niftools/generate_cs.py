#!/usr/bin/python

# generate_cs.py
#
# This script generates C# code for Niflib.
#
# --------------------------------------------------------------------------
# Command line options
#
# -p /path/to/niflib : specifies the path where niflib can be found
#
# -b : enable bootstrap mode (generates templates)
#
# -i : do NOT generate implmentation; place all code in defines.h
#
# -a : generate accessors for data in classes
#
# -n <block>: generate only files which match the specified name
#
# --------------------------------------------------------------------------
# ***** BEGIN LICENSE BLOCK *****
#
# Copyright (c) 2005, NIF File Format Library and Tools
# All rights reserved.
#
# Redistribution and use in source and binary forms, with or without
# modification, are permitted provided that the following conditions
# are met:
#
#    * Redistributions of source code must retain the above copyright
#      notice, this list of conditions and the following disclaimer.
#
#    * Redistributions in binary form must reproduce the above
#      copyright notice, this list of conditions and the following
#      disclaimer in the documentation and/or other materials provided
#      with the distribution.
#
#    * Neither the name of the NIF File Format Library and Tools
#      project nor the names of its contributors may be used to endorse
#      or promote products derived from this software without specific
#      prior written permission.
#
# THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
# "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
# LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS
# FOR A PARTICULAR PURPOSE ARE DISCLAIMED.  IN NO EVENT SHALL THE
# COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT,
# INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING,
# BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
# LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
# CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT
# LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN
# ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
# POSSIBILITY OF SUCH DAMAGE.
#
# ***** END LICENSE BLOCK *****
# --------------------------------------------------------------------------
from __future__ import unicode_literals
from custom_all import *
from nifxml_cs import *
from distutils.dir_util import mkpath
import os
import io
import itertools

#
# global data
#
ROOT_DIR = "../Gamer.Format.Nif/"
BOOTSTRAP = True
GENIMPL = True
GENACCESSORS = False
GENBLOCKS = []
GENALLFILES = True

prev = ""
for i in sys.argv:
    if prev == "-p":
        ROOT_DIR = i
    elif i == "-b":
        BOOTSTRAP = True
    elif i == "-i":
        GENIMPL = False
    elif i == "-a":
        GENACCESSORS = True
    elif prev == "-n":
        GENBLOCKS.append(i)
        GENALLFILES = False
        
    prev = i

# Fix known manual update attributes.  For now hard code here.
block_types["NiKeyframeData"].find_member("Num Rotation Keys").is_manual_update = True
#block_types["NiTriStripsData"].find_member("Num Triangles").is_manual_update =
#True
   
#
# generate compound code
#
mkpath(os.path.join(ROOT_DIR, "Objs"))
mkpath(os.path.join(ROOT_DIR, "Gen"))

for n in compound_names:
    x = compound_types[n]
    
    # skip natively implemented types
    if x.name in NATIVETYPES.keys(): continue
    
    if not GENALLFILES and not x.cname in GENBLOCKS:
            continue

    #Get existing custom code
    file_name = ROOT_DIR + '/Gen/' + x.cname + '.cs'
    customCtx = ExtractCustomCode(file_name)

    cs = CSFile(io.open(file_name, 'wb'))
    cs.code('/* Copyright (c) 2006, NIF File Format Library and Tools')
    cs.code('All rights reserved.  Please see niflib.h for license. */')
    cs.code()
    cs.code('//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//')
    cs.code()
    cs.code('//To change this file, alter the /niflib/gen_niflib_cs Python script.')
    cs.code()
    #if n in ["Header", "Footer"]:
    #    cs.code('using mylib2')
    cs.code(x.code_using())
    cs.write("namespace Niflib {\n")
    cs.code()
    # header
    cs.comment(x.description)
    if x.template:
        cs.code('public class %s<T> {' % x.cname)
    else:
        cs.code('public class %s {' % x.cname)
    
    #constructor/destructor/assignment
    #if not x.template:
    #    cs.code( '/*!  Default Constructor */' )
    #    cs.code( "NIFLIB_API %s();"%x.cname )
    #    cs.code( '/*!  Default Destructor */' )
    #    cs.code( "NIFLIB_API ~%s();"%x.cname )
    #    cs.code( '/*!  Copy Constructor */' )
    #    cs.code( 'NIFLIB_API %s( const %s & src );'%(x.cname, x.cname) )
    #    cs.code( '/*!  Copy Operator */' )
    #    cs.code( 'NIFLIB_API %s & operator=( const %s & src );'%(x.cname,
    #    x.cname) )

    # declaration
    cs.declare(x)

    if not x.template:

        cs.code('//Constructor')
        
        # constructor
        x_code_construct = x.code_construct()
        if x_code_construct:
            cs.code("public %s() { unchecked {\n%s\n} }" % (x.cname,x_code_construct))
            cs.code()

        #cs.code('//Copy Constructor')
        #cs.code( '%s(%s src) {'%(x.cname,x.cname) )
        #cs.code( '*this = src;' )
        #cs.code('}')
        #cs.code()
        #cs.code('//Copy Operator')
        #cs.code('%s & %s::operator=( const %s & src ) {' %
        #(x.cname,x.cname,x.cname))
        #for m in x.members:
        #    if not m.is_duplicate:
        #        cs.code('this->%s = src.%s;' % (m.cname, m.cname))
        #cs.code('return *this;')
        #cs.code('}')
        #cs.code()

        # header and footer functions
        if n == "Header":
            cs.code('public NifInfo Read(IStream s) {')
            cs.code('//Declare NifInfo structure')
            cs.code('var info = new NifInfo();')
            cs.code()
            cs.stream(x, ACTION_READ)
            cs.code()
            cs.code('//Copy info.version to local version var.')
            cs.code('version = info.version;')
            cs.code()
            cs.code('//Fill out and return NifInfo structure.')
            cs.code('info.userVersion = userVersion;')
            cs.code('info.userVersion2 = userVersion2;')
            cs.code('info.endian = (EndianType)endianType;')
            cs.code('info.author = exportInfo.author.str;')
            cs.code('info.processScript = exportInfo.processScript.str;')
            cs.code('info.exportScript = exportInfo.exportScript.str;')
            cs.code('return info;')
            cs.code('}')
            cs.code()
            cs.code('public void Write(OStream s, NifInfo info) {')
            cs.stream(x, ACTION_WRITE)
            cs.code('}')
            cs.code()
            cs.code('public string AsString(bool verbose = false) {')
            cs.stream(x, ACTION_OUT)
            cs.code('}')
        
        if n == "Footer":
            cs.code()
            cs.code('public void Read(IStream s, List<uint> link_stack, NifInfo info) {')
            cs.stream(x, ACTION_READ)
            cs.code('}')
            cs.code()
            cs.code('public void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {')
            cs.stream(x, ACTION_WRITE)
            cs.code('}')
            cs.code()
            cs.code('public string AsString(bool verbose = false) {')
            cs.stream(x, ACTION_OUT)
            cs.code('}')

    #Preserve Custom code from before
    WriteCustomCode(customCtx, cs, 'MISC')

    # done
    cs.code("}")
    cs.code()
    cs.write("}\n")
    cs.close()


    # Write out Public Enumeration header Enumerations
if GENALLFILES:
    cs = CSFile(io.open(ROOT_DIR + '/Gen/Enums.cs', 'wb'))
    cs.code('/* Copyright (c) 2006, NIF File Format Library and Tools')
    cs.code('All rights reserved.  Please see niflib.h for license. */')
    cs.code()
    cs.code('//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//')
    cs.code()
    cs.code('//To change this file, alter the /niflib/gen_niflib_cs.py Python script.')
    cs.code()
    cs.code('using System;')
    cs.code()
    cs.write('namespace Niflib {\n')
    cs.code()
    for n, x in itertools.chain(enum_types.items(), flag_types.items()):
      if x.options:
        if x.description:
          cs.comment(x.description)
        cs.code('public enum %s : uint {' % (x.cname))
        for o in x.options:
          cs.code('%s = %s, /*!< %s */' % (o.cname, o.value, o.description))
        cs.code('}')
        #: cs
        cs.code('static partial class Nif { //--' + x.cname + '--//')
        cs.code('public static void NifStream(out %s val, IStream s, NifInfo info) { %s temp; NifStream(out temp, s, info); val = (%s)temp; }' % (x.cname, x.storage, x.cname))
        cs.code('public static void NifStream(%s val, OStream s, NifInfo info) => NifStream((%s)val, s, info);' % (x.cname,x.storage))
        cs.code('public static string AsString(%s val) { switch (val) {' % (x.cname))
        for o in x.options:
          cs.code('case %s.%s: return "%s";' % (x.cname, o.cname, o.name))
        cs.code('default: return $"Invalid Value! - {val}";')
        cs.code('}}}')
        cs.code()

    cs.write('}\n')
    cs.close()


    #
    # NiObject Registration Function
    #
    cs = CSFile(io.open(ROOT_DIR + '/Gen/Register.cs', 'wb'))
    cs.code('/* Copyright (c) 2006, NIF File Format Library and Tools')
    cs.code('All rights reserved.  Please see niflib.h for license. */')
    cs.code()
    cs.code('//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//')
    cs.code()
    cs.code('//To change this file, alter the /niflib/generate_cs.py Python script.')
    cs.code()
    cs.code('namespace Niflib {')
    cs.code('public partial class ObjectRegistry {')
    cs.code()
    cs.code('public static void RegisterObjects() {')
    cs.code()
    for n in block_names:
        x = block_types[n]
        cs.code('RegisterObject("' + x.name + '", ' + x.cname + '.Create);')
    cs.code()
    cs.code('}')
    cs.code()
    cs.code('}')
    cs.code('}')
    cs.close()


#
# NiObject Files
#
for n in block_names:
    x = block_types[n]
    x_define_name = define_name(x.cname)

    if not GENALLFILES and not x.cname in GENBLOCKS:
        continue

    #
    # NiObject Header File
    #

    #Get existing custom code
    file_name = ROOT_DIR + '/Objs/' + x.cname + '.cs'
    customCtx = ExtractCustomCode(file_name)

    #output new file
    cs = CSFile(io.open(file_name, 'wb'))
    cs.code('/* Copyright (c) 2006, NIF File Format Library and Tools')
    cs.code('All rights reserved.  Please see niflib.h for license. */')
    cs.code()
    cs.code('//-----------------------------------NOTICE----------------------------------//')
    cs.code('// Some of this file is automatically filled in by a Python script.  Only    //')
    cs.code('// add custom code in the designated areas or it will be overwritten during  //')
    cs.code('// the next update.                                                          //')
    cs.code('//-----------------------------------NOTICE----------------------------------//')
    cs.code()
    cs.code(x.code_using())
    cs.code()
    #Preserve Custom code from before
    WriteCustomCode(customCtx, cs, 'FILE HEAD')
    cs.code()
    cs.write("namespace Niflib {\n")
    cs.code()
    cs.comment(x.description)
    if x.inherit:
        cs.code('public class %s : %s {' % (x.cname, x.inherit.cname))
    else:
        cs.code('public class %s : RefObject {' % x.cname)
    cs.code('//Definition of TYPE constant')
    if x.inherit:
        cs.code('public static readonly Type_ TYPE = new Type_("%s", %s.TYPE);' % (x.name, x.inherit.cname))
    else:
        cs.code('public static readonly Type_ TYPE = new Type_("%s", RefObject.TYPE);' % x.name)

    #
    # Show example naive implementation if requested
    #
    
    # Create a list of members eligable for functions
    #if GENACCESSORS:
    #    func_members = []
    #    for y in x.members:
    #        if not y.arr1_ref and not y.arr2_ref and
    #        y.cname.lower().find("unk") == -1:
    #            func_members.append(y)
    
    #    if len(func_members) > 0:
    #        cs.code('/***Begin Example Naive Implementation****')
    #        cs.code()
    #        for y in func_members:
    #            cs.comment(y.description + "\n\\return The current value.",
    #            False)
    #            cs.code(y.getter_declare("", ";"))
    #            cs.code()
    #            cs.comment(y.description + "\n\\param[in] value The new
    #            value.", False)
    #            cs.code(y.setter_declare("", ";"))
    #            cs.code()
    #        cs.code('****End Example Naive Implementation***/')
    #    else:
    #        cs.code('//--This object has no eligable attributes.  No example
    #        implementation generated--//')
    #    cs.code()
    
    #Preserve Custom code from before
    WriteCustomCode(customCtx, cs, 'MISC')

    cs.declare(x)

    cs.code()
    x_code_construct = x.code_construct()
    if x_code_construct:
        cs.code('public %s() {\n%s' % (x.cname,x_code_construct))
    else:
        cs.code('public %s() {' % x.cname)
    
    #Preserve Custom code from before
    WriteCustomCode(customCtx, cs, 'CONSTRUCTOR')
    cs.code('}')
    cs.code()

    cs.code('/*!')
    cs.code(' * Used to determine the type of a particular instance of this object.')
    cs.code(' * \\return The type constant for the actual type of the object.')
    cs.code(' */')
    cs.code('public override Type_ GetType() => TYPE;')
    cs.code()
    cs.code('/*!')
    cs.code(' * A factory function used during file reading to create an instance of this type of object.')
    cs.code(' * \\return A pointer to a newly allocated instance of this type of object.')
    cs.code(' */')
    cs.code('public static NiObject Create() => new ' + x.cname + '();')
    cs.code()

    cs.code('/*! NIFLIB_HIDDEN function.  For internal use only. */')
    cs.code("internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {")

    #Preserve Custom code from before
    WriteCustomCode(customCtx, cs, 'PRE-READ')
    cs.code()
    cs.stream(x, ACTION_READ)
    cs.code()

    #Preserve Custom code from before
    WriteCustomCode(customCtx, cs, 'POST-READ')
    cs.code("}")
    cs.code()
      
    cs.code('/*! NIFLIB_HIDDEN function.  For internal use only. */')
    cs.code("internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {")

    #Preserve Custom code from before
    WriteCustomCode(customCtx, cs, 'PRE-WRITE')
    cs.code()
    cs.stream(x, ACTION_WRITE)
    cs.code()
    #Preserve Custom code from before
    WriteCustomCode(customCtx, cs, 'POST-WRITE')
    cs.code("}")
    cs.code()
      
    cs.code('/*!')
    cs.code(' * Summarizes the information contained in this object in English.')
    cs.code(' * \\param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.')
    cs.code(' * \\return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.')
    cs.code(' */')
    cs.code("public override string AsString(bool verbose = false) {")

    #Preserve Custom code from before
    WriteCustomCode(customCtx, cs, 'PRE-STRING')
    cs.code()
    cs.stream(x, ACTION_OUT)
    cs.code()

    #Preserve Custom code from before
    WriteCustomCode(customCtx, cs, 'POST-STRING')
    cs.code("}")
    cs.code()

    cs.code('/*! NIFLIB_HIDDEN function.  For internal use only. */')
    cs.code("internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {")

    #Preserve Custom code from before
    WriteCustomCode(customCtx, cs, 'PRE-FIXLINKS')
    cs.code()
    cs.stream(x, ACTION_FIXLINKS)
    cs.code()

    #Preserve Custom code from before
    WriteCustomCode(customCtx, cs, 'POST-FIXLINKS')
    cs.code("}")
    cs.code()

    cs.code('/*! NIFLIB_HIDDEN function.  For internal use only. */')
    cs.code("internal override List<NiObject> GetRefs() {")
    cs.stream(x, ACTION_GETREFS)
    cs.code("}")
    cs.code()

    cs.code('/*! NIFLIB_HIDDEN function.  For internal use only. */')
    cs.code("internal override List<NiObject> GetPtrs() {")
    cs.stream(x, ACTION_GETPTRS)
    cs.code("}")
    cs.code()

    # Output example implementation of public getter/setter Mmthods if
    # requested
    #if GENACCESSORS:
    #    func_members = []
    #    for y in x.members:
    #        if not y.arr1_ref and not y.arr2_ref and
    #        y.cname.lower().find("unk") == -1:
    #            func_members.append(y)
    
    #    if len(func_members) > 0:
    #        cs.code('/***Begin Example Naive Implementation****')
    #        cs.code()
    #        for y in func_members:
    #            cs.code(y.getter_declare(x.name + "::", " {"))
    #            cs.code("return %s;" % y.cname)
    #            cs.code("}")
    #            cs.code()
                
    #            cs.code(y.setter_declare(x.name + "::", " {"))
    #            cs.code("%s = value;" % y.cname)
    #            cs.code("}")
    #            cs.code()
    #        cs.code('****End Example Naive Implementation***/')
    #    else:
    #        cs.code('//--This object has no eligable attributes.  No example
    #        implementation generated--//')
    #    cs.code()

    #Preserve Custom code from before
    WriteCustomCode(customCtx, cs, 'FILE FOOT')
    cs.code()

    cs.code('}')
    cs.code()

    cs.write("}")
    cs.close()

    ##Check if the temp file is identical to the target file
    #OverwriteIfChanged(file_name, 'temp')
