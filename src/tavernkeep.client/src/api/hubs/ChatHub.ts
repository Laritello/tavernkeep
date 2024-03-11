import { HttpTransportType, HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { getCookie } from 'typescript-cookie';

class ChatHub {
    private baseURL = 'https://' + window.location.hostname + ':7231/api/';

    connection: HubConnection;

    constructor() {
        this.connection = new HubConnectionBuilder()
            .withUrl(this.baseURL + 'hubs/chat', {
                skipNegotiation: true,
                transport: HttpTransportType.WebSockets,
                accessTokenFactory() {
                    const jwt = getCookie('tavernkeep.auth.jwt');
                    return jwt != undefined ? jwt : '';
                },
            })
            .build();
    }

    async start() {
        await this.connection.start();
    }

    async stop() {
        await this.connection.stop();
    }
}

// Singleton
export default new ChatHub();
