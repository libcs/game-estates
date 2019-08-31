#!/usr/bin/python

# TODO: split in multiple files
"""
This module generates C# code for Niflib from the NIF file format specification XML.

@author: Amorilia
@author: Shon

@contact: http://niftools.sourceforge.net

@copyright:
Copyright (c) 2005, NIF File Format Library and Tools.
All rights reserved.

@license:
Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions
are met:

  - Redistributions of source code must retain the above copyright
    notice, this list of conditions and the following disclaimer.

  - Redistributions in binary form must reproduce the above
    copyright notice, this list of conditions and the following
    disclaimer in the documentation and/or other materials provided
    with the distribution.

  - Neither the name of the NIF File Format Library and Tools
    project nor the names of its contributors may be used to endorse
    or promote products derived from this software without specific
    prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
"AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS
FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE
COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT,
INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING,
BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT
LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN
ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
POSSIBILITY OF SUCH DAMAGE.

@var native_types: Maps name of basic or compound type to name of type implemented manually in Niflib.
    These are the types tagged by the niflibtype tag in the XML. For example,
    if a (basic or compound) type with C{name="ferrari"} has C{niflibtype="car"}
    then C{native_types["ferrari"]} equals the string C{"car"}.
@type native_types: C{dictionary}

@var basic_types: Maps name of basic type to L{Basic} instance.
@type basic_types: C{dictionary}

@var compound_types:  Maps name of compound type to a L{Compound} instance.
@type compound_types: C{dictionary}

@var block_types: Maps name of the block name to a L{Block} instance.
@type block_types: C{list}

@var basic_names: Sorted keys of L{basic_types}.
@type basic_names: C{list}

@var compound_names: Sorted keys of L{compound_types}.
@type compound_names: C{list}

@var block_names: Sorted keys of L{block_types}.
@type block_names: C{list}

@var ACTION_READ: Constant for use with CFile::stream. Causes it to generate Niflib's Read function.
@type ACTION_READ: C{int}

@var ACTION_WRITE: Constant for use with CFile::stream.  Causes it to generate Niflib's Write function.
@type ACTION_WRITE: C{int}

@var ACTION_OUT: Constant for use with CFile::stream.  Causes it to generate Niflib's AsString function.
@type ACTION_OUT: C{int}

@var ACTION_FIXLINKS: Constant for use with CFile::stream.  Causes it to generate Niflib's FixLinks function.
@type ACTION_FIXLINKS: C{int}

@var ACTION_GETREFS: Constant for use with CFile::stream.  Causes it to generate Niflib's GetRefs function.
@type ACTION_GETREFS: C{int}

@var ACTION_GETPTRS: Constant for use with CFile::stream.  Causes it to generate Niflib's GetPtrs function.
@type ACTION_GETPTRS: C{int}
"""

from __future__ import unicode_literals

from xml.dom.minidom import *
from textwrap import fill

import sys
import os
import io
import re
import types

LISTARRAYS = ['bhkConstraint::entities',
    'NiAVObjects::properties',
    'NodeSet::nodes',
    'NiBoneLODController::nodeGroups',
    'NiBSplineData::floatControlPoints',
    'NiControllerManager::controllerSequences',
    'NiControllerSequence::controlledBlocks',
    'NiDefaultAVObjectPalette::objs',
    'NiNode::children',
    'NiNode::effects',
    'NiObjectNet::extraDataList',
    'NiPSysMeshEmitter::emitterMeshes']

#
# global data
#
native_types = {}
native_types['TEMPLATE'] = 'T'
basic_types = {}
enum_types = {}
flag_types = {}
compound_types = {}
block_types = {}
version_types = {}

basic_names = []
compound_names = []
enum_names = []
flag_names = []
block_names = []
version_names = []

NATIVETYPES = {
    'bool' : 'bool',
    'byte' : 'byte',
    'uint' : 'uint',
    'ulittle32' : 'uint',
    'ushort' : 'ushort',
    'int' : 'int',
    'short' : 'short',
    'BlockTypeIndex' : 'ushort',
    'char' : 'byte',
    'FileVersion' : 'uint',
    'Flags' : 'ushort',
    'float' : 'float',
    'hfloat' : 'hfloat',
    'HeaderString' : 'HeaderString',
    'LineString' : 'LineString',
    'Ptr' : '*',
    'Ref' : 'Ref',
    'StringOffset' : 'uint',
    'StringIndex' : 'IndexString',
    'SizedString' : 'string',
    'string' : 'IndexString',
    'Color3' : 'Color3',
    'Color4' : 'Color4',
    #'ByteColor3' : 'ByteColor3',
    #'ByteColor4' : 'ByteColor4',
    'FilePath' : 'IndexString',
    'Vector3' : 'Vector3',
    'Vector4' : 'Vector4',
    'Quaternion' : 'Quaternion',
    'Matrix22' : 'Matrix22',
    'Matrix33' : 'Matrix33',
    'Matrix34' : 'Matrix34',
    'Matrix44' : 'Matrix44',
    'hkMatrix3' : 'InertiaMatrix',
    'ShortString' : 'ShortString',
    'Key' : 'Key',
    'QuatKey' : 'Key',
    'TexCoord' : 'TexCoord',
    'Triangle' : 'Triangle',
    'BSVertexData' : 'BSVertexData',
    'BSVertexDataSSE' : 'BSVertexData',
    #'BSVertexDesc' : 'BSVertexDesc'
}

ACTION_READ = 0
ACTION_WRITE = 1
ACTION_OUT = 2
ACTION_FIXLINKS = 3
ACTION_GETREFS = 4
ACTION_GETPTRS = 5
        
#
# C# code formatting functions
#
class CSFile(io.TextIOWrapper):
    """
    This class represents a C# source file.  It is used to open the file for output
    and automatically handles indentation by detecting brackets and colons.
    It also handles writing the generated Niflib C# code.
    @ivar indent: The current level of indentation.
    @type indent: int
    @ivar backslash_mode: Determines whether a backslash is appended to each line for creation of multi-line defines
    @type backslash_mode: bool
    """
    def __init__(self, buffer, encoding='utf-8', errors=None, newline=None, line_buffering=False, write_through=False):
        io.TextIOWrapper.__init__(self, buffer, encoding, errors, newline, line_buffering)
        self.indent = 0
        self.backslash_mode = False
    

    def code(self, txt=None):
        r"""
        Formats a line of C# code; the returned result always ends with a newline.
        If txt starts with "E{rb}", indent is decreased, if it ends with "E{lb}", indent is increased.
        Text ending in "E{:}" de-indents itself.  For example "publicE{:}"
        Result always ends with a newline
        @param txt: None means just a line break.  This will also break the backslash, which is kind of handy.
            "\n" will create a backslashed newline in backslash mode.
        @type txt: string, None
        """
        # txt
        # this will also break the backslash, which is kind of handy
        # call code("\n") if you want a backslashed newline in backslash mode
        if txt == None:
            self.write('\n')
            return
    
        # block end
        if txt[:1] == '}': self.indent -= 1
        # special, private:, public:, and protected:
        if txt[-1:] == ':': self.indent -= 1
        # endline string
        if self.backslash_mode:
            endl = ' \\\n'
        else:
            endl = '\n'
        # indent string
        prefix = '\t' * self.indent
        # strip trailing whitespace, including newlines
        txt = txt.rstrip()
        # indent, and add newline
        result = prefix + txt.replace('\n', endl + prefix) + endl
        # block start
        if txt[-1:] == '{': self.indent += 1
        # special, private:, public:, and protected:
        if txt[-1:] == ':': self.indent += 1
        
        self.write(result.encode('utf-8').decode('utf-8', 'strict'))
    
    
    #
    def comment(self, txt, doxygen=True):
        """
        Wraps text in C# comments and outputs it to the file.  Handles multilined comments as well.
        Result always ends with a newline
        @param txt: The text to enclose in a Doxygen comment
        @type txt: string
        """
        # skip comments when we are in backslash mode
        if self.backslash_mode: return
        lines = txt.split('\n')
        txt = ''
        for l in lines:
            txt = txt + fill(l, 160) + '\n'
        txt = txt.strip()
        num_line_ends = txt.count('\n')
        if doxygen:
            if num_line_ends > 0:
                txt = txt.replace('\n', '\n * ')
                self.code('/*!\n * ' + txt + '\n */')
            else:
                self.code('/*! ' + txt + ' */')
        else:
            lines = txt.split('\n')
            for l in lines:
                self.code('// ' + l)
    
    def declare(self, block):
        """
        Formats the member variables for a specific class as described by the XML and outputs the result to the file.
        @param block: The class or struct to generate member functions for.
        @type block: Block, Compound
        """
        keyword = ''
        # branch
        if isinstance(block, Block):
            #keyword = 'protected'
            prot_mode = True
        for y in block.members:
            if not y.is_duplicate:
                if isinstance(block, Block):
                    if y.is_public and prot_mode:
                        keyword = 'public'
                        prot_mode = False
                    elif not y.is_public and not prot_mode:
                        keyword = 'protected'
                        prot_mode = True
                self.comment(y.description or y.cname)
                self.code(y.code_declare())
                if y.func:
                    self.comment(y.description or y.func)
                    self.code('%s %s %s();' % (keyword, y.ctype, y.func))

    def stream(self, block, action, localprefix='', prefix='', arg_prefix='', arg_member=None):
        """
        Generates the function code for various functions in Niflib and outputs it to the file.
        @param block: The class or struct to generate the function for.
        @type block: Block, Compound
        @param action: The type of function to generate, valid values are::
            ACTION_READ - Read function.
            ACTION_WRITE - Write function
            ACTION_OUT - AsString function
            ACTION_FIXLINKS - FixLinks function
            ACTION_GETREFS - GetRefs function
            ACTION_GETPTRS - GetPtrs function
        @type action: ACTION_X constant
        @param localprefix: ?
        @type localprefix: string
        @param prefix: ?
        @type prefix: string
        @param arg_prefix: ?
        @type arg_prefix: string
        @param arg_member: ?
        @type arg_member: None, ?
        """
        lastver1 = None
        lastver2 = None
        lastuserver = None
        lastuserver2 = None
        lastcond = None
        lastvercond = None
        # stream name
        stream = 's' if action == ACTION_READ else 's'

        # preperation
        if isinstance(block, Block) or block.name in ['Footer', 'Header']:
            if action == ACTION_READ:
                if block.has_links or block.has_crossrefs:
                    self.code('uint block_num;')
            if action == ACTION_OUT:
                self.code('var s = new System.Text.StringBuilder();')
                # declare array_output_count, only if it will actually be used
                if block.has_arr():
                    self.code('var array_output_count = 0U;')
            #if action == ACTION_GETREFS:
            #    self.code('List<NiObject> refs;')
            #if action == ACTION_GETPTRS:
            #    self.code('List<NiObject> ptrs;')

        # stream the ancestor
        if isinstance(block, Block):
            if block.inherit:
                if action == ACTION_READ:
                    self.code('base.Read(%s, link_stack, info);' % stream)
                elif action == ACTION_WRITE:
                    self.code('base.Write(%s, link_map, missing_link_stack, info);' % stream)
                elif action == ACTION_OUT:
                    self.code('%s.Append(base.AsString());' % stream)
                elif action == ACTION_FIXLINKS:
                    self.code('base.FixLinks(objects, link_stack, missing_link_stack, info);')
                elif action == ACTION_GETREFS:
                    self.code('var refs = base.GetRefs();')
                elif action == ACTION_GETPTRS:
                    self.code('var ptrs = base.GetPtrs();')

        # declare and calculate local variables (TODO: GET RID OF THIS; PREFERABLY NO LOCAL VARIABLES AT ALL)
        if action in [ACTION_READ, ACTION_WRITE, ACTION_OUT]:
            block.members.reverse() # calculated data depends on data further down the structure
            for y in block.members:
                if not y.is_duplicate and not y.is_manual_update and action in [ACTION_WRITE, ACTION_OUT]:
                    if y.func:
                        self.code('%s%s = %s%s();' % (prefix, y.cname, prefix, y.func))
                    elif y.is_calculated:
                        if action in [ACTION_READ, ACTION_WRITE]:
                            self.code('%s%s = %s%sCalc(info);' % (prefix, y.cname, prefix, y.cname))
                        # ACTION_OUT is in AsString(), which doesn't take version info so let's simply not print the field in this case
                    elif y.arr1_ref:
                        if not y.arr1 or not y.arr1.lhs: # Simple Scalar
                            cref = block.find_member(y.arr1_ref[0], True) 
                            #if not cref.is_duplicate and not cref.next_dup and (not cref.cond.lhs or cref.cond.lhs == y.name):
                            #    self.code('assert(%s%s == (%s)(%s%s.size()));'%(prefix, y.cname, y.ctype, prefix, cref.cname))
                            self.code('%s%s = (%s)%s%s.Count;' % (prefix, y.cname, y.ctype, prefix, cref.cname))
                    elif y.arr2_ref: # 1-dimensional dynamic array
                        cref = block.find_member(y.arr2_ref[0], True) 
                        if not y.arr1 or not y.arr1.lhs: # Second dimension
                            #if not cref.is_duplicate and not cref.next_dup (not cref.cond.lhs or cref.cond.lhs == y.name):
                            #    self.code('assert(%s%s == (%s)((%s%s.size() > 0) ? %s%s[0].size() : 0));' % (prefix, y.cname, y.ctype, prefix, cref.cname, prefix, cref.cname))
                            self.code('%s%s = (%s)(%s%s.Count > 0 ? %s%s[0].Count : 0);' % (prefix, y.cname, y.ctype, prefix, cref.cname, prefix, cref.cname))
                        else:
                            # index of dynamically sized array
                            self.code('for (var i%i = 0; i%i < %s%s.Count; i%i++)' % (self.indent, self.indent, prefix, cref.cname, self.indent))
                            self.code('\t%s%s[i%i] = (%s)%s%s[i%i].Count;' % (prefix, y.cname, self.indent, y.ctype, prefix, cref.cname, self.indent))
                    #else: #has duplicates needs to be selective based on version
                    #    self.code('assert(!"%s");' % (y.name))
            block.members.reverse() # undo reverse


        # now comes the difficult part: processing all members recursively
        for y in block.members:
            # get block
            if y.type in basic_types:
                subblock = basic_types[y.type]
            elif y.type in compound_types:
                subblock = compound_types[y.type]
            elif y.type in enum_types:
                subblock = enum_types[y.type]
            elif y.type in flag_types:
                subblock = flag_types[y.type]
                
            # check for links
            if action in [ACTION_FIXLINKS, ACTION_GETREFS, ACTION_GETPTRS]:
                if not subblock.has_links and not subblock.has_crossrefs:
                    continue # contains no links, so skip this member!
            if action == ACTION_OUT:
                if y.is_duplicate:
                    continue # don't write variables twice
            # resolve array & cond references
            y_arr1_lmember = None
            y_arr2_lmember = None
            y_cond_lmember = None
            y_arg = None
            y_arr1_prefix = ''
            y_arr2_prefix = ''
            y_cond_prefix = ''
            y_arg_prefix = ''
            if y.arr1.lhs or y.arr2.lhs or y.cond.lhs or y.arg:
                for z in block.members:
                    if not y_arr1_lmember and y.arr1.lhs == z.name:
                        y_arr1_lmember = z
                    if not y_arr2_lmember and y.arr2.lhs == z.name:
                        y_arr2_lmember = z
                    if not y_cond_lmember:
                        if y.cond.lhs == z.name:
                            y_cond_lmember = z
                        elif y.cond.op == '&&' and y.cond.lhs == z.name:
                            y_cond_lmember = z
                        elif y.cond.op == '||' and y.cond.lhs == z.name:
                            y_cond_lmember = z
                    if not y_arg and y.arg == z.name:
                        y_arg = z
                if y_arr1_lmember:
                    y_arr1_prefix = prefix
                if y_arr2_lmember:
                    y_arr2_prefix = prefix
                if y_cond_lmember:
                    y_cond_prefix = prefix
                if y_arg:
                    y_arg_prefix = prefix
            # resolve this prefix
            y_prefix = prefix
            # resolve arguments
            if y.arr1 and y.arr1.lhs == 'ARG':
                y.arr1.lhs = arg_member.name
                y.arr1.clhs = arg_member.cname
                y_arr1_prefix = arg_prefix
            if y.arr2 and y.arr2.lhs == 'ARG':
                y.arr2.lhs = arg_member.name
                y.arr2.clhs = arg_member.cname
                y_arr2_prefix = arg_prefix
            if y.cond and y.cond.lhs == 'ARG':
                y.cond.lhs = arg_member.name
                y.cond.clhs = arg_member.cname
                y_cond_prefix = arg_prefix
            # conditioning
            y_cond = y.cond.code(y_cond_prefix)
            y_vercond = y.vercond.code('info.')
            if action in [ACTION_READ, ACTION_WRITE, ACTION_FIXLINKS]:
                if lastver1 != y.ver1 or lastver2 != y.ver2 or lastuserver != y.userver or lastuserver2 != y.userver2 or lastvercond != y_vercond:
                    # we must switch to a new version block
                    # close old version block
                    if lastver1 or lastver2 or lastuserver or lastuserver2 or lastvercond: self.code('}')
                    # close old condition block as well
                    if lastcond:
                        self.code('}'); lastcond = None
                    # start new version block
                    
                    concat = ''
                    verexpr = ''
                    if y.ver1:
                        verexpr = 'info.version >= 0x%08X' % y.ver1; concat = ' && '
                    if y.ver2:
                        verexpr = '%s%sinfo.version <= 0x%08X' % (verexpr, concat, y.ver2); concat = ' && '
                    if y.userver != None:
                        verexpr = '%s%sinfo.userVersion == %s' % (verexpr, concat, y.userver); concat = ' && '
                    if y.userver2 != None:
                        verexpr = '%s%sinfo.userVersion2 == %s' % (verexpr, concat, y.userver2); concat = ' && '
                    if y_vercond:
                        verexpr = '%s%s(%s)' % (verexpr, concat, y_vercond)
                    if verexpr:
                        # remove outer redundant parenthesis
                        bleft, bright = scanBrackets(verexpr)
                        if bleft == 0 and bright == (len(verexpr) - 1):
                            self.code('if %s' % verexpr); self.code('{')
                        else:
                            self.code('if (%s)' % verexpr); self.code('{')
                    
                    # start new condition block
                    if lastcond != y_cond and y_cond:
                        self.code('if (%s)' % y_cond); self.code('{')
                else:
                    # we remain in the same version block
                    # check condition block
                    if lastcond != y_cond:
                        if lastcond:
                            self.code('}')
                        if y_cond:
                            self.code('if (%s)' % y_cond); self.code('{')
            elif action == ACTION_OUT:
                # check condition block
                if lastcond != y_cond:
                    if lastcond:
                        self.code('}')
                    if y_cond:
                        self.code('if (%s)' % y_cond); self.code('{')
    
            # loop over arrays
            # and resolve variable name
            if not y.arr1.lhs:
                z = '%s%s' % (y_prefix, y.cname)
            else:
                if action == ACTION_OUT:
                    self.code('array_output_count = 0;')
                if y.arr1.lhs.isdigit() == False:
                    if action == ACTION_READ:
                      # default to local variable, check if variable is in current scope if not then try to use definition from resized child
                      memcode = '%s%s = new %s[%s];' % (y_prefix, y.cname, y.ctype, y.arr1.code(y_arr1_prefix))
                      mem = block.find_member(y.arr1.lhs, True) # find member in self or parents
                      self.code(memcode)
                      
                    self.code('for (var i%i = 0; i%i < %s%s.Count; i%i++)' % (self.indent, self.indent, y_prefix, y.cname, self.indent)); self.code('{')
                else:
                    self.code('for (var i%i = 0; i%i < %s; i%i++)' % (self.indent, self.indent, y.arr1.code(y_arr1_prefix), self.indent)); self.code('{')
                if action == ACTION_OUT:
                        self.code('if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))'); self.code('{')
                        self.code('%s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");' % stream)
                        self.code('break;')
                        self.code('}')
                        
                if not y.arr2.lhs:
                    z = '%s%s[i%i]' % (y_prefix, y.cname, self.indent - 1)
                else:
                    if not y.arr2_dynamic:
                        if y.arr2.lhs.isdigit() == False:
                            if action == ACTION_READ:
                                self.code('%s%s[i%i].Resize(%s);' % (y_prefix, y.cname, self.indent - 1, y.arr2.code(y_arr2_prefix)))
                            self.code('for (var i%i = 0; i%i < %s%s[i%i].Count; i%i++)' % (self.indent, self.indent, y_prefix, y.cname, self.indent - 1, self.indent)); self.code('{')
                        else:
                            self.code('for (var i%i = 0; i%i < %s; i%i++)' % (self.indent, self.indent, y.arr2.code(y_arr2_prefix), self.indent)); self.code('{')
                    else:
                        if action == ACTION_READ:
                            self.code('%s%s[i%i].Resize(%s[i%i]);' % (y_prefix, y.cname, self.indent - 1, y.arr2.code(y_arr2_prefix), self.indent - 1))
                        self.code('for (var i%i = 0; i%i < %s[i%i]; i%i++)' % (self.indent, self.indent, y.arr2.code(y_arr2_prefix), self.indent - 1, self.indent)); self.code('{')
                    z = '%s%s[i%i][i%i]' % (y_prefix, y.cname, self.indent - 2, self.indent - 1)
    
            if y.type in native_types:
                # these actions distinguish between refs and non-refs
                if action in [ACTION_READ, ACTION_WRITE, ACTION_FIXLINKS, ACTION_GETREFS, ACTION_GETPTRS]:
                    if (not subblock.is_link) and (not subblock.is_crossref):
                        # not a ref
                        outprefix = ''
                        if action in [ACTION_READ, ACTION_WRITE] and y.is_abstract is False:
                            if action == ACTION_READ:
                                outprefix = 'out '
                            # hack required for vector<bool>
                            if y.type == 'bool' and y.arr1.lhs:
                                self.code('{')
                                if action == ACTION_READ:
                                    self.code('Nif.NifStream(out bool tmp, %s, info); %s = tmp;' % (stream, z))
                                else: # ACTION_WRITE
                                    self.code('bool tmp = %s; Nif.NifStream(tmp, %s, info);' % (z, stream))
                                self.code('}')
                            # the usual thing
                            elif not y.arg:
                                cast = '(%s)' % y.ctype if (y.is_duplicate) else ''
                                self.code('Nif.NifStream(%s%s%s, %s, info);' % (outprefix, cast, z, stream))
                            else:
                                self.code('Nif.NifStream(%s%s, %s, info, %s%s);' % (outprefix, z, stream, y_prefix, y.carg))
                    else:
                        # a ref
                        if action == ACTION_READ:
                            self.code('Nif.NifStream(out block_num, %s, info);' % stream)
                            self.code('link_stack.Add(block_num);')
                        elif action == ACTION_WRITE:
                            self.code('WriteRef((NiObject)%s, %s, info, link_map, missing_link_stack);' % (z, stream))
                        elif action == ACTION_FIXLINKS:
                            self.code('%s = FixLink<%s>(objects, link_stack, missing_link_stack, info);' % (z, y.ctemplate))
                                
                        elif action == ACTION_GETREFS and subblock.is_link:
                            if not y.is_duplicate:
                                self.code('if (%s != null)\n\trefs.Add((NiObject)%s);' % (z, z))
                        elif action == ACTION_GETPTRS and subblock.is_crossref:
                            if not y.is_duplicate:
                                self.code('if (%s != null)\n\tptrs.Add((NiObject)%s);' % (z, z))
                # the following actions don't distinguish between refs and non-refs
                elif action == ACTION_OUT:
                    if not y.arr1.lhs:
                        self.code('%s.AppendLine($"%*s%s:  {%s}");' % (stream, 2 * self.indent, '', y.name, z))
                    else:
                        self.code('if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))');
                        self.code('\tbreak;')
                        self.code('%s.AppendLine($"%*s%s[{i%i}]:  {%s}");' % (stream, 2 * self.indent, '', y.name, self.indent - 1, z))
                        self.code('array_output_count++;')
            else:
                subblock = compound_types[y.type]
                if not y.arr1.lhs:
                    self.stream(subblock, action, '%s%s_' % (localprefix, y.cname), '%s.' % z, y_arg_prefix,  y_arg)
                elif not y.arr2.lhs:
                    self.stream(subblock, action, '%s%s_' % (localprefix, y.cname), '%s.' % z, y_arg_prefix, y_arg)
                else:
                    self.stream(subblock, action, '%s%s_' % (localprefix, y.cname), '%s.' % z, y_arg_prefix, y_arg)

            # close array loops
            if y.arr1.lhs:
                self.code('}')
                if y.arr2.lhs:
                    self.code('}')

            lastver1 = y.ver1
            lastver2 = y.ver2
            lastuserver = y.userver
            lastuserver2 = y.userver2
            lastcond = y_cond
            lastvercond = y_vercond

        if action in [ACTION_READ, ACTION_WRITE, ACTION_FIXLINKS]:
            if lastver1 or lastver2 or not(lastuserver is None) or not(lastuserver2 is None) or lastvercond:
                self.code('}')
        if action in [ACTION_READ, ACTION_WRITE, ACTION_FIXLINKS, ACTION_OUT]:
            if lastcond:
                self.code('}')

        # the end
        if isinstance(block, Block) or block.name in ['Header', 'Footer']:
            if action == ACTION_OUT: 
                self.code('return s.ToString();')
            if action == ACTION_GETREFS:
                self.code('return refs;')
            if action == ACTION_GETPTRS:
                self.code('return ptrs;')

    # declaration
    # print '$t Get$n() const; \nvoid Set$n($t value);\n\n';
    def getset_declare(self, block, prefix=''): # prefix is used to tag local variables only
        for y in block.members:
            if not y.func:
                if y.cname.lower().find('unk') == -1:
                    self.code(y.getter_declare('', ';'))
                    self.code(y.setter_declare('', ';'))
                    self.code()


def class_name(n):
    """
    Formats a valid C# class name from the name format used in the XML.
    @param n: The class name to format in C# style.
    @type n: string
    @return The resulting valid C# class name
    @rtype: string
    """
    if n == None: return None
    try:
        return native_types[n]
    except KeyError:
        return n.replace(' ', '_').replace(':', '_')

    if n == None: return None
    try:
        return native_types[n]
    except KeyError:
        pass
    if n == 'TEMPLATE': return 'T'
    n2 = ''
    for i, c in enumerate(n):
        if ('A' <= c) and (c <= 'Z'):
            if i > 0: n2 += '_'
            n2 += c.lower()
        elif (('a' <= c) and (c <= 'z')) or (('0' <= c) and (c <= '9')):
            n2 += c
        else:
            n2 += '_'
    return n2

def define_name(n):
    """
    Formats an all-uppercase version of the name for use in C# defines.
    @param n: The class name to format in define style.
    @type n: string
    @return The resulting valid C# define name
    @rtype: string
    """
    n2 = ''
    for i, c in enumerate(n):
        if ('A' <= c) and (c <= 'Z'):
            if i > 0:
                n2 += '_'
                n2 += c
            else:
                n2 += c
        elif (('a' <= c) and (c <= 'z')) or (('0' <= c) and (c <= '9')):
            n2 += c.upper()
        else:
            n2 += '_'
    return n2

def member_name(n):
    """
    Formats a version of the name for use as a C# member variable.
    @param n: The attribute name to format in variable style.
    @type n: string
    @return The resulting valid C# variable name
    @rtype: string
    """
    if n == None: return None
    if n == 'ARG': return 'ARG'
    n2 = ''
    lower = True
    for i, c in enumerate(n):
        if c == ' ':
            lower = False
        elif (('A' <= c) and (c <= 'Z')) or (('a' <= c) and (c <= 'z')) or (('0' <= c) and (c <= '9')):
            if lower:
                n2 += c.lower()
            else:
                n2 += c.upper()
                lower = True
        elif c == '\\': # arg member access operator
            n2 += '.'
        else:
            n2 += '_'
            lower = True
    return n2
    
def version2number(s):
    """
    Translates a legible NIF version number to the packed-byte numeric representation. For example, "10.0.1.0" is translated to 0x0A000100.
    @param s: The version string to translate into numeric form.
    @type s: string
    @return The resulting numeric version of the given version string.
    @rtype: int
    """
    if not s: return None
    l = s.split('.')
    if len(l) > 4:
        assert(False)
        return int(s)
    
    if len(l) == 2:
        version = 0
        version += int(l[0]) << (3 * 8)
        if len(l[1]) >= 1:
            version += int(l[1][0]) << (2 * 8)
        if len(l[1]) >= 2:
            version += int(l[1][1]) << (1 * 8)
        if len(l[1]) >= 3:
            version += int(l[1][2:])
        return version
    else:
        version = 0
        for i in range(0, len(l)):
            version += int(l[i]) << ((3 - i) * 8)
            #return (int(l[0]) << 24) + (int(l[1]) << 16) + (int(l[2]) << 8) +
            #int(l[3])
        return version
    

def userversion2number(s):
    """
    Translates a legible NIF user version number to the packed-byte numeric representation.
    Currently just converts the string to an int as this may be a raw number.
    Probably to be used just in case this understanding changes.
    @param s: The version string to translate into numeric form.
    @type s: string
    @return The resulting numeric version of the given version string.
    @rtype: int
    """
    if not s: return None
    return int(s)

def scanBrackets(expr_str, fromIndex=0):
    """Looks for matching brackets.

    >>> scanBrackets('abcde')
    (-1, -1)
    >>> scanBrackets('()')
    (0, 1)
    >>> scanBrackets('(abc(def))g')
    (0, 9)
    >>> s = '  (abc(dd efy 442))xxg'
    >>> startpos, endpos = scanBrackets(s)
    >>> print s[startpos+1:endpos]
    abc(dd efy 442)
    """
    startpos = -1
    endpos = -1
    scandepth = 0
    for scanpos in range(fromIndex, len(expr_str)):
        scanchar = expr_str[scanpos]
        if scanchar == '(':
            if startpos == -1:
                startpos = scanpos
            scandepth += 1
        elif scanchar == ')':
            scandepth -= 1
            if scandepth == 0:
                endpos = scanpos
                break
    else:
        if startpos != -1 or endpos != -1:
            raise ValueError('expression syntax error (non-matching brackets?)')
    return (startpos, endpos)
    
class Expression(object):
    """This class represents an expression.

    >>> class A(object):
    ...     x = False
    ...     y = True
    >>> a = A()
    >>> e = Expression('x || y')
    >>> e.eval(a)
    1
    >>> Expression('99 & 15').eval(a)
    3
    >>> bool(Expression('(99&15)&&y').eval(a))
    True
    >>> a.hello_world = False
    >>> def nameFilter(s):
    ...     return 'hello_' + s.lower()
    >>> bool(Expression('(99 &15) &&WoRlD', name_filter = nameFilter).eval(a))
    False
    >>> Expression('c && d').eval(a)
    Traceback (most recent call last):
        ...
    AttributeError: 'A' object has no attribute 'c'
    >>> bool(Expression('1 == 1').eval())
    True
    >>> bool(Expression('1 != 1').eval())
    False
    """
    operators = ['==', '!=', '>=', '<=', '&&', '||', '&', '|', '-', '+', '>', '<', '/', '*']
    def __init__(self, expr_str, name_filter=None):
        self._code = expr_str
        left, self._op, right = self._partition(expr_str)
        self._left = self._parse(left, name_filter)
        if right:
            self._right = self._parse(right, name_filter)
        else:
            self._right = ''

    def eval(self, data=None):
        """Evaluate the expression to an integer."""

        if isinstance(self._left, Expression):
            left = self._left.eval(data)
        elif isinstance(self._left, basestring):
            left = getattr(data, self._left) if self._left != '""' else ''
        else:
            assert(isinstance(self._left, int)) # debug
            left = self._left

        if not self._op:
            return left

        if isinstance(self._right, Expression):
            right = self._right.eval(data)
        elif isinstance(self._right, basestring):
            right = getattr(data, self._right) if self._right != '""' else ''
        else:
            assert(isinstance(self._right, int)) # debug
            right = self._right

        if self._op == '==':
            return int(left == right)
        elif self._op == '!=':
            return int(left != right)
        elif self._op == '>=':
            return int(left >= right)
        elif self._op == '<=':
            return int(left <= right)
        elif self._op == '&&':
            return int(left and right)
        elif self._op == '||':
            return int(left or right)
        elif self._op == '&':
            return left & right
        elif self._op == '|':
            return left | right
        elif self._op == '-':
            return left - right
        elif self._op == '+':
            return left + right
        elif self._op == '/':
            return left / right
        elif self._op == '*':
            return left * right
        elif self._op == '!':
            return not left
        else:
            raise NotImplementedError("expression syntax error: operator '" + op + "' not implemented")

    def __str__(self):
        """Reconstruct the expression to a string."""

        left = str(self._left)
        if not self._op: return left
        right = str(self._right)
        return left + ' ' + self._op + ' ' + right

    def encode(self, encoding):
        """
        To allow encode() to be called on an Expression directly as if it were a string
        (For Python 2/3 cross-compatibility.)
        """
        return self.__str__().encode(encoding)

    @classmethod
    def _parse(cls, expr_str, name_filter=None):
        """Returns an Expression, string, or int, depending on the
        contents of <expr_str>."""
        # brackets or operators => expression
        if ('(' in expr_str) or (')' in expr_str):
            return Expression(expr_str, name_filter)
        for op in cls.operators:
            if expr_str.find(op) != -1:
                return Expression(expr_str, name_filter)
                
        mver = re.compile('[0-9]+\\.[0-9]+\\.[0-9]+\\.[0-9]+')
        iver = re.compile('[0-9]+')
        # try to convert it to an integer
        try:
            if mver.match(expr_str):
                return '0x%08X' % (version2number(expr_str))
            elif iver.match(expr_str):
                return str(int(expr_str))
        except ValueError:
            pass
        # failed, so return the string, passed through the name filter
        return name_filter(expr_str) if name_filter else expr_str

    @classmethod
    def _partition(cls, expr_str):
        """Partitions expr_str. See examples below.

        >>> Expression._partition('abc || efg')
        ('abc', '||', 'efg')
        >>> Expression._partition('abc||efg')
        ('abc', '||', 'efg')
        >>> Expression._partition('abcdefg')
        ('abcdefg', '', '')
        >>> Expression._partition(' abcdefg ')
        ('abcdefg', '', '')
        >>> Expression._partition(' (a | b) & c ')
        ('a | b', '&', 'c')
        >>> Expression._partition('(a | b)!=(b&c)')
        ('a | b', '!=', 'b&c')
        >>> Expression._partition('(a== b) &&(( b!=c)||d )')
        ('a== b', '&&', '( b!=c)||d')
        """
        # check for unary operators
        if expr_str.strip().startswith('!'):
            return expr_str.lstrip(' !'), '!', None
        lenstr = len(expr_str)
        # check if the left hand side starts with brackets
        # and if so, find the position of the starting bracket and the ending
        # bracket
        left_startpos, left_endpos = cls._scanBrackets(expr_str)
        if left_startpos >= 0:
            # yes, it is a bracketted expression
            # so remove brackets and whitespace,
            # and let that be the left hand side
            left_str = expr_str[left_startpos + 1:left_endpos].strip()
            
            # the next token should be the operator
            # find the position where the operator should start
            op_startpos = left_endpos + 1
            while op_startpos < lenstr and expr_str[op_startpos] == ' ':
                op_startpos += 1
            if op_startpos < lenstr:
                # to avoid confusion between && and &, and || and |,
                # let's first scan for operators of two characters
                # and then for operators of one character
                for op_endpos in range(op_startpos + 1, op_startpos - 1, -1):
                    op_str = expr_str[op_startpos:op_endpos + 1]
                    if op_str in cls.operators:
                        break
                else:
                    raise ValueError("expression syntax error: expected operator at '%s'" % expr_str[op_startpos:])
            else:
                return cls._partition(left_str)
        else:
            # it's not...  so we need to scan for the first operator
            for op_startpos, ch in enumerate(expr_str):
                if ch == ' ': continue
                if ch == '(' or ch == ')':
                    raise ValueError("expression syntax error: expected operator before '%s'" % expr_str[op_startpos:])
                # to avoid confusion between && and &, and || and |,
                # let's first scan for operators of two characters
                for op_endpos in range(op_startpos + 1, op_startpos - 1, -1):
                    op_str = expr_str[op_startpos:op_endpos + 1]
                    if op_str in cls.operators:
                        break
                else:
                    continue
                break
            else:
                # no operator found, so we are done
                left_str = expr_str.strip()
                op_str = ''
                right_str = ''
                return left_str, op_str, right_str
            # operator found!  now get the left hand side
            left_str = expr_str[:op_startpos].strip()
            
        return left_str, op_str, expr_str[op_endpos + 1:].strip()

    @staticmethod
    def _scanBrackets(expr_str, fromIndex=0):
        """Looks for matching brackets.

        >>> Expression._scanBrackets('abcde')
        (-1, -1)
        >>> Expression._scanBrackets('()')
        (0, 1)
        >>> Expression._scanBrackets('(abc(def))g')
        (0, 9)
        >>> s = '  (abc(dd efy 442))xxg'
        >>> startpos, endpos = Expression._scanBrackets(s)
        >>> print s[startpos+1:endpos]
        abc(dd efy 442)
        """
        startpos = -1
        endpos = -1
        scandepth = 0
        for scanpos in range(fromIndex, len(expr_str)):
            scanchar = expr_str[scanpos]
            if scanchar == '(':
                if startpos == -1:
                    startpos = scanpos
                scandepth += 1
            elif scanchar == ')':
                scandepth -= 1
                if scandepth == 0:
                    endpos = scanpos
                    break
        else:
            if startpos != -1 or endpos != -1:
                raise ValueError('expression syntax error (non-matching brackets?)')
        return (startpos, endpos)
        
    def code(self, prefix='', brackets=True, name_filter=None):
        """Format an expression as a string.
        @param prefix: An optional prefix.
        @type prefix: string
        @param brackets: If C{True}, then put expression between brackets.
        @type prefix: string
        @return The expression formatted into a string.
        @rtype: string
        """
        lbracket = '(' if brackets else ''
        rbracket = ')' if brackets else ''
        if not self._op:
            if not self.lhs: return ''
            if isinstance(self.lhs, int):
                return self.lhs               
            elif self.lhs in block_types:
                return 'IsDerivedType(%s.TYPE)' % self.lhs
            else:
                return prefix + (name_filter(self.lhs) if name_filter else self.lhs)
        elif self._op == '!':
            lhs = self.lhs
            if isinstance(lhs, Expression):
                lhs = lhs.code(prefix, True, name_filter)
            elif lhs in block_types:
                lhs = 'IsDerivedType(%s.TYPE)' % lhs
            elif lhs and not lhs.isdigit() and not lhs.startswith('0x'):
                lhs = prefix + (name_filter(lhs) if name_filter else lhs)
            return '%s%s%s%s' % (lbracket, self._op, lhs, rbracket)
        else:
            lhs = self.lhs
            rhs = self.rhs
            if isinstance(lhs, Expression):
                lhs = lhs.code(prefix, True, name_filter)
            elif lhs in block_types:
                lhs = 'IsDerivedType(%s.TYPE)' % lhs
            elif lhs and not lhs.isdigit() and not lhs.startswith('0x'):
                lhs = prefix + (name_filter(lhs) if name_filter else lhs)
            if isinstance(rhs, Expression):
                rhs = rhs.code(prefix, True, name_filter)
            elif rhs in block_types:
                rhs = 'IsDerivedType(%s.TYPE)' % rhs
            elif rhs and not rhs.isdigit() and not rhs.startswith('0x'):
                rhs = prefix + (name_filter(rhs) if name_filter else rhs)
            return '%s%s %s %s%s' % (lbracket, lhs, self._op, rhs, rbracket)

    def get_terminals(self):
        """Return all terminal names (without operators or brackets)."""
        if isinstance(self.lhs, Expression):
            for terminal in self.lhs.get_terminals():
                yield terminal
        elif self.lhs:
            yield self.lhs
        if isinstance(self.rhs, Expression):
            for terminal in self.rhs.get_terminals():
                yield terminal
        elif self.rhs:
            yield self.rhs
        
    def __getattr__(self, name):
        if (name == 'lhs'):
            return getattr(self, '_left')
        if (name == 'rhs'):
            return getattr(self, '_right')
        if (name == 'op'):
            return getattr(self, '_op')
        return object.__getattribute__(self, name)

    # ducktyping: pretend we're also a string with isdigit() method
    def isdigit(self):
        return False
        
class Expr(Expression):
    """
    Represents a mathmatical expression?
    @ivar lhs: The left hand side of the expression?
    @type lhs: string
    @ivar clhs: The C# formatted version of the left hand side of the expression?
    @type clhs: string
    @ivar op: The operator used in the expression.  ==, &&, !=, etc.
    @type op: string
    @ivar rhs: The right hand side of the expression?
    @type rhs: string
    """
    def __init__(self, n, name_filter=None):
        """
        This constructor takes the expression in the form of a string and tokenizes it into left-hand side, operator, right hand side, and something called clhs.
        @param n: The expression to tokenize.
        @type n: string
        """
        Expression.__init__(self, n, name_filter)
        
    def code(self, prefix='', brackets=True, name_filter=None):
        if not name_filter:
            name_filter = member_name
        return Expression.code(self, prefix, brackets, name_filter)
                        
class Option:
    """
    This class represents an option in an option list.
    @ivar value: The C# value of option variable.  Comes from the "value" attribute of the <option> tag.
    @type value: string
    @ivar name: The name of this member variable.  Comes from the "name" attribute of the <option> tag.
    @type name: string
    @ivar description: The description of this option.  Comes from the text between <option> and </option>.
    @type description: string
    @ivar cname: The name of this member for use in C#.
    @type cname: string
    """
    def __init__(self, element):
        """
        This constructor converts an XML <option> element into an Option object.
        """
        assert element.tagName == 'option'
        parent = element.parentNode
        #sisters = parent.getElementsByTagName('option')
        
        # member attributes
        self.value = element.getAttribute('value')
        self.name = element.getAttribute('name')
        if element.firstChild:
            assert element.firstChild.nodeType == Node.TEXT_NODE
            self.description = element.firstChild.nodeValue.strip()
        else:
            self.description = self.name
        self.cname = self.name.upper().replace(' ', '_').replace('-', '_').replace('/', '_').replace('=', '_').replace(':', '_')

class Member:
    """
    This class represents a member variable?
    @ivar name: The name of this member variable.  Comes from the "name" attribute of the <add> tag.
    @type name: string
    @ivar type: The type of this member variable.  Comes from the "type" attribute of the <add> tag.
    @type type: string
    @ivar arg: The argument of this member variable.  Comes from the "arg" attribute of the <add> tag.
    @type arg: string
    @ivar template: The template type of this member variable.  Comes from the "template" attribute of the <add> tag.
    @type template: string
    @ivar arr1: The first array size of this member variable.  Comes from the "arr1" attribute of the <add> tag.
    @type arr1: Eval
    @ivar arr2: The first array size of this member variable.  Comes from the "arr2" attribute of the <add> tag.
    @type arr2: Eval
    @ivar cond: The condition of this member variable.  Comes from the "cond" attribute of the <add> tag.
    @type cond: Eval
    @ivar func: The function of this member variable.  Comes from the "func" attribute of the <add> tag.
    @type func: string
    @ivar default: The default value of this member variable.  Comes from the "default" attribute of the <add> tag.
        Formatted to be ready to use in a C# constructor initializer list.
    @type default: string
    @ivar ver1: The first version this member exists.  Comes from the "ver1" attribute of the <add> tag.
    @type ver1: string
    @ivar ver2: The last version this member exists.  Comes from the "ver2" attribute of the <add> tag.
    @type ver2: string
    @ivar userver: The user version where this member exists.  Comes from the "userver" attribute of the <add> tag.
    @type userver: string
    @ivar userver2: The user version 2 where this member exists.  Comes from the "userver2" attribute of the <add> tag.
    @type userver2: string
    @ivar vercond: The version condition of this member variable.  Comes from the "vercond" attribute of the <add> tag.
    @type vercond: Eval    
    @ivar is_public: Whether this member will be declared public.  Comes from the "public" attribute of the <add> tag.
    @type is_public: string
    @ivar is_abstract: Whether this member is abstract.  This means that it does not factor into read/write.
    @type is_abstract: bool
    @ivar description: The description of this member variable.  Comes from the text between <add> and </add>.
    @type description: string
    @ivar uses_argument: Specifies whether this attribute uses an argument.
    @type uses_argument: bool
    @ivar type_is_native: Specifies whether the type is implemented natively
    @type type_is_native: bool
    @ivar is_duplicate: Specifies whether this is a duplicate of a previously declared member
    @type is_duplicate: bool
    @ivar arr2_dynamic: Specifies whether arr2 refers to an array (?)
    @type arr2_dynamic: bool
    @ivar arr1_ref: Names of the attributes it is a (unmasked) size of (?)
    @type arr1_ref: string array?
    @ivar arr2_ref: Names of the attributes it is a (unmasked) size of (?)
    @type arr2_ref: string array?
    @ivar cond_ref: Names of the attributes it is a condition of (?)
    @type cond_ref: string array?
    @ivar cname: Unlike default, name isn't formatted for C# so use this instead?
    @type cname: string
    @ivar ctype: Unlike default, type isn't formatted for C# so use this instead?
    @type ctype: string
    @ivar carg: Unlike default, arg isn't formatted for C# so use this instead?
    @type carg: string
    @ivar ctemplate: Unlike default, template isn't formatted for C# so use this instead?
    @type ctemplate: string
    @ivar carr1_ref: Unlike default, arr1_ref isn't formatted for C# so use this instead?
    @type carr1_ref: string
    @ivar carr2_ref: Unlike default, arr2_ref isn't formatted for C# so use this instead?
    @type carr2_ref: string
    @ivar ccond_ref: Unlike default, cond_ref isn't formatted for C# so use this instead?
    @type ccond_ref: string
    @ivar next_dup: Next duplicate member
    @type next_dup: Member
    @ivar is_manual_update: True if the member value is manually updated by the code
    @type is_manual_update: bool
    """
    def __init__(self, element):
        """
        This constructor converts an XML <add> element into a Member object.
        Some sort of processing is applied to the various variables that are copied from the XML tag...
        Seems to be trying to set reasonable defaults for certain types, and put things into C# format generally. 
        @param prefix: An optional prefix used in some situations?
        @type prefix: string
        @return The expression formatted into a string?
        @rtype: string?
        """
        assert element.tagName == 'add'
        parent = element.parentNode
        sisters = parent.getElementsByTagName('add')
        
        # member attributes
        self.name = element.getAttribute('name')
        self.suffix = element.getAttribute('suffix')
        self.type = element.getAttribute('type')
        self.arg = element.getAttribute('arg')
        self.template = element.getAttribute('template')
        self.arr1 = Expr(element.getAttribute('arr1'))
        self.arr2 = Expr(element.getAttribute('arr2'))
        self.cond = Expr(element.getAttribute('cond'))
        self.func = element.getAttribute('function')
        self.default = element.getAttribute('default')
        self.orig_ver1 = element.getAttribute('ver1')
        self.orig_ver2 = element.getAttribute('ver2')
        self.ver1 = version2number(element.getAttribute('ver1'))
        self.ver2 = version2number(element.getAttribute('ver2'))
        self.userver = userversion2number(element.getAttribute('userver'))
        self.userver2 = userversion2number(element.getAttribute('userver2'))
        self.vercond = Expr(element.getAttribute('vercond'))
        self.is_public = (element.getAttribute('public') == "1")
        self.is_abstract = (element.getAttribute('abstract') == "1")
        self.next_dup = None
        self.is_manual_update = False
        self.is_calculated = (element.getAttribute('calculated') == "1")

        #Get description from text between start and end tags
        if element.firstChild:
            assert element.firstChild.nodeType == Node.TEXT_NODE
            self.description = element.firstChild.nodeValue.strip()
        elif self.name.lower().find('unk') == 0:
            self.description = 'Unknown.'
        else:
            self.description = ''
        
        # Format default value so that it can be used in a C# initializer list
        if not self.default and (not self.arr1.lhs and not self.arr2.lhs):
            if self.type in ['uint', 'ushort', 'byte', 'int', 'short', 'char']:
                self.default = '0'
            elif self.type == 'bool':
                self.default = 'false'
            elif self.type in ['Ref', 'Ptr']:
                self.default = 'null'
            elif self.type in 'float':
                self.default = '0.0'
            elif self.type == 'HeaderString':
                pass
            elif self.type == 'Char8String':
                pass
            elif self.type == 'StringOffset':
                self.default = '-1'
            elif self.type in basic_names:
                self.default = '0'
            elif self.type in flag_names or self.type in enum_names:
                self.default = '0'
        if self.default:
            if self.default[0] == '(' and self.default[-1] == ')':
                self.default = self.default[1:-1]
            if self.arr1.lhs: # handle static array types
                if self.arr1.lhs.isdigit():
                    sep = (',(%s)' % class_name(self.type))
                    self.default = self.arr1.lhs + sep + sep.join(self.default.split(' ', int(self.arr1.lhs)))
            elif self.type == 'string' or self.type == 'IndexString':
                self.default = '"%s"' % self.default
            elif self.type == 'float':
                self.default += 'f'
            elif self.type in ['Ref', 'Ptr', 'bool', 'Vector3']:
                pass
            elif self.default.find(',') != -1:
                self.default = '(%s)' % self.default
            elif not self.default.isdigit() and self.type in enum_names:
                self.default = '%s.%s' % (class_name(self.type), self.default)
            else:
                self.default = '(%s)%s' % (class_name(self.type), self.default)
        
        # calculate other stuff
        self.uses_argument = (self.cond.lhs == '(ARG)' or self.arr1.lhs == '(ARG)' or self.arr2.lhs == '(ARG)')
        self.type_is_native = self.name in native_types # true if the type is implemented natively

        # calculate stuff from reference to previous members
        # true if this is a duplicate of a previously declared member
        self.is_duplicate = False
        self.arr2_dynamic = False  # true if arr2 refers to an array
        sis = element.previousSibling
        while sis:
            if sis.nodeType == Node.ELEMENT_NODE:
                sis_name = sis.getAttribute('name')
                if sis_name == self.name and not self.suffix:
                    self.is_duplicate = True
                sis_arr1 = Expr(sis.getAttribute('arr1'))
                sis_arr2 = Expr(sis.getAttribute('arr2'))
                if sis_name == self.arr2.lhs and sis_arr1.lhs:
                    self.arr2_dynamic = True
            sis = sis.previousSibling

        # calculate stuff from reference to next members
        self.arr1_ref = [] # names of the attributes it is a (unmasked) size of
        self.arr2_ref = [] # names of the attributes it is a (unmasked) size of
        self.cond_ref = [] # names of the attributes it is a condition of
        sis = element.nextSibling
        while sis != None:
            if sis.nodeType == Node.ELEMENT_NODE:
                sis_name = sis.getAttribute('name')
                sis_arr1 = Expr(sis.getAttribute('arr1'))
                sis_arr2 = Expr(sis.getAttribute('arr2'))
                sis_cond = Expr(sis.getAttribute('cond'))               
                if sis_arr1.lhs == self.name and (not sis_arr1.rhs or sis_arr1.rhs.isdigit()):
                        self.arr1_ref.append(sis_name)
                if sis_arr2.lhs == self.name and (not sis_arr2.rhs or sis_arr2.rhs.isdigit()):
                        self.arr2_ref.append(sis_name)
                if sis_cond.lhs == self.name:
                    self.cond_ref.append(sis_name)
            sis = sis.nextSibling

        # C# names
        self.cname = member_name(self.name if not self.suffix else self.name + "_" + self.suffix)
        self.ctype = class_name(self.type)
        self.carg = member_name(self.arg)
        self.ctemplate = class_name(self.template)
        self.carr1_ref = [member_name(n) for n in self.arr1_ref]
        self.carr2_ref = [member_name(n) for n in self.arr2_ref]
        self.ccond_ref = [member_name(n) for n in self.cond_ref]

    # construction
    # don't construct anything that hasn't been declared
    # don't construct if it has no default
    def code_construct(self):
        if self.default and not self.is_duplicate:
            return '%s = %s' % (self.cname, self.default)

    # declaration
    def code_declare(self, prefix=''): # prefix is used to tag local variables only
        result = self.ctype
        suffix1 = ''
        suffix2 = ''
        keyword = 'internal '
        if self.ctemplate:
            if result != '*' and result != 'Ref':
                result += '<%s>' % self.ctemplate
            else:
                result = '%s' % self.ctemplate
        if self.arr1.lhs:
            if self.arr1.lhs.isdigit():
                if self.arr2.lhs and self.arr2.lhs.isdigit():
                      result = 'Array%s<Array%s<%s>>' % (self.arr1.lhs, self.arr2.lhs, result)
                else:
                      result = 'Array%s<%s>' % (self.arr1.lhs, result) 
            else:
                if self.arr2.lhs and self.arr2.lhs.isdigit():
                    result = 'IList<Array%s<%s>>' % (self.arr2.lhs, result)
                else:
                    if self.arr2.lhs:
                        result = 'IList<%s[]>' % result
                    else:
                        result = 'IList<%s>' % result
        result = keyword + result + ' ' + prefix + self.cname + suffix1 + suffix2 + ';'
        return result

    #def getter_declare(self, scope='', suffix=''):
    #  ltype = self.ctype
    #  if self.ctemplate:
    #      if ltype != '*':
    #          ltype += '<%s>' % self.ctemplate
    #      else:
    #          ltype = '%s' % self.ctemplate
    #  if self.arr1.lhs:
    #      if self.arr1.lhs.isdigit():
    #          ltype = 'Array%s<%s>' % (self.arr1.lhs, ltype)
    #          #ltype = ltype
    #      else:
    #          if self.arr2.lhs and self.arr2.lhs.isdigit():
    #              ltype = 'Array%s<%s>[]' % (self.arr2.lhs, ltype)
    #          else:
    #              ltype = '%s[]' % ltype
    #      if self.arr2.lhs:
    #          if self.arr2.lhs.isdigit():
    #              if self.arr1.lhs.isdigit():
    #                ltype = 'Array%s<%s>' % (self.arr2.lhs,ltype)
    #                #ltype = ltype
    #          else:
    #              ltype = '%s[]' % ltype
    #  result = ltype + ' ' + scope + 'Get' + self.cname[0:1].upper() + self.cname[1:] + '()' + suffix
    #  return result

    #def setter_declare(self, scope='', suffix=''):
    #  ltype = self.ctype
    #  if self.ctemplate:
    #      if ltype != '*':
    #          ltype += '<%s>' % self.ctemplate
    #      else:
    #          ltype = '%s' % self.ctemplate
    #  if self.arr1.lhs:
    #      if self.arr1.lhs.isdigit():
    #        # ltype = 'const %s&' % ltype
    #        if self.arr2.lhs and self.arr2.lhs.isdigit():
    #              ltype = 'Array%s<Array%s<%s>>' % (self.arr1.lhs, self.arr2.lhs, ltype)
    #        else:
    #              ltype = 'Array%s<%s>' % (self.arr1.lhs, ltype)
            
    #      else:
    #          if self.arr2.lhs and self.arr2.lhs.isdigit():
    #              ltype = 'Array%s<%s>[]' % (self.arr2.lhs, ltype)
    #          else:
    #              ltype = '%s[]' % ltype
    #  else:
    #      if not self.type in basic_names:
    #        ltype = '%s' % ltype
             
    #  result = 'void ' + scope + 'Set' + self.cname[0:1].upper() + self.cname[1:] + '( ' + ltype + ' value )' + suffix
    #  return result
class Version:
    def __init__(self, element):
        self.num = element.getAttribute('num')
        self.description = element.firstChild.nodeValue.strip()
        
class Basic:
    def __init__(self, element):
        global native_types

        self.name = element.getAttribute('name')
        assert(self.name) # debug
        self.cname = class_name(self.name)
        self.niflibtype = NATIVETYPES.get(self.name)
        if element.firstChild and element.firstChild.nodeType == Node.TEXT_NODE:
            self.description = element.firstChild.nodeValue.strip()
        elif self.name.lower().find('unk') == 0:
            self.description = 'Unknown.'
        else:
            self.description = ''

        self.count = element.getAttribute('count')

        self.is_link = False
        self.is_crossref = False
        self.has_links = False
        self.has_crossrefs = False

        if self.niflibtype:
            native_types[self.name] = self.niflibtype
            if self.niflibtype == 'Ref':
                self.is_link = True
                self.has_links = True
            if self.niflibtype == '*':
                self.is_crossref = True
                self.has_crossrefs = True

        self.template = (element.getAttribute('istemplate') == '1')
        self.options = []

class Enum(Basic):
  def __init__(self, element):
      Basic.__init__(self, element)
      
      self.storage = element.getAttribute('storage')
      self.prefix = element.getAttribute('prefix')
      #find the Niflib type of the storage
      self.storage = basic_types[self.storage].niflibtype
      self.description = element.firstChild.nodeValue.strip()
             
      self.niflibtype = self.cname
      native_types[self.name] = self.niflibtype
      
      # Locate all special enumeration options
      for option in element.getElementsByTagName('option'):
          if self.prefix and option.hasAttribute('name'):
              option.setAttribute('name', self.prefix + '_' + option.getAttribute('name'))
          x = Option(option)
          self.options.append(x)

class Flag(Enum):
  def __init__(self, element):
      Enum.__init__(self, element)
      for option in self.options:
        option.bit = option.value
        option.value = 1 << int(option.value)
          
class Compound(Basic):
    # create a compound type from the XML <compound /> attributes
    def __init__(self, element):
        Basic.__init__(self, element)

        #the relative path to files in the gen folder
        self.gen_file_prefix = ''
        #the relative path to files in the obj folder
        self.obj_file_prefix = '../Objs/'
        #the relative path to files in the root folder
        self.root_file_prefix = '../'

        self.members = []     # list of all members (list of Member)
        self.argument = False # does it use an argument?

        # store all attribute data & calculate stuff
        for member in element.getElementsByTagName('add'):
            x = Member(member)
            #***********************
            #** NIFLIB HACK BEGIN **
            #***********************
            if self.name == 'BoundingVolume' and x.name == 'Union':
                # ignore this one because niflib cannot handle recursively defined structures...  so we remove
                # this one to avoid the problem as a result a minority of nifs won't load
                continue 
            #*********************
            #** NIFLIB HACK END **
            #*********************
            self.members.append(x)
            
            # detect templates
            #if x.type == 'TEMPLATE':
            #    self.template = True
            #if x.template == 'TEMPLATE':
            #    self.template = True

            # detect argument
            if x.uses_argument:
                self.argument = True
            else:
                self.argument = False

            # detect links & crossrefs
            y = None
            try:
                y = basic_types[x.type]
            except KeyError:
                try:
                    y = compound_types[x.type]
                except KeyError:
                    pass
            if y:
                if y.has_links:
                    self.has_links = True
                if y.has_crossrefs:
                    self.has_crossrefs = True
                    
        # create duplicate chains for items that need it (only valid in current
        # object scope)
        #  prefer to use iterators to avoid O(n^2) but I dont know how to reset
        #  iterators
        for x in self.members:
          atx = False
          for y in self.members:
            if atx:
              if x.name == y.name: # duplicate
                x.next_dup = y
                break
            elif x == y:
              atx = True

    def code_construct(self):
        # constructor
        result = ''
        for y in self.members:
            y_code_construct = y.code_construct()
            if y_code_construct:
                result += y_code_construct + ';\n'
        return result

    def code_using(self):
        if self.niflibtype: return ''
        result = 'using System;\n'
        result += 'using System.IO;\n'
        result += 'using System.Collections.Generic;\n'
        return result

    # find member by name
    def find_member(self, name, inherit=False):
      for y in self.members:
        if y.name == name:
          return y
      return None
      
    # find first reference of name in class
    def find_first_ref(self, name):
      for y in self.members:
        if y.arr1 and y.arr1.lhs == name:
          return y
        elif y.arr2 and y.arr2.lhs == name:
          return y
      return None
    
    # Tests recursively for members with an array size.
    def has_arr(self):
        for y in self.members:
            if y.arr1.lhs or (y.type in compound_types and compound_types[y.type].has_arr()):
                return True
        return False

class Block(Compound):
    def __init__(self, element):
        Compound.__init__(self, element)
        #the relative path to files in the gen folder
        self.gen_file_prefix = '../Gen/'
        #the relative path to files in the obj folder
        self.obj_file_prefix = ''
        
        self.is_ancestor = (element.getAttribute('abstract') == '1')
        inherit = element.getAttribute('inherit')
        if inherit:
            self.inherit = block_types[inherit]
        else:
            self.inherit = None
        self.has_interface = (element.getElementsByTagName('interface') != [])

    # find member by name
    def find_member(self, name, inherit=False):
      ret = Compound.find_member(self, name)
      if not ret and inherit and self.inherit:
        ret = self.inherit.find_member(name, inherit)
      return ret

    # find first reference of name in class
    def find_first_ref(self, name):
      ret = None
      if self.inherit:
        ret = self.inherit.find_first_ref(name)
      if not ret:
        ret = Compound.find_first_ref(self, name)
      return ret
      
#
# import elements into our code generating classes
#
# import via "import nifxml" from .
if os.path.exists('../../../../../lib/Niftools/nifxml/nif.xml'):
    doc = parse('../../../../../lib/Niftools/nifxml/nif.xml')
# import via "import nifxml" from .
elif os.path.exists('nif.xml'):
    doc = parse('nif.xml')
# import via "import docsys" from .
elif os.path.exists('docsys/nif.xml'):
    doc = parse('docsys/nif.xml')
# new submodule system
elif os.path.exists('nifxml/nif.xml'):
    doc = parse('nifxml/nif.xml')
else:
    raise ImportError('nif.xml not found')

for element in doc.getElementsByTagName('version'):
    x = Version(element)
    #print('version_types:%s' % x.num)
    version_types[x.num] = x
    version_names.append(x.num)

for element in doc.getElementsByTagName('basic'):
    x = Basic(element)
    assert not x.name in basic_types
    #print('basic_types:%s' % x.name)
    basic_types[x.name] = x
    basic_names.append(x.name)

for element in doc.getElementsByTagName('enum'):
    x = Enum(element)
    assert not x.name in enum_types
    #print('enum_types:%s' % x.name)
    enum_types[x.name] = x
    enum_names.append(x.name)

for element in doc.getElementsByTagName('bitflags'):
    x = Flag(element)
    assert not x.name in flag_types
    #print('flag_types:%s' % x.name)
    flag_types[x.name] = x
    flag_names.append(x.name)
    
for element in doc.getElementsByTagName('compound'):
    x = Compound(element)
    assert not x.name in compound_types
    #print('compound:%s' % x.name)
    compound_types[x.name] = x
    compound_names.append(x.name)

for element in doc.getElementsByTagName('niobject'):
    x = Block(element)
    assert not x.name in block_types
    #print('niobject:%s' % x.name)
    block_types[x.name] = x
    block_names.append(x.name)