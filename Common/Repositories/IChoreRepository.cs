using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Common.Repositories
{
    public interface IChoreRepository
    {
        /// <summary>
        /// Retrieves a chore from the Chore Table.
        /// </summary>
        /// <param name="id">Id of the Chore to retrieve.</param>
        /// <returns>Chore</returns>
        Task<Chore> GetChore(Guid id);
        Task CreateChore(Chore chore);

        Task DeleteChore(Guid id);
        Task<List<Chore>> GetAllChores();
        Task<List<Chore>> GetAllChoresByHouseId(string houseId);
        Task<List<Chore>> GetAllChoresByChoreTypeId(short choretypeId);
        Task<Chore> UpdateChore(Guid choreId);
       // Task<int> UpdateChore(Guid choreId);
        Task UpdateChore(Chore chore);

    }
}
