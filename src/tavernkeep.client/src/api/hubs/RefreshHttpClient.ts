import { DefaultHttpClient, type HttpRequest, HttpResponse } from '@microsoft/signalr';

import { useSession } from '@/composables/useSession';

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
            request.headers = { ...request.headers, Authorization: `Bearer ${token}` };
        }

        return super.send(request);
    }
}
