<script setup lang="ts">
import { ref } from 'vue';
import { useI18n } from 'vue-i18n';

import type { ProficiencyEditItemType } from '@/components/character/shared/ProficiencyListEdit';
import ProficiencyListEdit from '@/components/character/shared/ProficiencyListEdit/ProficiencyListEdit.vue';
import type { DialogResultCallback } from '@/composables/useModal';
import { Proficiency } from '@/contracts/enums';

const { t } = useI18n();

const { closeModal, items, localePrefix } = defineProps<{
    caption: string;
    items: ProficiencyEditItemType[];
    localePrefix: string;
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
        <div class="modal-box overflow-y-hidden">
            <h3 class="font-bold text-lg">{{ caption }}</h3>
            <ProficiencyListEdit v-model="currentItems" :locale-prefix="localePrefix" class="modal-content" />
            <form method="dialog" @submit.prevent="confirm">
                <div class="modal-action">
                    <button class="btn btn-success w-24" type="submit">{{ t('actions.save') }}</button>
                    <button class="btn w-24" type="button" @click="cancel">{{ t('actions.cancel') }}</button>
                </div>
            </form>
        </div>
    </dialog>
</template>

<style>
.modal-content {
    max-height: calc(100dvh - 15em);
    overflow-y: auto;
}
</style>
