using System;
using System.Collections.Generic;

namespace Social_Networks
{
    public partial class User
    {
        public void JoinSocialNetwork(SocialNetwork socialNetwork)
        {
            CheckForNull(socialNetwork);
            if (socialNetwork.IsMember(this))
                throw new ArgumentException($"Пользователь {nickname} принадлежит сети {socialNetwork.Name}");
            else
            {
                if (SocialNetwork == null)
                    SocialNetwork = new List<SocialNetwork>();
                SocialNetwork.Add(socialNetwork);
                socialNetwork.AddNewUser(this);
                AddedNewSocialWork?.Invoke(new UserEventsArgs($"{nickname},у вас есть новая социальная сеть {socialNetwork.Name}", this));
            }
        }
        public void JoinSocialNetwork(SocialNetwork socialNetwork, User user)       //приєднатися до мережі
        {
            CheckForNull(socialNetwork);
            CheckForNull(user);
            if (socialNetwork.IsAdmin(this))
                user.JoinSocialNetwork(socialNetwork);
            else
                throw new ArgumentException($"{nickname}, вы не авторизованы для этой операции" +
                    $" вам нужно быть администратором {socialNetwork.Name}");
          
        }
        public static User operator +(User user, SocialNetwork socialNetwork)
        {
            user.JoinSocialNetwork(socialNetwork);
            return user;
        }
        public void RemoveSocialNetwork(SocialNetwork socialNetwork)       
        {
            CheckForNull(socialNetwork);
            if (!socialNetwork.IsMember(this))
                throw new ArgumentException($"{nickname}, вы не являетесь членом {socialNetwork.Name} сети");
            for (int i = 0; i < this?.SocialNetwork?.Count; i++)
                if (socialNetwork == SocialNetwork[i]) // Цикл, в котором ищем социальную сеть
                {
                    SocialNetwork.RemoveAt(i);
                    socialNetwork.RemoveUser(this);
                    RemovedSocialWork?.Invoke(new UserEventsArgs($"{nickname}, удлён из социальной сети {socialNetwork.Name}", this));
                }
        }
        public void RemoveSocialNetwork(SocialNetwork socialNetwork, User user)     // //видалити мережу
        {
            CheckForNull(socialNetwork);
            CheckForNull(user);
            if (!socialNetwork.IsAdmin(this))
                throw new ArgumentException($"{nickname}, вы не авторизованы для этой операции" +
                    $" вам нужно быть администратором {socialNetwork.Name}");
            user.RemoveSocialNetwork(socialNetwork);
        }
        public static User operator -(User user, SocialNetwork socialNetwork)
        {
            user.RemoveSocialNetwork(socialNetwork);
            return user;
        }

        public void RemoveGroupOfFriends(GroupOfFriends groupOfFriends)     //метод видалення группи
        {
            CheckForNull(groupOfFriends);
            if (!groupOfFriends.IsMember(this))
                throw new ArgumentException($"{nickname}, вы не являетесь членом группы друзей {groupOfFriends.Name}");
            for (int i = 0; i < this?.groupOfFriends?.Count; i++)
                if (groupOfFriends == this.groupOfFriends[i]) // Цикл, в котором мы ищем группу друзей
                {
                    groupOfFriends.RemoveUser(this);
                    this.groupOfFriends.RemoveAt(i);
                    RemovedGroupOfFriends?.Invoke(new UserEventsArgs($"{nickname}, удалён из группы друзей {groupOfFriends.Name}", this));

                }
        }

        public static User operator -(User user, GroupOfFriends groupOfFriends)
        {
            user.RemoveGroupOfFriends(groupOfFriends);
            return user;
        }
        public void RemoveFriend(User user)     //видалення друга
        {
            CheckForNull(user);
            if (!IsFriend(user))
                throw new ArgumentException($"Пользователь {user.GetUserName()} не является другом");
            for (int i = 0; i < FriendList?.Count; i++)
                if (user == FriendList[i]) // Цикл, в котором мы ищем друга
                    FriendList.RemoveAt(i);
            RemovedFriend?.Invoke(new UserEventsArgs($"{nickname}, вы удалили {user.GetUserName()} из вашего списка друзей", user));
        }

        public void JoinGroupOfFriends(User friend, GroupOfFriends groupOfFriends)      //приєднатися до группи
        {
            CheckForNull(groupOfFriends);
            CheckForNull(friend, "Друг должен быть пользователем");
            if (!IsFriend(friend))
                throw new ArgumentException($"{nickname}, у вас нет такого друга {friend.GetUserName()}");
            if (!groupOfFriends.IsMember(this))
                throw new ArgumentException($"{nickname}, вы должены быть членом группы {groupOfFriends.Name}");
            friend.JoinGroupOfFriends(groupOfFriends);
            AddedNewGroupOfFriends?.Invoke(new UserEventsArgs($"{friend.GetUserName()}, у вас есть новая группа друзей {groupOfFriends.Name}"
                + $"\n{nickname} добавил вас ", friend));   
        }
        internal void JoinGroupOfFriends(GroupOfFriends groupOfFriends)
        {
            CheckForNull(groupOfFriends);
            if (!groupOfFriends.IsMember(this))
                groupOfFriends.AddNewUser(this);
            if (this.groupOfFriends == null)
                this.groupOfFriends = new List<GroupOfFriends>();
            this.groupOfFriends.Add(groupOfFriends);
        }

        protected static void CheckForNull(string name)     //перевірка на null
        {
            if (name == null)
                throw new ArgumentNullException("Имя не должно быть пустым");
        }
        protected static void CheckForNull(User user, string message = "Пользователь не должен быть пустым")
        {
            if (user == null)
                throw new ArgumentNullException(message);
        }
        protected static void CheckForNull(SocialNetwork socialNetwork)
        {
            if (socialNetwork == null)
                throw new ArgumentException("Социальная сеть не может быть пустой");
        }
        protected static void CheckForNull(GroupOfFriends groupOfFriends)
        {
            if (groupOfFriends == null)
                throw new ArgumentNullException("Группа друзей не может быть пустой");
        }
    }
}