﻿using Microsoft.Data.SqlClient;
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
                                PublishedDateTime = DbUtils.GetDateTime(reader, "CreateDateTime"),
                                IsApproved = DbUtils.GetBool(reader, "IsApproved"),
                                CategoryId = DbUtils.GetInt(reader, "CategoryId"),

                                Category = new Category { 
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "cName")
                                },
                                UserProfileId = DbUtils.GetInt(reader, "UserProfileId")
                            });

                        }

                        return posts;
                    }
                }
            }
        }
    }
}
