using Microsoft.Maui.Controls;
using Microsoft.Maui.Layouts;

namespace Horizon.UniversalLayout.Layout;

/// <summary>
/// This is a custom layout that is used to display a list of items in vertical orientation.
/// And respects the their alignment. Based on <see cref="VerticalStackLayout"/>.
/// </summary>
[ContentProperty(nameof(Children))]
public class UniversalVerticalStackLayout : StackBase
{
    protected override ILayoutManager CreateLayoutManager() => new UniversalVerticalStackLayoutManager(this);
}