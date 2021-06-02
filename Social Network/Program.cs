using System;
using System.Collections.Generic;

namespace Social_Networks
{
    class Program
    {
        static void Main(string[] args)
        {
            List<SocialNetwork> socialNetworks = new List<SocialNetwork>();
            List<GroupOfFriends> groupOfFriends = new List<GroupOfFriends>();
            List<User> users = new List<User>();

            bool alive = true;
            while (alive) // Command loop
            {
                Console.WriteLine("Список команд :");
                Console.WriteLine("\t1. Создать пользователя\n\t2. Создать социальную сеть\n\t3. Создать группу друзей");
                Console.WriteLine("\t4. Перейти к пользовательскому контролю\n\t5. Очистить консоль\n\t6. Выйти из программы");
                Console.WriteLine("Введите номер команды :");
                try
                {
                    int command = Convert.ToInt32(Console.ReadLine());
                    switch (command)
                    {
                        case 1:
                            Create.User(users);
                            break;
                        case 2:
                            Create.SocialNetwork(socialNetworks, users);
                            break;
                        case 3:
                            Create.GroupOfFriends(socialNetworks, groupOfFriends, users);
                            break;
                        case 4:
                            Control.User(socialNetworks, groupOfFriends, users);
                            break;
                        case 5:
                            Console.Clear();
                            break;
                        case 6:
                            alive = false;
                            continue;
                        default:
                            throw new ArgumentException("Неверный номер команды, попробуйте еще раз");
                    }
                }
                catch (Exception e) { Message.ExceptionHandler(e); }
            }
        }
    }
}