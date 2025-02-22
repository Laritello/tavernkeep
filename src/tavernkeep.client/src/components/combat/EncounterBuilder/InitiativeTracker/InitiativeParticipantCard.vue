<script setup lang="ts">
import HealthBar from '@/components/character/HealthBar.vue';
import type { Participant } from '@/contracts/encounter/Participant.ts';

const { participant, activeTurn } = defineProps<{
    participant: Participant;
    activeTurn: boolean;
}>();

defineEmits<{
    (e: 'edit', participant: Participant): void;
    (e: 'remove', id: string): void;
}>();
</script>

<template>
    <div
        class="flex flex-row items-center rounded-md"
        :class="[
            activeTurn ? 'border-2 border-accent animate-pulse' : 'border border-base-300',
            participant.type === 'Character' ? 'bg-primary bg-opacity-10' : 'bg-error bg-opacity-10',
        ]"
    >
        <div class="drag-handle p-1 cursor-grab">
            <span class="mdi mdi-drag text-xl"></span>
        </div>
        <div class="flex flex-row p-2 items-center gap-4">
            <!-- Initiative Value -->
            <label class="input input-bordered flex items-center p-0 max-w-12">
                <input type="text" value="--" class="text-center font-semibold text-lg w-full" />
            </label>

            <!-- Participant Info -->
            <div class="flex-1">
                <h3 class="font-bold">{{ participant.id.slice(0, 6) }}</h3>
                <div class="text-sm opacity-70">CLASS / TYPE</div>
            </div>

            <!-- HP Display -->
            <HealthBar :health="participant.health" width="10rem" :hidden="participant.type === 'Creature'" />

            <!-- Action Buttons -->
            <div class="flex gap-2">
                <button class="btn btn-circle btn-sm btn-ghost" @click="$emit('edit', participant)">
                    <span class="mdi mdi-pencil"></span>
                </button>
                <button class="btn btn-circle btn-sm btn-ghost text-error" @click="$emit('remove', participant.id)">
                    <span class="mdi mdi-delete"></span>
                </button>
            </div>
        </div>
    </div>
</template>
