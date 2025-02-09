import 'vue-router';

import { UserRole } from '@/contracts/enums';

declare module 'vue-router' {
    interface RouteMeta {
        protected: boolean;
        allowedRoles?: UserRole[];
        errorMessage?: string;
        layout?: 'AppLayout' | 'HeaderLayout' | 'BlankLayout';
        title?: string;
    }
}

export type KeyValue<T> = {
    [P in keyof T]: {
        key: P;
        value: T[P];
    };
}[keyof T];

// eslint-disable-next-line @typescript-eslint/no-unsafe-function-type
export type DTO<T> = { [K in keyof T as T[K] extends Function ? never : K]: T[K] };

// To ensure it is treated as a module, add at least one `export` statement
export {};
