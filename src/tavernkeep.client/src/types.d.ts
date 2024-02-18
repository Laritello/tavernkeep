import 'vue-router';
import { UserRole } from '@/contracts/enums/UserRole';

declare module 'vue-router' {
    interface RouteMeta {
        protected: boolean;
        allowedRoles?: UserRole[];
        errorMessage?: string;
    }
}

// To ensure it is treated as a module, add at least one `export` statement
export {};
