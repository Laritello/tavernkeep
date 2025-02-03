<script setup lang="ts">
import CharacterBuilder from '@/components/character/builder/CharacterBuilder.vue';
import type { Character } from '@/entities';
import { ApiClientFactory } from '@/factories/ApiClientFactory';
import { useCharacters } from '@/stores/characters';
import { useHeaderStore } from '@/stores/header';
import { onMounted, ref, type Ref } from 'vue';
import { useI18n } from 'vue-i18n';
import { useRouter } from 'vue-router';

const { t } = useI18n();
const characters = useCharacters();
const header = useHeaderStore();
const router = useRouter();

const api = ApiClientFactory.createApiClient();

const template: Ref<Character> = ref({} as Character);

onMounted(async () => {
    header.setHeader(t('builder.header'), t(`builder.stages.welcome.name`));
    template.value = await api.getCharacterTemplate();
});

function updateStage(stageName: string | undefined) {
    header.setHeader(t('builder.header'), t(`builder.stages.${stageName}.name`));
}

function updateCharacter({ key, value }: { key: keyof Character; value: unknown }) {
    (template.value[key] as typeof value) = value;
};

async function create() {
    const character = await characters.createCharacter(template.value);

    if (character !== undefined) {
        await router.push('/settings');
    }
}
</script>

<template>
    <CharacterBuilder v-on:updated-stage="updateStage" :character="template" @create="create"
        @update-character="updateCharacter" />
</template>

<style scoped></style>