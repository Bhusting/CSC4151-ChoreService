
using Common.Clients;
using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

            return chores[0];
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

        public async Task<List<Chore>> GetAllChoresByHouseId(string houseId)
        {
            var cmd = SqlCommandBuilder.GetRecordsByField(typeof(Chore), typeof(Guid), "HouseId", houseId);
            return await _sqlClient.Get<Chore>(cmd);
        }

        public async Task<List<Chore>> GetAllChoresByChoreTypeId(short choretypeId)
        {
            var cmd = SqlCommandBuilder.GetRecordsByField(typeof(Chore), typeof(short), "ChoreTypeId", choretypeId.ToString());
            return await _sqlClient.Get<Chore>(cmd);
        }

        public async Task UpdateChore(Chore chore)
        {
            var cmd = SqlCommandBuilder.UpdateRecord<Chore>(chore);
            await _sqlClient.Update(cmd);

        }

        public async Task<Chore> GetChoreByChoreId(Guid choreId)
        {
            var cmd = SqlCommandBuilder.GetIndividualRecordBuilder(typeof(Chore), choreId);

            var chores = await _sqlClient.Get<Chore>(cmd);

            if (chores.Count > 0)
            {

                var completionDate = chores[0].CompletionDate;
                var completionTime = chores[0].CompletionTime;
                var choreFrequency = chores[0].ChoreTypeId;


                if (Convert.ToDateTime(completionDate) == DateTime.Now.Date && Convert.ToDateTime(completionTime) <= DateTime.Now.ToLocalTime())
                {
                    //update the chore, date and time with whatever (weekly) 
                    //call the command builder for update 
                }
            }
            return chores[0];


        }
    }
}
