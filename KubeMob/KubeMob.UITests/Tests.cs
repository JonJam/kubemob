using NUnit.Framework;
using Xamarin.UITest;

namespace KubeMob.UITests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        private readonly Platform platform;
        private IApp app;

        public Tests(Platform platform) => this.platform = platform;

        [SetUp]
        public void BeforeEachTest() => this.app = AppInitializer.StartApp(this.platform);

        [Test]
        public void AppLaunches() => this.app.Screenshot("First screen.");
    }
}