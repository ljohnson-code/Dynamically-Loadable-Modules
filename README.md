# Dynamically Loadble Modules
This is a library for creating runtime loadable WPF UI elements.

# What is a Module
A module is a runtime loadble WPF UI.

# How to create one
1. Create a dll with your modules
   * A module needs to implement IModule and must have a valid visual element that can be rendered

# How to Load Modules at Runtime
1. create a module loader
   ```CSharp
   ModuleLoader loader = new ModuleLoader();
   loader.Load(Directory.GetFiles(PATH_TO_DIRECTORY_CONTAINING_MODULES,"*.dll"));
   ```

# How to Show Modules in UI
1. Create a Container to handle displaying the visual element of each module
  * A sample of this is the provided ModuleDisplay. This is essentially a StackPanel that renders each module's visual element. To re-render the modules visual componenets as you add/remove modules bind the ItemSource to an ObservableCollection of IModule


# How to Pass Data Between Modules and Running App
**You** (any developer implenting this library) are responsible for packaging the data to send between modules<br/>
Loading Data requires passing all parameters through LoadData(). A function which takes in an object[] of all data to pass through.
   ```CSharp
   module.LoadData(new object[]{YOUR_DATA_HERE});
   ```
Retrieving Data requires calling GetData(). A function which returns an object[] of all data to return to the running app
   ```CSharp
   object[] data = module.GetData();
   ```


# Notes:
* This currently is only supporting .NET core 8.0. More versions should be supported in the near future.
* The App loading the modules **MUST** refernce all the dlls the module references. 
