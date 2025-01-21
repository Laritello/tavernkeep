<script setup lang="ts">
import { computed } from 'vue';
import { useCurrentUserAccount } from '@/composables/useCurrentUserAccount'
import { ApiClientFactory } from '@/factories/ApiClientFactory';
import { Proficiency } from '@/contracts/enums';
import ProficiencyListEdit from '@/components/character/shared/ProficiencyListEdit/ProficiencyListEdit.vue';

const { activeCharacter } = useCurrentUserAccount();
const characterSkills = computed(() => activeCharacter.value ? [...activeCharacter.value.skills] : []);
async function save() {
    if(!activeCharacter.value){
        return;
    }
    
    const skills = {} as Record<string, Proficiency>;
    for (const item of characterSkills.value) {
        skills[item.name] = item.proficiency;
    }
    
    const api = ApiClientFactory.createApiClient();
    await api.editSkills(activeCharacter.value.id, skills);
}

</script>

<template>
    <ProficiencyListEdit locale-prefix="pf.skills." v-model="characterSkills" />
    <div class="flex justify-end mt-4">
        <button class="btn btn-sm btn-secondary btn-outline" @click="console.log('add custom skill')">
            Add custom skill
        </button>
    </div>
    <form @submit.prevent="save" method="dialog" class="sticky bottom-4 right-2">
        <div class="modal-action">
            <button class="btn btn-circle btn-secondary" type="submit">
                <span class="mdi mdi-account-edit text-lg text-base-content"></span>
            </button>
        </div>
    </form>
</template>