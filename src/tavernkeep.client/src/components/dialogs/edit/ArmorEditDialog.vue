<script setup lang="ts">
import { ref } from 'vue';

import { ArmorType, Proficiency } from '@/contracts/enums';
import type { DialogResultCallback } from '@/composables/useModal';
import type { ProficiencyEditItemType } from '@/components/character/shared/ProficiencyListEdit';
import ProficiencyListEdit from '@/components/character/shared/ProficiencyListEdit/ProficiencyListEdit.vue';
import type { Armor } from '@/contracts/character';
import { useI18n } from 'vue-i18n';

const { t } = useI18n();

const { closeModal, armor } = defineProps<{
    armor: Armor;
    closeModal: DialogResultCallback<Armor>;
}>();

const types: ArmorType[] = [ArmorType.Unarmored, ArmorType.Light, ArmorType.Medium, ArmorType.Heavy];
const currentItems = ref<ProficiencyEditItemType[]>(Object.entries(armor.proficiencies).map(k => ({ name: k[0].toString(), proficiency: k[1], userBonus: 0 })));

const type = ref<ArmorType>(armor.equipped.type);
const bonus = ref<number>(armor.equipped.bonus);
const hasCap = ref<boolean>(armor.equipped.hasDexterityCap);
const cap = ref<number>(armor.equipped.dexterityCap);

function confirm() {
    const payload = {} as Armor;
    payload.equipped = {
        type: type.value,
        bonus: bonus.value,
        hasDexterityCap: hasCap.value,
        dexterityCap: cap.value
    };

    payload.proficiencies = {} as Record<ArmorType, Proficiency>;
    
    for (const item of currentItems.value) {
        payload.proficiencies[item.name as ArmorType] = item.proficiency;
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
            <h3 class="font-bold text-lg">{{ t('widgets.armor.editDialog.header') }}</h3>

            <label class="form-control w-full max-w-xs">
                <div class="label">
                    <span class="label-text">{{ t('widgets.armor.type') }}</span>
                </div>
                <select class="select select-bordered" v-model="type">
                    <option disabled selected>{{ t('widgets.armor.editDialog.pickOne') }}</option>
                    <option v-for="type in types" :key="type" :value="type">{{ t(`pf.armor.${type.toLowerCase()}`) }}</option>
                </select>
            </label>

            <div class="flex flex-row gap-2">
                <label class="form-control w-full max-w-xs">
                    <div class="label">
                        <span class="label-text">{{ t('widgets.armor.bonus') }}</span>
                    </div>
                    <input type="text" class="input input-bordered w-full max-w-xs"
                        v-model="bonus" />
                </label>

                <label class="form-control w-full max-w-xs">
                    <div class="label">
                        <span class="label-text text-clip text-nowrap">{{ t('widgets.armor.dexterityCap') }}</span>
                        <input type="checkbox" class="toggle toggle-xs" v-model="hasCap" />
                    </div>
                    <input type="text" class="input input-bordered w-full max-w-xs"
                        v-bind:disabled="!hasCap" v-model="cap" />
                </label>
            </div>

            <div class="divider divider-neutral"></div>

            <ProficiencyListEdit locale-prefix="pf.armor." v-model="currentItems" class="modal-content" />
            <form @submit.prevent="confirm" method="dialog">
                <div class="modal-action">
                    <button class="btn btn-success w-24" type="submit">{{ t("actions.save") }}</button>
                    <button @click="cancel" class="btn w-24" type="button">{{ t("actions.cancel") }}</button>
                </div>
            </form>
        </div>
    </dialog>
</template>
