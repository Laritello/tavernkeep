import type { RouteLocation } from 'vue-router';

import { useSession } from '@/composables/useSession.ts';

export function authorizationMiddleware(to: RouteLocation) {
    const session = useSession();
    const isLoggedIn = session.isAuthenticated.value;
    const protectedRoute = to.meta.protected;

    // Redirect to login page if not logged in and trying to access a restricted page
    if (protectedRoute && !isLoggedIn) {
        return { path: '/login' };
    }

    // Redirect to home if logged in and trying to access login page
    if (to.path === '/login' && isLoggedIn) {
        return { path: '/' };
    }

    const dontHavePermissions = protectedRoute && !session.hasPermissions(to.meta.allowedRoles);

    // Redirect to not-allowed if logged in and trying to access a page that requires a specific role
    if (dontHavePermissions) {
        return { path: '/not-allowed' };
    }
}
