using System.Collections.Generic;
using IST.Models.DomainModels;

namespace IST.Interfaces.IServices
{
    public interface ITagGroupService
    {
        IEnumerable<TagGroup> GetAll();
        TagGroup FindTagGroupById(int tagGroupId);
        bool SaveOrUpdateTagGroup(TagGroup tagGroup);
        bool RemoveTagGroup(int tagGroupId);
        bool DeleteTagGroup(int tagGroupId);
    }
}
