<script setup lang="ts">
import { ref } from 'vue';
import { useI18n } from 'vue-i18n';

import ProficiencyListEdit from '@/components/character/shared/ProficiencyListEdit/ProficiencyListEdit.vue';
import type { DialogResultCallback } from '@/composables/useModal';
import type { Armor } from '@/contracts/character';
import { ArmorType, Proficiency, SkillDataType } from '@/contracts/enums';

const { t } = useI18n();

const { closeModal, armor } = defineProps<{
    armor: Armor;
    closeModal: DialogResultCallback<Armor>;
}>();

const types: ArmorType[] = [ArmorType.Unarmored, ArmorType.Light, ArmorType.Medium, ArmorType.Heavy];
const currentItems = ref(
    Object.entries(armor.proficiencies).map((k) => ({
        type: SkillDataType.Basic,
        name: k[0].toString(),
        proficiency: k[1],
    }))
);

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
        dexterityCap: cap.value,
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
            <h3 class="font-bold text-lg">{{ t('dialogs.armorEdit.header') }}</h3>

            <label class="form-control w-full max-w-xs">
                <div class="label">
                    <span class="label-text">{{ t('widgets.armor.type') }}</span>
                </div>
                <select v-model="type" class="select select-bordered">
                    <option disabled selected>{{ t('dialogs.armorEdit.pickOne') }}</option>
                    <option v-for="type in types" :key="type" :value="type">
                        {{ t(`pf.armor.${type.toLowerCase()}`) }}
                    </option>
                </select>
            </label>

            <div class="flex flex-row gap-2">
                <label class="form-control w-full max-w-xs">
                    <div class="label">
                        <span class="label-text">{{ t('widgets.armor.bonus') }}</span>
                    </div>
                    <input v-model="bonus" type="text" class="input input-bordered w-full max-w-xs" />
                </label>

                <label class="form-control w-full max-w-xs">
                    <div class="label">
                        <span class="label-text text-clip text-nowrap">{{ t('widgets.armor.dexterityCap') }}</span>
                        <input v-model="hasCap" type="checkbox" class="toggle toggle-xs" />
                    </div>
                    <input v-model="cap" type="text" class="input input-bordered w-full max-w-xs" :disabled="!hasCap" />
                </label>
            </div>

            <div class="divider divider-neutral"></div>

            <ProficiencyListEdit v-model="currentItems" locale-prefix="pf.armor." class="modal-content" />
            <form method="dialog" @submit.prevent="confirm">
                <div class="modal-action">
                    <button class="btn btn-success w-24" type="submit">{{ t('actions.save') }}</button>
                    <button class="btn w-24" type="button" @click="cancel">{{ t('actions.cancel') }}</button>
                </div>
            </form>
        </div>
    </dialog>
</template>
