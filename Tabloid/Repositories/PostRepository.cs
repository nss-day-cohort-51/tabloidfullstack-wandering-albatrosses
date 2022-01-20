using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tabloid.Models;
using Tabloid.Utils;

namespace Tabloid.Repositories
{
    public class PostRepository : BaseRepository, IPostRepository
    {
        public PostRepository(IConfiguration configuration) : base(configuration) { }

        public List<Post> GetAllPosts()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"select p.Id, p.Title, p.Content, p.ImageLocation,
		                                        p.CreateDateTime, p.PublishDateTime, p.IsApproved,
		                                        p.CategoryId, p.UserProfileID,
		
		                                        up.DisplayName as upDisplayName,

		                                        c.Name as cName, c.Id

                                        From Post p

	                                        Join UserProfile up on p.UserProfileId = up.Id
	                                        Join Category c on p.CategoryId = c.Id

                                        Where IsApproved = 1 AND p.PublishDateTime <= '2022-01-18'
                                        Order By PublishDateTime DESC";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var posts = new List<Post>();
                        while (reader.Read())
                        {
                            posts.Add(new Post()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Title = DbUtils.GetString(reader, "Title"),
                                Content = DbUtils.GetString(reader, "Content"),
                                ImageLocation = DbUtils.GetString(reader, "ImageLocation"),
                                CreateDateTime = DbUtils.GetDateTime(reader, "CreateDateTime"),
                                PublishDateTime = DbUtils.GetDateTime(reader, "PublishDateTime"),
                                IsApproved = DbUtils.GetBool(reader, "IsApproved"),
                                CategoryId = DbUtils.GetInt(reader, "CategoryId"),

                                Category = new Category { 
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "cName")
                                },

                                Userprofile = new UserProfile
                                {
                                    DisplayName = DbUtils.GetString(reader, "upDisplayName"),
                                },
                                    UserProfileId = DbUtils.GetInt(reader, "UserProfileId")
                            });

                        }

                        return posts;
                    }
                }
            }
        }

        public Post GetPostById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Select p.Id, p.Title, p.Content, p.ImageLocation, p.PublishDateTime, p.UserProfileId,

		                                        up.DisplayName

                                        From Post p
                                        Join UserProfile up on p.UserProfileId = up.Id

                                        Where p.Id = @Id";
                    DbUtils.AddParameter(cmd, "@Id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Post post = null;
                        if(reader.Read())
                        {
                            post = new Post()
                            {
                                Id = id,
                                Title = DbUtils.GetString(reader, "Title"),
                                Content = DbUtils.GetString(reader, "Content"),
                                ImageLocation = DbUtils.GetString(reader, "ImageLocation"),
                                PublishDateTime = DbUtils.GetDateTime(reader, "PublishDateTime"),

                                Userprofile = new UserProfile()
                                {
                                    DisplayName = DbUtils.GetString(reader, "DisplayName")
                                }
                            };
                        }
                        return post;
                    }
                }
            }
        }

        public void Add(Post post)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Insert Into Post (Title, Content, ImageLocation, CategoryId,
                                            PublishDateTime, UserProfileId, CreateDateTime, IsApproved)
                                        Output INSERTED.ID
                                        Values (@Title, @Content, @ImageLocation,
                                                @CategoryId, @PublishDateTime, @UserProfileId,
                                                SysDateTime(), @IsApproved);";

                    DbUtils.AddParameter(cmd, "@Title", post.Title);
                    DbUtils.AddParameter(cmd, "@Content", post.Content);
                    DbUtils.AddParameter(cmd, "@ImageLocation", post.ImageLocation);
                    DbUtils.AddParameter(cmd, "@CategoryId", post.CategoryId);
                    DbUtils.AddParameter(cmd, "@PublishDateTime", post.PublishDateTime);
                    DbUtils.AddParameter(cmd, "@UserProfileId", post.UserProfileId);
                    DbUtils.AddParameter(cmd, "@IsApproved", post.IsApproved);

                    post.Id = (int)cmd.ExecuteScalar();


                }
            }
        }

        //public void Update(Post post)
        //{
        //    using (var conn = Connection)
        //    {
        //        conn.Open();
        //        using (var cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandText = @""
        //        }
        //    }
        //}
    }
}
