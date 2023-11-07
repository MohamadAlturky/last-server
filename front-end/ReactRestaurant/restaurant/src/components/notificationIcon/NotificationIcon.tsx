import SignalRService from "../../services/SignalRService";
import { useNavigate } from "react-router-dom";
import notification from "./../../assets/plate.svg";
import "./notificationIcon.css";
import { useEffect, useState } from "react";
export default function NotificationIcon() {
  const navigate = useNavigate();
  const [notificationsPage, setNotificationsPage] = useState<NotificationsPage>(
    { count: 0, notificationMessages: [] }
  );
  useEffect(() => {
    SignalRService.startConnection();

    SignalRService.addNotificationListener(
      (messageSubject: string, messageContent: string, sentAt: string) => {
        let notification: NotificationMessage = {
          messageSubject: messageSubject,
          messageContent: messageContent,
          sentAt: sentAt,
          id: 0,
        };
        setNotificationsPage((prevNotifications) => {
          return {
            count: prevNotifications.count + 1,
            notificationMessages: [
              notification,
              ...prevNotifications.notificationMessages,
            ],
          };
        });
      }
    );
    return () => {
      SignalRService.stopConnection();
    };
  }, []);

  return (
    <div
      className="notification"
      onClick={() => {
        setNotificationsPage({
          count: 0,
          notificationMessages: [],
        });
      }}
    >
      {notificationsPage.count != 0 && (
        <div className="number-of-unread">{notificationsPage.count}</div>
      )}
      <img
        src={notification}
        alt=""
        onClick={() => {
          navigate("/NotificationReciever");
        }}
      />
    </div>
  );
}
