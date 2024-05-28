﻿using Blazor.Diagrams;
using NazarAspire.Web.Components.Behaviors;
using Blazor.Diagrams.Core.Anchors;
using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using Blazor.Diagrams.Core.PathGenerators;
using Blazor.Diagrams.Core.Routers;
using Blazor.Diagrams.Options;

namespace NazarAspire.Web.Components.Pages;

public partial class Home
{
    private BlazorDiagram Diagram { get; set; } = null!;

    protected override void OnInitialized()
    {
        var options = new BlazorDiagramOptions
        {
            AllowMultiSelection = true,
            Zoom =
            {
                Enabled = false,
            },
            Links =
            {
                DefaultRouter = new NormalRouter(),
                DefaultPathGenerator = new SmoothPathGenerator()
            },
        };

        Diagram = new BlazorDiagram(options);

        var firstNode = Diagram.Nodes.Add(new NodeModel(position: new Point(50, 50))
        {
            Title = "Node 1"
        });
        var secondNode = Diagram.Nodes.Add(new NodeModel(position: new Point(200, 100))
        {
            Title = "Node 2"
        });
        var leftPort = secondNode.AddPort(PortAlignment.Left);
        var rightPort = secondNode.AddPort(PortAlignment.Right);

        // The connection point will be the intersection of
        // a line going from the target to the center of the source
        var sourceAnchor = new ShapeIntersectionAnchor(firstNode);
        // The connection point will be the port's position
        var targetAnchor = new SinglePortAnchor(leftPort);
        var link = Diagram.Links.Add(new LinkModel(sourceAnchor, targetAnchor));

        //// Replace the existing selection behavior with the custom one
        //var oldSelectionBehavior = Diagram.GetBehavior<Blazor.Diagrams.Core.Behaviors.SelectionBehavior>()!;
        //Diagram.UnregisterBehavior<Blazor.Diagrams.Core.Behaviors.SelectionBehavior>();
        //Diagram.RegisterBehavior(new MySelectionBehavior(Diagram));
        DemonstrateOrdering();
    }

    private void DemonstrateOrdering()
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
}
