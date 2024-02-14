import type { User } from "@/entities/User";
import type { ApiResponse } from "./ApiResponse";
import type { UserRole } from "@/contracts/enums/UserRole";
import type { Message } from "@/entities/Message";
import type { MessageType } from "@/contracts/enums/MessageType";
import type { Character } from "@/entities/Character";

export interface ApiClient {
    auth(login: string, password: string): Promise<ApiResponse<string>>

    getUsers(): Promise<ApiResponse<User[]>>
    createUser(login: string, password: string, role: UserRole): Promise<ApiResponse<User>>
    deleteUser(id: number): Promise<ApiResponse<null>>

    createCharacter(name: string): Promise<ApiResponse<Character>>
    getCharacter(id: string): Promise<ApiResponse<Character>>

    sendMessage(message: string, type: MessageType): Promise<ApiResponse<Message>>
    getMessages(skip: number, take: number): Promise<ApiResponse<Message[]>>
}