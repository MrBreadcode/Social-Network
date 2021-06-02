using System;
using System.Collections.Generic;

namespace Social_Networks
{
    public class SocialNetwork : Administration
    {
        public SocialNetwork(string name, User admin)
        {
            CheckTheNameIsCorrect(name);
            CheckForNull(admin);
            Name = name;
            if (names == null)
                names = new List<string>();
            names.Add(name);
            admin.JoinSocialNetwork(this);
        }

        public bool IsAdmin(User user)      //перевірка користувача, чє є він адміністратором
        {
            if (user == membersList?[0])
                return true;
            return false;
        }

        internal override void AddNewUser(User user)        //додавання користувача
        {
            CheckForNull(user);
            if (membersList == null)
                membersList = new List<User>();
            membersList.Add(user);
        }
        internal override void RemoveUser(User user)       // видалення користувача
        {
            CheckForNull(user);
            if (!IsMember(user))
                throw new ArgumentException($"Пользователь,{user.GetUserName()} не принадлежит к этому {Name} сети");
            for (int i = 0; i < membersList?.Count; i++)
                if (user == membersList[i]) //Цикл, в котором мы ищем пользователя
                {
                    for (int j = 0; j < groupList?.Count; j++)
                        if (groupList[j].IsMember(user)) // Цикл, в котором мы проверяем, принадлежит ли пользователь какой-либо группе
                            user.RemoveGroupOfFriends(groupList[j]);
                    membersList.RemoveAt(i);
                }
        }
        internal void AddGroupOfFriends(GroupOfFriends groupOfFriends)      //дод групп до мережі
        {
            CheckForNull(groupOfFriends);
            if (groupList == null)
                groupList = new List<GroupOfFriends>();
            groupList.Add(groupOfFriends);
        }

        private List<GroupOfFriends> groupList;
    }
}