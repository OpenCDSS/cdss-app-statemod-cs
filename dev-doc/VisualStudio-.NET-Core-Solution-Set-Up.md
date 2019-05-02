## Visual Studio .NET Core Solution Set-Up ##

This documentation describes the steps taken to create a *solution* in [Visual Studio](https://visualstudio.microsoft.com/) 2017 Community edition, specifically to configure a development environment for the [StateMod C#](https://github.com/OpenCDSS/cdss-app-statemod-cs) program, using the .NET Core framework.

#### Outline: ####

* [Introduction](#introduction)
* [Set up folder structure](#set-up-folder-structure)
* [Download and install .NET Core](#download-and-install-net-core)
* [Download and configure Visual Studio](#download-and-configure-visual-studio)
* [Create empty solution](#create-empty-solution)
* [Create blank projects](#create-blank-projects)
* [Configuring projects](#configuring-projects) 
* [Adding files to project](#adding-files-to-project)
* [Set namespace](#set-namespace)
* [Dependencies](#dependencies) 
* [Setting run configuration](#setting-run-configurations)
* [Troubleshooting](#troubleshooting) 

## Introduction:

**Solution:**

A Visual Studio solution is similar to a workspace in Eclipse. A solution is a container that is necessary for building a program. The solution can contain one or more projects.

**Project:**

A project contains all the source code files, program, icons, images, data files, etc. that get compiled into an executable library or website.  A project also contains compiler settings and other configuration files that might be needed by various components that comprise a program. 

The projects used with StateMod-CS are either Console Applications (.NET Core) or Class Libraries (.NET Standard). The console application is used for the main project to run the application, and class libraries are used for supporting reusable code, which was initially automatically ported from java code to streamline prototype the c# program. 

There are other .NET frameworks available when creating projects, but .NET Core and .NET Standard are what has been decided as best for this application. .NET Core allows easier portability between different operating systems which is the desired behavior. .NET Standard is built with API's that hook into all separate .NET frameworks which makes it a good framework for class libraries which may be used across different console applications that may be designed using different .NET frameworks.

## Set up folder structure:

Before setting up any projects or solutions, clone the repositories in a folder structure that has been pre-defined for development on an application such as StateMod.

1. To clone all repositories, navigate to the [cdss-app-statemod-cs](https://github.com/OpenCDSS/cdss-app-statemod-cs) repository on GitHub.
2. `git clone https://github.com/OpenCDSS/cdss-app-statemod-cs.git` to clone and download the repository for the main program.
3. `cd` into `cdss-app-statemod-cs\build-util`.
4. Run `./git-clone-all-sm.sh`. This will automatically download the remaining repositories. The following is the recommended development environment folder structure.

```
C:\Users\user\                                 User's home folder for Windows.
/c/Users/user/                                 User's home folder for Git Bash.
/cygdrive/C/Users/user/                        User's home folder for Cygwin.
/home/user/                                    User's home folder for Linux.
  cdss-dev/                                    Projects that are part of Colorado's Decision Support Systems.
    StateMod-CS/                               StateMod C# product folder.
                                               (name of this folder is not critical).
      ---- below here folder names should match exactly ----
      git-repos/                               Git repositories for the StateMod Application.
        cdss-app-statemod-cs/                  StateMod Application
        cdss-lib-cdss-cs/                      CDSS library files
        cdss-lib-common-cs/                    Common library files
        cdss-lib-models-cs/                    Model library files
     vs-solution/                              Empty folder for solution files
```



## Download and install .NET Core:

In order to implement a .NET Core framework in Visual Studio, first download .NET Core sdk:

1. Go to the .NET Core Microsoft [download](https://dotnet.microsoft.com/download/visual-studio-sdks) page.
2. Select the .NET Core SDK to work with. Visual Studio 2017 Community currently only supports up to .NET Core 2.0.
3. Run `dotnet-sdk-2.1.202-win-x64.exe`. 
4. Click through the options in the install wizard to install.



## Download and configure Visual Studio:

1. Navigate to https://visualstudio.microsoft.com/ and select ***Community 2017*** from the dropdown menu **Download for Windows**. 
2. Open and run the downloaded executable, *vs_community.exe*. 
3. Click **Continue**. 
4. Select the workload **.NET Core cross-platform development**. This allows .NET Core capability within Visual Studio.
5. Click **Install**.

## Create empty solution:

The first step to setting up the development environment in Visual Studio is to create an empty solution: .

1. Open Visual Studio.
2. Select ***File*** > **New** > **Project...**. 
3. In the **New Project** window select the **Other Project Types** dropdown menu and click on **Visual Studio Solutions**. 
4. Select **Blank Solution**. 
5. Name the solution `cdss-app-statemod-cs` and set the location to `~\User\cdss-dev\StateMod-CS\vs-solution\`. 
6. Uncheck **Create directory for solution** as this just adds an un-necessary folders to the directory.

## Create blank projects:

Next, create four separate blank projects for each cloned repository.

1. Right click on the solution in the ***Solution Explorer***.

2. Click **Add** > **New Project**.

3. Follow the next steps according to which project is being set up. 

   First create the main program project as a console application project:

   1. `cdss-app-statemod-cs`:
      1. Select **Visual C#** in the right hand menu.
      2. Select **Console App (.NET Core)** from the options.
      3. Name the project the same as the repository. ie: "cdss-app-statemod-cs".
      4. Click **Browse...** and select the cloned repository file location: `~\User\cdss-dev\StateMod-CS\git-repos\cdss-app-statemod-cs`\.
      5. Hit **OK**. This will create a subfolder `cdss-app-statemod-cs\` in the location specified in the step above. This will also open the new project  file, `Program.cs` in the Visual Studio browser. 
   2. Create class library projects for the remaining cloned repositories (`cdss-lib-cdss-cs`, `cdss-lib-common-cs`, `cdss-lib-models-cs`). The following example is for `cdss-lib-cdss-cs`.
      1. Select **Visual C#** in the right hand menu.
      2. Select **Class Library (.NET Standard)** from the options.
      3. Name the project the same as the repository. ie: "cdss-lib-cdss-cs".
      4. Click **Browse...** and select the cloned repository file location: `~\User\cdss-dev\StateMod-CS\git-repos\cdss-lib-cdss-cs`.
      5. Hit **OK**. This will create a subfolder `cdss-lib-cdss-cs\` in the location specified in the step above.
      6. Repeat for `cdss-lib-common-cs` and `cdss-lib-models-cs` adjusting the name and the path accordingly.

4. All of the project folders are now saved in a subfolder under each repository folder, but these files will be moved to prevent cluttering up the directory space and having redundant folder structures. 

   1. Close Visual Studio.

   2. Manually move the `obj\` folder, `bin\` folder (if a class library), and the project file out of the project folder and move it up a level into the repository folder. 

      **Example**: 

      Move `obj\` and `cdss-app-statemod-cs.csproj` from the location `~\User\cdss-dev\StateMod-CS\git-repos\cdss-app-statemod-cs\cdss-app-statemod-cs` to  `~\User\cdss-dev\StateMod-CS\git-repos\cdss-app-statemod-cs\`. 

   3. Delete the project folder along with any automatically generated files such as `Program.cs` and `Class1.cs`. 

   4. Edit the solution file to properly reflect the new location of the project file. 

      1. Open the solution file `~vs-solution\cdss-app-statemod-cs.sln` with a text editor and edit the file accordingly. Remove the subfolder from each project path so it reflects similarly to the example below. 

         **Example**: 

         ```
         Project("{9A19103F-16F7-4668-BE54-9A1E7A4F7556}") = "cdss-app-statemod-cs", "..\git-repos\cdss-app-statemod-cs\cdss-app-statemod-cs.csproj", "{87C3B8CD-4C58-487F-934F-AD37F2ACA97A}"
         EndProject
         Project("{9A19103F-16F7-4668-BE54-9A1E7A4F7556}") = "cdss-lib-cdss-cs", "..\git-repos\cdss-lib-cdss-cs\cdss-lib-cdss-cs.csproj", "{55087D31-EF8B-4527-8762-3AE040090049}"
         EndProject
         Project("{9A19103F-16F7-4668-BE54-9A1E7A4F7556}") = "cdss-lib-common-cs", "..\git-repos\cdss-lib-common-cs\cdss-lib-common-cs.csproj", "{8F5B21E5-10FD-47CE-880D-6EA4C3A7DE87}"
         EndProject
         Project("{9A19103F-16F7-4668-BE54-9A1E7A4F7556}") = "cdss-lib-models-cs", "..\git-repos\cdss-lib-models-cs\cdss-lib-models-cs.csproj", "{3D085A22-0F28-497D-A149-11F25107E461}"
         EndProject
         ```

   5. Re-open the solution in Visual Studio and Right Click each solution then click **Rebuild Solution**.

*Double check that solution is working properly by right clicking on the solution > **Build Solution***. 

## Configuring Projects:

Ensure that the projects are configured properly within Visual Studio:

Right click project name > **:wrench: Properties**. 

**Assembly Name:** If multiple projects have identical assembly names they will conflict with one another when trying to add them to a file as a dependency. Set the assembly name to the repository name. Example: `cdss-app-statemod-cs`. 

**.NET framework:** Make sure the .NET framework's match across projects. Ex: `cdss-app-statemod-cs-console-app-proj` uses Target framework: **.NET Core 2.0**. Therefore, `cdss-lib-cdss-cs-class-lib-proj`, `cdss-lib-common-cs-class-lib-proj`, `cdss-lib-models-cs-class-lib-proj` should all have Target framework: **.NET Standard 2.0**. If running into .NET issues see [Troubleshooting](#troubleshooting). 

**Default namespace**: Set this field to be blank. When creating new files follow the namespace conventions specified in the section [Set namespace](#set-namespace).

## Adding files to project: ## 

*Make sure the default files created in setup,`Class1.cs` or `Program.cs`, have been removed.* 

To keep the workspace/solution simple and ensure the solution can be compiled, add files one at a time, copied over from the autogenerated Java code, during the initial development phase. Only add files referenced and add more as needed. When adding files follow the same directory structure as the original repository. 

Example adding `StateMod_Data.cs` to `cdss-lib-models-cs`:

1. Create new `src` folder:
   1. Rename the original `src` folder to `src-autogen-from-java`. 
   1. Right click on `src-autogen-from-java` and select **Exclude From Project** since these files are not going to be used until they are copied into the new `src` folder. 
   1. Right click on project and select **Add** > **New Folder**.
   1. Name the folder `src`.
   1. Repeat to add remaining subfolders. e.g. `src/DWR/StateMod/`.
1. Add new file:
   1. Right Click on project and select **Add** > **New Item...**.
   2. Select **Visual C# Items**.
   3. Select **Class**.
   4. Name the file `StateMod_Data.cs`.
   5. Hit **Add**.
   6. Set the namespace according to the next section [Set namespace](#set-namespace). 

Follow similar steps above for adding any other files or folders to the workspace/solution.

## Set namespace: ##

Namespaces in C# are similar to packages in java. See [Java Packages V. C# Namespaces](http://www.javacamp.org/javavscsharp/namespace.html). When configuring the project the "Default namespace" should have been set to blank. With the current configuration, when creating a new file it will default to a namespace similar to:

```c#
namespace src.cdss.statemod.app {}
```

but the `scr` folder is redundant throughout all projects so remove this from the front:

```c#
namespace cdss.statemod.app {}
```

## Dependencies: ##

**Add dependencies:** 

The next step is to add all the library projects as dependencies for the main console application project.

1. Right click on the sub folder **Dependencies** under the project folder.
2. Select **Add Reference...**.
3. Check all projects that this project depends on.

**Using dependencies:** 

Make sure classes can be discoverable by other projects by ensuring that the class uses the `public` keyword when defining that class:

```c#
​```
using System;

namespace DWR.StateMod
{
    public class StateMod_DataSet
    {
    }
}
​```
```

See the second `using` statement to understand how to include a class from a separate namespace:

```c#
using System;

namespace cdss.statemod.app
{
	using StateMod_DataSet = DWR.StateMod.StateMod_DataSet;

    public class StateModMain
    {
    }
}
```

## Setting run configurations: ##

To add command line arguments to the project:

1. Right click on `cdss-app-statemod-cs` in the solution explorer. 

2. Select **:wrench:Properties**.

3. Click **Debug** from the left hand menu.

4. Add command line arguments in the "Application arguments" text-box. 

   ex:

   `statemod-cs -sim ../../../test/datasets/cdss-yampa/StateMod/ym2015.rsp`

## Troubleshooting: ##

**Environment setup issues:**

Issues can arise in the development set up. Project files and folders need to be placed correctly within the workspace.

To ensure the set up is working properly try doing a rebuild of the solution after adding each additional project and dependency. This will help catch errors early on.

1. Right click on solution.
2. Click **Clean Solution**. Wait for solution to clean.
3. Right click on solution.
4. Click **Rebuild Solution**. 

If the projects cannot be properly added as a dependency it may be necessary to remove the project (Right Click > **:x:Remove**) and re-add the project back to the solution. 

If the above does not solve the issue, remove the project. In the directory where the project is saved remove the `bin` folder. Add the project back to the solution, and rebuild the solution.

**.NET Core issues:**

An issue faced might include not seeing the correct .NET Core versions in the projects configuration settings. There may be other possible .NET Core issues.

If running into issues with .NET Core, double check that all projects are using compatible .NET frameworks. See above in [Configuring Projects](#configuring-projects). Another cause for issue may be not having the correct .NET Core SDK installed, or if the .NET Core needs to be added to the PATH.

Another issue may be in Visual Studio configuration. If facing .NET Core issues:

1. Go to ***Tools*** > **:gear: Options** >. 
2. In Options select **Projects and Solutions** > **.NET Core**. 
3. Make sure "Use previews of the .NET Core SDK" is checked to allow using any downloaded .NET Core SDK previews.
