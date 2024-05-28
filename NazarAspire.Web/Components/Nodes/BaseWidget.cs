using Blazor.Diagrams.Core.Models;
using Blazor.Diagrams.Core.Geometry;

namespace NazarAspire.Web.Components.Nodes
{
    public abstract class BaseWidget : NodeModel
    {
        protected BaseWidget(Point? position = null) : base(position) { }

        // Add any common logic or properties for your widgets here
    }
}
