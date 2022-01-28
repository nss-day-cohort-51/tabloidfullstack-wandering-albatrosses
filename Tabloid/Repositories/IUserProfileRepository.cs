using System.Collections.Generic;
using Tabloid.Models;

namespace Tabloid.Repositories
{
    public interface IUserProfileRepository
    {
        void Add(UserProfile userProfile);
        UserProfile GetByFirebaseUserId(string firebaseUserId);
        List<UserProfile> GetAllUsers();
        void ReactivateAndDeactivate(UserProfile userProfile);
        UserProfile GetUserProfileId(int id);
        void UpdateUserTypeId(int userTypeId, int userId);
        UserProfile GetUserProfileById(int id);
        List<UserType> AllUserTypes();
    }
}