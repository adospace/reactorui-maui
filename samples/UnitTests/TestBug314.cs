using MauiReactor;
using MauiReactor.Internals;
using MauiReactor.Parameters;
using MauiReactor.TestApp.Pages;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTests;

public class TestBug324
{
    public interface ISomething
    {

    }
    public class Something : ISomething
    {

    }
    public partial class Page1 : Component
    {
        [Inject] private ISomething Something;
        /// <inheritdoc />
        public override VisualNode Render() => ContentPage("page1", Label("Page1"), ToolbarItem("go").AutomationId("go").OnClicked(async () => await Navigation.PushAsync<Page2>()));
    }
    public partial class Page2 : Component
    {
        [Inject] private ISomething Something;
        /// <inheritdoc />
        public override VisualNode Render() => ContentPage("page1", Label("Page1").AutomationId("label1"));
    }


    // Issue #314 https://github.com/adospace/reactorui-maui/issues/324
    [Test]
    public void RunRenderTestsInParallel_ExpectTestsToRunToCompletion()
    {
        Given_the_equipment_list_page_with_data();
        When_click_go();
        Then_the_page2_has_the_label();
    }

    [TearDown]
    public void EndTest()
    {
        serviceContext?.Dispose();
        navigationContainer?.Dispose();
    }

    private ServiceContext? serviceContext;

    private NavigationContainer? navigationContainer;
    private ITemplateHost page1;
    private ITemplateHost page2;

    private void Then_the_page2_has_the_label()
    {
        this.page2.Find<MauiControls.Label>("label1").Text.ShouldBe("Page1");
    }

    private void When_click_go()
    {
        var go = page1.Find<MauiControls.ToolbarItem>("go");
        ((MauiControls.IMenuItemController)go).Activate();
        page2 = navigationContainer.AttachTopPage();
    }

    private ISomething something;
    private void Given_the_equipment_list_page_with_data()
    {
        something = new Something();
        navigationContainer = new NavigationContainer();
        serviceContext = new ServiceContext(services =>
        {
            services.AddSingleton(something);
        });
        page1 = TemplateHost.Create(new Page1());
    }
}