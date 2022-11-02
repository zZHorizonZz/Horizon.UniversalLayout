using Microsoft.Maui;
using Microsoft.Maui.Controls.Internals;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Layouts;
using StackLayoutManager = Microsoft.Maui.Layouts.StackLayoutManager;
using LayoutAlignment = Microsoft.Maui.Primitives.LayoutAlignment;

namespace Horizon.UniversalLayout.Layout;

/// <summary>
/// This is a custom layout manager that is used to display a list of items in horizontal orientation.
/// And respects the their alignment. Based on <see cref="HorizontalStackLayoutManager"/>.
/// </summary>
public class UniversalHorizontalStackLayoutManager : StackLayoutManager
{
    public UniversalHorizontalStackLayoutManager(IStackLayout layout) : base(layout)
    {
    }

    public override Size Measure(double widthConstraint, double heightConstraint)
    {
        var padding = Stack.Padding;

        var measuredWidth = 0d;
        var measuredHeight = 0d;
        var spacingCount = 0;

        foreach (var child in Stack)
        {
            if (child.Visibility == Visibility.Collapsed)
            {
                continue;
            }

            spacingCount += 1;
            var measure = child.Measure(double.PositiveInfinity, heightConstraint - padding.VerticalThickness);
            measuredWidth += measure.Width;
            measuredHeight = Math.Max(measuredHeight, measure.Height);
        }

        measuredWidth += MeasureSpacing(Stack.Spacing, spacingCount);
        measuredWidth += padding.HorizontalThickness;
        measuredHeight += padding.VerticalThickness;

        var finalHeight = ResolveConstraints(heightConstraint, Stack.Height, measuredHeight, Stack.MinimumHeight,
            Stack.MaximumHeight);
        var finalWidth = ResolveConstraints(widthConstraint, Stack.Width, measuredWidth, Stack.MinimumWidth,
            Stack.MaximumWidth);

        return new Size(finalWidth, finalHeight);
    }

    public override Size ArrangeChildren(Rect bounds)
    {
        var padding = Stack.Padding;
        var spacing = Stack.Spacing;
        var childCount = Stack.Count;

        var top = padding.Top + bounds.Top;

        var height = bounds.Height - padding.VerticalThickness;

        // Figure out where we're starting from 
        var startXPosition = padding.Left + bounds.Left;
        var centerXPosition = bounds.Right / 2 - CenteredWidth(Stack) / 2;
        var endXPosition = padding.Right + bounds.Right;

        for (var n = 0; n < Stack.Count; n++)
        {
            var child = Stack[n];

            if (child.Visibility == Visibility.Collapsed)
            {
                continue;
            }

            switch (child.HorizontalLayoutAlignment)
            {
                case LayoutAlignment.Fill:
                case LayoutAlignment.Start:
                {
                    startXPosition += ArrangeChild(child, height, top, startXPosition);
                    if (n < childCount - 1)
                    {
                        startXPosition += spacing;
                    }

                    break;
                }

                case LayoutAlignment.Center:
                {
                    centerXPosition += ArrangeChild(child, height, top, centerXPosition);
                    if (n < childCount - 1)
                    {
                        centerXPosition += spacing;
                    }

                    break;
                }

                case LayoutAlignment.End:
                {
                    endXPosition -= ArrangeChild(child, height, top, endXPosition - child.DesiredSize.Width);
                    if (n < childCount - 1)
                    {
                        endXPosition -= spacing;
                    }

                    break;
                }
            }
        }

        var actual = new Size(bounds.Width, height);
        return actual.AdjustForFill(bounds, Stack);
    }

    private static double ArrangeChild(IView child, double height, double top, double x)
    {
        var destination = new Rect(x, top, child.DesiredSize.Width, height);
        child.Arrange(destination);
        return destination.Width;
    }

    private static double CenteredWidth(IStackLayout stack)
    {
        var children = stack.Where(child =>
                child.Visibility != Visibility.Collapsed && child.HorizontalLayoutAlignment == LayoutAlignment.Center)
            .ToList();
        return children.Sum(c => c.DesiredSize.Width) + MeasureSpacing(stack.Spacing, children.Count);
    }
}