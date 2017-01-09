using System.Collections.Generic;
using IST.Models.DomainModels;

namespace IST.Interfaces.IServices
{
    public interface ITagGroupService
    {
        IEnumerable<TagGroup> GetAll();
        TagGroup FindTagGroupById(int tagGroupId);
        bool SaveTagGroup(TagGroup tagGroup);
        bool UpdateTagGroup(TagGroup tagGroup);
        bool DeleteTagGroup(int tagGroupId);
    }
}
