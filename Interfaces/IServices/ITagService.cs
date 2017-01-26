using System.Collections.Generic;
using IST.Models.DomainModels;

namespace IST.Interfaces.IServices
{
    public interface ITagService
    {
        IEnumerable<Tag> GetAll();
        Tag FindTagById(int tagId);
        bool SaveOrUpdateTag(Tag tag);
        bool DeleteTag(int tagId);
    }
}
