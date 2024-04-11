import type { UserRole } from '@/contracts/enums';
import { StorageSerializers, useStorage } from '@vueuse/core';
import { jwtDecode } from 'jwt-decode';
import { defineStore } from 'pinia';
import { computed } from 'vue';

export type SessionStorageData = {
    accessToken: string;
    refreshToken: string;
};

export type JwtTokenData = {
    id: string;
    userId: string;
    userLogin: string;
    userRole: UserRole;
    exp: number;
};

export const storeKey: string = 'tavernkeep.auth.data';

export const useSessionLocalStorage = defineStore('sessionData', () => {
    const data = useStorage<SessionStorageData>(storeKey, null, localStorage, {
        serializer: StorageSerializers.object,
    });

    const accessToken = computed(() => data.value?.accessToken || undefined);
    const refreshToken = computed(() => data.value?.refreshToken || undefined);

    const hasData = computed(() => {
        return !!data.value;
    });

    const jwt = computed(() => {
        if (!accessToken.value) return undefined;
        return jwtDecode<JwtTokenData>(accessToken.value);
    });

    const expiresAt = computed(() => {
        if (!jwt.value) return 0;
        return jwt.value.exp;
    });

    const userId = computed(() => {
        if (!jwt.value) return undefined;
        return jwt.value.userId;
    });

    const userRole = computed(() => {
        if (!jwt.value) return undefined;
        return jwt.value.userRole;
    });

    const userLogin = computed(() => {
        if (!jwt.value) return undefined;
        return jwt.value.userLogin;
    });

    return {
        hasData,
        accessToken,
        refreshToken,
        expiresAt,
        userId,
        userRole,
        userLogin,
        setData(newData: SessionStorageData | undefined) {
            data.value = newData;
        },
    };
});
