<template>
    <dialog class="modal">
        <div class="modal-box">
            <h3 class="font-bold text-lg">Edit ability</h3>
            <form @submit.prevent="save" method="dialog" class="space-x-2">
                <label class="form-control w-full max-w-md">
                    <span class="label label-text">{{ ability.type }}</span>
                    <input
                        ref="input"
                        v-model.number="score"
                        type="text"
                        :placeholder="ability.type"
                        required
                        class="input input-bordered outline-2 outline-slate-400 outline w-full max-w-md"
                    />
                </label>
                <div class="modal-action">
                    <button class="btn btn-success w-24" type="submit">Save</button>
                    <button @click="cancel" class="btn w-24" type="button">Cancel</button>
                </div>
            </form>
        </div>
    </dialog>
</template>
<script setup lang="ts">
import { ref, watchEffect } from 'vue';
import { useModal, type DialogResultCallback } from '@/composables/useModal';
import type { Ability } from '@/contracts/character';

const modal = useModal();
const input = ref<HTMLInputElement>();

const { closeModal, ability } = defineProps<{
    ability: Ability;
    closeModal: DialogResultCallback<Ability>;
}>();

watchEffect(() => {
    if (!modal.isOpen.value) return;
    input.value?.focus();
});

const score = ref(ability.score);

function save() {
    const payload = { type: ability.type, score: score.value, modifier: ability.modifier };
    closeModal({ action: 'result', payload });
}

function cancel() {
    closeModal({ action: 'reject' });
}
</script>
