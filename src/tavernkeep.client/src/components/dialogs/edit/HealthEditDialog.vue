<script setup lang="ts">
import { ref } from 'vue';
import { VueScrollPicker } from 'vue-scroll-picker';
import 'vue-scroll-picker/lib/style.css';
import type { DialogResultCallback } from '@/composables/useModal';
const generateArray = (start: number, end: number): number[] => {
    return Array.from({ length: end - start + 1 }, (_, i) => start + i);
};

const options = generateArray(-100, 100).map(number => ({
    name: number.toString(),
    value: number,
}));

type ResultType = { type: 'current' | 'temporary', amount: number };
const { closeModal } = defineProps<{
    closeModal: DialogResultCallback<ResultType>;
}>();

const selected = ref(0);


function applyCurrent() {
    const payload: ResultType = {
        type: 'current',
        amount: selected.value
    };
    closeModal({ action: 'result', payload });
}

function applyTemporary() {
    const payload: ResultType = {
        type: 'temporary',
        amount: selected.value
    };
    closeModal({ action: 'result', payload });
}

function cancel() {
    closeModal({ action: 'reject' });
}

</script>

<template>
    <dialog class="modal">
        <div class="modal-box">
            <h3 class="font-bold text-lg">{{ $t('dialogs.healthEdit.header') }}</h3>
            <form method="dialog" class="space-x-2 justify-items-center">
                <VueScrollPicker v-model="selected" :options="options" class="text-2xl max-w-32" />
                <div class="modal-action justify-center">
                    <button type="button" 
                            @click="applyCurrent()"
                            class="btn w-24"
                            :class="{ 'btn-success': selected >= 0, 'btn-error': selected < 0 }">
                        {{ selected >= 0 ? 'Heal' : 'Damage' }}
                    </button>
                    <button type="button"
                            @click="applyTemporary()"
                            class="btn btn-info w-24"
                            :class="{ 'btn-disabled': selected < 0 }">
                        Temporary
                    </button>
                    <button @click="cancel" class="btn w-24" type="button">{{ $t('actions.cancel') }}</button>
                </div>
            </form>
        </div>
    </dialog>
</template>


<style>
.vue-scroll-picker-layer-top {
    box-sizing: border-box;
    @apply bg-gradient-to-b from-base-100 from-10% to-base-100/70;
    @apply border-b border-base-content/30;
    top: 0;
    height: calc(50% - 1em);
    cursor: pointer;
}

.vue-scroll-picker-layer-bottom {
    @apply bg-gradient-to-t from-base-100 from-10% to-base-100/70;
    @apply border-t border-base-content/30;
    bottom: 0;
    height: calc(50% - 1em);
    cursor: pointer;
}

.vue-scroll-picker-item {
    @apply text-base-content;
    
}
</style>