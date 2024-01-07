using System.Windows;

namespace Dynamic_Modules;

public interface IModule
{
    /// <summary>
    /// Gets the visual element of the Module
    /// </summary>
    /// <returns></returns>
    public UIElement GetVisual();
    /// <summary>
    /// Method to handle loading data into the module.
    /// </summary>
    /// <param name="data"></param>
    public void LoadData(object[] data);
    /// <summary>
    /// Method to handle retrieving data from the module
    /// </summary>
    /// <returns></returns>
    public object[] GetData();
}