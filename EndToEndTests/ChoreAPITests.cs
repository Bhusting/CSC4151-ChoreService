using Domain;
using Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EndToEndTests
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
        public async Task Test_CreateChore()
        {
            var httpClient = new HttpClient() { BaseAddress = new Uri("http://localhost:50279/") };

            var chore = new Chore
            {
                ChoreId = choreId,
                ChoreName="Test Chore",
                CompletionDate="05/23/2020",
                CompletionTime="03:30",
                HouseId = Guid.Empty,
                ChoreTypeId=0
            };
            var json = JsonConvert.SerializeObject(chore);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            var res = await httpClient.PostAsync($"Chore",stringContent);

            Assert.IsTrue(res.IsSuccessStatusCode);

            var body = await res.Content.ReadAsStringAsync();
           
            Assert.IsTrue(body == "Created");
        }

        [TestMethod]
        public async Task Test_DeleteChore()
        {
            var httpClient = new HttpClient() { BaseAddress = new Uri("http://localhost:50279/") };

            var res = await httpClient.DeleteAsync($"Chore/{choreId}");

            Assert.IsTrue(res.IsSuccessStatusCode);

            var body = await res.Content.ReadAsStringAsync();

            Assert.IsTrue(body == "Deleted");
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
            Assert.IsTrue(chore.CompletionDate == "05/23/2020");
        }
    }
}
