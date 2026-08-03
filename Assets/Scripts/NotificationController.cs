using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif

public class NotificationController : MonoBehaviour
{
    public void ActivarNotificacion()
    {
        DateTime dateActive = DateTime.Now.AddSeconds(5f);
#if UNITY_ANDROID

        CreateNotification(dateActive);
#endif

    }

#if UNITY_ANDROID
    private const string idChanel = "canalNotificacion";

    public static NotificationController instance;

    private void Start()
    {
        instance = this;
        StartCoroutine(PermisoNotificacion());

    }

    public void CreateNotification(DateTime date)
    {
        AndroidNotificationChannel androidNotificationChannel = new AndroidNotificationChannel
        {
            Id = idChanel,
            Name = "CanalNotificacion",
            Description = "Canal para notificaciones",
            Importance = Importance.Default
        };

        AndroidNotificationCenter.RegisterNotificationChannel(androidNotificationChannel);

        AndroidNotification androidNotification = new AndroidNotification
        {
            Title = "Tienes un corazon, puedes volver a jugar :D",
            Text = "Notificacion de volver a jugar",
            SmallIcon = "default",
            LargeIcon = "default",
            FireTime = date
        };

        AndroidNotificationCenter.SendNotification(androidNotification, idChanel);
    }

    IEnumerator PermisoNotificacion()
    {
        var request = new PermissionRequest();
        while (request.Status == PermissionStatus.RequestPending)
        {
            yield return null;
        }
    }
#endif

}
