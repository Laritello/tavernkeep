import {
    HttpTransportType,
    HubConnection,
    HubConnectionBuilder,
} from '@microsoft/signalr';
import { getCookie } from 'typescript-cookie';

class CharacterHub {
    private baseURL = 'https://' + window.location.hostname + ':7231/api/';

    connection: HubConnection;

    constructor() {
        this.connection = new HubConnectionBuilder()
            .withUrl(this.baseURL + 'hubs/character', {
                skipNegotiation: true,
                transport: HttpTransportType.WebSockets,
                accessTokenFactory() {
                    const jwt = getCookie('taverkeep.auth.jwt');
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
export default new CharacterHub();
