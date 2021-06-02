using System.Collections.Generic;

namespace Social_Networks
{
    public class UserEventsArgs : IUserInfo
    {
        public UserEventsArgs(string message, User user)
        {
            Message = message;
            this.user = user;
        }

        public List<string> GetUserGroupOfFriendsNames() => user.GetUserGroupOfFriendsNames();
        public List<string> GetUserSocialNetworkNames() => user.GetUserSocialNetworkNames();
        public List<string> GetUserFriendNicknames() => user.GetUserFriendNicknames();
        public string GetUserName() => user.GetUserName();

        public string Message { get; protected set; }
        protected User user;
    }
}