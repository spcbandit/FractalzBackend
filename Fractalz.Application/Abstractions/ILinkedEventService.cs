using Fractalz.Application.Domains.Entities.Chat;
using Fractalz.Application.Domains.Entities.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using Fractalz.Application.Domains.MappingEntities.Chat;
using Fractalz.Application.Domains.Requests.Notification;

namespace Fractalz.Application.Abstractions
{
    public interface ILinkedEventService
    {
        public delegate void SendNotification(SendNotificationRequest message);

        public  event SendNotification SendNotificationEvent;
        public delegate void GetMessage(MessageMappedDto message);

        public  event GetMessage GetMessageEvent;

        public delegate void UserUpdateStatus(User user);

        public event UserUpdateStatus UserUpdateStatusEvent;

        public delegate void DialogUpdate(DialogsMappedDto dialog);

        public event DialogUpdate DialogUpdateEvent;

        public void InvokeSendNotification(SendNotificationRequest message);
        public void InvokeGetMessage(MessageMappedDto message);

        public void InvokeUserUpdateStatus(User message);

        public void InvokeDialogUpdate(DialogsMappedDto message);  
    }
}
