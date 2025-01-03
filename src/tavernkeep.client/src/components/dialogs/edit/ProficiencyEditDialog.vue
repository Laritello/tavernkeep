<script setup lang="ts">
import { ref } from 'vue';

import { Proficiency } from '@/contracts/enums';
import type { DialogResultCallback } from '@/composables/useModal';
import type { ProficiencyEditItemType } from '@/components/character/shared/ProficiencyListEdit';
import ProficiencyListEdit from '@/components/character/shared/ProficiencyListEdit/ProficiencyListEdit.vue';

const { closeModal, items } = defineProps<{
    caption: string;
    items: ProficiencyEditItemType[];
    closeModal: DialogResultCallback<Record<string, Proficiency>>;
}>();

const currentItems = ref<ProficiencyEditItemType[]>([...items]);

function confirm() {
    const payload = {} as Record<string, Proficiency>;
    for (const item of currentItems.value) {
        payload[item.name] = item.proficiency;
    }
    closeModal({ action: 'result', payload });
}

function cancel() {
    closeModal({ action: 'reject' });
}
</script>

<template>
    <dialog class="modal">
        <div class="modal-box">
            <h3 class="font-bold text-lg">{{ caption }}</h3>
            <form @submit.prevent="confirm" method="dialog" class="space-x-2">
                <ProficiencyListEdit v-model="currentItems" />
                <div class="modal-action">
                    <button class="btn btn-success w-24" type="submit">Save</button>
                    <button @click="cancel" class="btn w-24" type="button">Cancel</button>
                </div>
            </form>
        </div>
    </dialog>
</template>