using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Windows;

namespace AppiumTests
{
    [TestFixture]
    public class CalculatorTests
    {
        private const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";
        private const string CalculatorAppId = "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App";
        private static WindowsElement calculatorResult;

        protected static WindowsDriver<WindowsElement>? session;

        [Test]
        public void AddTwoNumbers()
        {
            StartApp();

            session.FindElementByName("One").Click();
            session.FindElementByName("Plus").Click();
            session.FindElementByName("Seven").Click();
            session.FindElementByName("Equals").Click();
            Assert.AreEqual("8", GetCalculatorResultText());

            session.CloseApp();
            session.Quit();
            session = null;
        }


        public void StartApp()
        {
            if (session != null)
                return; 

            var capabilities = new AppiumOptions();
            // change this location based on where you cloned the git repo
            capabilities.AddAdditionalCapability(MobileCapabilityType.App, CalculatorAppId);
            capabilities.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Windows");
            capabilities.AddAdditionalCapability(MobileCapabilityType.DeviceName, "WindowsPC");

            var appiumLocalService = new AppiumServiceBuilder().UsingAnyFreePort().Build();
            appiumLocalService.Start();
            session = new WindowsDriver<WindowsElement>(appiumLocalService, capabilities);
        }

        private string GetCalculatorResultText()
        {
            calculatorResult = session.FindElementByAccessibilityId("CalculatorResults");
            return calculatorResult.Text.Replace("Display is", string.Empty).Trim();
        }
    }
}