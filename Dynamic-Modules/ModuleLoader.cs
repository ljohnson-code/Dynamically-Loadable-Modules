using System.Reflection;
using System.Runtime.Loader;

namespace Dynamic_Modules;

public class ModuleLoader
{
    private List<IModule> _modules;

    public IModule[] Modules => _modules.ToArray();

    public ModuleLoader(List<IModule> modules)
    {
        _modules = modules;
    }

    public ModuleLoader() => _modules = new List<IModule>();


    /// <summary>
    /// Loads all modules in the provided directory
    /// </summary>
    /// <param name="directoryInfo">Directory info for the directory to load modules from</param>
    public void Load(DirectoryInfo directoryInfo)
    {
        foreach (var dll in directoryInfo.GetFiles("*.dll"))
        {
            AssemblyLoadContext context = new(dll.FullName);
            var assembly = context.LoadFromAssemblyPath(dll.FullName);
            var types = assembly.GetTypes();
            if (Activator.CreateInstance(types[0]) is IModule module) _modules.Add(module);
        }
    }

    /// <summary>
    /// Loads the provided module
    /// </summary>
    /// <param name="filePath">Path pointing to the module to load</param>
    public void Load(string filePath)
    {
        AssemblyLoadContext context = new(filePath);
        var assembly = context.LoadFromAssemblyPath(filePath);
        var types = assembly.GetTypes();
        if (Activator.CreateInstance(types[0]) is IModule module) _modules.Add(module);
    }

    /// <summary>
    /// Loads the provided modules
    /// </summary>
    /// <param name="paths">string[] containing the paths of each module to load</param>
    public void Load(string[] paths)
    {
        foreach (string path in paths)
        {
            AssemblyLoadContext context = new(path);
            var assembly = context.LoadFromAssemblyPath(path);
            var types = assembly.GetTypes();
            if (Activator.CreateInstance(types[0]) is IModule module) _modules.Add(module);
        }
    }
}