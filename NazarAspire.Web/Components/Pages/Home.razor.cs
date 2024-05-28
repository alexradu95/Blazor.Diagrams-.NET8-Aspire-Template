using Blazor.Diagrams;
using Microsoft.JSInterop;
using NazarAspire.Web.Components.Behaviors;
using Blazor.Diagrams.Core.Anchors;
using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using Blazor.Diagrams.Options;
using Blazor.Diagrams.Core.Behaviors;
using NazarAspire.Web.Components.Nodes;
using Blazor.Diagrams.Core.Models.Base;
using Microsoft.AspNetCore.Components;

namespace NazarAspire.Web.Components.Pages;

public partial class Home
{
    private BlazorDiagram Diagram { get; set; } = null!;

    [Inject] private IJSRuntime JSRuntime { get; set; } = null!;

    protected override void OnInitialized()
    {
        // Initialization logic here

        var options = new BlazorDiagramOptions
        {
            AllowMultiSelection = true,
            LinksLayerOrder = 1,
            NodesLayerOrder = 2,
        };


        Diagram = new BlazorDiagram(options);

        Diagram.RegisterComponent<AddTwoNumbersNode, AddTwoNumbersWidget>();

        var addNode = Diagram.Nodes.Add(new AddTwoNumbersNode(new Point(80, 80)));
        addNode.AddPort(PortAlignment.Top);
        addNode.AddPort(PortAlignment.Bottom);
        Diagram.RegisterComponent<SvgNode, SvgWidget>();

        var svgNode = Diagram.Nodes.Add(new SvgNode(new Point(200, 200)));
        svgNode.AddPort(PortAlignment.Left);
        svgNode.AddPort(PortAlignment.Right);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await JSRuntime.InvokeVoidAsync("console.log", "OnAfterRenderAsync called");
            Diagram.Links.Added += async (link) => await OnLinkAdded(link);
            Diagram.Links.Removed += async (link) => await OnLinkRemoved(link);
        }
    }

    private async Task OnLinkAdded(BaseLinkModel link)
    {
        await JSRuntime.InvokeVoidAsync("console.log", "Link added");

        if (link.Source is SinglePortAnchor && !link.IsAttached)
        {
            link.TargetChanged += (l, oldAnchor, newAnchor) => OnLinkTargetChanged(l);
        }
    }

    private async Task OnLinkRemoved(BaseLinkModel link)
    {
        await JSRuntime.InvokeVoidAsync("console.log", "Link removed");

        link.TargetChanged -= (l, oldAnchor, newAnchor) => OnLinkTargetChanged(l);
    }


    private async Task OnLinkTargetChanged(BaseLinkModel link)
    {
        await JSRuntime.InvokeVoidAsync("console.log", "Link target changed");
        Console.WriteLine("Link target changed");
        // Add your custom code here
    }

    private async Task ReplaceBehaviour()
    {
        var oldSelectionBehavior = Diagram.GetBehavior<Blazor.Diagrams.Core.Behaviors.SelectionBehavior>()!;
        Diagram.UnregisterBehavior<Blazor.Diagrams.Core.Behaviors.SelectionBehavior>();
        Diagram.RegisterBehavior(new MySelectionBehavior(Diagram));
    }

    private async Task DemonstrateOrdering()
    {
        var node1 = Diagram.Nodes.Add(new NodeModel(new Point(50, 50)) { Title = "Node 1" });
        var node2 = Diagram.Nodes.Add(new NodeModel(new Point(200, 100)) { Title = "Node 2" });

        Console.WriteLine($"Initial Order - Node 1: {node1.Order}, Node 2: {node2.Order}");

        node1.Order = 10;
        Console.WriteLine($"After setting Node 1 Order to 10 - Node 1: {node1.Order}, Node 2: {node2.Order}");

        Diagram.SendToFront(node2);
        Console.WriteLine($"After sending Node 2 to front - Node 1: {node1.Order}, Node 2: {node2.Order}");

        Diagram.SendToBack(node2);
        Console.WriteLine($"After sending Node 2 to back - Node 1: {node1.Order}, Node 2: {node2.Order}");

        Diagram.SuspendSorting = true;
        node1.Order = 100;
        node2.Order = 200;
        Diagram.SuspendSorting = false;
        Diagram.RefreshOrders();
        Console.WriteLine($"After suspending sorting and setting orders - Node 1: {node1.Order}, Node 2: {node2.Order}");
    }

    private async Task AddKeyboardShortcuts()
    {
        var ksb = Diagram.GetBehavior<KeyboardShortcutsBehavior>();
        ksb.SetShortcut("q", ctrl: false, shift: false, alt: false, async (diagram) => await SaveToMyServer(diagram));
    }

    private async Task SaveToMyServer(Blazor.Diagrams.Core.Diagram diagram)
    {
        Console.WriteLine("SaveToMyServer called");
        await Task.Delay(1000); // Simulate an async operation
    }
}
