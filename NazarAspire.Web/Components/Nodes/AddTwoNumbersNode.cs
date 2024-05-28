using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using NazarAspire.Web.Components.Widgets;

namespace NazarAspire.Web.Components.Nodes;

public class AddTwoNumbersNode : BaseWidget
{
    public AddTwoNumbersNode(Point? position = null) : base(position) { }

    public double FirstNumber { get; set; }
    public double SecondNumber { get; set; }

    public double Result => FirstNumber + SecondNumber;
}
