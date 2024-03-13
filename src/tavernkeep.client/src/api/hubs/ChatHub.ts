import { useAuthStore } from '@/stores/auth.store';
import { HttpTransportType, HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
class ChatHub {
    private baseURL = 'https://' + window.location.hostname + ':7231/api/';

    connection: HubConnection;

    constructor() {
        this.connection = new HubConnectionBuilder()
            .withUrl(this.baseURL + 'hubs/chat', {
                skipNegotiation: true,
                transport: HttpTransportType.WebSockets,
                async accessTokenFactory() {
                    const authStore = useAuthStore();
                    const accessToken = await authStore.getAccessToken();
                    return accessToken ?? '';
                },
            })
            .build();
    }

    async start(): Promise<void> {
        return this.connection.start().catch((error) => {
            if (!error) return;
            console.log('[ChatHub] Refresh token and try to reconnect');
            const authStore = useAuthStore();
            return authStore.refresh().then(() => this.connection.start());
        });
    }

    async stop() {
        await this.connection.stop();
    }
}

// Singleton
export default new ChatHub();
