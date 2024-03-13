import { DefaultHttpClient, type HttpRequest, HttpResponse, HttpError } from '@microsoft/signalr';
import { useAuthStore } from '@/stores/auth.store';

export class RefreshHttpClient extends DefaultHttpClient {
    constructor() {
        super(console); // the base class wants a signalR.ILogger
    }
    public async send(request: HttpRequest): Promise<HttpResponse> {
        const authStore = useAuthStore();
        const token = await authStore.getAccessToken();
        request.headers = { ...request.headers, Autorization: `Bearer ${token}` };

        try {
            const response = await super.send(request);
            return response;
        } catch (er) {
            if (er instanceof HttpError) {
                const error = er as HttpError;
                if (error.statusCode == 401) {
                    const { accessToken } = await authStore.refresh();
                    request.headers = { ...request.headers, Autorization: `Bearer ${accessToken}` };
                }
            } else {
                throw er;
            }
        }
        //re try the request
        return super.send(request);
    }
}
