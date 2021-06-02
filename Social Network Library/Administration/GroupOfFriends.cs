using System;
using System.Collections.Generic;

namespace Social_Networks
{
    public class GroupOfFriends : Administration
    {
        public GroupOfFriends(string name, User creator, SocialNetwork socialNetwork)
        {
            CheckTheNameIsCorrect(name);
            CheckForNull(creator);
            CheckForNull(socialNetwork);
            Name = name;
            if (names == null)
                names = new List<string>();
            names.Add(name);
            membersList = new List<User> { creator };
            creator.JoinGroupOfFriends(this);
            if (!socialNetwork.IsMember(creator))
                creator.JoinSocialNetwork(socialNetwork);
            this.socialNetwork = socialNetwork;
            socialNetwork.AddGroupOfFriends(this);
        }

        internal override void AddNewUser(User user)        //дод користувача
        {
            CheckForNull(user);
            if (IsMember(user))
                throw new ArgumentException($"Друг {user.GetUserName()} уже является членом группы {Name}");
            membersList.Add(user);
            for (int i = 0; i < membersList.Count - 1; i++)
            {
                int countOfInvitations = 0;
                if (!user.IsFriend(membersList[i]))
                {
                    user.InviteToFriends(membersList[i]);
                    countOfInvitations++;
                }   
                if (!membersList[i].IsFriend(user))
                {
                    user.RequestAnInvitation(membersList[i]);
                    countOfInvitations++;
                }
                // Цикл, в котором ищем приглашение от пользователя
                for (int j = membersList[i].Invitations.Count - 1; j >= 0; j--)
                    if (membersList[i].Invitations[j].Sender == user && countOfInvitations > 0) 
                    {
                        membersList[i].RespondToTheInvitation(membersList[i].Invitations[j], Invitation.Respond.Accept);
                        countOfInvitations--;
                    }
            }
            if (!socialNetwork.IsMember(user))
                user.JoinSocialNetwork(socialNetwork);
        }
        internal override void RemoveUser(User user)        //видалення користувача
        {
            CheckForNull(user);
            if (!IsMember(user))
                throw new ArgumentException($"Пользователь,{user.GetUserName()} не принадлежит к группе друзей \"{Name}\"");
            for (int i = 0; i < membersList?.Count; i++)
                if (user == membersList[i]) // Цикл, в котором мы ищем пользователя
                {
                    membersList.RemoveAt(i);
                    for (int j = 0; j < membersList.Count; j++) // Цикл, в котором среди списка участников
                        if (user.IsFriend(membersList[j]))      // поиск друзей пользователя
                            user.RemoveFriend(membersList[j]);
                }
        }

        internal SocialNetwork socialNetwork;
    }
}