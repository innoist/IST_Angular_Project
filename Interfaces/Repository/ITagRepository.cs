using System.Collections.Generic;
using IST.Models.DomainModels;

namespace IST.Interfaces.Repository
{
    public interface ITagRepository : IBaseRepository<Tag, long>
    {
        IEnumerable<Tag> GetByTagIds(int[] ids);
    }
}
