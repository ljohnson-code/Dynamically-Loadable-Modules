using System.Windows;

namespace Dynamic_Modules;

public interface IModule
{
    public UIElement GetVisual();
    public void LoadData(object[] data);
    public object[] GetData();
}