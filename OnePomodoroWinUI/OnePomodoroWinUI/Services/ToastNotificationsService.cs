﻿using Microsoft.Toolkit.Uwp.Notifications;
using System;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using System.Linq;
using System.Diagnostics;

namespace OnePomodoro.Services
{
    internal partial class ToastNotificationsService
    {
        private const string ToastTag = "ToastTag";
        private const string PomodoroGroup = "Pomodoro";
        private const string BreakGroup = "Break";
        private const string ToastGroup = "ToastGroup";
        private const string PomodoroTag = "Pomodoro";
        private const string BreakTag = "Break";

        private readonly XmlDocument PomodoroToastContent = GetPomodoroToastContent();

        private readonly XmlDocument BreakToastContent = GetBreakToastContent();

        private int _id;

        public ToastNotificationsService()
        {
            _id = 0;
        }

        private static XmlDocument GetPomodoroToastContent()
        {
            var content = new ToastContent()
            {
                Launch = "ToastContentActivationParams",
                Visual = new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        Children =
                        {
                            new AdaptiveText()
                            {
                                Text = "Pomodoro has finished"
                            },

                            new AdaptiveText()
                            {
                                 Text = @"Pomodoro has finished,let's take a break."
                            }
                        }
                    }
                },
            };

            return content.GetXml();
        }


        private static XmlDocument GetBreakToastContent()
        {
            var content = new ToastContent()
            {
                Launch = "ToastContentActivationParams",
                Visual = new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        Children =
                        {
                            new AdaptiveText()
                            {
                                Text = "Break has ended"
                            },

                            new AdaptiveText()
                            {
                                 Text = @"Break has ended,it's time to work."
                            }
                        }
                    }
                },
            };

            return content.GetXml();
        }

        public void ShowToastNotification(ToastNotification toastNotification)
        {
            try
            {
                ToastNotificationManager.CreateToastNotifier().Show(toastNotification);
            }
            catch (Exception)
            {
                // TODO WTS: Adding ToastNotification can fail in rare conditions, please handle exceptions as appropriate to your scenario.
            }
        }

        public void ShowPomodoroFinishedToastNotification()
        {
            var toast = new ToastNotification(PomodoroToastContent)
            {
                Tag = ToastTag
            };

            ShowToastNotification(toast);
        }

        public void ShowBreakFinishedToastNotification()
        {
            var toast = new ToastNotification(BreakToastContent)
            {
                Tag = ToastTag
            };

            ShowToastNotification(toast);
        }

        public ScheduledToastNotification AddPomodoroFinishedToastNotificationSchedule(DateTime time, bool isRemoveOthers = true)
        {
            if (isRemoveOthers)
                RemovePomodoroFinishedToastNotificationSchedule();

            var notifier = ToastNotificationManager.CreateToastNotifier();

            var toast = new ScheduledToastNotification(PomodoroToastContent, time)
            {
                Tag = ToastTag,
                ExpirationTime = time.AddHours(1),
                Id = _id++.ToString()
            };

            notifier.AddToSchedule(toast);
            Debug.WriteLine("add pomodoro:" + toast.Id);
            return toast;
        }

        public ScheduledToastNotification AddBreakFinishedToastNotificationSchedule(DateTime time, bool isRemoveOthers = true)
        {
            if (isRemoveOthers)
                RemoveBreakFinishedToastNotificationSchedule();

            var notifier = ToastNotificationManager.CreateToastNotifier();

            var toast = new ScheduledToastNotification(BreakToastContent, time)
            {
                Tag = ToastTag,
                ExpirationTime = time.AddHours(1),
                Id = _id++.ToString()
            };

            notifier.AddToSchedule(toast);
            Debug.WriteLine("add break:" + toast.Id);
            return toast;
        }

        public void RemovePomodoroFinishedToastNotificationSchedule(string id = null)
        {
            var notifier = ToastNotificationManager.CreateToastNotifier();
            foreach (var scheduledToast in notifier.GetScheduledToastNotifications())
            {
                if (scheduledToast.Content.InnerText == PomodoroToastContent.InnerText && (string.IsNullOrWhiteSpace(id) || scheduledToast.Id == id))
                {
                    notifier.RemoveFromSchedule(scheduledToast);
                    Debug.WriteLine("remove pomodoro:" + scheduledToast.Id);
                }
            }
        }

        public void RemoveBreakFinishedToastNotificationSchedule(string id = null)
        {
            var notifier = ToastNotificationManager.CreateToastNotifier();
            foreach (var scheduledToast in notifier.GetScheduledToastNotifications())
            {
                if (scheduledToast.Content.InnerText == BreakToastContent.InnerText && (string.IsNullOrWhiteSpace(id) || scheduledToast.Id == id))
                {
                    notifier.RemoveFromSchedule(scheduledToast);
                    Debug.WriteLine("remove break:" + scheduledToast.Id);
                }
            }
        }
    }
}
