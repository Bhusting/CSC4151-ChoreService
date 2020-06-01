
using Common.Clients;
using Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public async Task<List<Chore>> GetAllChoresByHouseId(Guid houseId)
        {
            var cmd = $"SELECT * FROM Chore WHERE HouseId = \'{houseId}\'";

            var chores = await _sqlClient.Get<Chore>(cmd);

            var updatedChores = new List<Chore>();
            foreach (var chore in chores)
            {
                var updatedChore = await UpdateChore(chore, false);
                updatedChores.Add(updatedChore);
            }

            return updatedChores;
        }

        public async Task<List<Chore>> GetAllChoresByHouseIdToday(Guid houseId)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;

            var cmd = $"SELECT * FROM Chore WHERE HouseId = \'{houseId}\'";

            var chores = await _sqlClient.Get<Chore>(cmd);

            var today = DateTime.Today;
            var todaysChores = new List<Chore>();
            foreach (var chore in chores)
            {
                var updatedChore = await UpdateChore(chore, false);
                var completionDate = DateTime.ParseExact(updatedChore.CompletionDate, "MM/dd/yyyy", provider).Date;

                if (completionDate == today.Date)
                {
                    todaysChores.Add(chore);
                }

            }

            return todaysChores;
        }

        public async Task<List<Chore>> GetAllChoresByChoreTypeId(short choretypeId)
        {
            var cmd = $"SELECT * FROM Chore WHERE ChoreTypeId = \'{choretypeId.ToString()}\'";

            return await _sqlClient.Get<Chore>(cmd);
        }

        

        public async Task<Chore> UpdateChore(Chore chore, bool isComplete)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            var completionDate = DateTime.ParseExact(chore.CompletionDate, "MM/dd/yyyy", provider).Date;
            var completionTime = DateTime.ParseExact(chore.CompletionTime, "HH:mm", provider);
            var choreFrequency = (ChoreServiceType)chore.ChoreTypeId;
            var today = DateTime.Now;
            var updated = false;
            //0- daily
            //1- weekly
            //2- monthly
            //3- yearly
            //we should maintian the chore freq in an enum. 
            //update the chore, date and time with whatever (weekly)
            //calling the chore, if there is a chore, checking the logic, then update the date. 


            //if (Convert.ToDateTime(completionDate) == DateTime.Now.Date && Convert.ToDateTime(completionTime) <= DateTime.Now.ToLocalTime())
            // on the date of the completion date , it updates to the next freq. 
            // example ; User puts in completion date of chore 4:00am 5/23/2020
            // 1 options : user say its completed - this triggers the update , and it updates to next freq . if the completion time is 5:00am,
            // and the user completes it at 4:30am, it should trigger the update.
            // 2 option: even if the user does not complete it, at the end of the time, it will update anyways. 
            if (completionDate < today.Date || isComplete)
            {
                if (!isComplete)
                {
                    while (completionDate < today.Date)
                    {
                        if (choreFrequency == ChoreServiceType.Daily)
                            completionDate = completionDate.AddDays(1);
                        else if (choreFrequency == ChoreServiceType.Weekly)
                            completionDate = completionDate.AddDays(7);
                        else if (choreFrequency == ChoreServiceType.Monthly)
                            completionDate = completionDate.AddMonths(1);
                        else if (choreFrequency == ChoreServiceType.Yearly)
                            completionDate = completionDate.AddYears(1);
                    }
                }
                else
                {
                    while (completionDate <= today.Date)
                    {
                        if (choreFrequency == ChoreServiceType.Daily)
                            completionDate = completionDate.AddDays(1);
                        else if (choreFrequency == ChoreServiceType.Weekly)
                            completionDate = completionDate.AddDays(7);
                        else if (choreFrequency == ChoreServiceType.Monthly)
                            completionDate = completionDate.AddMonths(1);
                        else if (choreFrequency == ChoreServiceType.Yearly)
                            completionDate = completionDate.AddYears(1);
                    }
                }

                updated = true;
            }

            if (updated)
            {
                chore.CompletionDate = completionDate.ToString("MM/dd/yyyy");
                var updateCmd = SqlCommandBuilder.UpdateRecord(typeof(Chore), chore.ChoreId, "CompletionDate",
                    chore.CompletionDate);
                await _sqlClient.Update(updateCmd);
            }

            return chore;
        }


        public enum ChoreServiceType
        {
            Daily = 0,
            Weekly = 1,
            Monthly = 2,
            Yearly = 3
        }
    }
}
