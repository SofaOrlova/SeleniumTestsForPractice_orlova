using System.Net;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V122.Security;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Seleniumests_orlovas;

public class SeleniumTestForPractice
{
    public ChromeDriver driver;
    [SetUp]
    public void Setup()
    {var options = new ChromeOptions();
        options.AddArguments("--no-sandbox", "--start-maxinized", "--disable-extensions");
        driver = new ChromeDriver(options);
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
        Autorization();
    }

    [Test]
    public void Authorization()
    {
        var news = driver.FindElement(By.CssSelector("[data-tid='Title']"));
        var currentUrl = driver.Url;
        currentUrl.Should().Be("https://staff-testing.testkontur.ru/news");
    }


    [Test]
    public void NavigationTest()
    { 
        var sideMenu = driver.FindElement(By.CssSelector("[data-tid='SidebarMenuButton']"));
        sideMenu.Click();
        var community = driver.FindElements(By.CssSelector("[data-tid='Community']")).First(element => element.Displayed);
        community.Click();
        var currentUrl = driver.Url;
        currentUrl.Should().Be("https://staff-testing.testkontur.ru/communities");        
    }

    [Test]
    public void NewEventValidation()
    {
       driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/events");
       var create = driver.FindElement(By.CssSelector("[data-tid='AddButton']")); 
       create.Click();
       var name = driver.FindElement(By.ClassName("react-ui-seuwan"));
       name.SendKeys("Для ДЗ Орлова");       
       var inn = driver.FindElement(By.XPath("//input[@placeholder='Введите ИНН']"));
       inn.Click();
       inn.SendKeys("000000000000");    
        var butcreate = driver.FindElement(By.CssSelector("[data-tid='CreateButton']"));
        butcreate.Click();
        var message = driver.FindElement(By.XPath("//div[@data-tid='validationMessage' and text()='Поле обязательно для заполнения.']")); 
        //Проверка была бы красивее, если бы можно было зацепиться не за текст
        message.Text.Should().Be("Поле обязательно для заполнения.");       

    }

    [Test]
    public void NewCommunity()
    {
      driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/communities");  
      var createbutton = driver.FindElement(By.XPath("//button[text()='СОЗДАТЬ']"));
      createbutton.Click();
      var name = driver.FindElement(By.CssSelector("[data-tid='Name']"));
      name.SendKeys("Для ДЗ ОрловаСА");
      var description = driver.FindElement(By.CssSelector("[data-tid='Message']"));
      description.SendKeys("Тест");
      var create = driver.FindElement(By.CssSelector("[data-tid='CreateButton']"));
      create.Click();
      var closebutton = driver.FindElement(By.XPath("//button[text()='Закрыть']"));
      closebutton.Click();
      var title = driver.FindElement(By.CssSelector("[data-tid='Title']"));
      var DescriptionInCommuniry = driver.FindElement(By.CssSelector("[data-tid='Description']"));      
      title.Text.Should().Be("Для ДЗ ОрловаСА");
      DescriptionInCommuniry.Text.Should().Be("Тест");

          }

        [Test]
        public void Moderation()
        {
            driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/communities/bd79acc6-e163-41c4-8c09-58ec14c9295d");
            var message = driver.FindElement(By.CssSelector("[data-tid='AddButton']"));
            message.Click();
            var textinput = driver.FindElement(By.XPath("//div[@data-contents]"));
            textinput.SendKeys("Какой хороший день!");
            var sendbutton = driver.FindElement(By.CssSelector("[data-tid='SendButton']"));
            sendbutton.Click();      
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));      
            wait.Until(ExpectedConditions.UrlToBe("https://staff-testing.testkontur.ru/communities/bd79acc6-e163-41c4-8c09-58ec14c9295d?tab=moderation"));
             var Name = driver.FindElement(By.XPath("//*[contains(@class,'sc-kLojOw sc-hndLF dkRFpn iWOYzY')]")).Text;
             Name.Should().Contain("Модерация");

        } 

    public void Autorization()
    {
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru");
        var login = driver.FindElement(By.Id("Username"));
        login.SendKeys("sofya88888@list.ru");
        var password = driver.FindElement(By.Name("Password"));
        password.SendKeys("Keshunya.112");
        var enter = driver.FindElement(By.Name("button"));
        enter.Click();
        var communityTitle = driver.FindElement(By.CssSelector("[data-tid='Title']"));
        communityTitle.Should().NotBeNull();
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
    }
}