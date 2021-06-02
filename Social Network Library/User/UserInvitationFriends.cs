using System;
using System.Collections.Generic;

namespace Social_Networks
{
    public partial class User : IUserInfo
    {
        public User(string nickname)
        {
            CheckForNull(nickname);
            if (nickname.Length < 3 || nickname.Length > 20)
                throw new ArgumentException("Никнейм должен содержать от 3 до 20 символов.");
            for (int i = 0; i < nickname.Length; i++)
                if (!Char.IsLetter(nickname[i]))
                    throw new ArgumentException("Ник должен состоять только из букв");
            for (int i = 0; i < userNames?.Count; i++)
                if (userNames[i] == nickname)
                    throw new ArgumentException("Этот никнейм уже используется");
            this.nickname = nickname;
            if (userNames == null)
                userNames = new List<string>();
            userNames.Add(nickname);
        }

        public delegate void UserHandler(UserEventsArgs info);

        public event UserHandler AddedNewSocialWork;
        public event UserHandler AddedNewGroupOfFriends;
        public event UserHandler AddedNewFriend;
        public event UserHandler RemovedSocialWork;
        public event UserHandler RemovedGroupOfFriends;
        public event UserHandler RemovedFriend;

        public List<string> GetUserSocialNetworkNames()     //повертає список соц мереж до яких належить користувач
        {
            List<string> socialNetworkName = new List<string>();
            for (int i = 0; i < SocialNetwork?.Count; i++)
                socialNetworkName.Add(SocialNetwork[i].Name);
            return socialNetworkName;
        }
        public List<string> GetUserGroupOfFriendsNames()         //повертає список групп до яких належить користувач
        {
            List<string> GroupOfFriendsName = new List<string>();
            for (int i = 0; i < groupOfFriends?.Count; i++)
                GroupOfFriendsName.Add(groupOfFriends[i].Name);
            return GroupOfFriendsName;
        }
        public List<string> GetUserFriendNicknames()         //повертає список друзів користувач
        {
            List<string> listOfFrendsName = new List<string>();
            for (int i = 0; i < FriendList?.Count; i++)
                listOfFrendsName.Add(FriendList[i].nickname);
            return listOfFrendsName;
        }
        public List<string> GetUserInvitationSenders()       //повертає список імен відправникві запрошень користувач
        {
            List<string> senders = new List<string>();
            for (int i = 0; i < Invitations?.Count; i++)
                senders.Add(Invitations[i].GetUserName());
            return senders;
        }
        public string GetUserName() => nickname;        //повертає 'мя користувача

        public void InviteToFriends(User recipient, string message = null)      //запрошення в друзі
        {   
            if (IsFriend(recipient))
                throw new ArgumentException($"{recipient.GetUserName()} уже  {nickname} друг");
            SendAnInvitation(recipient, message);
        }
        public void RequestAnInvitation(User recipient, string message = null)      //запросити стати другом
        {  
            if (recipient.IsFriend(this))
                throw new ArgumentException($"{nickname} уже {recipient.GetUserName()} друг");
            SendAnInvitation(recipient, message);

        }
        public void RespondToTheInvitation(Invitation invitation, Invitation.Respond respond)       //відповісти на запрошення
        {
            bool haveAnInvitation = false;
            for (int i = 0; i < Invitations?.Count; i++)
                if (invitation == Invitations[i]) // Цикл, в котором мы ищем необходимое приглашение
                {
                    haveAnInvitation = true;
                    if (respond == Invitation.Respond.Accept)
                    {
                        if (Invitations[i].Sender.Invitations == null)
                            Invitations[i].Sender.Invitations = new List<Invitation>();
                        Invitations[i].Sender.Invitations.Add(new Invitation(this)); // Добавить ответ
                        Invitations[i].Sender.UpdateFriendList();
                        AddedNewFriend?.Invoke(new UserEventsArgs($"{Invitations[i].Sender.GetUserName()}, у  вас есть новый друг", Invitations[i].Sender));

                        if (FriendList == null)
                            FriendList = new List<User>();
                        FriendList.Add(Invitations[i].Sender);
                        AddedNewFriend?.Invoke(new UserEventsArgs($"{nickname}, у  вас есть новый друг {Invitations[i].GetUserName()}", this));

                        if (!IsFriend(Invitations[i].Sender))
                        {
                            if (FriendList == null)
                                FriendList = new List<User>();
                            FriendList.Add(Invitations[i].Sender);
                            AddedNewFriend?.Invoke(new UserEventsArgs($"{nickname}, у  вас есть новый друг {Invitations[i].GetUserName()}", this));
                        }
                    }
                    Invitations.RemoveAt(i);
                }
            if (!haveAnInvitation)
                throw new ArgumentException("Нет такого приглашения");
        }

        public bool IsFriend(User user)     //перевірка чи є користувач другом
        {
            for (int i = 0; i < FriendList?.Count; i++)
                if (user == FriendList[i])
                    return true;
            return false;
        }

        private string nickname;
        public List<Invitation> Invitations { get; private set; }
        internal List<SocialNetwork> SocialNetwork { get; private set; }
        internal List<User> FriendList { get; private set; }
        private List<GroupOfFriends> groupOfFriends;
        private static List<string> userNames;

        private void SendAnInvitation(User recipient, string message)       //відправити запрошення
        {
            CheckForNull(recipient);
            if (recipient == this)
                throw new ArgumentException($"{recipient.GetUserName()} не можете отправить себе приглашение");
            if (recipient.Invitations == null)
                recipient.Invitations = new List<Invitation>();
            recipient.Invitations.Add(new Invitation(this, message));
        }
        private void UpdateFriendList()     //обновити список друзів
        {
            for (int i = 0; i < Invitations?.Count; i++)
            {
                if (FriendList == null)
                    FriendList = new List<User>();
                FriendList.Add(Invitations[i].Sender);
                Invitations.RemoveAt(i);
            }
        }
    }
}