using System;
using System.Collections.Generic;

namespace Social_Networks
{
    struct Choose
    {
        public static SocialNetwork SocialNetwork(List<SocialNetwork> socialNetworks, List<User> users)
        {
            if (users.Count == 0)
                throw new ArgumentException("Нужен пользователь");
            if (socialNetworks.Count == 0)
                throw new ArgumentException("Нужны социальные сети");
            for (int i = 0; i < socialNetworks.Count; i++)
                Console.WriteLine($"\t{i + 1}. " + socialNetworks[i].Name);
            Console.WriteLine("Введите номер команды :");
            SocialNetwork socialNetwork = null;
            bool alive = true;
            int command = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < socialNetworks.Count; i++)
                if (command == i + 1) // Цикл, в котором ищем социальную сеть
                {
                    socialNetwork = socialNetworks[i];
                    alive = false;
                }
            if (alive)
                throw new ArgumentException("Неверный номер команды, попробуйте ещё раз");

            return socialNetwork;
        }

        public static GroupOfFriends GroupOfFriends(List<GroupOfFriends> groupOfFriends)
        {
            for (int i = 0; i < groupOfFriends.Count; i++)
                if (groupOfFriends[i].GetMembersInfo().Count == 0)
                    groupOfFriends.RemoveAt(i);
            
            if (groupOfFriends.Count == 0)
                throw new ArgumentException("Нужна группа друзей");
            for (int i = 0; i < groupOfFriends.Count; i++)
                Console.WriteLine($"\t{i + 1}. " + groupOfFriends[i].Name);
            Console.WriteLine("Введите номер команды :");
            GroupOfFriends groupOfFriends1 = null;
            bool alive = true;

            int command = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < groupOfFriends.Count; i++)
                if (command == i + 1) // Цикл, в котором мы ищем компанию друзей
                {
                    groupOfFriends1 = groupOfFriends[i];
                    alive = false;
                }
            if (alive)
                throw new ArgumentException("Неверный номер команды, попробуйте ещё раз");

            return groupOfFriends1;
        }

        public static User User(List<User> users)
        {
            if (users.Count == 0)
                throw new ArgumentException("Нужен пользователь");
            for (int i = 0; i < users.Count; i++)
                Console.WriteLine($"\t{i + 1}. " + users[i].GetUserName());
            Console.WriteLine("Введите номер команды :");
            User user = null;
            bool alive = true;

            int command = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < users.Count; i++)
                if (command == i + 1) // Цикл, в котором мы ищем пользователя
                {
                    user = users[i];
                    alive = false;
                }
            if (alive)
                throw new ArgumentException("Неверный номер команды, попробуйте ещё раз");

            return user;
        }

        public static string User(List<string> usernames, string message = "Нужен пользователь")
        {
            if (usernames.Count == 0)
                throw new ArgumentException(message);
            for (int i = 0; i < usernames.Count; i++)
                Console.WriteLine($"\t{i + 1}. " + usernames[i]);
            Console.WriteLine("Введите номер команды :");
            string user = null;
            bool alive = true;

            int command = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < usernames.Count; i++)
                if (command == i + 1) // Цикл, в котором мы ищем пользователя
                {
                    user = usernames[i];
                    alive = false;
                }
            if (alive)
                throw new ArgumentException("Неверный номер команды, попробуйте еще раз");
 
            return user;
        }
    }
}