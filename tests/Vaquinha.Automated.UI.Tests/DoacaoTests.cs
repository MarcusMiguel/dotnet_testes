using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using Vaquinha.Tests.Common.Fixtures;
using Xunit;

namespace Vaquinha.AutomatedUITests
{
    public class DoacaoTests : IDisposable, IClassFixture<DoacaoFixture>,
                                               IClassFixture<EnderecoFixture>,
                                               IClassFixture<CartaoCreditoFixture>
    {
        private DriverFactory _driverFactory = new DriverFactory();
        private IWebDriver _driver;
        private readonly DoacaoFixture _doacaoFixture;
        private readonly EnderecoFixture _enderecoFixture;
        private readonly CartaoCreditoFixture _cartaoCreditoFixture;

        public DoacaoTests(DoacaoFixture doacaoFixture, EnderecoFixture enderecoFixture, CartaoCreditoFixture cartaoCreditoFixture)
        {
            _doacaoFixture = doacaoFixture;
            _enderecoFixture = enderecoFixture;
            _cartaoCreditoFixture = cartaoCreditoFixture;
        }
        public void Dispose()
        {
            _driverFactory.Close();
        }

        [Fact]
        public void DoacaoUI_AcessoTelaHome()
        {
            // Arrange
            _driverFactory.NavigateToUrl("https://localhost:5001/");
            _driver = _driverFactory.GetWebDriver();

            // Act
            IWebElement webElement = null;
            webElement = _driver.FindElement(By.ClassName("vaquinha-logo"));

            // Assert
            webElement.Displayed.Should().BeTrue(because: "logo exibido");
        }
        [Fact]
        public void DoacaoUI_CriacaoDoacao()
        {
            //Arrange
            var doacao = _doacaoFixture.DoacaoValida();
            doacao.AdicionarEnderecoCobranca(_enderecoFixture.EnderecoValido());
            doacao.AdicionarFormaPagamento(_cartaoCreditoFixture.CartaoCreditoValido());
            _driverFactory.NavigateToUrl("https://localhost:5001/");
            _driver = _driverFactory.GetWebDriver();

            //Act
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(_driver => _driver.FindElement(By.ClassName("btn-yellow")).Displayed);

            IWebElement webElement = null;
            webElement = _driver.FindElement(By.ClassName("btn-yellow"));
            webElement.Click();

            webElement = _driver.FindElement(By.Id("valor"));
            webElement.Clear();
            webElement.SendKeys(doacao.Valor.ToString());

            webElement = _driver.FindElement(By.Id("DadosPessoais_Nome"));
            webElement.Clear();
            webElement.SendKeys(doacao.DadosPessoais.Nome);

            webElement = _driver.FindElement(By.Id("DadosPessoais_Email"));
            webElement.Clear();
            webElement.SendKeys(doacao.DadosPessoais.Email);

            webElement = _driver.FindElement(By.Id("DadosPessoais_MensagemApoio"));
            webElement.Clear();
            webElement.SendKeys(doacao.DadosPessoais.MensagemApoio);

            webElement = _driver.FindElement(By.Id("EnderecoCobranca_TextoEndereco"));
            webElement.Clear();
            webElement.SendKeys(doacao.EnderecoCobranca.TextoEndereco);

            webElement = _driver.FindElement(By.Id("EnderecoCobranca_Numero"));
            webElement.Clear();
            webElement.SendKeys(doacao.EnderecoCobranca.Numero);

            webElement = _driver.FindElement(By.Id("EnderecoCobranca_Cidade"));
            webElement.Clear();
            webElement.SendKeys(doacao.EnderecoCobranca.Cidade);

            webElement = _driver.FindElement(By.Id("cep"));
            webElement.Clear();
            webElement.SendKeys(doacao.EnderecoCobranca.CEP);

            webElement = _driver.FindElement(By.Id("estado"));
            webElement.Clear();
            webElement.SendKeys(doacao.EnderecoCobranca.Estado);

            webElement = _driver.FindElement(By.Id("EnderecoCobranca_Complemento"));
            webElement.Clear();
            webElement.SendKeys(doacao.EnderecoCobranca.Complemento);

            webElement = _driver.FindElement(By.Id("telefone"));
            webElement.Clear();
            webElement.SendKeys(doacao.EnderecoCobranca.Telefone);

            webElement = _driver.FindElement(By.Id("FormaPagamento_NomeTitular"));
            webElement.Clear();
            webElement.SendKeys(doacao.FormaPagamento.NomeTitular);

            webElement = _driver.FindElement(By.Id("cardNumber"));
            webElement.Clear();
            webElement.SendKeys(doacao.FormaPagamento.NumeroCartaoCredito);

            webElement = _driver.FindElement(By.Id("validade"));
            webElement.Clear();
            webElement.SendKeys(doacao.FormaPagamento.Validade);

            webElement = _driver.FindElement(By.Id("cvv"));
            webElement.Clear();
            webElement.SendKeys(doacao.FormaPagamento.CVV);

            webElement = _driver.FindElement(By.Id("FormaPagamento_NomeTitular"));
            webElement.Clear();
            webElement.SendKeys(doacao.FormaPagamento.NomeTitular);

            webElement = _driver.FindElement(By.ClassName("btn-yellow"));
            webElement.Click();

            //Assert
            _driver.Url.Should().Contain("https://localhost:5001/");

        }
    }
}