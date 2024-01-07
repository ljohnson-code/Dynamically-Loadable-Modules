using System.Collections;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;

namespace Dynamic_Modules;

/// <summary>
/// A Simple control to display the visual content of the module.
/// </summary>
public class ModuleDisplay : StackPanel
{
    public IEnumerable ItemsSource
    {
        get => (IEnumerable)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    public static readonly DependencyProperty ItemsSourceProperty =
        DependencyProperty.Register(nameof(ItemsSource), typeof(IEnumerable), typeof(ModuleDisplay),
            new PropertyMetadata(OnItemsSourcePropertyChanged));

    private static void OnItemsSourcePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
        var control = sender as ModuleDisplay;
        control?.OnItemsSourceChanged((IEnumerable)e.OldValue, (IEnumerable)e.NewValue);
    }

    private void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
    {
        // Remove handler for oldValue.CollectionChanged

        if (oldValue is INotifyCollectionChanged oldValueINotifyCollectionChanged)
        {
            oldValueINotifyCollectionChanged.CollectionChanged -= newValueINotifyCollectionChanged_CollectionChanged;
        }

        // Add handler for newValue.CollectionChanged (if possible)d
        if (newValue is INotifyCollectionChanged newValueINotifyCollectionChanged)
        {
            newValueINotifyCollectionChanged.CollectionChanged += newValueINotifyCollectionChanged_CollectionChanged;
        }
    }

    void newValueINotifyCollectionChanged_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.NewItems != null)
        {
            foreach (var item in e.NewItems)
            {
                if (item is IModule module)
                    Children.Add(module.GetVisual());
            }
        }
    }
}