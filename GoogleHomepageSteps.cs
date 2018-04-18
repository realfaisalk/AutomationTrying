using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;
using NUnit.Framework;
using CheckMyGoogle.POM;

namespace CheckMyGoogle
{
    [Binding]
    public class GoogleHomepageSteps
    {
        public static IWebDriver driver;
        const string Url = "http://google.com/";
        private Homepage homePage;

        [Given(@"I navigate to homepage")]
        public void GivenINavigateToHomepage()
        {
            try
            {
                driver = new ChromeDriver();
                driver.Navigate().GoToUrl(Url);
                driver.Manage().Window.Maximize();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5); 
                driver.Manage().Timeouts().PageLoad = TimeSpan.FromMinutes(1);

                homePage = new Homepage(driver);
            }
            catch (Exception)
            {
                if (driver != null)
                {
                    driver.Quit();
                    Assert.Fail();
                }
            }
        }

        [Then(@"the Google logo is displayed")]
        public void ThenTheGoogleLogoIsDisplayed()
        {
            Assert.That(homePage.imgGoogle.Displayed);
        }

        [Then(@"the Search box is displayed")]
        public void ThenTheSearchBoxIsDisplayed()
        {
            Assert.That(homePage.txtSearch.Displayed);
        }

        [When(@"I enter (.*) in the Search Box")]
        public void WhenIEnterSpecFlowInTheSearchBox(string searchtext)
        {
            homePage.txtSearch.SendKeys(searchtext);
            homePage.txtSearch.SendKeys(Keys.Escape);
            Thread.Sleep(2000);
            homePage.btnSearch.Click();
        }

        [Then(@"Search Results are displayed")]
        public void ThenSearchResultsAreDisplayed()
        {
            Assert.That(homePage.divResults.Displayed);
        }


    }//end of class
}//end of namespace
