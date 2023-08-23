using OpenQA.Selenium;

namespace CustomerTest.AcceptanceTests.Pages
{
    public class IndexPage
    {

        public IWebDriver Driver;

        //Ui Elements
        public IWebElement btnCreateBtn => Driver.FindElement(By.Id("CreateBtn"));
        public IWebElement btnEditBtn => Driver.FindElement(By.Id("EditBtn"));
        public IWebElement lblMessage => Driver.FindElement(By.Id("Message"));
        public IWebElement txtFirstName => Driver.FindElement(By.Id("FirstName"));
        public IWebElement txtLastname => Driver.FindElement(By.Id("LastName"));
        public IWebElement txtPhoneNumber => Driver.FindElement(By.Id("PhoneNumber"));
        public IWebElement txtEmail => Driver.FindElement(By.Id("Email"));
        public IWebElement txtDateOfBirth => Driver.FindElement(By.Id("DateOfBirth"));
        public IWebElement txtAddress => Driver.FindElement(By.Id("Address"));


        public IndexPage(IWebDriver driver)
        {
            Driver = driver;
        }

        public void FillForm(string FirstName, string LastName, string DateOfBirth, double PhoneNumber, string Email, string Address)
        {
            txtFirstName.SendKeys(FirstName);
            txtLastname.SendKeys(LastName);
            txtPhoneNumber.SendKeys(PhoneNumber.ToString());
            txtEmail.SendKeys(Email);
            txtDateOfBirth.SendKeys(DateOfBirth);
            txtAddress.SendKeys(Address);
        }

        public void ClickCreateBtn() => btnCreateBtn.Submit();
        public void ClickEditBtn() => btnEditBtn.Submit();

        public bool IsCreateMessageSuccess() => lblMessage.Text == "Customer Created Successfully!";
        public bool IsCreateMessageDuplicate() => lblMessage.Text == "Duplicate User Is Found";
        public bool IsCreateMessageInvalidEmail() => lblMessage.Text == "Email is not Valid.";
        public bool IsDeleteMessageSuccess() => lblMessage.Text == "Customer Deleted Successfully!";
        public bool IsEditMessageSuccess() => lblMessage.Text == "Customer Edited Successfully!";


    }
}
