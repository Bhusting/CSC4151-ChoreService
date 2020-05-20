using Domain;
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
                ChoreId = new Guid("A53E6B32-02E4-4E6C-970E-C2973D3CC8C2"),
                ChoreName="Test Chore",
                CompletionDate="07/05/2019",
                CompletionTime="15:30",
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

            var res = await httpClient.DeleteAsync($"Chore/A53E6B32-02E4-4E6C-970E-C2973D3CC8C2");

            Assert.IsTrue(res.IsSuccessStatusCode);

            var body = await res.Content.ReadAsStringAsync();

            Assert.IsTrue(body == "Deleted");
        }
    }
}
