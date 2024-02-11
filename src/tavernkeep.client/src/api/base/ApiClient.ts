import type { User } from "@/entities/User";
import type { ApiResponse } from "./ApiResponse";
import type { UserRole } from "@/contracts/enums/UserRole";

export interface ApiClient {
    getUsers() : Promise<ApiResponse<User[]>>
    createUser(login: string, password: string, role: UserRole) : Promise<ApiResponse<User>>
    deleteUser(id: number) : Promise<ApiResponse<null>>
    auth(login: string, password: string) : Promise<ApiResponse<string>>
}