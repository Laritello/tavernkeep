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

// To ensure it is treated as a module, add at least one `export` statement
export {};
