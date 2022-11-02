using Microsoft.Maui.Controls;
using Microsoft.Maui.Layouts;

namespace Horizon.UniversalLayout.Layout;

/// <summary>
/// This is a custom layout that is used to display a list of items in horizontal orientation.
/// And respects the their alignment. Based on <see cref="HorizontalStackLayout"/>.
/// </summary>
[ContentProperty(nameof(Children))]
public class UniversalHorizontalStackLayout : StackBase
{
    protected override ILayoutManager CreateLayoutManager() => new UniversalHorizontalStackLayoutManager(this);
}