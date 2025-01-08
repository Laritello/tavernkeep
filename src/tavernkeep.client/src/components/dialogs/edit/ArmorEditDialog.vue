<script setup lang="ts">
import { ref } from 'vue';

import { ArmorType, Proficiency } from '@/contracts/enums';
import type { DialogResultCallback } from '@/composables/useModal';
import type { ProficiencyEditItemType } from '@/components/character/shared/ProficiencyListEdit';
import ProficiencyListEdit from '@/components/character/shared/ProficiencyListEdit/ProficiencyListEdit.vue';
import type { Armor } from '@/contracts/character';

const { closeModal, armor } = defineProps<{
    armor: Armor;
    closeModal: DialogResultCallback<Record<string, Proficiency>>;
}>();

const types: ArmorType[] = [ArmorType.Unarmored, ArmorType.Light, ArmorType.Medium, ArmorType.Heavy];
const currentItems = ref<ProficiencyEditItemType[]>(Object.entries(armor.proficiencies).map(k => ({ name: k[0].toString(), proficiency: k[1], userBonus: 0 })));

const type = ref<ArmorType>(armor.equipped.type);
const bonus = ref<number>(armor.equipped.bonus);
const hasCap = ref<boolean>(armor.equipped.hasDexterityCap);
const cap = ref<number>(armor.equipped.dexterityCap);

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
            <h3 class="font-bold text-lg">Edit armor</h3>

            <label class="form-control w-full max-w-xs">
                <div class="label">
                    <span class="label-text">Type</span>
                </div>
                <select class="select select-bordered" v-model="type">
                    <option disabled selected>Pick one</option>
                    <option v-for="type in types" :key="type">{{ type }}</option>
                </select>
            </label>


            <div class="flex flex-row gap-2">
                <label class="form-control w-full max-w-xs">
                    <div class="label">
                        <span class="label-text">Bonus</span>
                    </div>
                    <input type="text" placeholder="Type here" class="input input-bordered w-full max-w-xs"
                        v-model="bonus" />
                </label>

                <label class="form-control w-full max-w-xs">
                    <div class="label">
                        <span class="label-text">Dexterity cap</span>
                        <input type="checkbox" class="toggle toggle-xs" v-model="hasCap" />
                    </div>
                    <input type="text" placeholder="Type here" class="input input-bordered w-full max-w-xs"
                        v-bind:disabled="!hasCap" v-model="cap" />
                </label>
            </div>

            <div class="divider divider-neutral"></div>

            <ProficiencyListEdit v-model="currentItems" class="modal-content" />
            <form @submit.prevent="confirm" method="dialog">
                <div class="modal-action">
                    <button class="btn btn-success w-24" type="submit">Save</button>
                    <button @click="cancel" class="btn w-24" type="button">Cancel</button>
                </div>
            </form>
        </div>
    </dialog>
</template>
