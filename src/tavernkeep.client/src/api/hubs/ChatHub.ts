import {
    HttpTransportType,
    HubConnection,
    HubConnectionBuilder,
} from '@microsoft/signalr';
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
        this.start();
    }

    start() {
        this.connection.start();
    }
}

// Singleton
export default new ChatHub();
