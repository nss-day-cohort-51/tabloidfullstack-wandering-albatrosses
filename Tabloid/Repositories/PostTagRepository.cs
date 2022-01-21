using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Tabloid.Models;
using Tabloid.Utils;

namespace Tabloid.Repositories
{
    public class PostTagRepository : BaseRepository, IPostTagRepository
    {
        public PostTagRepository(IConfiguration configuration) : base(configuration) { }

        public List<PostTag> Get(int postId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT pt.*, t.Name FROM PostTag pt JOIN Tag t ON pt.TagId = t.Id
                                        WHERE PostId = @postId";

                    DbUtils.AddParameter(cmd, "@postId", postId);

                    List<PostTag> postTags = new List<PostTag>();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            postTags.Add(new PostTag
                            {
                                PostId = postId,
                                TagId = DbUtils.GetInt(reader, "TagId"),
                                TagName = DbUtils.GetString(reader, "Name")
                            });
                        }
                    }
                    return postTags;
                }
            }
        }
        public void Add(PostTag postTag)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO PostTag (PostId, TagId)
                                        OUTPUT INSERTED.Id
                                        VALUES (@postId, @tagId)";

                    DbUtils.AddParameter(cmd, "@postId", postTag.PostId);
                    DbUtils.AddParameter(cmd, "@tagId", postTag.TagId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(PostTag postTag)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM PostTag
                                        WHERE PostId = @postId AND TagId = @tagId";
                    DbUtils.AddParameter(cmd, "@postId", postTag.PostId);
                    DbUtils.AddParameter(cmd, "@tagId", postTag.TagId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}