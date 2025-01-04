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
</script>

<template>
    <dialog class="modal">
        <div class="modal-box">
            <h3 class="font-bold text-lg">Attributes edit</h3>
            <form @submit.prevent="confirm" method="dialog" class="space-x-2">
                <div class="grid grid-cols-3 md:grid-cols-6">
                    <template v-for="ability in currentItems" :key="ability.type">
                        <div class="box-border flex flex-col m-2 p-2 border-2 rounded-lg red">
                            <input v-model.number="ability.score" class="input input-bordered text-3xl font-extrabold text-center select-none" />
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