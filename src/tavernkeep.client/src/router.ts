import { createRouter, createWebHistory, type RouteRecordRaw } from 'vue-router';
import { UserRole } from './contracts/enums/UserRole';

//Pages
import LoginPage from './pages/LoginPage.vue';
import HomePage from './pages/HomePage.vue';
import ErrorPage from './pages/ErrorPage.vue';
import CharactersPage from './pages/CharactersPage.vue';
import AdminPage from './pages/AdminPage.vue';
import CharacterPage from './pages/mobile/CharacterPage.vue';
import ChatPage from './pages/mobile/ChatPage.vue';
import { useSession } from './composables/useSession';

const routes: RouteRecordRaw[] = [
    {
        path: '/',
        component: HomePage,
        meta: {
            layout: 'TestLayout',
            protected: true,
        },
    },
    {
        path: '/character',
        component: CharacterPage,
        meta: {
            layout: 'TestLayout',
            protected: true,
        },
    },
    {
        path: '/chat',
        component: ChatPage,
        meta: {
            layout: 'TestLayout',
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
            layout: 'TestLayout',
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
            layout: 'TestLayout',
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
    const session = useSession();

    const protectedRoute = to.meta.protected;
    const isLoggedIn = session.isAuthenticated.value;

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
    const dontHavePermissions = !session.havePermissions(allowedRoles);

    // Redirect to not-allowed if logged in and trying to access a page that requires a specific role
    if (dontHavePermissions) {
        return { path: '/not-allowed' };
    }
});
