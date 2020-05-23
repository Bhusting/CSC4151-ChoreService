using Common.Clients;
using Common.Repositories;
using Common.Settings;
using Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ChoreServiceTests.UnitTests
{
    [TestClass]
    public class RepositoryTests
    {
        private SqlSettings _sqlSettings;
        private SqlClient _sqlClient;
        private IChoreRepository _repository;
        public RepositoryTests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true);
            IConfiguration configuration = builder.Build();
            var settings = configuration.GetSection("SQL");
            _sqlSettings = new SqlSettings(settings["Server"], settings["Catalog"], settings["User"], settings["Password"]);
            _sqlClient = new SqlClient(_sqlSettings);
            _repository = new ChoreRepository(_sqlClient);


        }
        [TestMethod]
        public async Task GetChore_ValidChore()
        {
            // Arrange
            
            // Act
            var chore = await _repository.GetChore(new Guid("00000000-0000-0000-0000-000000000000"));
            // Assert
            Assert.IsNotNull(chore);
            Assert.AreEqual<string>("test", chore.ChoreName);
        }

        [TestMethod]
        public async Task GetChore_InvalidChore()
        {
            // Act
            var chore = await _repository.GetChore(new Guid("00000000-0000-0000-0000-000000000001"));
            // Assert
            Assert.IsNull(chore);
           
        }

        [TestMethod]
        public async Task UpdateChore_WhenNotDueToday()
        {
            // Arange
            var choreId = new Guid("D0EF2AAC-6279-4294-92DE-96B6E320E469");

            // Act
            var chore = await _repository.UpdateChore(choreId, false);

            // Assert
            Assert.IsNotNull(chore);
            Assert.AreEqual<string>("05/23/2021", chore.CompletionDate);
        }

        [TestMethod]
        public async Task UpdateChore_WhenDueTodayButTimeIsNotPassed()
        {
            // Arange
            var choreId = new Guid("802AE871-4282-424C-8C01-FE8267D30E1C");

            // Act
            var chore = await _repository.UpdateChore(choreId, false);

            // Assert
            Assert.IsNotNull(chore);
            Assert.AreEqual<string>("05/23/2020", chore.CompletionDate);
        }

        [TestMethod]
        public async Task UpdateChore_WhenDueTodayButTimeIsPassed()
        {
            // Arange
            var choreId = new Guid("D0EF2AAC-6279-4294-92DE-96B6E320E468");

            // Act
            var chore = await _repository.UpdateChore(choreId, false);

            // Assert
            Assert.IsNotNull(chore);
            Assert.AreEqual<string>("06/23/2020", chore.CompletionDate);
        }
        [TestMethod]
        public async Task UpdateChore_WhenDueTodayAndIsCompleted()
        {
            // Arange
            var choreId = new Guid("802AE871-4282-424C-8C01-FE8267D30E1C");

            // Act
            var chore = await _repository.UpdateChore(choreId, true);

            // Assert
            Assert.IsNotNull(chore);
            Assert.AreEqual<string>("05/24/2020", chore.CompletionDate);
        }

    }
}
