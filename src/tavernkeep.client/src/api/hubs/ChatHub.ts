import { HttpTransportType, HubConnection, HubConnectionBuilder } from "@microsoft/signalr"

class ChatHub {
    private baseURL = 'https://192.168.0.102:7231/api/';

    connection: HubConnection

    constructor() {
        this.connection = new HubConnectionBuilder()
            .withUrl(this.baseURL + "hubs/chat", {
                skipNegotiation: true,
                transport: HttpTransportType.WebSockets
            })
            .build();
        this.start()
    }

    start() {
        this.connection.start();
    }
}

// Singleton
export default new ChatHub();