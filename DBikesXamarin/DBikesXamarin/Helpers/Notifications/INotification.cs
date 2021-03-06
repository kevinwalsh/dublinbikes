﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DBikesXamarin.Helpers.Notifications
{
    public interface INotification
    {
        void Notify(string title, string message, bool ispriority);
        void ClearNotifications();
        void ToastNotify(string message);
    }
}
