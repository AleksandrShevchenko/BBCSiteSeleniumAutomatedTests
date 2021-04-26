using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;
using System.Collections.Generic;
using OpenQA.Selenium.Support.UI;
using System;
using SeleniumExtras.WaitHelpers;

namespace TestProject2
{
    public class Tests
    {
        IWebDriver driver;
        readonly By _newsButton = By.XPath("//li[@class='orb-nav-newsdotcom']");
        readonly By _mainHeadlineName = By.XPath("//h3[@class='gs-c-promo-heading__title gel-paragon-bold nw-o-link-split__text']");
        string expectedHeadline = "Desperation as Indian hospitals buckle under Covid";
        readonly By _secondaryTitles = By.XPath("//h3[@class='gs-c-promo-heading__title gel-pica-bold nw-o-link-split__text']");
        readonly List<string> _secondaryArticleTiles = new List<string>()
        {         "Japan imposes new Covid measures as Olympics loom",
                  "More than 100 injured in East Jerusalem clashes",
                  "US joins race to find stricken Indonesia submarine",
                  "Gymnast's outfit takes on sexualisation in sport",
                  "Lift-off: SpaceX Dragon crew head to space station",
                  "Malaria vaccine hailed as potential breakthrough"
        };
        readonly By _CategotyOfMain = By.XPath("//a[@class = 'gs-c-section-link gs-c-section-link--truncate nw-c-section-link nw-o-link nw-o-link--no-visited-state']");
        readonly By _SearchBarInput = By.XPath("//input[@id='orb-search-q']");
        readonly By _searchButton = By.XPath("//button[@id='orb-search-button']");
        readonly By _searchResultFirstArticle = By.XPath("//p[@class='ssrcss-6k9l0l-PromoHeadline e1f5wbog1']");
        readonly By _coronavirusButton = By.XPath("//a[@href='/news/coronavirus']");
        readonly By _yourCoronavirusStoriesButton = By.XPath("//li[@class='gs-o-list-ui__item--flush gel-long-primer gs-u-display-block gs-u-float-left nw-c-nav__secondary-menuitem-container']");
        readonly By _sendUsYourCovidStoriesLink = By.XPath("//a[@href='/news/52143212']");

        #region CoronavirusForm
        readonly By _coronaFormNameInputField = By.XPath("//input[@aria-label='Name']");
        readonly By _coronaFormEmailInputField = By.XPath("//input[@aria-label='Email address']");
        readonly By _coronaFormAgeInputField = By.XPath("//input[@aria-label='Age']");
        readonly By _coronaFormPostcodeInputField = By.XPath("//input[@aria-label='Postcode']");
        readonly By _coronaFormPhoneNumInputField = By.XPath("//input[@aria-label='Telephone number']");
        readonly By _coronaFormAgeCheckbox = By.XPath("//div[p='I am over 16 years old']");
        readonly By _coronaFormTermsOfServiceCheckbox = By.XPath("//p[text()='I accept the ']");
        readonly By _coronaFormSubmitButton = By.XPath("//button[@class = 'button']");
        readonly By _coronaFormErrorInputMessage = By.XPath("//div[@class='input-error-message']");
        readonly By _coronaFormErrorCheckBoxMessage = By.XPath("//div[@class='input-error-message']");
        readonly By _coronaFormTextInputField = By.XPath("//textarea[@aria-label='Let us know your question.']");
        string coronaFormName = "SomeName";
        string coronaFormEmail = "someemail@somemailserver.somedomain";
        string coronaFormAge = "25";
        string coronaFormPostCode = "2525";
        string coronaFormPhoneNumber = "380353533535";
        string expectedErrorInputFieldText = "can't be blank";
        string coronaFormText = "SomeText";
        string expectedErrorCheckboxText = "must be accepted";
        Dictionary<By, string> CoronavirusFormElements = new Dictionary<By, string>();
        #endregion


        void CoronavirusForm_FillInputFields(By element, string elementText)
        {
            CoronavirusFormElements.Remove(element);
            driver.FindElement(_newsButton).Click();
            driver.FindElement(_coronavirusButton).Click();
            driver.FindElement(_yourCoronavirusStoriesButton).Click();
            driver.FindElement(_sendUsYourCovidStoriesLink).Click();
            foreach (var inputField in CoronavirusFormElements)
                    driver.FindElement(inputField.Key).SendKeys(inputField.Value);
            driver.FindElement(_coronaFormAgeCheckbox).Click();
            driver.FindElement(_coronaFormTermsOfServiceCheckbox).Click();
            driver.FindElement(_coronaFormSubmitButton).Click();
            CoronavirusFormElements.Add(element, elementText);
            new WebDriverWait(driver, TimeSpan.FromSeconds(1)).Until(ExpectedConditions.ElementIsVisible(_coronaFormErrorInputMessage));
            var error = driver.FindElement(_coronaFormErrorInputMessage).Text;
            Assert.IsTrue(error.Contains(expectedErrorInputFieldText) && (_coronaFormNameInputField != null || _coronaFormEmailInputField != null));
        }
        void CoronavirusForm_CheckboxCheck(By element)
        {
            driver.FindElement(_newsButton).Click();
            driver.FindElement(_coronavirusButton).Click();
            driver.FindElement(_yourCoronavirusStoriesButton).Click();
            driver.FindElement(_sendUsYourCovidStoriesLink).Click();
            foreach (var inputField in CoronavirusFormElements)
                    driver.FindElement(inputField.Key).SendKeys(inputField.Value);
            if (element != _coronaFormAgeCheckbox)
                driver.FindElement(_coronaFormAgeCheckbox).Click();
            if (element != _coronaFormTermsOfServiceCheckbox)
                driver.FindElement(_coronaFormTermsOfServiceCheckbox).Click();
            driver.FindElement(_coronaFormSubmitButton).Click();
            new WebDriverWait(driver, TimeSpan.FromSeconds(1)).Until(ExpectedConditions.ElementIsVisible(_coronaFormErrorCheckBoxMessage));
            var error = driver.FindElement(_coronaFormErrorCheckBoxMessage).Text;
            Assert.IsTrue(error.Contains(expectedErrorCheckboxText));
        }

        void CoronavirusForm_CheckInput_InvalidData(By element, string data)
        {
            string elementOriginalData;
            CoronavirusFormElements.TryGetValue(element, out elementOriginalData);
            CoronavirusFormElements.Remove(element);
            CoronavirusFormElements.Add(element, data);
            driver.FindElement(_newsButton).Click();
            driver.FindElement(_coronavirusButton).Click();
            driver.FindElement(_yourCoronavirusStoriesButton).Click();
            driver.FindElement(_sendUsYourCovidStoriesLink).Click();
            foreach (var inputField in CoronavirusFormElements)
                    driver.FindElement(inputField.Key).SendKeys(inputField.Value);
            driver.FindElement(_coronaFormAgeCheckbox).Click();
            driver.FindElement(_coronaFormTermsOfServiceCheckbox).Click();
            driver.FindElement(_coronaFormSubmitButton).Click();
            CoronavirusFormElements.Remove(element);
            CoronavirusFormElements.Add(element, elementOriginalData);
            new WebDriverWait(driver, TimeSpan.FromSeconds(1)).Until(ExpectedConditions.ElementIsVisible(_coronaFormErrorCheckBoxMessage));
            var error = driver.FindElement(_coronaFormErrorInputMessage).Text;
            Assert.IsTrue(error.Contains(expectedErrorInputFieldText) && (_coronaFormNameInputField != null || _coronaFormEmailInputField != null));
        }


        [SetUp]
        public void Setup()
        {
            driver = new OpenQA.Selenium.Chrome.ChromeDriver();
            driver.Navigate().GoToUrl("https://www.bbc.com/");
            if (CoronavirusFormElements.Count == 0)
            {
                CoronavirusFormElements.Add(_coronaFormNameInputField, coronaFormName);
                CoronavirusFormElements.Add(_coronaFormEmailInputField, coronaFormEmail);
                CoronavirusFormElements.Add(_coronaFormTextInputField, coronaFormText);
                CoronavirusFormElements.Add(_coronaFormPostcodeInputField, coronaFormPostCode);
                CoronavirusFormElements.Add(_coronaFormPhoneNumInputField, coronaFormPhoneNumber);
                CoronavirusFormElements.Add(_coronaFormAgeInputField, coronaFormAge);
            }
        }

        [Test]
        public void BBCNewsHeadlineArticleCheckTest()
        {
            driver.FindElement(_newsButton).Click();
            var actualHeadline = driver.FindElement(_mainHeadlineName).Text;
            Assert.AreEqual(expectedHeadline, actualHeadline, "Header is wrong");
        }
        [Test]
        public void BBCNewsCheckAllSecondaryArticles()
        {

            driver.FindElement(_newsButton).Click();
            int expectedAmount = _secondaryArticleTiles.Count;
            int actualAmount = 0;
            var elements = driver.FindElements(_secondaryTitles);
            for (int i = 0; i < elements.Count; i++)
                for (int j = 0; j < _secondaryArticleTiles.Count; j++)
                    if (elements[i].Text==_secondaryArticleTiles[j])
                        actualAmount++;
            Assert.AreEqual(expectedAmount, actualAmount, "Headers doesnt exist" + actualAmount + ", " + expectedAmount);
        }
        [Test]
        public void CategoriesSearchArticleTest()
        {
            string expectedHeadline = "Asia weather forecast";
            driver.FindElement(_newsButton).Click();
            string mainHeadlineCategoryText = driver.FindElement(_mainHeadlineName).FindElement(_CategotyOfMain).Text;
            driver.FindElement(_SearchBarInput).SendKeys(mainHeadlineCategoryText);
            driver.FindElement(_searchButton).Click();
            var titles = driver.FindElements(_searchResultFirstArticle);
            string actualFirstHeadline = titles[0].Text;
            Assert.AreEqual(expectedHeadline, actualFirstHeadline, "First title is wrong");
        }
        [Test]
        public void CoronavirusFormEmptyTextFieldTest()
        {
            CoronavirusForm_FillInputFields(_coronaFormTextInputField, coronaFormText);
        }

        [Test]
        public void CoronavirusFormEmptyNameFieldTest()
        {
            CoronavirusForm_FillInputFields(_coronaFormNameInputField, coronaFormName);
        }
        
        [Test]
        public void CoronavirusFormEmptyEmailFieldTest()
        {
            CoronavirusForm_FillInputFields(_coronaFormEmailInputField, coronaFormEmail);
        }
        [Test]
        public void CoronavirusFormEmptyPhoneNumberFieldTest()
        {
            CoronavirusForm_FillInputFields(_coronaFormPhoneNumInputField, coronaFormPhoneNumber);
        }
        [Test]
        public void CoronavirusFormInactiveAgeCheckboxFieldTest()
        {
            CoronavirusForm_CheckboxCheck(_coronaFormAgeCheckbox);
        }
        [Test]
        public void CoronavirusFormInactiveTermsOfServiceCheckboxFieldTest()
        {
            CoronavirusForm_CheckboxCheck(_coronaFormTermsOfServiceCheckbox);
        }
        [Test]
        public void CoronavirusFormEmailFieldInvalidDataTest()
        {
            CoronavirusForm_CheckInput_InvalidData(_coronaFormEmailInputField, "--");
        }

        [TearDown]
        public void TearDown()
        {
           driver.Quit();
        }
    }
}
