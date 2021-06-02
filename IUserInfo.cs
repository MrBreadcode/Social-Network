using System.Collections.Generic;

namespace Social_Networks
{
    public interface IUserInfo
    {
        List<string> GetUserGroupOfFriendsNames();
        List<string> GetUserSocialNetworkNames();
        List<string> GetUserFriendNicknames();
        string GetUserName();
    }
}