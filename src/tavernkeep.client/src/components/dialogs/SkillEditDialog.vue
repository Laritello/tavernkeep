<template>
    <dialog class="modal">
        <div class="modal-box">
            <h3 class="font-bold text-lg">Edit skill</h3>
            <form @submit.prevent="save" method="dialog" class="space-x-2">
                <ProficiencySelectComponent v-model="selected" />
                <div class="modal-action">
                    <button class="btn btn-success w-24" type="submit">Save</button>
                    <button @click="cancel" class="btn w-24" type="button">Cancel</button>
                </div>
            </form>
        </div>
    </dialog>
</template>
<script setup lang="ts">
import { ref } from 'vue';
import { type DialogResultCallback } from '@/composables/useModal';
import type { Skill } from '@/contracts/character';
import ProficiencySelectComponent from '../ProficiencySelectComponent.vue';

const { closeModal, skill } = defineProps<{
    skill: Skill;
    closeModal: DialogResultCallback<Skill>;
}>();

const selected = ref(skill.proficiency);

async function save() {
    if (selected.value === undefined) {
        closeModal({ action: 'reject' });
        return;
    }

    const payload = { ...skill, proficiency: selected.value };

    closeModal({ action: 'result', payload });
}

function cancel() {
    closeModal({ action: 'reject' });
}
</script>
