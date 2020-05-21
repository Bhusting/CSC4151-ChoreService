
using Common.Clients;
using Common.Time;
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
            var cmd = SqlCommandBuilder.UpdateRecord(typeof(Chore), chore.ChoreId, "ChoreName", chore.ChoreName );
            await _sqlClient.Update(cmd);

        }

        public async Task<Chore> UpdateChore(Guid choreId)
        {
           
            var cmd = SqlCommandBuilder.GetIndividualRecordBuilder(typeof(Chore), choreId);

            var chores = await _sqlClient.Get<Chore>(cmd);

            if (chores.Count > 0)
            {

                var completionDate = chores[0].CompletionDate;
                var completionTime = chores[0].CompletionTime.ParseEndTime(); 
                var choreFrequency = chores[0].ChoreTypeId;

                //0- daily
                //1- weekly
                //2- monthly
                //3- yearly
                //we should maintian the chore freq in an enum. 
                //update the chore, date and time with whatever (weekly)
                //calling the chore, if there is a chore, checking the logic, then update the date. 


                //if (Convert.ToDateTime(completionDate) == DateTime.Now.Date && Convert.ToDateTime(completionTime) <= DateTime.Now.ToLocalTime())

                // 

                if (Convert.ToDateTime(completionDate) == DateTime.Now.Date)
                {
                    DateTime updatedCompletionDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, completionTime.Hours, completionTime.Minutes, completionTime.Seconds);

                   
                    if (Convert.ToInt16(choreFrequency) == Convert.ToInt16(ChoreServiceType.Daily))
                    {
                        updatedCompletionDate = Convert.ToDateTime(completionDate).AddDays(1);
                    }
                    if (Convert.ToInt16(choreFrequency) == Convert.ToInt16(ChoreServiceType.Weekly))
                    {
                        updatedCompletionDate = Convert.ToDateTime(completionDate).AddDays(7);
                    }
                    if (Convert.ToInt16(choreFrequency) == Convert.ToInt16(ChoreServiceType.Monthly))
                    {
                        updatedCompletionDate = Convert.ToDateTime(completionDate).AddMonths(1);
                    }
                    if (Convert.ToInt16(choreFrequency) == Convert.ToInt16(ChoreServiceType.Yearly))
                    {
                        updatedCompletionDate = Convert.ToDateTime(completionDate).AddYears(1);
                    }


                    var updateCmd = SqlCommandBuilder.UpdateRecord(typeof(Chore), choreId, "CompletionDate", updatedCompletionDate.Date.ToString("dd/MM/yyyy"));
                    await _sqlClient.Update(updateCmd);

                }
            }
            return chores[0]; //updating the old one.


        }



        //public async Task<int> UpdateChore(Guid choreId)
        //{

        //    var cmd = SqlCommandBuilder.GetIndividualRecordBuilder(typeof(Chore), choreId);

        //    var chores = await _sqlClient.Get<Chore>(cmd);

        //    if (chores.Count > 0)
        //    {

        //        var completionDate = chores[0].CompletionDate;
        //        // var completionTime = chores[0].CompletionTime;
        //        var choreFrequency = chores[0].ChoreTypeId;

        //        //0- daily
        //        //1- weekly
        //        //2- monthly
        //        //3- yearly
        //        //we should maintian the chore freq in an enum. 
        //        //update the chore, date and time with whatever (weekly)
        //        //calling the chore, if there is a chore, checking the logic, then update the date. 


        //        //if (Convert.ToDateTime(completionDate) == DateTime.Now.Date && Convert.ToDateTime(completionTime) <= DateTime.Now.ToLocalTime())

        //        if (Convert.ToDateTime(completionDate) == DateTime.Now.Date)
        //        {
        //            DateTime updatedCompletionDate = DateTime.Now;

        //            if (Convert.ToInt16(choreFrequency) == Convert.ToInt16(ChoreServiceType.Daily))
        //            {
        //                updatedCompletionDate = Convert.ToDateTime(completionDate).AddDays(1);
        //            }
        //            if (Convert.ToInt16(choreFrequency) == Convert.ToInt16(ChoreServiceType.Weekly))
        //            {
        //                updatedCompletionDate = Convert.ToDateTime(completionDate).AddDays(7);
        //            }
        //            if (Convert.ToInt16(choreFrequency) == Convert.ToInt16(ChoreServiceType.Monthly))
        //            {
        //                updatedCompletionDate = Convert.ToDateTime(completionDate).AddMonths(1);
        //            }
        //            if (Convert.ToInt16(choreFrequency) == Convert.ToInt16(ChoreServiceType.Yearly))
        //            {
        //                updatedCompletionDate = Convert.ToDateTime(completionDate).AddYears(1);
        //            }


        //            var updateCmd = SqlCommandBuilder.UpdateRecord(typeof(Chore), choreId, "CompletionDate", updatedCompletionDate.Date.ToString("dd/MM/yyyy"));
        //            await _sqlClient.Update(updateCmd);

        //        }
        //    }
        //    return chores[0];


        //}
        // have a switch case 
        public enum ChoreServiceType
        {
            Daily = 0,
            Weekly = 1,
            Monthly = 2,
            Yearly = 3
        }
    }
}
