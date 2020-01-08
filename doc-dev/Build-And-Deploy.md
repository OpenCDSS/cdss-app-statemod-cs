# Build and Deploy #

The following documentation explains how to build and deploy the StateMod C# .NET Core application using Advanced Installer and Visual Studio 2017. 

## Build the application ##

This section details how to build the application for deployment in Visual Studio 2017. 

There are four options for deploying a .NET Core app with Visual Studio 2017.

1. Framework-dependent deployment: This assumes that .NET Core is already installed on the machine it is being deployed to and doesn't package any .NET Core libraries with the deployment.
2. Framework-dependent deployment with third-party dependencies: This assumes that .NET Core is already installed on the machine, but there are third party libraries that need to be packaged with the deployed build.
3. Self-contained deployment without third-party dependencies: This ensures that .NET Core libraries are included in the build for deployment but does not require any third-party dependencies.
4. **Self-contained deployment with third-party dependencies**: This ensures that .NET Core libraries are included in the build as well as including any third party libraries for deployment.

#### Add third parties ####

The safest and best option for building the application is to use the self-contained deployment with third-party dependencies. This ensures that all the libraries needed to run the application are a part of the deployed build. To do this, include Newtonsoft.JSON in each separate project within the solution. 

1. Use the **NuGet Package Manager** to add a reference to a NuGet package to your project. To open the package manager, select **Tools** > **NuGet Package Manager** > **Manage NuGet Packages for Solution**.
2. Confirm that `Newtonsoft.Json` is installed on your system and, if it is not, install it. The **Installed** tab lists NuGet packages installed on your system. If `Newtonsoft.Json` is not listed there, select the **Browse** tab and enter "Newtonsoft.Json" in the search box. Select `Newtonsoft.Json` and, in the right pane, select your project before selecting **Install**.
3. If `Newtonsoft.Json` is already installed on your system, add it to your project by selecting your project in the right pane of the **Manage Packages for Solution** tab.

#### Publish the application #### 

After doing the steps above you may now go through the steps to publish the application. 

1. Create a profile for your target platform.

   If this is the first profile you've created, right-click on the project (not the solution) in **Solution Explorer** and select **Publish**.

   If you've already created a profile, right-click on the project to open the **Publish** dialog if it isn't already open. Then select **New Profile**.

   The **Pick a Publish Target** dialog box opens.

2. Select the location where Visual Studio publishes your application.

   If you're only publishing to a single platform, you can accept the default value in the **Choose a folder** text box; this publishes the framework dependent deployment of your application to the *<project-directory>\bin\Release\netcoreapp2.x\publish* directory.

   If you're publishing to more than one platform, append a string that identifies the target platform. For example, if you append the string "linux" to the file path, Visual Studio publishes the framework dependent deployment of your application to the *<project-directory>\bin\Release\netcoreapp2.1\publish\linux* directory.

3. Create the profile by selecting the drop-down list icon next to the **Publish** button and selecting **Create Profile**. Then select the **Create Profile** button to create the profile.

4. Indicate that you are publishing a self-contained deployment and define a platform that your app will target.

   1. In the **Publish** dialog, select the **Configure** link to open the **Profile Settings** dialog.
   2. Select **Self-contained** in the **Deployment Mode** list box.
   3. In the **Target Runtime** list box, select one of the platforms that your application targets.
   4. Select **Save** to accept your changes and close the dialog.

5. Name your profile.

   1. Select **Actions** > **Rename Profile** to name your profile.
   2. Assign your profile a name that identifies the target platform, then select **Save*.

Repeat these steps to define any additional target platforms that your application targets.

You've configured your profiles and are now ready to publish your app. To do this:

1. If the **Publish** window isn't currently open, right-click on the project (not the solution) in **Solution Explorer** and select **Publish**.
2. Select the profile that you'd like to publish, then select **Publish**. Do this for each profile to be published.

Note that each target location (in the case of our example, bin\release\netcoreapp2.1\publish\*profile-name* contains the complete set of files (both your app files and all .NET Core files) needed to launch your app.

Along with your application's files, the publishing process emits a program database (.pdb) file that contains debugging information about your app. The file is useful primarily for debugging exceptions. You can choose not to package it with your application's files. You should, however, save it in the event that you want to debug the Release build of your app.

To learn more about publishing a C# .NET Core application with Visual Studio 2017 go [here](https://docs.microsoft.com/en-us/dotnet/core/deploying/deploy-with-vs?tabs=vs157). 

## Create Installer ##

The following section details how to create an installer for the files created after publishing the application as outlined above. This method uses the Visual Studio extension "Advanced Installer". The first step is to ensure Advanced Installer is installed. 

1. Click ***Tools*** > ***Extensions and Updates...*** then on the left side panel select ***Online***. 
2. Search for "Advanced Installer" and click Install.

Now that Advanced Installer is installed, create an installer project. 

1. Click on ***File*** > ***New*** > ***Project*** 
2. On the left hand side panel select ***Advanced Installer*** and select the **Advanced Installer Project** type. 
3. Configure the project. 
   * Give the installer a name. For example, "StateMod_Installer"
   * For the "Solution" option select "Add to solution". This should automatically set the directory path for the new installer project to be created in the solution folder. 
   * Select .NET Framework 2.0 
4. Click the button in the bottom left hand corner that says "Edit In Advanced Installer" as this will give more freedom of options and configuration settings to work with. 
5. Once Advanced Installer is opened click ***Files and Folders*** under the "Resources" tab. 
6. In the "Folders" panel right click on ***Application Folder*** and select ***Add Folder***. 
7. In the file explorer select the folder location of where the application build was deployed. For example, select <project-directory>\bin\Release\netcoreapp2.x\publish\windows\. This will load all the files created from the steps above into the installer.
8. In the very top row of the Advanced Installer window click on the image that resembles a brick wall being built and from that dropdown click ***Build All***. This will now create the `.msi` installer in the location that the project was created. For example, /vs-solution/StateMod_Installer/ and then under the directory /StateMod_Installer-SetupFiles/. 

Now it should be possible to run the installer, follow the prompts in that installer, and the files will be installed on the machine it is run on. All the libraries needed to run the application should be included in the file and the app should be able to be run from the command line as such:

`./cdss-app-statemod-cs -sim path/to/rsp/file` 