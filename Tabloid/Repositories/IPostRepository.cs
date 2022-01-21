
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tabloid.Models;

namespace Tabloid.Repositories
{
   public interface IPostRepository
    {
        List<Post> GetAllPosts();

        Post GetPostById(int id);

        void Add(Post post);
    }
}
