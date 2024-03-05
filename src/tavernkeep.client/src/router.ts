import { createRouter, createWebHistory, type RouteRecordRaw } from 'vue-router';
import { useAuthStore } from './stores/auth.store';
import { UserRole } from './contracts/enums/UserRole';

//Pages
import LoginPage from './pages/LoginPage.vue';
import HomePage from './pages/HomePage.vue';
import ErrorPage from './pages/ErrorPage.vue';
import CharactersPage from './pages/CharactersPage.vue';
import AdminPage from './pages/AdminPage.vue';

const routes: RouteRecordRaw[] = [
    {
        path: '/',
        component: HomePage,
        meta: {
            layout: 'AppLayout',
            protected: true,
        },
    },
    {
        path: '/login',
        component: LoginPage,
        meta: {
            protected: false,
        },
    },
    {
        path: '/characters',
        component: CharactersPage,
        meta: {
            layout: 'AppLayout',
            protected: true,
        },
    },
    {
        path: '/not-allowed',
        component: ErrorPage,
        meta: {
            protected: false,
            errorMessage: 'You have no access to this page',
        },
    },
    {
        path: '/:pathMatch(.*)*',
        component: ErrorPage,
        meta: {
            protected: false,
            errorMessage: 'Page not found',
        },
    },
    {
        path: '/admin',
        component: AdminPage,
        meta: {
            layout: 'AppLayout',
            protected: true,
            allowedRoles: [UserRole.Master],
        },
    },
];
export const router = createRouter({
    history: createWebHistory(),
    routes,
});

router.beforeEach((to) => {
    const auth = useAuthStore();

    const protectedRoute = to.meta.protected;
    const isLoggedIn = auth.isLoggedIn;

    // Redirect to login if not logged in and trying to access a restricted page
    if (protectedRoute && !isLoggedIn) {
        return { path: '/login' };
    }

    // Redirect to home if logged in and trying to access login page
    if (to.path === '/login' && isLoggedIn) {
        return { path: '/' };
    }

    const allowedRoles = to.meta.allowedRoles;
    if (allowedRoles === undefined) return;
    const dontHavePermissions = !auth.havePermissions(allowedRoles);

    // Redirect to not-allowed if logged in and trying to access a page that requires a specific role
    if (dontHavePermissions) {
        return { path: '/not-allowed' };
    }
});
