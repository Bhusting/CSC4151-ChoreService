using Domain;
using Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChoreServiceTests.EndToEndTests
{
    [TestClass]
    public class ChoreAPITests
    {
        Guid choreId = new Guid("802AE871-4282-424C-8C01-FE8267D30E1C");
        [TestMethod]
        public async Task  Test_GetChore()
        {
                var httpClient = new HttpClient() { BaseAddress = new Uri("http://localhost:50279/") };

                var res = await httpClient.GetAsync($"Chore/{Guid.Empty}");

                Assert.IsTrue(res.IsSuccessStatusCode);

                var body = await res.Content.ReadAsStringAsync();

                var chore = JsonConvert.DeserializeObject<Chore>(body);

                Assert.IsTrue(chore.ChoreId== Guid.Empty);
                Assert.IsTrue(chore.ChoreName== "test");
        }

        

        
        [TestMethod]
        public async Task Test_UpdateChore()
        {
            var httpClient = new HttpClient() { BaseAddress = new Uri("http://localhost:50279/") };

            var model = new UpdateChoreModel() { IsCompleted = true };
            var json = JsonConvert.SerializeObject(model);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");            
            var res = await httpClient.PutAsync($"Chore/{choreId}", stringContent);

            Assert.IsTrue(res.IsSuccessStatusCode);

            var body = await res.Content.ReadAsStringAsync();

            var chore = JsonConvert.DeserializeObject<Chore>(body);

            Assert.IsTrue(chore.ChoreId == choreId);
            Assert.IsTrue(chore.CompletionDate == "05/24/2020");
        }
    }
}
