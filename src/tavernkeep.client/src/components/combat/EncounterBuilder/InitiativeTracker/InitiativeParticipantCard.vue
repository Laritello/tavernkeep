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
            <div
                class="flex input input-bordered justify-center items-center size-12 p-0 cursor-default hover:border-2"
            >
                <span class="text-center font-semibold text-lg">
                    {{ participant.initiative ?? '--' }}
                </span>
            </div>

            <!-- Participant Info -->
            <div class="flex-1">
                <h3 class="font-bold">{{ participant.name }}</h3>
                <div class="text-sm opacity-70">{{ participant.type }}</div>
            </div>

            <!-- HP Display -->
            <HealthBar :health="participant.health" width="10rem" height="1.25rem" />

            <!-- Saves -->
            <div class="bg-base-100 rounded-md p-2">
                <div v-for="[name, value] in Object.entries(participant.savingThrows)" :key="name">
                    {{ name }}: {{ value }}
                </div>
            </div>

            <!-- Perception -->
            <div class="bg-base-100 rounded-md p-2 text-center">
                <div class="mdi mdi-eye"></div>
                {{ participant.perception }}
            </div>

            <!-- Armor -->
            <div class="bg-base-100 rounded-md p-2 text-center">
                <div class="mdi mdi-shield"></div>
                {{ participant.armorClass }}
            </div>

            <!-- Action Buttons -->
            <div class="flex gap-2 justify-self-end">
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
