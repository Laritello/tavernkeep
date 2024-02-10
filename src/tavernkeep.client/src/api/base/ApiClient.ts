import type { ApiResponse } from "./ApiResponse";

export interface ApiClient {
    getUsers() : Promise<ApiResponse<User[]>>
}