import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import url from "../configurations/apiConfiguration.json";

class SignalRService {
  private connection: HubConnection | null = null;

  private readonly hubUrl: string = url.baseUrl + "notifications";

  startConnection = () => {
    this.connection = new HubConnectionBuilder().withUrl(this.hubUrl).build();
    console.log(this.hubUrl);
    this.connection
      .start()
      .then(() => {
        console.log("SignalR Connection started");
      })
      .catch((err) => {
        console.error("Error starting SignalR connection:", err);
      });
    console.log(this.connection);
  };

  stopConnection = () => {
    if (this.connection) {
      this.connection.stop();
    }
  };

  addNotificationListener = (
    callback: (
      messageSubject: string,
      messageContent: string,
      sentAt: string
    ) => void
  ) => {
    if (this.connection) {
      this.connection.on("RecieveMessage", callback);
    }
  };
}

export default new SignalRService();
