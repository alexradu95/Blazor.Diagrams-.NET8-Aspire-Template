using Blazor.Diagrams.Core.Models;
using Blazor.Diagrams.Core.Geometry;

using Blazor.Diagrams.Core.Models.Base;

namespace NazarAspire.Web.Components.Widgets
{
    public abstract class BaseWidget : NodeModel
    {
        protected BaseWidget(Point? position = null) : base(position) { }

        public event Action<BaseLinkModel>? OnLinkAdded;
        public event Action<BaseLinkModel>? OnLinkRemoved;

        public void RaiseOnLinkAdded(BaseLinkModel link)
        {
            OnLinkAdded?.Invoke(link);
        }

        public void RaiseOnLinkRemoved(BaseLinkModel link)
        {
            OnLinkRemoved?.Invoke(link);
        }
    }
}
