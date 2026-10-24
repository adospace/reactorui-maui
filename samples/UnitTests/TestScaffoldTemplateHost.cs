using System;
using System.Collections.Generic;
using MauiReactor;
using MauiReactor.Internals;
using MauiReactor.TestApp.Controls.Native;
using MauiReactor.TestApp.Pages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shouldly;

namespace UnitTests;

/// <summary>
/// Tests for scaffold-generated controls with <c>implementItemTemplate: true</c>
/// running in the headless test host (<see cref="TemplateHost"/>).
/// </summary>
public sealed class TestScaffoldTemplateHost
{
    [Test]
    public void ScaffoldWithItemTemplate_RendersWithoutErrors()
    {
        var (_, errors) = Render(new ScaffoldItemTemplatePage());

        errors.ShouldBeEmpty();
    }

    /// <summary>
    /// A search the native tree doesn't satisfy falls back to the visual tree, which calls
    /// <c>ForceItemsLoad()</c> on the control while its native control has no <c>ItemsSource</c>.
    /// <c>ForceItemsLoad()</c> must still initialize <c>_loadedForciblyChildren</c>, or
    /// <c>Descendants&lt;T&gt;()</c> throws.
    /// </summary>
    [Test]
    public void ScaffoldWithItemTemplate_FindOptionalReturnsNull_WhenControlHasNoItemsSource()
    {
        var (host, errors) = Render(new ScaffoldItemTemplateWithoutItemsSourcePage());

        errors.ShouldBeEmpty();
        host.FindOptional<MauiControls.Label>("MissingLabel").ShouldBeNull();
    }

    /// <summary>
    /// <see cref="TestItemsControl.SimulateHandlerDependency"/> makes the control throw when
    /// <c>ItemTemplate</c> is set, as handler-dependent controls like PanCardView.CardsView do
    /// in the headless host. <c>base.OnUpdate()</c> must still apply the queued properties, and
    /// the exception must propagate to the host, which logs it.
    /// </summary>
    [Test]
    public void ScaffoldWithItemTemplate_AppliesAutomationIdAndPropagatesException_WhenTemplateSetupThrows()
    {
        var (host, errors) = Render(new ScaffoldItemTemplateFailingPage());

        host.FindOptional<TestItemsControl>("TestItems").ShouldNotBeNull();
        errors
            .ShouldHaveSingleItem()
            .ShouldBeOfType<InvalidOperationException>()
            .Message.ShouldBe(TestItemsControl.SimulatedFailureMessage);
    }

    /// <summary>Renders the page in a host, collecting the errors the host logs while doing so.</summary>
    /// <param name="page">The page to render.</param>
    /// <returns>The host and the errors it logged.</returns>
    private static (ITemplateHost Host, IReadOnlyList<Exception?> Errors) Render(VisualNode page)
    {
        var logger = new RecordingLogger();
        using var serviceContext = new ServiceContext(services =>
            services.AddSingleton<ILogger<TemplateHost>>(logger)
        );

        return (TemplateHost.Create(page), logger.Errors);
    }

    private sealed class RecordingLogger : ILogger<TemplateHost>
    {
        private readonly List<Exception?> _errors = [];

        public IReadOnlyList<Exception?> Errors => _errors;

        public IDisposable? BeginScope<TState>(TState state)
            where TState : notnull => null;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception? exception,
            Func<TState, Exception?, string> formatter
        )
        {
            if (logLevel >= LogLevel.Error)
            {
                _errors.Add(exception);
            }
        }
    }
}
