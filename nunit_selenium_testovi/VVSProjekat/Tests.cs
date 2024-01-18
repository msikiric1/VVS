using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace VVSProjekat
{
    public class Tests
    {
        private IWebDriver driver;

        [SetUp]
        public void SetUp()
        {
            // Otvaranje browsera i navigiranje na stranicu
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://demoblaze.com");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
        }

        [Test]
        public void OdjavaKorisnikaTest()
        {
            try
            {
                Thread.Sleep(1000);
                IWebElement logInLink = driver.FindElement(By.Id("login2"));
                logInLink.Click();

                IWebElement usernameInput = driver.FindElement(By.Id("loginusername"));
                usernameInput.SendKeys("selenium_test1");

                IWebElement passwordInput = driver.FindElement(By.Id("loginpassword"));
                passwordInput.SendKeys("test");

                IWebElement logInButton = driver.FindElement(By.XPath("//*[@id=\"logInModal\"]/div/div/div[3]/button[2]"));
                logInButton.Click();
                Thread.Sleep(1000);
            }
            catch (Exception err)
            {
                // Korisnik je vec prijavljen
            }

            IWebElement logoutLink = driver.FindElement(By.Id("logout2"));
            logoutLink.Click();

            IWebElement logIn = driver.FindElement(By.Id("login2"));
            Assert.IsNotNull(logIn);
        }

        [Test]
        public void PretragaPoKategorijiIDodavanjeUShoppingCartTest()
        {
            IWebElement kategorijaLaptopi = driver.FindElement(By.XPath("//a[@onclick=\"byCat('notebook')\"]"));
            kategorijaLaptopi.Click();
            Thread.Sleep(1000);

            IWebElement laptop = driver.FindElement(By.XPath("//*[@id=\"tbodyid\"]/div[1]/div/div/h4/a"));
            laptop.Click();

            string laptopName = driver.FindElement(By.ClassName("name")).Text;

            IWebElement addToCart = driver.FindElement(By.CssSelector(".btn.btn-success.btn-lg"));
            addToCart.Click();
            Thread.Sleep(1000);

            IAlert alert = driver.SwitchTo().Alert();
            Assert.That(alert.Text, Is.EqualTo("Product added"));
            alert.Accept();

            IWebElement cartLink = driver.FindElement(By.Id("cartur"));
            cartLink.Click();

            IWebElement tableBody = driver.FindElement(By.Id("tbodyid"));
            var children = tableBody.FindElements(By.XPath($"//td[text()='{laptopName}']"));
            Assert.That(children.Count, Is.Not.EqualTo(0));
        }

        [TearDown]
        public void TearDown()
        {
            // Zatvaranje browsera
            driver.Quit();
        }
    }
}