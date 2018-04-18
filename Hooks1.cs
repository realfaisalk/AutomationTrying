using System;
using System.Globalization;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using CheckMyGoogle;

namespace CheckMyGoogle
{
    [Binding]
    public sealed class Hooks1
    {
        private static IWebDriver _driver;

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            CultureInfo.CurrentUICulture = new CultureInfo("en-GB", false);
        }

        [BeforeFeature]
        public static void BeforeFeature()
        {
            var fi = FeatureContext.Current.FeatureInfo;

            string testName = "Feature: " + fi.Title;

            Console.WriteLine(testName);
        }

        [BeforeScenario]
        public static void BeforeScenario()
        {
        }

        [BeforeStep]
        public static void BeforeStep()
        {
            //Get the Step Information
            var sc = ScenarioContext.Current.StepContext.StepInfo;
            string steptype, stepinfo;

            steptype = sc.StepDefinitionType.ToString();
            stepinfo = sc.Text;

            Console.WriteLine(steptype + " " + stepinfo); 
        }

        [AfterStep]
        public static void AfterStep()
        {
            string errMsg = "", errInnExc = "";

            try
            {
                var error = ScenarioContext.Current.TestError;

                if (error != null)
                {
                    errMsg = error.Message;

                    if (error.InnerException != null)
                    {
                        errInnExc = error.InnerException.Message;
                    }
                }

                Console.WriteLine("An error ocurred: " + errMsg + Environment.NewLine + errInnExc);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [AfterScenario]
        public static void AfterScenario()
        {
            //try
            //{
            //    _driver = GoogleHomepageSteps.driver;
            //    if (_driver != null)
            //    {
            //        _driver.Close();
            //        _driver.Quit();
            //    }

            //}
            //catch (Exception)
            //{
            //    _driver.Quit();
            //}
        }

        [AfterFeature]
        public static void AfterFeature()
        {
            //Console.WriteLine("AfterFeature");
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            //Console.WriteLine("AfterTestRun");
            try
            {
                _driver = GoogleHomepageSteps.driver;
                if (_driver != null)
                {
                    _driver.Close();
                    _driver.Quit(); 
                }
                
            }
            catch (Exception)
            {
            }

        }
    }//end of class
}//end of namespace
