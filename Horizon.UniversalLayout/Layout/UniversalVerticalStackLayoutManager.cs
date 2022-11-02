using Microsoft.Maui;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Layouts;
using StackLayoutManager = Microsoft.Maui.Layouts.StackLayoutManager;
using LayoutAlignment = Microsoft.Maui.Primitives.LayoutAlignment;

namespace Horizon.UniversalLayout.Layout;

/// <summary>
/// This is a custom layout manager that is used to display a list of items in vertical orientation.
/// And respects the their alignment. Based on <see cref="VerticalStackLayoutManager"/>.
/// </summary>
public class UniversalVerticalStackLayoutManager : StackLayoutManager
{
    public UniversalVerticalStackLayoutManager(IStackLayout layout) : base(layout)
    {
    }

    public override Size Measure(double widthConstraint, double heightConstraint)
    {
        var padding = Stack.Padding;

        var measuredHeight = 0d;
        var measuredWidth = 0d;
        var childWidthConstraint = widthConstraint - padding.HorizontalThickness;
        var spacingCount = 0;

        foreach (var child in Stack)
        {
            if (child.Visibility == Visibility.Collapsed)
            {
                continue;
            }

            spacingCount += 1;
            var measure = child.Measure(childWidthConstraint, double.PositiveInfinity);
            measuredHeight += measure.Height;
            measuredWidth = Math.Max(measuredWidth, measure.Width);
        }

        measuredHeight += MeasureSpacing(Stack.Spacing, spacingCount);
        measuredHeight += padding.VerticalThickness;
        measuredWidth += padding.HorizontalThickness;

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

        var left = padding.Left + bounds.Left;

        var width = bounds.Width - padding.HorizontalThickness;

        // Figure out where we're starting from 
        var startYPosition = padding.Top + bounds.Top;
        var centerYPosition = bounds.Bottom / 2 - CenteredHeight(Stack) / 2;
        var endYPosition = padding.Bottom + bounds.Bottom;

        for (var n = 0; n < Stack.Count; n++)
        {
            var child = Stack[n];

            if (child.Visibility == Visibility.Collapsed)
            {
                continue;
            }

            switch (child.VerticalLayoutAlignment)
            {
                case LayoutAlignment.Fill:
                case LayoutAlignment.Start:
                {
                    startYPosition += ArrangeChild(child, width, left, startYPosition);
                    if (n < childCount - 1)
                    {
                        startYPosition += spacing;
                    }

                    break;
                }

                case LayoutAlignment.Center:
                {
                    centerYPosition += ArrangeChild(child, width, left, centerYPosition);
                    if (n < childCount - 1)
                    {
                        centerYPosition += spacing;
                    }

                    break;
                }

                case LayoutAlignment.End:
                {
                    endYPosition -= ArrangeChild(child, width, left, endYPosition - child.DesiredSize.Height);
                    if (n < childCount - 1)
                    {
                        endYPosition -= spacing;
                    }

                    break;
                }
            }
        }

        var actual = new Size(width, bounds.Height);
        return actual.AdjustForFill(bounds, Stack);
    }

    private static double ArrangeChild(IView child, double width, double left, double y)
    {
        var destination = new Rect(left, y, width, child.DesiredSize.Height);
        child.Arrange(destination);
        return destination.Height;
    }

    private static double CenteredHeight(IStackLayout stack)
    {
        var children = stack.Where(child =>
                child.Visibility != Visibility.Collapsed && child.VerticalLayoutAlignment == LayoutAlignment.Center)
            .ToList();
        return children.Sum(c => c.DesiredSize.Height) + MeasureSpacing(stack.Spacing, children.Count);
    }
}