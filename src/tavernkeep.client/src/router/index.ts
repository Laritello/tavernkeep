import { createRouter, createWebHistory, type RouteRecordRaw } from 'vue-router';

import { UserRole } from '@/contracts/enums';
import AdminPage from '@/pages/AdminPage.vue';
import CharactersPage from '@/pages/CharactersPage.vue';
import EncounterBuilderPage from '@/pages/EncounterBuilderPage.vue';
import ErrorPage from '@/pages/ErrorPage.vue';
import LoginPage from '@/pages/LoginPage.vue';
import UploadPortraitPage from '@/pages/UploadPortraitPage.vue';
import BuilderPage from '@/pages/mobile/BuilderPage.vue';
import CharacterPage from '@/pages/mobile/CharacterPage.vue';
import ChatPage from '@/pages/mobile/ChatPage.vue';
import CombatPage from '@/pages/mobile/CombatPage.vue';
import SettingsPage from '@/pages/mobile/SettingsPage.vue';
import SkillsEditPage from '@/pages/mobile/edit/SkillsEditPage.vue';
import { authorizationMiddleware } from '@/router/middleware/authorizationMiddleware.ts';
import { loadLayoutMiddleware } from '@/router/middleware/loadLayoutMiddleware.ts';

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
            layout: 'HeaderLayout',
            protected: true,
            title: 'Create Character',
        },
    },
    {
        path: '/character/skills/edit',
        component: SkillsEditPage,
        meta: {
            layout: 'HeaderLayout',
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
        name: 'EditCharacterPortrait',
        path: '/characters/:characterId/portrait',
        component: UploadPortraitPage,
        meta: {
            layout: 'HeaderLayout',
            title: 'Upload avatar image',
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
    {
        path: '/encounter/build',
        component: EncounterBuilderPage,
        meta: {
            layout: 'BlankLayout',
            protected: true,
            allowedRoles: [UserRole.Master],
        },
    },
];
export const router = createRouter({
    history: createWebHistory(),
    routes,
});

router.beforeEach(authorizationMiddleware);
router.beforeEach(loadLayoutMiddleware);
