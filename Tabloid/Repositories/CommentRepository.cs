using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Tabloid.Models;
using Tabloid.Utils;
using Tabloid.Repositories;


namespace Tabloid.Repositories
{
    public class CommentRepository : BaseRepository, ICommentRepository
    {
        public CommentRepository(IConfiguration configuration) : base(configuration) { }





        public List<Comment> GetAllComments()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
               select C.Id, C.PostId, C.UserProfileId, C.Content, C.CreateDateTime, C.Subject, Up.Id, Up.FirebaseUserId, Up.DisplayName, Up.FirstName, Up.LastName, Up.Email, Up.CreateDateTime UserCreateDateTime, Up.ImageLocation, Up.UserTypeId 
from Comment C
left join UserProfile Up on Up.Id = C.UserProfileId
            ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        var comment = new List<Comment>();
                        while (reader.Read())
                        {
                            comment.Add(new Comment()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                PostId = DbUtils.GetInt(reader, "PostId"),
                                UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
                                Subject = DbUtils.GetString(reader, "Subject"),
                                Content = DbUtils.GetString(reader, "Content"),
                                CreateDateTime = DbUtils.GetDateTime(reader, "CreateDateTime"),
                                UserProfile = new UserProfile()
                                {
                                    Id = DbUtils.GetInt(reader, "UserProfileId"),
                                    FirebaseUserId = DbUtils.GetString(reader, "FirebaseUserId"),
                                    DisplayName = DbUtils.GetString(reader, "DisplayName"),
                                    FirstName = DbUtils.GetString(reader, "FirstName"),
                                    LastName = DbUtils.GetString(reader, "LastName"),
                                    Email = DbUtils.GetString(reader, "Email"),
                                    CreateDateTime = DbUtils.GetDateTime(reader, "UserCreateDateTime"),
                                    ImageLocation = DbUtils.GetString(reader, "ImageLocation"),
                                    UserTypeId = DbUtils.GetInt(reader, "UserTypeId"),
                                },
                            });
                        }

                        return comment;
                    }
                }
            }
        }






        public void Add(Comment comment)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Comment (Content, UserProfileId, CreateDateTime, PostId, Subject)
                                        OUTPUT INSERTED.ID
                                        VALUES (@Content, @UserProfileId, @CreateDateTime, @PostId, @Subject)";
                    DbUtils.AddParameter(cmd, "@Content", comment.Content);
                    DbUtils.AddParameter(cmd, "@CreateDateTime", comment.CreateDateTime);
                    DbUtils.AddParameter(cmd, "@UserProfileId", comment.UserProfileId);
                    DbUtils.AddParameter(cmd, "@PostId", comment.PostId);
                    DbUtils.AddParameter(cmd, "@Subject", comment.Subject);

                    comment.Id = (int)cmd.ExecuteScalar();
                }
            }
        }



        public void Update(Comment comment)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE Comment
                           SET Subject = @Subject,
                               Content = @Content,
                               CreateDateTime = @CreateDateTime,
                               PostId = @PostId,
                               UserProfileId = @UserProfileId
                         WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Subject", comment.Subject);
                    DbUtils.AddParameter(cmd, "@Content", comment.Content);
                    DbUtils.AddParameter(cmd, "@CreateDateTime", comment.CreateDateTime);
                    DbUtils.AddParameter(cmd, "@PostId", comment.PostId);
                    DbUtils.AddParameter(cmd, "@UserProfileId", comment.UserProfileId);
                    DbUtils.AddParameter(cmd, "@Id", comment.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }



        public void Delete(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Comment WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
