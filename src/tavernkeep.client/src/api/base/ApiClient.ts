import type { ApiResponse } from "./ApiResponse";

export interface ApiClient {
    getUsers() : Promise<ApiResponse<User[]>>
    auth(login: string, password: string) : Promise<ApiResponse<string>>
}