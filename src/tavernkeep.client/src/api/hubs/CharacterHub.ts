import { HttpTransportType, HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { RefreshHttpClient } from './RefreshHttpClient';
import { useSession } from '@/composables/useSession';

class CharacterHub {
    private baseURL = 'https://' + window.location.hostname + ':7231/api/';

    connection: HubConnection;

    constructor() {
        this.connection = new HubConnectionBuilder()
            .withUrl(this.baseURL + 'hubs/character', {
                httpClient: new RefreshHttpClient(),
                skipNegotiation: true,
                transport: HttpTransportType.WebSockets,
                async accessTokenFactory() {
                    const session = useSession();
                    if (!session.isAuthenticated.value) throw Error('No access token available. Authorize first.');
                    if (session.isExpired.value) {
                        const refreshResult = await session.refresh();
                        if (refreshResult.status === 'error') throw Error('Refresh failed');
                        return refreshResult.accessToken;
                    }
                    const accessToken = await session.getAccessToken();
                    return accessToken ?? '';
                },
            })
            .build();
    }

    async start(): Promise<void> {
        return this.connection.start();
    }

    async stop() {
        await this.connection.stop();
    }
}

// Singleton
export default new CharacterHub();
