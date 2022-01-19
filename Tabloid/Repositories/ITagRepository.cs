using System.Collections.Generic;
using Tabloid.Models;

namespace Tabloid.Repositories
{
    public interface ITagRepository
    {
        void Add(Tag tag);
        List<Tag> GetAllTags();
        Tag GetTagById(int id);
        void Delete(int tagId);
        void Update(Tag tag);
    }
}