import {
  HttpTransportType,
  HubConnection,
  HubConnectionBuilder,
} from "@microsoft/signalr";

class ChatHub {
  private baseURL = "https://" + window.location.hostname + ":7231/api/";

  connection: HubConnection;

  constructor() {
    this.connection = new HubConnectionBuilder()
      .withUrl(this.baseURL + "hubs/chat", {
        skipNegotiation: true,
        transport: HttpTransportType.WebSockets,
      })
      .build();
    this.start();
  }

  start() {
    this.connection.start();
  }
}

// Singleton
export default new ChatHub();
