import { useSession } from '@/composables/useSession';
import { DefaultHttpClient, type HttpRequest, HttpResponse } from '@microsoft/signalr';

export class RefreshHttpClient extends DefaultHttpClient {
    constructor() {
        super(console); // the base class wants a signalR.ILogger
    }
    public async send(request: HttpRequest): Promise<HttpResponse> {
        const session = useSession();

        if (session.isExpired.value) {
            const refreshResult = await session.refresh();
            if (refreshResult.status === 'error') throw Error('Refresh failed');
            const token = refreshResult.accessToken;
            request.headers = { ...request.headers, Autorization: `Bearer ${token}` };
        }

        return super.send(request);
    }
}
