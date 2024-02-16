import type { User } from "@/entities/User";
import type { ApiResponse } from "./ApiResponse";
import type { UserRole } from "@/contracts/enums/UserRole";
import type { Message } from "@/entities/Message";
import type { MessageType } from "@/contracts/enums/MessageType";
import type { Character } from "@/entities/Character";
import type { SkillType } from "@/contracts/enums/SkillType";
import type { Proficiency } from "@/contracts/enums/Proficiency";
import type { Skill } from "@/contracts/character/Skill";
import type { AbilityType } from "@/contracts/enums/AbilityType";
import type { Ability } from "@/contracts/character/Ability";

export interface ApiClient {
    auth(login: string, password: string): Promise<ApiResponse<string>>

    getUsers(): Promise<ApiResponse<User[]>>
    createUser(login: string, password: string, role: UserRole): Promise<ApiResponse<User>>
    deleteUser(id: number): Promise<ApiResponse<null>>

    createCharacter(name: string): Promise<ApiResponse<Character>>
    getCharacter(id: string): Promise<ApiResponse<Character>>

    editAbility(characterId: string, type: AbilityType, score: number) : Promise<ApiResponse<Ability>>
    editSkill(characterId: string, type: SkillType, proficiency: Proficiency) : Promise<ApiResponse<Skill>>

    sendMessage(message: string, type: MessageType): Promise<ApiResponse<Message>>
    getMessages(skip: number, take: number): Promise<ApiResponse<Message[]>>
}