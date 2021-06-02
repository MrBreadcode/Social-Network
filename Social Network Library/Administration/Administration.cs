using System;
using System.Collections.Generic;

namespace Social_Networks
{
    public abstract class Administration
    {
        public string Name { get; protected set; }

        public List<string> GetMembersInfo()        //метод що поветає інф про учасників
        {
            List<string> membersNames = new List<string>();
            for (int i = 0; i < membersList.Count; i++)
                membersNames.Add(membersList[i].GetUserName());
            return membersNames;
        }
        public bool IsMember(User user)     //Метод перевірки, чи  користувач є учасником
        {
            for (int j = 0; j < membersList?.Count; j++)
                if (user == membersList[j])
                    return true;
            return false;
        }

        internal abstract void AddNewUser(User user);
        internal abstract void RemoveUser(User user);
        protected internal List<User> membersList;

        protected static void CheckTheNameIsCorrect(string name)
        {
            if (name == null)
                throw new ArgumentNullException("Имя не должно быть пустым");
            if (name.Length < 3 || name.Length > 20)
                throw new ArgumentException("Имя должно быть от 3 до 20 символов");
            for (int i = 0; i < name.Length; i++) // цикл, в котором мы проверяем, является ли каждый символ буквой, числом или разделителем
                if (!Char.IsLetterOrDigit(name[i]) && !Char.IsSeparator(name[i]))
                    throw new ArgumentException("Имя должно содержать только цифры, буквы или разделители");
            for (int i = 0; i < names?.Count; i++)
                if (names[i] == name)
                    throw new ArgumentException("Это имя уже используется");
        }
        protected static void CheckForNull(User user)
        {
            if (user == null)
                throw new ArgumentNullException("Пользователь не должен быть пустым");
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
        protected static List<string> names;
    }
}