#!/usr/bin/python

# custom_all.py
#
# This script generates C# code for Niflib.

"""
This module generates C# code for Niflib from the NIF file format specification XML.

@author: Amorilia

@var custom_lines: Maps custom block names.
@type custom_lines: C{list}
"""

from __future__ import unicode_literals
from distutils.dir_util import mkpath
import os
import io
import itertools

CUSTOMTYPES = [
    'MISC',
    'FILE HEAD',
    'FILE FOOT',
    'PRE-READ',
    'POST-READ',
    'PRE-WRITE',
    'POST-WRITE',
    'PRE-STRING',
    'POST-STRING',
    'PRE-FIXLINKS',
    'POST-FIXLINKS',
    'CONSTRUCTOR',
    'DESTRUCTOR',
    'INCLUDE']

#
# Function to extract custom code from existing file
#
def ExtractCustomCode(file_name):
    custom_lines = {}
    for type in CUSTOMTYPES:
        custom_lines[type] = []
    
    if os.path.isfile(file_name) == False:
        #for type in CUSTOMTYPES:
        #    custom_lines[type].append("\n")
        return custom_lines
    
    f = io.open(file_name, 'rt', 1, 'utf-8')
    lines = f.readlines()
    f.close()
   
    custom_flag = False
    custom_name = ""
    
    for l in lines:
        if custom_flag == True:
            if l.find('//--END:CUSTOM--//') != -1:
                custom_flag = False
            else:
                if not custom_lines[custom_name]:
                    custom_lines[custom_name] = [l]
                else:
                    custom_lines[custom_name].append(l)
        for type in CUSTOMTYPES:
            if l.find('//--BEGIN:%s--//'%type) != -1:
                custom_flag = True
                custom_name = type
                break
    
    return custom_lines

#
# Preserve Custom code from before
#
def WriteCustomCode(ctx, cs, name):
    if ctx[name]:
        cs.code('//--BEGIN:%s--//' % name)
        for l in ctx[name]:
            cs.write(l)
        cs.code('//--END:CUSTOM--//')

#
# Function to compare two files
#
def OverwriteIfChanged(original_file, candidate_file):
    files_differ = False

    if os.path.isfile(original_file):
        f1 = file(original_file, 'r')
        f2 = file(candidate_file, 'r')

        s1 = f1.read()
        s2 = f2.read()

        f1.close()
        f2.close()
        
        if s1 != s2:
            files_differ = True
            #remove original file
            os.unlink(original_file)
    else:
        files_differ = True

    if files_differ:
        #Files differ, so overwrite original with candidate
        os.rename(candidate_file, original_file)
   