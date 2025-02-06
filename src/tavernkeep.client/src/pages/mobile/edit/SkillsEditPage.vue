<script setup lang="ts">
import { ref, computed, onMounted } from 'vue';
import { useRouter } from 'vue-router';

import ProficiencyListEdit from '@/components/character/shared/ProficiencyListEdit/ProficiencyListEdit.vue';
import CreateCustomSkillMenu from '@/components/dialogs/CreateCustomSkillMenu.vue';
import { useCurrentUserAccount } from '@/composables/useCurrentUserAccount';
import type { SkillEditDto } from '@/contracts/dtos';
import { ApiClientFactory } from '@/factories/ApiClientFactory';
import { useHeaderStore } from '@/stores/header';

const { activeCharacter } = useCurrentUserAccount();
const characterSkills = computed(() => (activeCharacter.value ? [...activeCharacter.value.skills] : []));
const createCustomSkillMenu = ref<InstanceType<typeof CreateCustomSkillMenu>>();
const header = useHeaderStore();
const router = useRouter();

async function save() {
    if (!activeCharacter.value) {
        return;
    }

    const skills = {} as Record<string, SkillEditDto>;
    for (const item of characterSkills.value) {
        skills[item.name] = { proficiency: item.proficiency };
    }

    const api = ApiClientFactory.createApiClient();
    await api.editSkills(activeCharacter.value.id, skills);

    await router.push('/');
}

onMounted(() => {
    header.setHeader('Edit skills');
});
</script>

<template>
    <ProficiencyListEdit v-model="characterSkills" locale-prefix="pf.skills." />
    <div class="flex justify-end mt-4">
        <button class="btn btn-sm btn-secondary btn-outline" @click="createCustomSkillMenu?.open()">
            Add custom skill
        </button>
    </div>
    <form class="sticky bottom-4 right-2" @submit.prevent="save">
        <div class="modal-action">
            <button class="btn btn-circle btn-secondary" type="submit">
                <span class="mdi mdi-content-save text-lg"></span>
            </button>
        </div>
    </form>

    <CreateCustomSkillMenu ref="createCustomSkillMenu" />
</template>

<style>
@import '@douxcode/vue-spring-bottom-sheet/dist/style.css';
</style>
