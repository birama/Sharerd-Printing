#Shared-Printing

##Purpose
>A example of distributed printing through the allocation and managment of digital to physical resources.

##Design
Check [Design Documentation](Documentation/desgin/) for information.

##Setup
###Dependencies
* log4net

###Building
msbuild files called makefile.csproj files are located in the:
1. [General Libary](general/makefile.csproj)
2. [Main Program](makefile.csprog)
By default they compile all files with a cs extenstion.
~~~~~~~~~~~~~~~~~~~~~~~~~
$ git clone https://github.com/vashman/Shared-Printing.git
$ cd Shared-Printing/general
$ msbuild  makefile.csproj
$ cd Shared-Printing
$ msbuild  makefile.csproj
~~~~~~~~~~~~~~~~~~~~~~~~~

##CopyRight
* Copyright (C) 2013 Sunny Sangha
* Copyright (C) 2013 Tristin Babbini
* Copyright (C) 2013 Nhiem Truong
