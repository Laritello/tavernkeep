import type { ApiResponse } from '../base/ApiResponse';

export class AxiosApiResponse<T> implements ApiResponse<T> {
    data: T;
    status: number;
    statusText: string;

    constructor(data: T, status: number, statusText: string) {
        this.data = data;
        this.status = status;
        this.statusText = statusText;
    }

    isSuccess(): boolean {
        return this.status >= 200 && this.status < 300;
    }
}
