using System;

namespace Social_Networks
{
    public class Invitation : UserEventsArgs
    {
        public Invitation(User sender, string message = null) : base(message, sender)
        {
            if (sender == null)
                throw new ArgumentNullException("Sender must not be null");
            if (message == null)
                Message = DefaultMessage(sender);
            else
            {
                if (message.Length > 100)
                    throw new ArgumentException("Message must be no more than 100 characters");
                Message = message;
            }
        }
        private string DefaultMessage(User sender)
        {
            string message = $"Привет, мое имя {sender.GetUserName()} и ";
            message += $"Я хочу добавить тебя в друзья";
            return message;
        }
        public enum Respond
        {
            Accept,
            Decline
        }
        internal User Sender { get { return user; } }
    }
}