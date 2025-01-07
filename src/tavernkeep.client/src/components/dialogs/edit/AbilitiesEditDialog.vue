<script setup lang="ts">
import type { Ability } from '@/contracts/character';
import { type AbilityType } from '@/contracts/enums';
import type { DialogResultCallback } from '@/composables/useModal';


const { abilities, closeModal } = defineProps<{ 
    abilities: Record<AbilityType, Ability>, 
    closeModal: DialogResultCallback<Record<AbilityType, Ability>> 
}>();

const currentItems = { ...abilities };

function confirm() {
    const payload = currentItems;
    closeModal({ action: 'result', payload });
}

function cancel() {
    closeModal({ action: 'reject' });
}

function increase(ability: Ability) {
    if (ability.score >= 20) return;
    ability.score++;
}

function decrease(ability: Ability) {
    if (ability.score <= 8) return;
    ability.score--;
}


</script>

<template>
    <dialog class="modal">
        <div class="modal-box">
            <h3 class="font-bold text-lg">Attributes edit</h3>
            <form @submit.prevent="confirm" method="dialog" class="space-x-2">
                <div class="grid grid-cols-3 md:grid-cols-6">
                    <template v-for="ability in currentItems" :key="ability.type">
                        <div class="box-border flex flex-col m-2 p-2 border-2 rounded-lg">
                            <div class="flex flex-row input input-bordered text-3xl font-extrabold select-none p-0">
                                <input type="number" 
                                       v-model.number="ability.score" 
                                       class="max-w-12 text-end" />
                                <div class="flex flex-col">
                                    <div class="btn btn-ghost btn-xs btn-square"
                                         @click="increase(ability)">
                                        <span class="mdi mdi-chevron-up"></span>
                                    </div>
                                    <div class="btn btn-ghost btn-xs btn-square"
                                         @click="decrease(ability)">
                                        <span class="mdi mdi-chevron-down"></span>
                                    </div>
                                </div>
                            </div>
                            <p class="text-sm font-light text-center select-none">{{ ability.type }}</p>
                        </div>
                    </template>
                </div>
                <div class="modal-action">
                    <button class="btn btn-success w-24" type="submit">Save</button>
                    <button @click="cancel" class="btn w-24" type="button">Cancel</button>
                </div>
            </form>
        </div>
    </dialog>
    
</template>

<style scoped>

</style>