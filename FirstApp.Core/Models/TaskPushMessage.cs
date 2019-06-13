using MvvmCross.Plugin.Messenger;

namespace FirstApp.Core.Models
{
    public class TaskPushMessage : MvxMessage

    {
        public string PushMessage;

        public TaskPushMessage(object sender, string pushMessege)
           : base(sender)
        {
            PushMessage = pushMessege;
        }
    }
}
