
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Common.Builders;
using Common.Clients;
using Common.Settings;
using Domain;
using SqlCommandBuilder = Common.Builders.SqlCommandBuilder;

namespace Common.Repositories
{
    public class ChoreRepository : IChoreRepository
    {

        private readonly SqlClient _sqlClient;

        public ChoreRepository(SqlClient sqlClient)
        {
            _sqlClient = sqlClient;
        }

        /// <summary>
        /// Retrieves a chore from the Chore Table.
        /// </summary>
        /// <param name="id">Id of the Chore to retrieve.</param>
        /// <returns>Chore</returns>
        public async Task<Chore> GetChore(Guid id)
        {
            var cmd = SqlCommandBuilder.GetIndividualRecordBuilder(typeof(Chore), id);

            var chores = await _sqlClient.Get<Chore>(cmd);

            if (chores.Count > 0)
                return chores[0];
            else
                return null;
        }

        public async Task CreateChore(Chore chore)
        {
            var cmd = SqlCommandBuilder.InsertRecord<Chore>(chore);
            await _sqlClient.Insert(cmd);
        }

        public async Task DeleteChore(Guid id)
        {
            var cmd = SqlCommandBuilder.DeleteRecord(typeof(Chore), id);
            await _sqlClient.Delete(cmd);
        }

        public async Task<List<Chore>> GetAllChores()
        {
            var cmd = SqlCommandBuilder.GetRecords(typeof(Chore));
            return await _sqlClient.Get<Chore>(cmd);
        }
    }
}
