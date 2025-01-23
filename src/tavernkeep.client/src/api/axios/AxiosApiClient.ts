import axios, { type AxiosInstance, type AxiosResponse } from 'axios';
import AxiosAuthInterceptors from './AxiosAuthInterceptors';

import type { AuthenticationResponse } from '@/contracts/auth/AuthenticationResponse';
import type { User, Message, Character, SkillRollMessage } from '@/entities';
import type { Health, Lore, Perception } from '@/contracts/character';
import { UserRole, Proficiency, RollType, ArmorType, SpeedType, SkillType } from '@/contracts/enums';
import type { Condition } from '@/entities/Condition';
import type { SavingThrowRollMessage } from '@/entities/Message';
import type { ConditionShortDto } from '@/contracts/conditions/ConditionShortDto';
import type { CharacterInformationEditDto, SpeedEditDto } from '@/contracts/dtos';

export class AxiosApiClient {
    client: AxiosInstance;
    private baseURL = 'http://' + window.location.hostname + ':5207/api/';

    constructor() {
        this.client = axios.create({
            baseURL: this.baseURL,
            timeout: 5000, // 5 seconds
            headers: {
                'Content-Type': 'application/json',
            },
        });

        const { request, response } = AxiosAuthInterceptors(this.client);

        this.client.interceptors.request.use(request);
        this.client.interceptors.response.use(undefined, response);
    }

    async auth(login: string, password: string): Promise<AuthenticationResponse> {
        const response = await this.client.post<AuthenticationResponse>('authentication/auth', {
            login: login,
            password: password,
        });

        return getPayloadOrThrow(response);
    }

    async refresh(accessToken: string, refreshToken: string): Promise<AuthenticationResponse> {
        const response = await this.client.post<AuthenticationResponse>('authentication/refresh', {
            accessToken: accessToken,
            refreshToken: refreshToken,
        });

        return getPayloadOrThrow(response);
    }

    async getUsers(): Promise<Record<string, User>> {
        const response = await this.client.get<Record<string, User>>('users');
        return getPayloadOrThrow(response);
    }

    async getCurrentUser(): Promise<User> {
        const response = await this.client.get<User>('users/current');
        return getPayloadOrThrow(response);
    }

    // TODO: User request types instead of separate parameters
    async createUser(
        login: string,
        password: string,
        role: UserRole,
        initializeCharacter: boolean,
        characterName?: string
    ): Promise<User> {
        const response = await this.client.post<User>('users', {
            login,
            password,
            role,
            initializeCharacter,
            characterName,
        });
        return getPayloadOrThrow(response);
    }

    async editUser(id: string, login: string, password: string, role: UserRole): Promise<User> {
        const response = await this.client.patch<User>('users/' + id, {
            login: login,
            password: password,
            role: role,
        });
        return getPayloadOrThrow(response);
    }

    async deleteUser(id: string): Promise<void> {
        const response = await this.client.delete('users/' + id);
        return getPayloadOrThrow(response);
    }

    async setActiveCharacter(userId: string, characterId: string): Promise<User> {
        const response = await this.client.put<User>('users/active-character', {
            userId,
            characterId,
        });
        return getPayloadOrThrow(response);
    }

    async getCharacters(): Promise<Record<string, Character>> {
        const response = await this.client.get<Record<string, Character>>('characters');
        return getPayloadOrThrow(response);
    }

    async createCharacter(ownerId: string, name: string): Promise<Character> {
        const response = await this.client.post<Character>('characters', { ownerId, name });
        return getPayloadOrThrow(response);
    }

    async deleteCharacter(id: string): Promise<void> {
        const response = await this.client.delete('characters/' + id);
        return getPayloadOrThrow(response);
    }

    async getCharacter(id: string): Promise<Character> {
        const response = await this.client.get<Character>('characters/' + id);
        return getPayloadOrThrow(response);
    }

    // TODO: This method need to be updated. API method moved to the user controller
    async assignUserToCharacter(characterId: string, userId: string): Promise<Character> {
        const response = await this.client.patch<Character>('characters/assign', {
            characterId: characterId,
            userId: userId,
        });
        return getPayloadOrThrow(response);
    }

    async createLore(characterId: string, topic: string, proficiency: Proficiency): Promise<Lore> {
        const response = await this.client.post<Lore>('lore', {
            characterId: characterId,
            topic: topic,
            proficiency: proficiency,
        });

        return getPayloadOrThrow(response);
    }

    async editLore(characterId: string, topic: string, proficiency: Proficiency): Promise<Lore> {
        const response = await this.client.patch<Lore>('lore', {
            characterId: characterId,
            topic: topic,
            proficiency: proficiency,
        });

        return getPayloadOrThrow(response);
    }

    async deleteLore(characterId: string, topic: string): Promise<void> {
        const response = await this.client.delete('lore/' + characterId, { params: { topic: topic } });

        return getPayloadOrThrow(response);
    }

    async editInformation(characterId: string, information: CharacterInformationEditDto): Promise<void> {
        const response = await this.client.patch(`characters/${characterId}/information`, information);

        return getPayloadOrThrow(response);
    }

    async editHeroPoints(characterId: string, amount: number): Promise<void> {
        const response = await this.client.patch(`characters/${characterId}/hero-points`, null, {
            params: {
                amount: amount,
            }
        });
        return getPayloadOrThrow(response);
    }

    async editAbilities(characterId: string, scores: Record<string, number>): Promise<void> {
        const response = await this.client.patch(`characters/${characterId}/abilities`, {
            scores: scores,
        });

        return getPayloadOrThrow(response);
    }

    async editSkills(characterId: string, proficiencies: Record<string, Proficiency>): Promise<void> {
        const response = await this.client.patch(`characters/${characterId}/skills`, {
            proficiencies: proficiencies,
        });

        return getPayloadOrThrow(response);
    }

    async createSkill(characterId: string, type: SkillType, baseAbility: string, name: string ): Promise<void> {
        const response = await this.client.post(`custom/skill`, {
            type,
            baseAbility,
            name,
        }, {
            params: {
                characterId,
            }
        });

        return getPayloadOrThrow(response);
    }

    async createCustomSkill(characterId: string, baseAbility: string, name: string ): Promise<void> {
        return this.createSkill(characterId, SkillType.Custom, baseAbility, name);
    }

    async createLoreSkill(characterId: string, name: string ): Promise<void> {
        return this.createSkill(characterId, SkillType.Lore, 'Intelligence', name);
    }

    async editSavingThrows(characterId: string, proficiencies: Record<string, Proficiency>): Promise<void> {
        const response = await this.client.patch(`characters/${characterId}/saving-throws`, {
            proficiencies: proficiencies,
        });

        return getPayloadOrThrow(response);
    }

    async editSpeeds(characterId: string, speeds: Record<SpeedType, SpeedEditDto>): Promise<void> {
        const response = await this.client.patch(`characters/${characterId}/speeds`, {
            speeds: speeds,
        });

        return getPayloadOrThrow(response);
    }

    async editPerception(characterId: string, proficiency: Proficiency): Promise<Perception> {
        const response = await this.client.patch<Perception>(`characters/${characterId}/perception`, {
            proficiency: proficiency,
        });

        return getPayloadOrThrow(response);
    }

    async editConditions(characterId: string, conditions: ConditionShortDto[]): Promise<void> {
        const response = await this.client.patch(`characters/${characterId}/conditions`, {
            conditions: conditions,
        });

        return getPayloadOrThrow(response);
    }

    async editArmor(characterId: string, type: ArmorType, bonus: number, hasDexterityCap: boolean, dexterityCap: number, proficiencies: Record<ArmorType, Proficiency>): Promise<void> {
        const response = await this.client.patch(`characters/${characterId}/armor`, {
            type: type,
            bonus: bonus,
            hasDexterityCap: hasDexterityCap,
            dexterityCap: dexterityCap,
            proficiencies: proficiencies
        });

        return getPayloadOrThrow(response);
    }

    async performLongRest(characterId: string, noComfort: boolean, inArmor: boolean): Promise<void> {
        // TODO: Toast notification for successfull long rest
        const response = await this.client.patch(`characters/${characterId}/long-rest`, null, {
            params: {
                restWithoutComfort: noComfort,
                sleepInArmor: inArmor
            }
        });
        return getPayloadOrThrow(response);
    }

    async sendMessage(content: string, recipientId?: string): Promise<Message> {
        const response = await this.client.post<Message>('chat/message', {
            recipientId: recipientId,
            content: content,
        });

        return getPayloadOrThrow(response);
    }

    async getMessages(skip: number, take: number): Promise<Message[]> {
        const response = await this.client.get<Message[]>('chat', {
            params: { skip: skip, take: take },
        });

        return getPayloadOrThrow(response);
    }

    async deleteMessage(messageId: string): Promise<void> {
        const response = await this.client.delete(`chat/${messageId}`);
        return getPayloadOrThrow(response);
    }

    async sendRollMessage(expression: string, rollType: RollType): Promise<Message> {
        const response = await this.client.get<Message>('roll/custom', {
            params: { expression, rollType },
        });

        return getPayloadOrThrow(response);
    }

    async performSkillCheck(characterId: string, skillType: string, rollType: RollType): Promise<SkillRollMessage> {
        const response = await this.client.post<Message>('roll/skill', {
            characterId: characterId,
            skillType: skillType,
            rollType: rollType,
        });

        return getPayloadOrThrow(response);
    }

    async performSavingThrow(characterId: string, savingThrowType: string, rollType: RollType): Promise<SavingThrowRollMessage> {
        const response = await this.client.post<Message>('roll/saving-throw', {
            characterId: characterId,
            savingThrowType: savingThrowType,
            rollType: rollType,
        });

        return getPayloadOrThrow(response);
    }

    async getConditions(): Promise<Condition[]> {
        const response = await this.client.get<Condition[]>('conditions');

        return getPayloadOrThrow(response);
    }

    async editHealth(characterId: string, health: Health): Promise<Health> {
        const response = await this.client.patch(`characters/${characterId}/health`, health);
        return getPayloadOrThrow(response);
    }
    
    /*
     * @param amount: positive number to heal, negative number to damage
     */
    async applyHealOrDamage(characterId: string, amount: number): Promise<Health> {
        const response = await this.client.patch(`characters/${characterId}/health-modify`, { change: amount });
        return getPayloadOrThrow(response);
    }
}

function getPayloadOrThrow<T>(response: AxiosResponse): T {
    if (response.status < 100 || response.status >= 400) {
        throw new Error(response.statusText);
    }

    return response.data;
}
