using System.Net;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Seleniumests_orlovas;

public class SeleniumTestForPractice 
{
    [Test]
    public void Autorization()
    {
        var options = new ChromeOptions();
        options.AddArguments("--no-sandbox", "--start-maxinized", "--disable-extensions");
        //-зайти в хром (с помощью вебдрайера)
        var driver = new ChromeDriver(options);
        //-перейти по урлу https://staff-testing.testkontur.ru
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru"); 
        Thread.Sleep(5000);     
        //-ввести логин и пароль
        var login = driver.FindElement(By.Id("Username"));
        login.SendKeys("sofya88888@list.ru");
        var password = driver.FindElement(By.Name("Password"));
        password.SendKeys("Keshunya.112");
        Thread.Sleep(5000);
//-нажать на кнопку "войти"
        var enter = driver.FindElement(By.Name("button"));
        enter.Click();
        Thread.Sleep(5000);
        //-проверем что мы находимся на нужной странице
        var currentUrl = driver.Url;
        Assert.That(currentUrl == "https://staff-testing.testkontur.ru/news");
        //-закрыть браузер и убить процесс драйвера
        driver.Quit();
    }
}