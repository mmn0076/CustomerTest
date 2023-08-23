using CustomerTest.AcceptanceTests.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow.Assist;

namespace CustomerTest.AcceptanceTests.Steps
{
    [Binding]
    public class CustomerManagerStepDefinitions
    {
        public IndexPage page;
        public IWebDriver driver;

        //Setup
        [BeforeScenario]
        public void BeforeScenario()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://localhost:5091");
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            page = new IndexPage(driver);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            driver.Quit();
        }


        //Create Customer Happy flow
        [Given(@"site is loaded and i enter the following details")]
        public void GivenSiteIsLoadedAndIEnterTheFollowingDetails(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            page.FillForm((string)data.FirstName, (string)data.LastName, (string)data.DateOfBirth,
                (double)data.PhoneNumber, (string)data.Email, (string)data.Address);
        }

        [Given(@"i prees create")]
        public void GivenIPreesCreate()
        {
            page.ClickCreateBtn();
        }


        [Then(@"i should see customer created message")]
        public void ThenIShouldSeeCustomerCreatedMessage()
        {
            Assert.True(page.IsCreateMessageSuccess());
        }

        //Create Duplicate Customer
        [Given(@"site is loaded and i enter the following Duplicate details")]
        public void GivenSiteIsLoadedAndIEnterTheFollowingDuplicateDetails(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            page.FillForm((string)data.FirstName, (string)data.LastName, (string)data.DateOfBirth,
                (double)data.PhoneNumber, (string)data.Email, (string)data.Address);
        }

        [Then(@"i should see Duplicate User Is Found message")]
        public void ThenIShouldSeeDuplicateUserIsFoundMessage()
        {
            Assert.True(page.IsCreateMessageDuplicate());
        }

        //Create Invalid Email Customer
        [Given(@"site is loaded and i enter the following details with wrong email")]
        public void GivenSiteIsLoadedAndIEnterTheFollowingDetailsWithWrongEmail(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            page.FillForm((string)data.FirstName, (string)data.LastName, (string)data.DateOfBirth,
                (double)data.PhoneNumber, (string)data.Email, (string)data.Address);
        }

        [Then(@"i should see Email is not Valid\. message")]
        public void ThenIShouldSeeEmailIsNotValid_Message()
        {
            Assert.True(page.IsCreateMessageInvalidEmail());
        }



        //Get Single Customers List Happy flow
        [Given(@"site is loaded")]
        public void GivenSiteIsLoaded()
        {
            Assert.IsNotNull(page);
        }

        [Then(@"i should see customer list in tableView")]
        public void ThenIShouldSeeCustomerListInTableView()
        {
            var tableRows = driver.FindElements(By.Id("tablerows"));

            Assert.True(tableRows.Count > 0);
        }


        //Get Single Customer Happy flow
        [Given(@"site is loaded and i press on a single customer whit name ""([^""]*)""")]
        public void GivenSiteIsLoadedAndIPressOnASingleCustomerWhitName(string p0)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var tableRows = driver.FindElement(By.Name(p0));
            tableRows.Click();
        }


        [Then(@"i should see customer details whit firstname ""([^""]*)"" in text input")]
        public void ThenIShouldSeeCustomerDetailsWhitFirstnameInTextInput(string p0)
        {
            Assert.AreEqual(p0, page.txtFirstName.GetAttribute("value"));
        }


        //Edit Customer Happy flow
        [Given(@"site is loaded and i press on a customer with name if ""([^""]*)"" in table to view")]
        public void GivenSiteIsLoadedAndIPressOnACustomerWithNameIfInTableToView(string p0)
        {
            var tableRows = driver.FindElement(By.Name(p0));
            tableRows.Click();
        }



        [Given(@"i change customer firstname to ""([^""]*)"" and press Edit btn")]
        public void GivenIChangeCustomerFirstnameToAndPressEditBtn(string firstNameEdited)
        {
            page.txtFirstName.Clear();
            page.txtFirstName.SendKeys("firstNameEdited");
            page.ClickEditBtn();
        }


        [Then(@"i should see customer edited message")]
        public void ThenIShouldSeeCustomerEditedMessage()
        {
            Assert.True(page.IsEditMessageSuccess());
        }


        //Delete Customer Happy flow
        [Given(@"site is loaded and i press delete customer button for customer with email ""([^""]*)""")]
        public void GivenSiteIsLoadedAndIPressDeleteCustomerButtonForCustomerWithEmail(string p0)
        {
            var btnid = $"{p0}+delete";
            var btn = driver.FindElement(By.Id(btnid));
            btn.Submit();
        }

        [Then(@"i should see True in result")]
        public void ThenIShouldSeeTrueInResult()
        {
            Assert.True(page.IsDeleteMessageSuccess());
        }
    }
}