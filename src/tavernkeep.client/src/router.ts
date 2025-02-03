import { createRouter, createWebHistory, type RouteRecordRaw } from 'vue-router';

import { UserRole } from '@/contracts/enums';
import CombatPage from '@/pages/mobile/CombatPage.vue';
import SkillsEditPage from '@/pages/mobile/edit/SkillsEditPage.vue';

import { useSession } from './composables/useSession';
import AdminPage from './pages/AdminPage.vue';
import CharactersPage from './pages/CharactersPage.vue';
import ErrorPage from './pages/ErrorPage.vue';
//Pages
import LoginPage from './pages/LoginPage.vue';
import BuilderPage from './pages/mobile/BuilderPage.vue';
import CharacterPage from './pages/mobile/CharacterPage.vue';
import ChatPage from './pages/mobile/ChatPage.vue';
import SettingsPage from './pages/mobile/SettingsPage.vue';

const routes: RouteRecordRaw[] = [
    {
        path: '/',
        component: CharacterPage,
        meta: {
            layout: 'AppLayout',
            protected: true,
        },
    },
    {
        path: '/chat',
        component: ChatPage,
        meta: {
            layout: 'AppLayout',
            protected: true,
        },
    },
    {
        path: '/combat',
        component: CombatPage,
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
        path: '/characters/build',
        component: BuilderPage,
        meta: {
            layout: 'BlankLayout',
            protected: true,
            title: 'Create Character',
        },
    },
    {
        path: '/character/skills/edit',
        component: SkillsEditPage,
        meta: {
            layout: 'BlankLayout',
            protected: true,
            title: 'Edit character skills',
        },
    },
    {
        path: '/settings',
        component: SettingsPage,
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
    const session = useSession();

    const protectedRoute = to.meta.protected;
    const isLoggedIn = session.isAuthenticated.value;

    // Redirect to login page if not logged in and trying to access a restricted page
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
