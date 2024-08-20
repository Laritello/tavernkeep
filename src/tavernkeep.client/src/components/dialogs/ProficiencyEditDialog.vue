<template>
    <dialog class="modal">
        <div class="modal-box">
            <h3 class="font-bold text-lg">Edit proficiency level</h3>
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
import ProficiencySelectComponent from '@/components/ProficiencySelectComponent.vue';
import type { Proficiency } from '@/contracts/enums';

const { closeModal, proficiency } = defineProps<{
    proficiency: Proficiency;
    closeModal: DialogResultCallback<{ value: Proficiency }>;
}>();

const selected = ref(proficiency);

async function save() {
    if (selected.value === undefined) {
        closeModal({ action: 'reject' });
        return;
    }

    const payload = { value: selected.value };

    closeModal({ action: 'result', payload });
}

function cancel() {
    closeModal({ action: 'reject' });
}
</script>
