using System;
using System.Net;
using System.Net.WebSockets;
using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Entities.Chat;
using Fractalz.Application.Domains.Entities.Profile;
using Fractalz.Application.Domains.MappingEntities.Chat;
using Fractalz.Application.Domains.Requests.Notification;

namespace Fractalz.Infrastructure.LinkedEvent
{
    public class LinkedEventService : ILinkedEventService
    {
        public delegate void SendNotification(SendNotificationRequest message);
        public  event ILinkedEventService.SendNotification SendNotificationEvent;
        
        public delegate void GetMessage(Message message);

        public  event ILinkedEventService.GetMessage GetMessageEvent;

        public delegate void UserUpdateStatus(User user);

        public event ILinkedEventService.UserUpdateStatus UserUpdateStatusEvent;

        public delegate void DialogUpdate(DialogsMappedDto dialog);

        public event ILinkedEventService.DialogUpdate DialogUpdateEvent;

        public void InvokeSendNotification(SendNotificationRequest message) =>
            SendNotificationEvent?.Invoke(message);
        
        public void InvokeGetMessage(MessageMappedDto message) =>
            GetMessageEvent?.Invoke(message);

        public void InvokeUserUpdateStatus(User message) =>
            UserUpdateStatusEvent?.Invoke(message);

        public void InvokeDialogUpdate(DialogsMappedDto message) =>
            DialogUpdateEvent?.Invoke(message);

    }
}