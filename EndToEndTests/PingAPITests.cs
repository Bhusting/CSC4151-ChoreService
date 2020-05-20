using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace EndToEndTests
{
    [TestClass]
    public class PingAPITests
    {
        [TestMethod]
        public async Task Test_Ping()
        {
            var httpClient = new HttpClient() { BaseAddress = new Uri("http://localhost:50279/") };

            var res = await httpClient.GetAsync($"Ping");

            Assert.IsTrue(res.IsSuccessStatusCode);

            var body = await res.Content.ReadAsStringAsync();

            Assert.IsTrue(body == "Chore Pong");
        }
    }
}
