using Blazor.Diagrams.Core.Models.Base;
using Blazor.Diagrams.Core.Events;
using Blazor.Diagrams.Core.Behaviors;

namespace NazarAspire.Web.Components.Behaviors
{
    public class MySelectionBehavior : Behavior
    {
        public MySelectionBehavior(Diagram diagram) : base(diagram)
        {
            Diagram.PointerDown += OnPointerDown;
        }

        private void OnPointerDown(Model? model, PointerEventArgs e)
        {
            if (model == null) // Canvas
            {
                Diagram.UnselectAll();
            }
            else if (model is SelectableModel sm)
            {
                Diagram.SelectModel(sm);
            }
        }

        public override void Dispose()
        {
            Diagram.PointerDown -= OnPointerDown;
        }
    }
}
