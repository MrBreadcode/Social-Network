using System;
using System.Collections.Generic;

namespace Social_Networks
{
    struct Create
    {
        public static void User(List<User> users)
        {
            Console.Clear();
            Console.WriteLine("Введите ник пользователя :");
            string nickname = Console.ReadLine();
            User user = new User(nickname);
            Message.SuccessOperationHandler("Пользователь успешно создан");
            user.AddedNewSocialWork += Message.EventHandler;
            user.AddedNewGroupOfFriends += Message.EventHandler;
            user.AddedNewFriend += Message.EventHandler;
            user.RemovedSocialWork += Message.EventHandler;
            user.RemovedGroupOfFriends += Message.EventHandler;
            user.RemovedFriend += Message.EventHandler;
            users.Add(user);
        }

        public static void GroupOfFriends(List<SocialNetwork> socialNetworks, List<GroupOfFriends> groupOfFriends, List<User> users)
        {
            if (users.Count == 0)
                throw new ArgumentException("Нужен пользователь");
            if (socialNetworks.Count == 0)
                throw new ArgumentException("Эта операция требует наличия социальной сети");

            Console.Clear();
            Console.WriteLine("Выберите пользователя, который станет первым членом этой группы друзей");
            User firstmember = Choose.User(users);

            Console.WriteLine("Выберите социальную сеть, в которой будет создана группа");
            SocialNetwork socialNetwork = Choose.SocialNetwork(socialNetworks, users);

            Console.WriteLine("Введите имя группы друзей :");
            string name = Console.ReadLine();
            GroupOfFriends groupOfFriends1 = new GroupOfFriends(name, firstmember, socialNetwork);

            Message.SuccessOperationHandler("Группа друзей создана успешно");
            groupOfFriends.Add(groupOfFriends1);
        }

        public static void SocialNetwork(List<SocialNetwork> socialNetworks, List<User> users)
        {
            if (users.Count == 0)
                throw new ArgumentException("Нужен пользователь");

            Console.Clear();
            Console.WriteLine("Выберите пользователя, который будет администратором этой сети");
            User admin = Choose.User(users);

            Console.WriteLine("Введите название социальной сети :");
            string name = Console.ReadLine();
            SocialNetwork socialNetwork = new SocialNetwork(name, admin);
            Message.SuccessOperationHandler("Социальная сеть успешно создана");

            socialNetworks.Add(socialNetwork);
        }
    }
}