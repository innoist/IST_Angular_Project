using System.Collections.Generic;
using IST.Models.DomainModels;

namespace IST.Interfaces.Repository
{
    public interface ITagGroupRepository : IBaseRepository<TagGroup, long>
    {
        IEnumerable<TagGroup> GetAllTagGroups();
        TagGroup FindTagGroupById(int tagGroupId);
    }
}
