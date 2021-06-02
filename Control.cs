using System;
using System.Collections.Generic;

namespace Social_Networks
{
    struct Control
    {
        public static void User(List<SocialNetwork> socialNetworks, List<GroupOfFriends> groupOfFriends, List<User> users)
        {
            if (users.Count == 0)
                throw new ArgumentException("Нужен пользователь");
            Console.WriteLine("Выберите пользователя, которым хотите управлять");
            User user = Choose.User(users);
            Console.Clear();
            if (user.GetUserInvitationSenders().Count != 0)
                Message.SuccessOperationHandler("У вас есть новые приглашения!\nПроверить список приглашений");

            bool alive = true;
            while (alive) // Цикл команд
            {
                Console.WriteLine("Список команд :");
                Console.WriteLine("\t1. Отправить приглашение\n\t2. Ответить на приглашение\n\t3. Удалить из друзей");
                Console.WriteLine("\t4. Добавить социальную сеть\n\t5. Удалить социальную сеть\n\t6. Удалить группу друзей");
                Console.WriteLine("\t7. Добавить друга в группу друзей\n\t8. Проверить список социальных сетей\n\t9. Проверить группу друзей в списке");
                Console.WriteLine("\t10. Проверить список друзей\n\t11. Проверить приглашения\n\t12. Вернуться в главное меню");
                Console.WriteLine("Введите номер команды :");
                int command = Convert.ToInt32(Console.ReadLine());
                try
                {
                    switch (command)
                    {
                        case 1:
                            SendAnInvitation(user, users);
                            break;
                        case 2:
                            RespondToTheInvitation(user, users);
                            break;
                        case 3:
                            RemoveFriend(user, users);
                            break;
                        case 4:
                            JoinSocialNetwork(user, users, socialNetworks);
                            break;
                        case 5:
                            RemoveSocialNetwork(user, users, socialNetworks);
                            break;
                        case 6:
                            {
                                Console.WriteLine("Выберите группу друзей");
                                GroupOfFriends groupOfFriends1 = Choose.GroupOfFriends(groupOfFriends);
                                user.RemoveGroupOfFriends(groupOfFriends1);
                                Message.SuccessOperationHandler("Операция прошла успешно");
                            }
                            break;
                        case 7:
                            {
                                Console.WriteLine("Выберите группу друзей");
                                GroupOfFriends groupOfFriends1 = Choose.GroupOfFriends(groupOfFriends);

                                Console.WriteLine("Выберите друга из списка друзей :");
                                string friend = Choose.User(user.GetUserFriendNicknames());
                                for (int i = 0; i < users.Count; i++)
                                    if (users[i].GetUserName() == friend)
                                        user.JoinGroupOfFriends(users[i], groupOfFriends1);
                                Message.SuccessOperationHandler($"Друг {friend} успешно добавлен в {groupOfFriends1.Name}");
                            }
                            break;
                        case 8:
                            {
                                Console.WriteLine("Ваш список социальных сетей :");
                                List<string> names = user.GetUserSocialNetworkNames();
                                for (int i = 0; i < names.Count; i++)
                                    Console.WriteLine($"\t{i + 1}. " + names[i]);
                            }
                            break;
                        case 9:
                            {
                                Console.WriteLine("Список вашей группы друзей :");
                                List<string> names = user.GetUserGroupOfFriendsNames();
                                for (int i = 0; i < names.Count; i++)
                                    Console.WriteLine($"\t{i + 1}. " + names[i]);
                            }
                            break;
                        case 10:
                            {
                                Console.WriteLine("Ваш список друзей :");
                                List<string> names = user.GetUserFriendNicknames();
                                for (int i = 0; i < names.Count; i++)
                                    Console.WriteLine($"\t{i + 1}. " + names[i]);
                            }
                            break;
                        case 11:
                            {
                                Console.WriteLine("Ваш список приглашений :");
                                List<string> names = user.GetUserInvitationSenders();

                                List<string> message = new List<string>();
                                for (int i = 0; i < user.Invitations?.Count; i++)
                                    message.Add(user.Invitations[i].Message);
                                for (int i = 0; i < names.Count; i++)
                                    Console.WriteLine($"\t{i + 1}. " + names[i] + "\n\tСообщение : " + message[i]);
                            }
                            break;
                        case 12:
                            alive = false;
                            break;
                        default:
                            throw new ArgumentException("Неверный номер команды, попробуйте ещё раз");
                    }
                }
                catch (Exception e) { Message.ExceptionHandler(e); }
            }
        }
        
        private static void SendAnInvitation(User user, List<User> users)
        {
            Console.WriteLine("Выберите, кому отправить приглашение");
            User recipient = Choose.User(users);

            string message = null;

            Console.WriteLine("Хотите написать сообщение, которое будет в приглашении ?");
            Console.WriteLine("\t1. Да\n\t2. Нет");
            Console.WriteLine("Введите номер команды :");

            int command1 = Convert.ToInt32(Console.ReadLine());
            switch (command1)
            {
                case 1:
                    Console.WriteLine("Ваше сообщение :");
                    message = Console.ReadLine();
                    break;
                case 2:
                    break;
                default:
                    throw new ArgumentException("Неверный номер команды, попробуйте еще раз");
            }
            user.InviteToFriends(recipient, message);
            Message.SuccessOperationHandler("Приглашение отправлено");
        }
        private static void RespondToTheInvitation(User user, List<User> users)
        {
            Console.WriteLine("Выберите отправителя приглашения :");
            string sender = Choose.User(user.GetUserInvitationSenders(), "Необходимо наличие приглашения и отправителя");

            Console.WriteLine("Принять или отклонить приглашение ?");
            Console.WriteLine("\t1. Приянть\n\t2. Отклонить");
            Console.WriteLine("Введите номер команды :");
            int command1 = Convert.ToInt32(Console.ReadLine());
            switch (command1)
            {
                case 1:
                    for (int i = 0; i < user.Invitations.Count; i++)
                        if (user.Invitations[i].GetUserName() == sender)
                            user.RespondToTheInvitation(user.Invitations[i], Invitation.Respond.Accept);
                    break;
                case 2:
                    for (int i = 0; i < user.Invitations.Count; i++)
                        if (user.Invitations[i].GetUserName() == sender)
                            user.RespondToTheInvitation(user.Invitations[i], Invitation.Respond.Decline);
                    break;
                default:
                    throw new ArgumentException("Неверный номер команды, попробуйте еще раз");
            }
            if (command1 == 1)
                Message.SuccessOperationHandler("Приглашение принято");
            if (command1 == 2)
                Message.SuccessOperationHandler("Приглашение отклонено");
        }
        private static void RemoveFriend(User user, List<User> users)
        {
            Console.WriteLine("Выберите друга, которого хотите удалить из списка друзей :");
            string friend = Choose.User(user.GetUserFriendNicknames(), "У вас нет друзей");
            for (int i = 0; i < users.Count; i++)
                if (users[i].GetUserName() == friend)
                {
                    user.RemoveFriend(users[i]);
                    users[i].RemoveFriend(user);
                }
            Message.SuccessOperationHandler($"Друг {friend} успешно удален");
        }
        private static void JoinSocialNetwork(User user, List<User> users, List<SocialNetwork> socialNetworks)
        {
            Console.WriteLine("Выберите социальную сеть :");
            SocialNetwork socialNetwork = Choose.SocialNetwork(socialNetworks, users);

            Console.WriteLine("Вы хотите добавить пользователя или себя?");
            Console.WriteLine("\t1. Себя\n\t2. Пользователя");
            Console.WriteLine("Введите номер команды :");
            int command1 = Convert.ToInt32(Console.ReadLine());
            switch (command1)
            {
                case 1:
                    user.JoinSocialNetwork(socialNetwork);
                    break;
                case 2:
                    {
                        Console.WriteLine("Выберите друга из списка друзей :");
                        user.JoinSocialNetwork(socialNetwork, Choose.User(users));
                    }
                    break;
                default:
                    throw new ArgumentException("Неверный номер команды, попробуйте ещё раз");
            }
            Message.SuccessOperationHandler("Операция прошла успешно");
        }
        private static void RemoveSocialNetwork(User user, List<User> users, List<SocialNetwork> socialNetworks)
        {
            Console.WriteLine("Выберите социальную сеть :");
            SocialNetwork socialNetwork = Choose.SocialNetwork(socialNetworks, users);

            Console.WriteLine("Вы хотите удалить пользователя или себя ?");
            Console.WriteLine("\t1. Себя\n\t2. Пользователя");
            Console.WriteLine("Введите номер команды :");
            int command1 = Convert.ToInt32(Console.ReadLine());
            switch (command1)
            {
                case 1:
                    user.RemoveSocialNetwork(socialNetwork);
                    break;
                case 2:
                    {
                        Console.WriteLine("Выберите друга из списка друзей :");
                        user.RemoveSocialNetwork(socialNetwork, Choose.User(users));
                    }
                    break;
                default:
                    throw new ArgumentException("Неверный номер команды, попробуйте ещё раз");
            }
            Message.SuccessOperationHandler("Операция прошла успешно");
        }
    }
}