<template>
    <dialog class="modal">
        <div class="modal-box">
            <h3 class="font-bold text-lg">Edit conditions</h3>

            <h4 v-if="active.length > 0" class="font-bold text-base">Active</h4>
            <div v-if="active.length > 0" class="flex flex-col max-h-36 overflow-y-auto scroll-smooth">
                <div v-for="condition in currentActive" :key="condition.name"
                    class="flex flex-row border-base-200 border-b-2 p-1 align-items-center">
                    <p class="flex-1 self-center">{{ condition.name }}</p>
                    <div v-if="condition.hasLevels" class="flex flex-row items-center mr-2">
                        <span class="btn btn-sm btn-circle btn-ghost" @click="decreaseLevel(condition)">
                            <svg xmlns="http://www.w3.org/2000/svg" class="w-8 h-8" viewBox="0 -960 960 960"
                                fill="currentColor">
                                <path d="M560-240 320-480l240-240 56 56-184 184 184 184-56 56Z" />
                            </svg>
                        </span>
                        <p>{{ condition.level }}</p>
                        <span class="btn btn-sm btn-circle btn-ghost " @click="increaseLevel(condition)">
                            <svg xmlns="http://www.w3.org/2000/svg" class="w-8 h-8" viewBox="0 -960 960 960"
                                fill="currentColor">
                                <path d="M504-480 320-664l56-56 240 240-240 240-56-56 184-184Z" />
                            </svg>
                        </span>
                    </div>
                    <button class="btn btn-sm btn-outline btn-circle btn-error" v-on:click="removeCondition(condition)">
                        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 -960 960 960" class="w-4 h-4"
                            fill="currentColor">
                            <path
                                d="M280-120q-33 0-56.5-23.5T200-200v-520h-40v-80h200v-40h240v40h200v80h-40v520q0 33-23.5 56.5T680-120H280Zm400-600H280v520h400v-520ZM360-280h80v-360h-80v360Zm160 0h80v-360h-80v360ZM280-720v520-520Z" />
                        </svg>
                    </button>
                </div>
            </div>
            <h4 class="font-bold text-base p-1">Available</h4>
            <div class="flex flex-col max-h-72 overflow-y-auto scroll-smooth">
                <div v-for="condition in conditions.filter(c => !currentActive.some(x => x.name === c.name))"
                    :key="condition.name" class="flex flex-row border-base-200 border-b-2 p-1 align-items-center">
                    <p class="flex-1 self-center">{{ condition.name }}</p>
                    <button class="btn btn-sm btn-outline btn-circle text-primary" v-on:click="addCondition(condition)">
                        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 -960 960 960" class="w-4 h-4"
                            fill="currentColor">
                            <path d="M440-440H200v-80h240v-240h80v240h240v80H520v240h-80v-240Z" />
                        </svg>
                    </button>
                </div>
            </div>

            <div class="modal-action">
                <button class="btn btn-success w-24" type="submit">Save</button>
                <button @click="cancel" class="btn w-24" type="button">Cancel</button>
            </div>
        </div>
    </dialog>
</template>
<script setup lang="ts">
import { type DialogResultCallback } from '@/composables/useModal';
import type { Condition } from '@/entities';
import { ref } from 'vue';

const { active, conditions, closeModal } = defineProps<{
    conditions: Condition[];
    active: Condition[];
    closeModal: DialogResultCallback<Condition>;
}>();

const currentActive = ref<Condition[]>([...active]);

var selected: Condition | undefined = undefined;

function addCondition(condition: Condition) {
    if (currentActive.value.find((e) => e.name == condition.name) !== undefined) {
        return;
    }

    currentActive.value.push(condition);
}

function removeCondition(condition: Condition) {
    const index = currentActive.value.indexOf(condition);

    if (index > -1) {
        currentActive.value.splice(index, 1);
    }
}

function decreaseLevel(condition: Condition) {
    if (condition.level > 1) {
        condition.level -= 1;
    }
}

function increaseLevel(condition: Condition) {
    condition.level += 1;
}

async function save() {
    if (selected === undefined) {
        closeModal({ action: 'reject' });
        return;
    }

    closeModal({ action: 'result', payload: selected });
}

function cancel() {
    closeModal({ action: 'reject' });
}
</script>
