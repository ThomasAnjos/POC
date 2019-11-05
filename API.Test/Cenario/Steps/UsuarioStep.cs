using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using TechTalk.SpecFlow;

namespace API.Test.Cenario.Steps
{
    [Binding]
    public sealed class UsuarioStep
    {
        [Given(@"que a url do endpoint é http://localhost:50003/api/Usuario")]
        public void DadoQueAUrlDoEndpointEHttpLocalhostApiUsuario(string url)
        {
            ScenarioContext.Current["Endpoint"] = url;
        }

        [Given(@"o metodo HTTP é '(.*)'")]
        public void DadoOMetodoHTTPE(string p0)
        {
            var metodo = Method.POST;

            switch (p0.ToUpper())
            {
                case "POST":
                    metodo = Method.POST;
                    break;
                case "GET":
                    metodo = Method.GET;
                    break;
                case "PUT":
                    metodo = Method.PUT;
                    break;
                case "DELETE":
                    metodo = Method.DELETE;
                    break;
                case "PATCH":
                    metodo = Method.PATCH;
                    break;
                default:
                    Assert.Fail("Método HTTP não esperado");
                    break;
            }

            ScenarioContext.Current["HttpMethod"] = metodo;
        }


        [When(@"chamar o servico")]
        public void QuandoChamarOServico()
        {
            var endpoint = (String)ScenarioContext.Current["Endpoint"];

            ExecutarRequest(endpoint);
        }

        #region
        private void ExecutarRequest(string endpoint)
        {
            var url = endpoint;

            var request = new RestRequest();

            request.Method = (Method)ScenarioContext.Current["HttpMethod"];

            request.Parameters.Clear();

            if (request.Method == Method.POST || request.Method == Method.PUT || request.Method == Method.GET)
            {
                var json = (String)ScenarioContext.Current["Data"];

                if (!String.IsNullOrWhiteSpace(json))
                    request.AddParameter("application/json", json, ParameterType.RequestBody);
            }

            var restClient = new RestClient(url);

            var response = restClient.Execute(request);

            ScenarioContext.Current["Response"] = response;

           // return response;
        }
        #endregion

        [Then(@"Statuscod da resposta deverá ser '(.*)'")]
        public void EntaoStatuscodDaRespostaDeveraSer(string p0)
        {
            var response = (IRestResponse)ScenarioContext.Current["Response"];

            string errorMessage;

            switch (response.StatusCode)
            {
                case HttpStatusCode.InternalServerError:
                case HttpStatusCode.NotFound:
                    errorMessage = "ResponseUri:" + response.ResponseUri;
                    break;
                case HttpStatusCode.Forbidden:
                    var auth = response.Request.Parameters.Where(x => x.Name == "Authorization").First();
                    errorMessage = "Authorization:" + (auth != null ? auth.Value : "none");
                    break;
                default:
                    errorMessage = response.Content;
                    break;
            }

            Assert.AreEqual(p0, response.StatusCode.ToString(), errorMessage);

        }

    }
}
